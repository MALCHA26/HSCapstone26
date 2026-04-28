/*
 * 작성자: 김승윤
 * 작성일: 2026.04.27
 * 역할: 문 손잡이 가이드용 캔버스 회전 제어 스크립트
 */
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float rotateSpeed = 100f; 
    void Update()
    {
        Vector3 rot = transform.localEulerAngles;
        rot.z -= 100f * Time.deltaTime;
        transform.localEulerAngles = rot;
    }
}