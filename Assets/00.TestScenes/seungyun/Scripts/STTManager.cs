/*
 * 작성자: 김승윤
 * 작성일: 2026.04.04
 * 역할: Meta XR STT 기능 제어하는 스크립트
 */

using Meta.XR.BuildingBlocks.AIBlocks;
using UnityEngine;
using System.Collections;

public class STTManager : MonoBehaviour
{
    public SpeechToTextAgent sttAgent;
    public GaugeManager gaugeManager;

    void Start()
    {
        if (sttAgent == null)
            sttAgent = FindFirstObjectByType<SpeechToTextAgent>();
    }

    // STT Start 함수
    public void StartSTT()
    {
        if (sttAgent != null)
        {
            StopAllCoroutines();
            StartCoroutine(RestartSTTRoutine());
            if (gaugeManager != null)
            {
                gaugeManager.VoiceUIActive(true);
            }
        }
    }
    IEnumerator RestartSTTRoutine()
    {
        sttAgent.StopNow();

        // 0.2초 대기
        yield return new WaitForSeconds(0.2f);

        // 다시 STT 시작
        sttAgent.StartListening();
    }
    // STT Stop 함수
    public void StopSTT()
    {
        if (sttAgent != null)
        {
            StopAllCoroutines();
            sttAgent.StopNow();
            Debug.Log("STT 중지됨");
        }
    }
}