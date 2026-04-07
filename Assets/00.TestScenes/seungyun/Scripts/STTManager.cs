/*
 * 작성자: 김승윤
 * 작성일: 2026.04.04
 * 역할: Meta XR STT 기능을 UI 버튼으로 제어하는 매니저
 */

using Meta.XR.BuildingBlocks.AIBlocks;
using UnityEngine;

public class STTManager : MonoBehaviour
{
    public SpeechToTextAgent sttAgent;
    void Start()
    {
        if (sttAgent == null)
            sttAgent = FindFirstObjectByType<SpeechToTextAgent>();
    }

    // 말하기 버튼
    public void StartSTT()
    {
        if (sttAgent != null)
        {
            sttAgent.StartListening();
            Debug.Log("STT 시작");
        }
    }

    // 말하기 중지 버튼
    public void StopSTT()
    {
        if (sttAgent != null)
        {
            sttAgent.StopNow();
            Debug.Log("STT 중지");
        }
    }
}
