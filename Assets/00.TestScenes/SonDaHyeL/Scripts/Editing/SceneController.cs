/*
 * 작성자: 손다혜
 * 작성일: 2026.04.08
 * 역할: Scene1 흐름 제어 개선
 */

using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour
{
    [Header("Scene")]
    [SerializeField] private SceneFade sceneFade;
    [SerializeField] private string nextSceneName = "Scene2";

    [Header("Components")]
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private PaperDrop paperDrop;
    [SerializeField] private Paper paper;
    [SerializeField] private TextureChange textureChange;
    [SerializeField] private MapEffect mapEffect;
    [SerializeField] private ObjectSpawn printerSpawner;
    [SerializeField] private SoundManager soundManager;

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

        yield return Narrate("영상 나레이션 텍스트");
        yield return new WaitUntil(() => videoDone);
        // 영상 종료 후 페이드 아웃 → 페이드 인
        sceneFade.FadeOut();
        yield return new WaitForSeconds(sceneFade.FadeDuration);
        Destroy(videoPlayer.gameObject);
        sceneFade.FadeIn();
        yield return new WaitForSeconds(sceneFade.FadeDuration);

        // 2. 종이 FallPaper 애니메이션 실행
        bool dropDone = false;
        paperDrop.onComplete = () => dropDone = true;
        paperDrop.Drop();
        soundManager.Play("PaperFall");

        yield return new WaitUntil(() => dropDone);

        // 3. 종이 잡기 대기
        bool grabbed = false;
        paper.onGrabbed = () => grabbed = true;

        yield return new WaitUntil(() => grabbed);

        // 4. 스크린 연출 (FadeIn → 나레이션 → 종료 → FadeOut)
        yield return PlayScreen("첫 번째 나레이션");
        yield return PlayScreen("두 번째 나레이션");
        yield return PlayScreen("세 번째 나레이션");

        // 5. 지도 연출
        yield return StartCoroutine(mapEffect.Play("지도 나레이션 텍스트"));

        // 6. 인쇄기 스폰
        printerSpawner.Spawn();

    }

    // 나레이션 재생 + 종료 대기 기능 
    private IEnumerator Narrate(string text)
    {
        bool done = false;
        TTSManager.Instance.Speak(text, () => done = true);
        yield return new WaitUntil(() => done);
    }

    // 스크린 FadeIn,Out + 나레이션 흐름 
    private IEnumerator PlayScreen(string narration)
    {
        bool fadeInDone = false;
        textureChange.onFadeInComplete = () => fadeInDone = true;
        textureChange.PlayNext();
        yield return new WaitUntil(() => fadeInDone);

        yield return Narrate(narration);

        bool fadeOutDone = false;
        textureChange.onFadeOutComplete = () => fadeOutDone = true;
        textureChange.FadeOut();
        yield return new WaitUntil(() => fadeOutDone);
    }

    // 다음 씬 호출
    public void LoadNextScene()
    {
        Invoke(nameof(LoadScene), 3f);
    }

    private void LoadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
    }
}
