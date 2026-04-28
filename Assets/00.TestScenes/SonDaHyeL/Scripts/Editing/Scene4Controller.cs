/*
 * 작성자: 손다혜
 * 작성일: 2026.04.27
 * 역할: Scene4 흐름 제어
 */

using UnityEngine;
using System.Collections;

public class Scene4Controller : MonoBehaviour
{
    [Header("Scene")]
    [SerializeField] private SceneFade sceneFade;

    [Header("Components")]
    [SerializeField] private VideoPlayer videoPlayer;

    private void Start()
    {
        sceneFade.FadeIn();
        StartCoroutine(RunSequence());
    }

    private IEnumerator RunSequence()
    {
        // 1. 영상 재생
        bool videoDone = false;
        videoPlayer.onComplete = () => videoDone = true;

        yield return Narrate("여러분이 선언서를 무사히 인쇄하고 옮겨준 덕분에, 기미독립선언서는 전국 각지의 독립운동가들에게 안전하게 전달될 수 있었습니다.");
        yield return Narrate("이후 3월 1일 정오 무렵, 민족대표 33인은 서울 인사동 태화관에 모여 독립선언식을 거행했습니다.");
        yield return Narrate("한편 탑골공원에서는 신원 미상의 한 청년이 단상에 올라 선언서를 낭독하였고, 이 순간을 기점으로 민족사적, 사상사적. 경제사적인 측면에서 중요한 의의를 남긴 삼일 운동이 전국으로 확산될 수 있었습니다.");

        yield return new WaitUntil(() => videoDone);        
        // 영상 종료 후 페이드 아웃 > 페이드 인
        sceneFade.FadeOut();
        yield return new WaitForSeconds(sceneFade.FadeDuration);
        Destroy(videoPlayer.gameObject);
        sceneFade.FadeIn();
    }

    private IEnumerator Narrate(string text)
    {
        bool done = false;
        TTSManager.Instance.Speak(text, () => done = true);
        yield return new WaitUntil(() => done);
    }
}
