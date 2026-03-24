using UnityEngine;

public class Pickup : MonoBehaviour
{
    public float maxDistance = 10f;
    public float minDistance = 1f;
    public float scrollSpeed = 2f;
    private bool hasTriggered = false;

    [SerializeField] private ScreenController screenController;

    private Camera playerCamera;
    private GameObject heldObject;
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
        if (Input.GetMouseButton(0))
        {
            if (heldObject == null)
                TryPickup();
            else
                AdjustDistanceWithScroll();
        }
        else
        {
            if (heldObject != null)
                Drop();
        }
    }

    private void TryPickup()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            if (hit.collider.CompareTag("Pickup"))
            {
                heldObject = hit.collider.gameObject;
                holdDistance = hit.distance;

                Rigidbody rb = heldObject.GetComponent<Rigidbody>();
                if (rb != null)
                    rb.isKinematic = true;

                Animator anim = heldObject.GetComponent<Animator>();
                if (anim != null)
                    anim.enabled = false;

                if (!hasTriggered && screenController != null)
                {
                    screenController.ActivateScreen();
                    hasTriggered = true;
                }
            }
        }
    }

    private void Drop()
    {
        if (heldObject == null) return;

        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        if (rb != null)
            rb.isKinematic = false;

        Animator anim = heldObject.GetComponent<Animator>();
        if (anim != null)
            anim.enabled = true;

        heldObject = null;
    }

    private void AdjustDistanceWithScroll()
    {
        float scroll = Input.mouseScrollDelta.y;
        holdDistance += scroll * scrollSpeed;
        holdDistance = Mathf.Clamp(holdDistance, minDistance, maxDistance);
    }

    private void UpdateHeldObjectPosition()
    {
        if (heldObject != null)
        {
            Vector3 targetPos = playerCamera.transform.position + playerCamera.transform.forward * holdDistance;
            heldObject.transform.position = targetPos;
            heldObject.transform.rotation = Quaternion.identity;
        }
    }
}