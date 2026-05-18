/*
 * 작성자: 손다혜
 * 작성일: 2026.04.08
 * 수정 : 2026.04.27
 * 역할: Scene1 흐름 제어
 */

using UnityEngine;
using System.Collections;

public class Scene1Controller : AllSceneController
{
    [Header("Scene1 Components")]
    [SerializeField] private PaperDrop paperDrop;
    [SerializeField] private TextureChange textureChange;
    [SerializeField] private MapEffect mapEffect;
    [SerializeField] private ObjectSpawn printerSpawner;
    [SerializeField] protected Paper paper;

    [SerializeField] private GameObject LEE;

    [Header("캔버스 제어")]
    [SerializeField] private GameObject videoCanvas;
    [SerializeField] private UIManager uiManagerPrinter;
    [SerializeField] private UIManager uiManagerPaper;

    protected override IEnumerator RunSequence()
    {
        
        // 1. 영상 재생
        bool videoDone = false;
        videoPlayer.onComplete = () => videoDone = true;
        yield return new WaitForSeconds(2.5f);
        yield return new WaitUntil(() => videoDone);
        yield return VideoFadeTransition();
        
        //비디오 캔버스 비활성화
        videoCanvas.SetActive(false);
        yield return StartCoroutine(soundManager.PlayAndWait("Scene1_2", 0.4f));

        // 2. 종이 FallPaper 애니메이션 실행
        bool dropDone = false;
        paperDrop.onComplete = () => dropDone = true;
        paperDrop.Drop();
        soundManager.Play("PaperFall", 0.5f);
        yield return new WaitUntil(() => dropDone);

        //희연 추가 : 종이 잡기 안내 UI 띄우기
        uiManagerPrinter.ShowGuide("1Scenegrab");

        // 3. 종이 잡기 대기
        bool grabbed = false;
        paper.onGrabbed = () => grabbed = true;
        yield return new WaitUntil(() => grabbed);
        LEE.SetActive(true);


        //희연 추가 : 종이 잡기 안내 UI 숨기기
        uiManagerPrinter.HideGuide();

        
        // 4. 스크린 연출
        yield return StartCoroutine(textureChange.PlaySequence());

        // 5. 지도 연출
        yield return StartCoroutine(mapEffect.PlaySequence());
        
        // 6. 인쇄기 스폰
        uiManagerPaper.ShowGuide("1Sceneprint");
        soundManager.Play("Typing", 0.1f);
        yield return StartCoroutine(soundManager.PlayAndWait("Scene1_3", 0.4f));
        printerSpawner.Spawn();

    }

    // 씬 전환
    public override void LoadNextScene()
    {
        sceneFade.FadeOut();
        soundManager.Play("Printer", 0.7f);
        Invoke(nameof(LoadNextSceneDelayed), 3f);
    }

    private void LoadNextSceneDelayed()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
    }
}
