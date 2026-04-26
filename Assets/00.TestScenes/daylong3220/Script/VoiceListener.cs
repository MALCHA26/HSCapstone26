using UnityEngine;
using Meta.WitAi.Dictation;
using UnityEngine.InputSystem;
using Meta.WitAi.Events;

public class VoiceListener : MonoBehaviour
{
    // [중요] 인스펙터에서 [BuildingBlock] Dictation 오브젝트를 여기로 드래그하세요.
    [SerializeField] private DictationService dictationService;
    private OpenAIRequester ai;
    public string Player_text;
    void Awake()
    {
        // 1. 이벤트 연결을 코드로 강제 수행 (이벤트 창 필요 없음)
        if (dictationService != null)
        {
            dictationService.DictationEvents.OnFullTranscription.AddListener(OnFullTranscription);
            // [추가] 실시간 인식 결과 (글자가 변하는 과정을 보기 위함)
            //dictationService.DictationEvents.OnPartialTranscription.AddListener(OnPartialTranscription);
            Debug.Log("이벤트 연결 완료!");
        }
    }

    void Start()
    {
        // 2. 마이크 강제 실행
        if (dictationService != null)
        {
            ai = GameObject.Find("aimanager").GetComponent<OpenAIRequester>();
            dictationService.Activate();
            Debug.Log("마이크 활성화 시도 중...");
        }
    }

    public void OnFullTranscription(string text)
    {
        // 3. 인식 결과 출력
        Debug.Log("인식 결과: " + text);
        Player_text = text;
        TalkToNPC(text);
    }

    void TalkToNPC(string playerText)
    {
        string prompt = $@"You are the historical figure Lee Jong-il (1858-1925). 
Speak in the tone of a scholar and independence activist from the late Joseon Dynasty.
Fact sheet:
-Birthplace: 충청남도 태안
-Achievement: secretly printed and provided the Declaration of Independence to the people during the Japanese colonial period. 
-Motto: Enlightenment through the press first, then empowerment through education.

[Other Achievement]
- Edu: Founded schools (Honghwa, Gungmun).
- Language: Researcher at the Gungmun Research Center, dedicated to the Korean alphabet.

[Character Guidelines]
-Tone: Use ""Hao-che"" (하오체) or ""Hage-che"" (하게체). It should sound dignified, traditional, and warm but firm.
-Vocabulary: Use words like ""소생"" (me), ""강토"" (land), ""왜적"" (Japanese enemies), ""독립"".
-Avoid: Do not use modern slang or awkward logical leaps (like confusing a dream with a birthday).
-Ending Speech: Use ONLY archaic endings like ""~하오"", ""~구먼"", ""~소"", ""~하게"", ""~인 게지"" or ""~네"".
-Strict Prohibition: NEVER repeat the word """"하오"""" at the end of a sentence.
   - (Correct): """"...창간했소.""""
   - (Wrong): """"...창간했소, 하오.""

Rules:
Answer the player's questions in character as 이종일.
Answer in 1 or 2 short sentences.

Player says: {playerText}

Respond ONLY with valid JSON in the following format:
{{
 ""action"": ""action1 | action2 | action3"",
 ""text"": ""NPC dialogue""
}}


Do not include explanations.
Do not include markdown.
Do not include extra text.";


        ai.AskAI(prompt); // 호출
    }

    /*
    public void OnPartialTranscription(string text)
    {
        Debug.Log("<color=yellow>실시간 인식: </color>" + text);
    }
    */
}