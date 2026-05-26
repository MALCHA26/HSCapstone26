/*
 * 작성자: 김승윤
 * 작성일: 2026.04.04
 * 역할: 랜덤 제시어 제공 및 STT 일치 여부 판별
 */

using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreProvider : MonoBehaviour
{
    public TextMeshProUGUI promptText; // 제시 문장
    public float scoreAmount = 10f; // 추가되는 게이지 양
    public string[] sentences = {
    "당신도 우리나라 백성이 아니오.",
    "제발 부탁이오.",
    "오늘 하루만 못 본 것으로 해 주시오.",
    "하루만 기다리면 내일은 온 세상이 다 알게 될 일이오.",
    "당신도 조선 사람인데 이러면 안 되지 않소.",
    "이번 한 번만 모른 척 넘어가 주시오.",
    "어차피 내일이면 다 끝날 일이오.",
    "나 혼자 살자고 하는 일이 아니오.",
    "나중에 어떻게 감당하려고 이러시오.",
    "여러 사람 목숨이 달린 일이오",
    };
    private string currentTarget;
    private int currentIndex = 0;
    private bool checkingSTT = false;
    private bool isFound = false;
    void Start()
    {
        SetNewPrompt();
    }
    void Update()
    {
        if (!isFound)
        {
            CheckCanvas();
        }
    }
    void CheckCanvas()
    {
        GameObject cameraRig = GameObject.FindWithTag("Camera");
        if (cameraRig != null)
        {
            Canvas foundCanvas = cameraRig.GetComponentInChildren<Canvas>(true);
            if (foundCanvas != null)
            {
                Transform sttScript = foundCanvas.transform.Find("STTScript");
                if (sttScript != null)
                {
                    promptText = sttScript.GetComponent<TextMeshProUGUI>();
                    isFound = true;

                    // 연결되자마자 첫번째 문장을 화면에 표시
                    if (promptText != null)
                    {
                        promptText.text = $"[다음 문장을 읽으세요]\n\n{currentTarget}";
                    }
                    Debug.Log("ScoreProvider: UI 연결 완료!");
                }
            }
        }
    }
    // 새로운 제시 문장 제공
    public void SetNewPrompt()
    {
        currentTarget = sentences[currentIndex];
        if (promptText != null)
        {
            promptText.text = $"[다음 문장을 읽으세요]\n\n{currentTarget}";
        }
        currentIndex++;
        if (currentIndex >= sentences.Length)
        {
            currentIndex = 0;
        }
    }

    // STT가 인식한 텍스트를 검사
    public void ProcessScore(string recognizedText)
    {
        if (checkingSTT) return;

        // 띄어쓰기나 마침표 제거 후 비교
        string cleanRecognized = recognizedText.Replace(" ", "").Replace(".", "").Trim();
        string cleanTarget = currentTarget.Replace(" ", "").Replace(".", "").Trim();
        bool isMatch = cleanRecognized.Contains(cleanTarget);
        if (isMatch)
        {
            Debug.Log("대사 일치");

            GaugeManager gauge = FindFirstObjectByType<GaugeManager>();
            if (gauge != null)
            {
                gauge.AddScore(scoreAmount); // 게이지 회복
            }
        }
        else
        {
            Debug.Log("대사 일치하지 않음");
        }
        StartCoroutine(ShowResultMent(isMatch));
    }

    // 인식된 문장 표시 -> 새로운 문장 제시
    private IEnumerator ShowResultMent(bool isMatch)
    {
        checkingSTT = true; // 중복 표시 방지 락 걸기

        // 제시 문장 확인용
        if (promptText != null)
        {
            string colorTag = isMatch ? "green" : "red";
            promptText.text = $"[다음 문장을 읽으세요]\n\n<color={colorTag}>{currentTarget}</color>";
        }

        // 1.5초 동안 표시
        yield return new WaitForSeconds(1.5f);

        // 텍스트 지우기 (사라짐 효과)
        if (promptText != null)
        {
            promptText.text = "";
        }

        // 0.5초 대기 
        yield return new WaitForSeconds(0.5f);

        // 다음 문장 제시
        SetNewPrompt();
        checkingSTT = false; 
    }
}