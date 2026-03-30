/*
 * 작성자: 손다혜
 * 최초 작성일: 2026.03.21
 * 수정일 : 2026.03.30
 * 역할: 종이가 인쇄기에 충돌 시 Into 애니메이션을 재생
 */

using UnityEngine;

public class Paper : MonoBehaviour
{
    private Transform into;                     // 미리 연결하지 않음
    [SerializeField] private SceneController sceneController;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PRINTER"))
        {
            if (into == null)
            {
                into = other.transform.Find("Into");
                if (into == null)
                {
                    Debug.LogError("PRINTER에 'Into' Transform이 없습니다.");
                    return;
                }
            }

            // 이동
            transform.position = into.position;
            transform.rotation = into.rotation;

            // 물리 고정
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            // 애니메이션
            if (anim != null)
            {
                anim.Play("Into", 0, 0f);
            }

            // 다음 씬 호출
            if (sceneController != null)
            {
                sceneController.LoadNextScene();
            }
        }
    }
}