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
                + "\r\n당장 내일 아침이면 이 종이들이 전국으로 퍼져 나가야 하니, 지체할 여유란 털끝만큼도 없었다.\r\n"
                + "\r\n 이 종이들을 그대로 실어 나르기에는 아무래도 눈에 띄겠어..... 겉보기에는 그저 평범한 짐처럼 보이도록 해야 할 텐데.\r\n"
                + "\r\n…옳지, 저기 성주 이씨 족보가 있었지.\r\n"
                + "\r\n 그걸 위에 얹어 두면, 의심을 조금은 피할 수 있을 것이다.\r\n!");
        }
    }
}