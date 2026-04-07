using UnityEngine;

public class TTS_Test : MonoBehaviour
{
    private void Start()
    {
        // 1초 후 테스트 대사 출력
        Invoke(nameof(TestSpeak), 1f);
    }

    private void TestSpeak()
    {
        if (TTSManager.Instance == null)
        {
            Debug.LogError("[TTS_Test] TTSManager.Instance is NULL");
            return;
        }
        Debug.Log("w지금 재생해야 합니다.");
        TTSManager.Instance.Speak("TTS 테스트를 시작합니다. 이 문장이 들리면 설정이 정상입니다.");
    }
}