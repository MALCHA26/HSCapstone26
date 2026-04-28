/*
 * 작성자: 손다혜
 * 작성일: 2026.04.28
 * 역할: 전체 씬의 공통 기능 정의
 */


using UnityEngine;
using System.Collections;

public abstract class AllSceneController : MonoBehaviour
{
    [Header("Scene")]
    [SerializeField] protected SceneFade sceneFade;
    [SerializeField] protected string nextSceneName;

    [Header("Components")]
    [SerializeField] protected VideoPlayer videoPlayer;
    [SerializeField] protected SoundManager soundManager;

    protected virtual void Start()
    {
        sceneFade.FadeIn();
        StartCoroutine(RunSequence());
    }

    protected abstract IEnumerator RunSequence();

    // 나레이션 재생 + 종료 대기
    protected IEnumerator Narrate(string text)
    {
        bool done = false;
        TTSManager.Instance.Speak(text, () => done = true);
        yield return new WaitUntil(() => done);
    }

    // 영상 종료 대기
    protected IEnumerator WaitForVideo()
    {
        bool done = false;
        videoPlayer.onComplete = () => done = true;
        yield return new WaitUntil(() => done);
    }

    // 영상 종료 후 페이드 아웃 → 인
    protected IEnumerator VideoFadeTransition()
    {
        sceneFade.FadeOut();
        yield return new WaitForSeconds(sceneFade.FadeDuration);
        Destroy(videoPlayer.gameObject);
        sceneFade.FadeIn();
    }

    // 다음 씬 호출
    public virtual void LoadNextScene()
    {
        sceneFade.FadeOut();
        Invoke(nameof(LoadScene), 3f);
    }

    private void LoadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
    }
}
