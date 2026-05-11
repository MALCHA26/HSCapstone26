/*
 * 작성자: 김승윤
 * 작성일: 2026.04.01
 * 역할: 지속 감소하는 설득 게이지 관리 스크립트
 */

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement; 

public class GaugeManager : MonoBehaviour
{
    public Image gaugeImage; // 게이지 이미지(독립선언서)
    public GameObject voiceUI; // STT 활성화 시 표시 텍스트
    public STTManager sttManager; // STT 관리 스크립트
    public GameObject canvas;
    public AudioSource printerSound;
    public AudioSource EndingTTS; 
    public float decreaseGauge = 2f; // 1초에 깎이는 수치 
    private OVRScreenFade screenFade2;
    private float currentScore = 100; // 현재 점수
    private const float maxScore = 100f; // 최대 점수
    private bool isOver = false; // 페이드 아웃 기준
    private bool isStarted = false; // 게이지 감소 시작 기준

    void Start()
    {
        currentScore = maxScore;
        if (gaugeImage != null)
        {
            gaugeImage.fillAmount = currentScore / maxScore; 
        }
        screenFade2 = FindFirstObjectByType<OVRScreenFade>();
    }

    void Update()
    {
        if (!isStarted || isOver) return;
        // 지속적으로 게이지 감소
        currentScore -= decreaseGauge * Time.deltaTime;

        if (gaugeImage != null)
        {
            gaugeImage.fillAmount = currentScore / maxScore;
        }

        // 0이 되면 페이드 아웃
        if (currentScore <= 0)
        {
            currentScore = 0;
            isOver = true;
            Debug.Log("게이지 0");
            StartCoroutine(ProcessEnding());
        }
    }

    IEnumerator ProcessEnding()
    {
        Debug.Log("페이드 아웃");
        canvas.SetActive(false);
        printerSound.Stop();

        // 암전
        if (screenFade2 != null)
        {
            screenFade2.FadeOut(); 
        }
        yield return new WaitForSeconds(2.0f);
        
        // 나레이션 재생
        if (EndingTTS != null)
        {
            EndingTTS.Play();
            yield return new WaitForSeconds(EndingTTS.clip.length + 2.0f); // 대사가 끝날 때까지 대기(초 설정)
        }

        // 다음 씬 이동
        SceneManager.LoadScene("Scene3-LightEffect");
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
        VoiceUIActive(false);

        
        ScoreProvider provider = FindFirstObjectByType<ScoreProvider>();
        if (provider != null)
        {
            provider.ProcessScore(text);
        }
        StartCoroutine(RestartSTT());
    }

    IEnumerator RestartSTT()
    {
        // 2초 동안 자막 제공
        yield return new WaitForSeconds(2.0f);
       
        // STT 다시 시작
        if (sttManager != null)
        {
            sttManager.StartSTT();
        }
    }

    // 게이지 추가
    public void AddScore(float amount)
    {
        currentScore += amount;
        if (currentScore > maxScore) currentScore = maxScore; 
    }

    public void StartGauge()
    {
        isStarted = true;
        Debug.Log("설득 시작");
    }
}