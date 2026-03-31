/*
 * 작성자: 김승윤
 * 작성일: 2026.04.01
 * 역할: LLM 제공 점수 가져와서 설득 게이지 조절 스크립트
 */

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GaugeManager : MonoBehaviour
{
    public Image gaugeImage;
    public float fillSpeed = 3f; // 채워지는 속도
    public TextMeshProUGUI subtitleText; // STT 자막

    private float targetFill = 0; // 적용 게이지
    private float currentScore = 0; // 현재 점수
    private const float maxScore = 100f; // 최대 점수

    void Update()
    {
        // 게이지 채울 때 부드럽게 설정
        if (gaugeImage != null)
        {
            gaugeImage.fillAmount = Mathf.Lerp(gaugeImage.fillAmount, targetFill, Time.deltaTime * fillSpeed);
        }
    }

    // STT -> 텍스트 -> LLM(OpenAI)
    public void GetSTTText(string text)
    {
        Debug.Log("STT가 들은 말: " + text);

        if (subtitleText != null)
        {
            subtitleText.text = $"{text}";
        }

        //OpenAIRequester requester = FindFirstObjectByType<OpenAIRequester>();
        //if (requester != null)
        //{
        //    requester.AskAI(text);
        //}
        //else
        //{
        //    Debug.LogError("OpenAIRequester를 씬에서 찾을 수 없습니다!");
        //}
    }

    // 점수 받아서 게이지 업데이트
    public void UpdateScore(float score)
    {
        currentScore += score;
        if (currentScore >= maxScore)
        {
            currentScore = maxScore;
        }
        targetFill = currentScore / maxScore;
        Debug.Log($"현재 누적 점수: {currentScore}점 / {maxScore}점 (목표: {targetFill * 100}%)");
    }
}