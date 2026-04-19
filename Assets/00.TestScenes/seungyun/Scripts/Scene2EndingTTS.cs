/*
 * 작성자: 김승윤
 * 작성일: 2026.04.18
 * 역할: TTS 자동 재생 스크립트(Scene2 엔딩)
 */
using UnityEngine;
using Meta.XR.BuildingBlocks.AIBlocks;

public class Scene2EndingTTS : MonoBehaviour
{
    public TextToSpeechAgent agent;


    public void PlayEndingNarration()
    {
        if (agent != null)
        {
            agent.SpeakText("나는 여러 차례 말을 건네며 상황을 수습하려 했으나, 그의 눈초리는 점점 더 날카로워져 갔다."
                + "\r\n더는 기회가 없을 것 같던 순간, 전화기 너머로 다급하면서도 단호한 목소리가 들려왔다."
                + "\r\n서랍을 열어보면 그간 모아둔 봉투가 있을 것이오. 그걸 건네주시오. \r\n!");
        }
    }
}