using UnityEngine;
using Meta.WitAi.Dictation;
using Meta.WitAi.Events;

public class VoiceListener : MonoBehaviour
{
    // [중요] 인스펙터에서 [BuildingBlock] Dictation 오브젝트를 여기로 드래그하세요.
    [SerializeField] private DictationService dictationService;

    void Awake()
    {
        // 1. 이벤트 연결을 코드로 강제 수행 (이벤트 창 필요 없음)
        if (dictationService != null)
        {
            dictationService.DictationEvents.OnFullTranscription.AddListener(OnFullTranscription);
            // [추가] 실시간 인식 결과 (글자가 변하는 과정을 보기 위함)
            dictationService.DictationEvents.OnPartialTranscription.AddListener(OnPartialTranscription);
            Debug.Log("이벤트 연결 완료!");
        }
    }

    void Start()
    {
        // 2. 마이크 강제 실행
        if (dictationService != null)
        {
            dictationService.Activate();
            Debug.Log("마이크 활성화 시도 중...");
        }
    }

    public void OnFullTranscription(string text)
    {
        // 3. 인식 결과 출력
        Debug.Log("인식 결과: " + text);
    }
    public void OnPartialTranscription(string text)
    {
        Debug.Log("<color=yellow>실시간 인식: </color>" + text);
    }
}