/*
 * 작성자: 김승윤
 * 작성일: 2026.04.01
 * 역할: 점수 가져와서 설득 게이지 조절 스크립트
 */

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GaugeManager : MonoBehaviour
{
    public Image gaugeImage;
    public float fillSpeed = 3f; // 채워지는 속도
    public TextMeshProUGUI subtitleText; // STT 자막
    public GameObject voiceUI;

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

    // 마이크가 켜질 때 UI에 표시
    public void VoiceUIActive(bool isActive)
    {
        if (voiceUI != null)
        {
            voiceUI.SetActive(isActive);
        }
    }

    // STT -> 텍스트 
    public void GetSTTText(string text)
    {
        Debug.Log("STT가 들은 말: " + text);
        VoiceUIActive(false);
        if (subtitleText != null)
        {
            subtitleText.text = text;
        }

        ScoreProvider provider = FindFirstObjectByType<ScoreProvider>();
        if (provider != null)
        {
            provider.ProcessScore(text);
        }
    }

    // 점수 받아서 게이지 업데이트
    public void UpdateScore(float score)
    {
        currentScore = score;
        if (currentScore >= maxScore)
        {
            currentScore = maxScore;
        }
        targetFill = currentScore / maxScore;
        Debug.Log($"현재 목표 게이지: {targetFill * 100}%");
    }
}