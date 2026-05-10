/*
 * 작성자: 김승윤
 * 작성일: 2026.03.24
 * 역할: 보성사 문 인터랙션을 위한 스크립트
 */

using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public Transform handleTransform;
    public float triggerAngle = 100f;   // 문 열리는 손잡이 회전 각도
    public GameObject guideCanvas;
    public AudioSource knockAudio;
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
        gaugeCanvas = GameObject.FindWithTag("GaugeCanvas");
        // 닫힌 상태
        closedRotation = transform.rotation;
        // 열린 상태의 목표 회전값
        openRotation = closedRotation * Quaternion.Euler(0, openAngle, 0);
    }

    void Update()
    {
        if (!isOpen && handleTransform != null)
        {
            CheckHandleRotation();
        }
        Quaternion target = isOpen ? openRotation : closedRotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smoothTime);
    }
    void CheckHandleRotation()
    {
        float currentHandleAngle = handleTransform.localEulerAngles.z;

        if (currentHandleAngle > 180) currentHandleAngle -= 360;
        if (Mathf.Abs(currentHandleAngle) >= triggerAngle)
        {
            guideCanvas.SetActive(false);
            OpenDoor();
        }
    }
    public void OpenDoor()
    {
        if (!isOpen) // 문을 열 때
        {
            isOpen = true;
            knockAudio.Stop();
            gaugeCanvas.GetComponent<CanvasGroup>().alpha = 1f;
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
    }
}