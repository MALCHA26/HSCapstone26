/*
 * 작성자: 김승윤
 * 작성일: 2026.03.29
 * 역할: TTS 자동 재생 스크립트(Scene2 시작)
 */
using UnityEngine;
using Meta.XR.BuildingBlocks.AIBlocks;

public class AutoSpeakSTTS2 : MonoBehaviour
{
    public TextToSpeechAgent agent2;

    void Start()
    {
        Invoke("PlayTTS", 0.5f);
    }

    void PlayTTS()
    {
        if (agent2 != null)
        {
            agent2.SpeakText("때는 1919년 2월 28일. 나는 보성사 인쇄소에서 밤새 독립선언서를 찍어내고 있었다." 
                +"\r\n당장 내일 아침이면 이 종이들이 전국으로 퍼져 나가야 하니, 지체할 여유란 털끝만큼도 없었다." 
                +"\r\n거대한 윤전기가 쉴 새 없이 돌아가던 그때, 밖에서 문을 두드리는 소리가 들렸다. \r\n!");
        }
    }
}