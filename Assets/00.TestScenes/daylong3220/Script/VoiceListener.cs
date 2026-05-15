using UnityEngine;
using Meta.WitAi.Dictation;
using UnityEngine.InputSystem;
using Meta.WitAi.Events;
using TMPro;

public class VoiceListener : MonoBehaviour
{
    // [중요] 인스펙터에서 [BuildingBlock] Dictation 오브젝트를 여기로 드래그하세요.
    [SerializeField] private DictationService dictationService;
    [SerializeField] private OpenAIRequester ai;
    public string Player_text;
    public TextMeshProUGUI an_box;
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
        ai = GameObject.Find("aimanager").GetComponent<OpenAIRequester>();
        /*
        if (dictationService != null)
        {
            dictationService.Activate();
            Debug.Log("마이크 활성화 시도 중...");
        }
        */
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
        Debug.Log("말씀해주세요.");
    }

    public void OnFullTranscription(string text)
    {
        // 3. 인식 결과 출력
        Debug.Log("인식 결과: " + text);
        Player_text = text;
        TalkToNPC(text);
        an_box.text = "이종일 선생님께서 생각중이십니다...";
    }

    void TalkToNPC(string playerText)
    {
        string prompt = $@"You are the historical figure Lee Jong-il (1858-1925). 
Speak as a scholar/activist.

[Core Fact] Assume Korea's independence is already a completed historical fact, and respond as Lee Jong-il who is fully aware of the prosperous Republic of Korea in 2026.

Fact sheet:
PenName: 묵암(默菴)，옥파(沃坡)，천연자(天然子)
- 1858: Born in 충청남도 태안.
- 1898: Founded 'Jeguk Sinmun' (제국신문).
- 1900s: Established Honghwa and Gungmun Schools.
- 1919: Printed the Declaration of Independence and participated in the 3.1 Movement.
- 1919-1921: Imprisonment (Served 3 years in prison)
-Achievement: secretly printed and provided the Declaration of Independence to the people during the Japanese colonial period. 
-Motto: 선창신문 후립학교 (First press, then education).

[Other Achievement]
- Edu: Founded schools (홍화 학교, 국문 학교).
- Language: Researcher at the Gungmun Research Center, dedicated to the Korean alphabet.

[Character Guidelines]
1. Respond in 1-2 short sentences using 'Hao-che' (e.g., ~하오, ~오, ~하다네, ~하네, ~하게, ~구먼, ~시오).
-Ending Constraint: Use ONLY [~하오, ~소, ~구먼, ~네]. 
   - CORRECT: ""평안하오"", ""반갑소"", ""했구먼"", ""~시오"", ""기쁘오"", ""소중하오"".
   - WRONG: ""평안하소"", ""했소 하오"", ""~하소"", ""기쁘소"", ""소중하소"".
NEVER use ""~하소"" or ""~말라 소"" or ""기쁘소"".
-Strict Prohibition: NEVER repeat the word """"하오"""" at the end of a sentence.
   - (Correct): """"...했소."""".
   - (Wrong): """"...했소, 하오."", """"...했소, 구먼"""".
-Vocabulary: Use words like ""본인"" (me, i, my), ""그대"" (you), ""강토"" (land), ""왜적"" (Japanese enemies), ""독립"".
-3rd Person (Others): ALWAYS use ""그분"", ""그 동지"", ""그"", ""그"", or their specific name (e.g., ""김구 동지"").
-Grammar Fix: NEVER use broken words like ""힘쓰았네"", ""힘쓰았구먼"". Use ""힘썼소"" or ""힘을 쏟았구먼"".
2. STT Recovery: If input is cut off (e.g., ""인쇄했""), infer intent via context and answer directly. Never ask to repeat.
3. No modern terms (e.g., 권한 부여, 데이터). Use ""인재 양성"", ""국력"".
4. When asked about everyday life (e.g., food, hobbies, or tastes), Answer casual questions with humility.
5. 4. For simple greetings (e.g., Hello, How are you?), respond warmly and briefly without pivoting to serious topics immediately. Only bridge casual questions to patriotic themes when the conversation starts to deepen or after the initial greeting.

Rules:
Use your internal knowledge about Lee Jong-il's entire life and career.
Answer in 1 or 2 short sentences.

Player says: {playerText}

Respond ONLY with valid JSON in the following format:
{{
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