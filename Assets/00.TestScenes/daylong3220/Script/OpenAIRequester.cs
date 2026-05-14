using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class OpenAIRequester : MonoBehaviour
{
    private string apiKey = "";
    private string model = "gpt-4o-mini";
    public string answer;
    public bool isSpeaking = false;
    public System.Action<string> onAnswerReceived; // 답변 수신 시 외부 콜백

    private OpenAIRequest requestData;

    void Awake()
    {
        answer = "그대, 본인에게 물어보고 싶은 것이 있소?";
        //api key 불러오기
        string directoryPath = System.IO.Path.Combine(Application.dataPath, "00.TestScenes", "daylong3220", "script");
        string filePath = System.IO.Path.Combine(directoryPath, "Secrets.txt");
        apiKey = System.IO.File.ReadAllText(filePath).Trim();
        Debug.Log(apiKey);

    }

    public void AskAI(string prompt)
    {
        StartCoroutine(SendOpenAIRequest(prompt));
    }

    IEnumerator SendOpenAIRequest(string prompt)
    {
        //만약 Awake() 함수에 의해 초기화 되지 않고서 실행이 된다면 다시 초기화 하도록(생글턴 응용?)
        if (requestData == null)
        {
            requestData = new OpenAIRequest();
            requestData.model = model;
        }

        string url = "https://api.openai.com/v1/chat/completions";

        //요청용 JSON 데이터 구조 생성(requestData 인스턴스는 계속해서 재사용(메시지 부분만 갱신))
        requestData.messages = new Message[] {
            new Message { role = "user", content = prompt }
        };

        //요청 데이터 json방식으로 변환
        string jsonBody = JsonUtility.ToJson(requestData);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);

        //요청데이터를 보내기 위한 헤더 준비(나는 요청을 이런이런 식으로 보낼 것이며 이런 방식으로 받을 것이다.)
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        // 헤더 설정 (OpenAI 핵심: Bearer 인증(제미나이에는 없는 공정이다.))
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + apiKey);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string responseJson = request.downloadHandler.text;
            //응답 클래스 파싱
            OpenAIResponse openAIResponse = JsonUtility.FromJson<OpenAIResponse>(responseJson);

            if (openAIResponse != null &&
                openAIResponse.choices != null &&
                openAIResponse.choices.Length > 0)
            {
                //응답 내용
                string chatContent = openAIResponse.choices[0].message.content;

                //AIResponse 클래스로 다시 파싱(응답 추출을 위해)
                AIResponse finalResponse = JsonUtility.FromJson<AIResponse>(chatContent);
                HandleAIResponse(finalResponse);
            }
        }
        else
        {
            Debug.LogError("OpenAI 요청 실패: " + request.error);
        }
    }

    void HandleAIResponse(AIResponse response)
    {
        if (response != null)
        {
            Debug.Log("OpenAI 대답: " + response.text);
            answer = response.text;

            // 답변 텍스트를 외부(Scene4Controller)로 전달
            onAnswerReceived?.Invoke(response.text);
        }
    }


    //OpenAI 전용 데이터 구조
    [System.Serializable]
    public class OpenAIRequest
    {
        public string model;
        public Message[] messages;
    }

    [System.Serializable]
    public class Message
    {
        public string role;
        public string content;
    }

    //응답용 구조
    [System.Serializable]
    public class OpenAIResponse
    {
        public Choice[] choices;
    }

    [System.Serializable]
    public class Choice
    {
        public Message message;
    }

    [System.Serializable]
    public class AIResponse
    {
        //public int score;
        public string text;
    }
}

