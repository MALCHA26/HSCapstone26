/*
 * 작성자: 김승윤
 * 작성일: 2026.03.24
 * 역할: 보성사 문 인터랙션을 위한 스크립트
 */

using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public STTManager sttManager;
    public GameObject gaugeCanvas;
    public GaugeManager gaugeManager;
    private bool isOpen = false;
    public float openAngle = 90f; // 문 각도
    public float smoothTime = 2f; // 열리는 속도

    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        // 닫힌 상태
        closedRotation = transform.rotation;
        // 열린 상태의 목표 회전값
        openRotation = closedRotation * Quaternion.Euler(0, openAngle, 0);
    }

    void Update()
    { 
        Quaternion target = isOpen ? openRotation : closedRotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smoothTime);
    }
    public void OpenDoor()
    {
        if (!isOpen) // 문을 열 때
        {
            isOpen = true;
            gaugeCanvas.SetActive(true);

            if (gaugeManager != null)
            {
                gaugeManager.StartGauge();
                gaugeManager.VoiceUIActive(false);
            }

            if (sttManager != null)
            {
                sttManager.StartSTT();
            }
        }
        else // 문 닫을 때
        {
            isOpen = false;
            gaugeCanvas.SetActive(false);

          
            if (sttManager != null)
            {
                sttManager.StopSTT();
            }
            
        }
    }
}
