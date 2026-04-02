using UnityEngine;
using Meta.XR.BuildingBlocks.AIBlocks;

public class TTSManager : MonoBehaviour
{
    public static TTSManager Instance;
    public TextToSpeechAgent agent;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    /// <summary>
    /// 외부에서 대사만 넘기면 자동 재생됨
    /// </summary>
    public void Speak(string text)
    {
        if (agent == null)
        {
            Debug.LogError("TTSManager: agent가 할당되지 않았습니다.");
            return;
        }

        agent.SpeakText(text);
    }
}