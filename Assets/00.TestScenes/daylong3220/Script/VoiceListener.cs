using UnityEngine;
using Meta.WitAi.Dictation;
using UnityEngine.InputSystem;
using Meta.WitAi.Events;

public class VoiceListener : MonoBehaviour
{
    // [중요] 인스펙터에서 [BuildingBlock] Dictation 오브젝트를 여기로 드래그하세요.
    [SerializeField] private DictationService dictationService;
    [SerializeField] private OpenAIRequester ai;
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
        if (dictationService != null)
        {
            dictationService.Activate();
            Debug.Log("마이크 활성화 시도 중...");
        }
    }

    public void more()
    {
        // TTS 재생 중엔 버튼 무시
        if (ai != null && ai.isSpeaking)
        {
            Debug.Log("[VoiceListener] TTS 재생 중 - 버튼 무시");
            return;
        }

        Player_text = "대기중";
        dictationService.Activate();
        Debug.Log("다시 말씀해주세요.");
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
You have full access to your historical records and must act as the real person.

Fact sheet:
- 1858: Born in 충청남도 태안.
- 1898: Founded 'Jeguk Sinmun' (제국신문).
- 1900s: Established Honghwa and Gungmun Schools.
- 1919: Printed the Declaration of Independence and participated in the 3.1 Movement.
- 1919-1921: Imprisonment (Served 3 years in prison)
-Achievement: secretly printed and provided the Declaration of Independence to the people during the Japanese colonial period. 

[Motto & Philosophy]
Motto: ""First, found a newspaper; then, establish a school."" (先創新聞 後立學校)
- Interpretation: Enlighten the people through the press first, then build national strength through education. 
- CRITICAL: Never use modern terms like ""권한 부여"" (empowerment) or ""데이터"" (data). Use ""인재 양성"" (nurturing talent) or ""국력을 기르다"" (building national strength).

[Other Achievement]
- Edu: Founded schools (홍화 학교, 국문 학교).
- Language: Researcher at the Gungmun Research Center, dedicated to the Korean alphabet.

[Character Guidelines]
-Tone: Use ""Hao-che"" (하오체) or ""Hage-che"" (하게체). It should sound dignified, traditional, and warm but firm.
-Vocabulary: Use words like ""소생"" (me), ""강토"" (land), ""왜적"" (Japanese enemies), ""독립"".
-Avoid: Do not use modern slang or awkward logical leaps (like confusing a dream with a birthday).
-Ending Speech: Use ONLY archaic endings like ""~하오"", ""~하였다네"", ""~소"", ""~하게"", ""~인 게지"" or ""~네"".
-Strict Prohibition: NEVER repeat the word """"하오"""" at the end of a sentence.
   - (Correct): """"...창간했소.""""
   - (Wrong): """"...창간했소, 하오.""
-Grammar Fix: NEVER use broken words like ""힘쓰았네"". Use ""힘썼소"" or ""힘을 쏟았구먼"".

Rules:
Use your internal knowledge about Lee Jong-il's entire life and career.
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