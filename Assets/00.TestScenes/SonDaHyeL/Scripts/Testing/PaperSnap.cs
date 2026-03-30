/*
 * 작성자: 손다혜
 * 작성일: 2026.03.21
 * 역할: 종이가 인쇄기에 충돌 시 Into 애니메이션을 재생하고 지정 위치로 이동 (Animator 기반)
 */

using Unity.VectorGraphics;
using UnityEngine;

public class PaperSnap : MonoBehaviour
{
    [SerializeField] private string sceneName = "Scene2"; 
    [SerializeField] private SceneFade sceneFade;
    [SerializeField] private Transform into;      // 이동할 목표 위치
    private Animator anim;                        // Animator (Mecanim)

    private void Start()
    {
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogError("Animator 컴포넌트가 Paper 오브젝트에 없습니다.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PRINTER"))
        {
            // 1. 위치 및 회전 이동
            transform.position = into.position;
            transform.rotation = into.rotation;

            // 2. 물리 고정
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            // 3. Animator의 Into 애니메이션 재생
            if (anim != null)
            {
                anim.Play("Into", 0, 0f);
                sceneFade.FadeOutAndLoad(sceneName);
                // layer 0, normalizedTime 0f → 즉시 처음부터 재생
            }
        }
    }
}