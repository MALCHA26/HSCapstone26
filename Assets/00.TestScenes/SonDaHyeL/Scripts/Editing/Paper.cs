/*
 * 작성자: 손다혜
 * 최초 작성일: 2026.03.21
 * 수정일 : 2026.03.30
 * 역할: 종이가 인쇄기에 충돌 시 Into 애니메이션을 재생
 */

using UnityEngine;
using System;

public class Paper : MonoBehaviour, IGrabbable
{
    [SerializeField] private SceneController sceneController;
    private Animator anim;

    public Action onGrabbed;
    private bool grabTriggered = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
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
        if (other.CompareTag("PRINTER"))
        {
            anim.Play("Into", 0, 0f);
            sceneController.LoadNextScene();
        }
    }
}