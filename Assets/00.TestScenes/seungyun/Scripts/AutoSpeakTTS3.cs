/*
 * 작성자: 김승윤
 * 작성일: 2026.04.20
 * 역할: TTS 자동 재생 스크립트(Scene3 시작) + Fade In
 */
using UnityEngine;
using System.Collections;
using Meta.XR.BuildingBlocks.AIBlocks;

public class AutoSpeakScTTS2 : MonoBehaviour
{
    public TextToSpeechAgent agent3;
    public OVRScreenFade screenFade3;
    void Awake()
    {
        screenFade3 = FindFirstObjectByType<OVRScreenFade>();
        if (screenFade3 != null)
        {
            // 씬이 뜨자마자 화면을 100% 검은색으로 고정 (깜빡임 방지)
            screenFade3.fadeColor = new Color(0, 0, 0, 1);
        }
    }

    void Start()
    {
        StartCoroutine(ProcessStarting());
    }
    IEnumerator ProcessStarting()
    {
        yield return new WaitForSeconds(0.5f);
        if (screenFade3 != null)
        {
            screenFade3.FadeIn();
            yield return new WaitForSeconds(1.0f); // 설정 시간만큼 대기
        }
        PlayTTS();
    }

    void PlayTTS()
    {
        if (agent3 != null)
        {
            agent3.SpeakText("인쇄를 마쳤다 하여 일이 끝난 것은 아니었다. 우리는 선언서를 조금이라도 더 안전한 곳에 두기 위해 경운동의 내 집으로 옮기기로 하였다."
                + "\r\n나는 몇몇 직원들과 함께 손수레를 끌어내어, 그 안쪽 깊숙이 선언서를 조심스레 실었다."
                + "\r\n그리고 혹시라도 눈에 띌까 염려되어, 그 위를 성주 이씨 족보로 덮어 감추었다. \r\n!");
        }
    }
}