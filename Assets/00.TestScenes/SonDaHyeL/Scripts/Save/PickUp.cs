/*
 * 작성자: 손다혜
 * 작성일: 2026.03.21
 * 역할: (테스트용) 마우스와 시선 레이를 통해 물체를 집고 이동, 조작할 수 있는 스크립트
 */

using UnityEngine;

public class PickUp : MonoBehaviour
{
    [Header("Settings")]
    public float maxDistance = 10f;
    public float minDistance = 1f;
    public float scrollSpeed = 2f;

    private Camera playerCamera;
    private GameObject heldObject;
    private IGrabbable heldGrabbable;
    private float holdDistance;

    void Start()
    {
        playerCamera = Camera.main;
    }

    void Update()
    {
        HandlePickupInput();
        UpdateHeldObjectPosition();
    }

    private void HandlePickupInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryPickup();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Drop();
        }

        if (heldObject != null)
            AdjustDistanceWithScroll();
    }

    private void TryPickup()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (!Physics.Raycast(ray, out RaycastHit hit, maxDistance)) return;
        if (!hit.collider.CompareTag("Pickup")) return;

        heldObject = hit.collider.gameObject;
        holdDistance = hit.distance;

        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;

        Animator anim = heldObject.GetComponent<Animator>();
        if (anim != null) anim.enabled = false;

        // IGrabbable 구현체에 잡힘 알림
        heldGrabbable = heldObject.GetComponent<IGrabbable>();
        heldGrabbable?.OnGrab();
    }

    private void Drop()
    {
        if (heldObject == null) return;

        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = false;

        Animator anim = heldObject.GetComponent<Animator>();
        if (anim != null) anim.enabled = true;

        heldGrabbable?.OnRelease();
        heldGrabbable = null;
        heldObject = null;
    }

    private void AdjustDistanceWithScroll()
    {
        float scroll = Input.mouseScrollDelta.y;
        holdDistance = Mathf.Clamp(holdDistance + scroll * scrollSpeed, minDistance, maxDistance);
    }

    private void UpdateHeldObjectPosition()
    {
        if (heldObject == null) return;
        heldObject.transform.position = playerCamera.transform.position + playerCamera.transform.forward * holdDistance;
    }
}
