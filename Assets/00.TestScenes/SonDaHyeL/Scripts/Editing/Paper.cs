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
    private cshLobbyManager lobbyManager;

    private void Start()
    {
        if (sceneController == null)
            sceneController = FindObjectOfType<AllSceneController>();
        if (lobbyManager == null)
            lobbyManager = FindObjectOfType<cshLobbyManager>();
    }

    // IGrabbable
    public void OnGrab()
    {
        if (grabTriggered) return;
        grabTriggered = true;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null) rb.useGravity = true;

        Animator anim = GetComponent<Animator>();
        if (anim != null) anim.enabled = false;

        onGrabbed?.Invoke();
    }

    public void OnRelease() { }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log($"충돌 발생: {other.name}, 태그: {other.tag}");
        if (isTriggered) return;

        if (other.CompareTag("PRINTER"))
        {
            Debug.Log("인쇄기 충돌 확인! 포톤 연결 시도...");
            isTriggered = true;

            if (intoPrefab != null)
                Instantiate(intoPrefab, intoSpawnPoint.position, intoSpawnPoint.rotation);

            // SceneController로 충돌 신호 전달 > 다음 씬 로드 호출
            //sceneController?.LoadNextScene();
            lobbyManager.Connect();
            Debug.Log("로비매니저커넥트 호출");

            Destroy(gameObject);
        }
    }
}