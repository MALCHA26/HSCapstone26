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
            agent.SpeakText("나는 그의 앞에 몸을 낮추며 간절히 말했다."
            + "\r\n“당신 또한 이 땅의 백성이라면, 이 뜻을 모를 리 없을 것입니다."
            + "\r\n내일이면 모든 것이 세상에 밝혀질 터이니, 오늘 하루만… 부디 못 본 것으로 해 주시오.”"
            + "\r\n그러나 형사의 표정은 좀처럼 누그러지지 않았다."
            + "\r\n더는 물러설 길이 없다고 판단한 나는, 마지막 방법을 택할 수밖에 없었다."
            + "\r\n“여기 잠시만 기다리고 계시면 내 잠깐 우리 의암 선생을 뵙고 오겠으니 조금만 시간을 주시게.”"
            + "\r\n잠시 후, 나는 손에 거금 오천 원 뭉치를 들고 인쇄소로 돌아왔다."
            + "\r\n잠시 침묵이 흐른 뒤, 형사는 더 캐묻지 않겠다는 듯 시선을 거두었다."
            + "\r\n그리고 아무 일도 없다는 듯, 조용히 발걸음을 돌렸다.");
        }
    }
}