/*
 * 작성자: 손다혜
 * 작성일: 2026.04.27
 * 역할: Scene4 흐름 제어
 */

using UnityEngine;
using System.Collections;
using TMPro;

public class Scene4Controller : AllSceneController
{
    [Header("Scene4 Components")]
    [SerializeField] private VoiceListener voiceListener;
    [SerializeField] private Canvas uiCanvas;
    [SerializeField] private OpenAIRequester aiRequester;

    [Header("캔버스 제어")]
    [SerializeField] private GameObject videoCanvas;
    [SerializeField] private GameObject QACanvas;


    public TextMeshProUGUI an_box;

    protected override IEnumerator RunSequence()
    {
        aiRequester = GameObject.Find("aimanager").GetComponent<OpenAIRequester>();

        // 1. 영상 재생 + 나레이션 음원 재생
        bool videoDone = false;
        videoPlayer.onComplete = () => videoDone = true;

        //yield return new WaitForSeconds(2.5f);
        //yield return StartCoroutine(soundManager.PlayAndWait("Scene4Narration", 0.2f));

        yield return new WaitUntil(() => videoDone);
        yield return VideoFadeTransition();

        videoCanvas.SetActive(false);
        //QACanvas.SetActive(true);

        // 2. AI 답변 수신 시 TTS + 버튼 제어 연결

        if (aiRequester != null)
        {
            aiRequester.onAnswerReceived = (text) =>
            {
                StartCoroutine(SpeakAndUnlock(text));
            };
        }


        // 3. Q&A 시작 (STT 활성화)
        if (uiCanvas != null)
        {
            uiCanvas.gameObject.SetActive(true);
            UIChangeManager uiManager = uiCanvas.GetComponent<UIChangeManager>();
            if (uiManager != null)
            {
                uiManager.ShowQuestionUI();
            }
        }
        if (voiceListener != null)
            voiceListener.gameObject.SetActive(true);
    }

    private IEnumerator SpeakAndUnlock(string text)
    {
        // TTS 재생 중 버튼 잠금
        if (aiRequester != null) aiRequester.isSpeaking = true;

        an_box.text = aiRequester.answer;
        yield return Narrate(text);

        // TTS 완료 후 버튼 해제
        if (aiRequester != null) aiRequester.isSpeaking = false;
    }
}
