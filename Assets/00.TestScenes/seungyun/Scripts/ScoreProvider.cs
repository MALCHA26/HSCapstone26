/*
 * 작성자: 김승윤
 * 작성일: 2026.04.04
 * 역할: 게이지 점수 조작 관련 스크립트
 */

using UnityEngine;

public class ScoreProvider : MonoBehaviour
{
    float[] setScore = { 40f, 70f, 50f, 20f, 0f };
    int currentTime = 0;

    public void ProcessScore(string prompt)
    {
        // 1초 뒤에 점수 전달 
        Invoke("SendScore", 1f);
    }

    public void SendScore()
    {
        if (currentTime < setScore.Length)
        {
            float scoreToSend = setScore[currentTime];

            GaugeManager gauge = FindFirstObjectByType<GaugeManager>();
            if (gauge != null)
            {
                gauge.UpdateScore(scoreToSend);
            }

            if (currentTime == setScore.Length - 1)
            {
                Scene2Ending ending = FindFirstObjectByType<Scene2Ending>();
                if (ending != null)
                {
                    ending.StartEnding();
                }
            }
            // 다음 점수 제공 대기
            currentTime++;
        }
        else
        {
            Debug.Log("점수 배정 끝");
        }
    }
   
}