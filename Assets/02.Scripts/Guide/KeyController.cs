/*
 * 작성자: 김승윤
 * 작성일: 2026.05.19
 * 역할: 태엽 회전 스크립트
 */
using UnityEngine;

public class KeyController : MonoBehaviour
{
    private float lastFrameAngle = 0f;

    void Start()
    {
        lastFrameAngle = transform.localEulerAngles.x;
    }

    void Update()
    {
        // 한 축 고정
        float currentAngle = transform.localEulerAngles.x;
        float deltaAngle = Mathf.DeltaAngle(lastFrameAngle, currentAngle);

        lastFrameAngle = currentAngle;
    }

   
}
