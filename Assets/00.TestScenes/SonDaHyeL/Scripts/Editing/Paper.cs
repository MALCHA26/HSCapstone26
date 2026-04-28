/*
 * 작성자: 손다혜
 * 최초 작성일: 2026.03.21
 * 수정일: 2026.04.23
 * 역할: 종이가 인쇄기에 충돌 시 Into 프리팹 스폰 후 SceneController에 신호 전달
 */
using UnityEngine;
using System;

public class Paper : MonoBehaviour, IGrabbable
{
    [SerializeField] private AllSceneController sceneController;
    [SerializeField] private GameObject intoPrefab;
    [SerializeField] private Transform intoSpawnPoint;

    public Action onGrabbed;
    private bool grabTriggered = false;
    private bool isTriggered = false;

    private void Start()
    {
        if (sceneController == null)
            sceneController = FindObjectOfType<AllSceneController>();
    }

    // IGrabbable
    public void OnGrab()
    {
        if (grabTriggered) return;
        grabTriggered = true;
        onGrabbed?.Invoke();
    }

    public void OnRelease() { }

    private void OnTriggerEnter(Collider other)
    {
        if (isTriggered) return;

        if (other.CompareTag("PRINTER"))
        {
            isTriggered = true;

            if (intoPrefab != null)
                Instantiate(intoPrefab, intoSpawnPoint.position, intoSpawnPoint.rotation);

            // SceneController로 충돌 신호 전달 > 다음 씬 로드 호출
            sceneController?.LoadNextScene();

            Destroy(gameObject);
        }
    }
}