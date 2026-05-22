/*
 * 작성자: 손다혜
 * 작성일: 2026.04.08
 * 수정 : 2026.04.27
 * 역할: Scene1 흐름 제어
 */

using UnityEngine;
using System.Collections;

public class Scene1ControllerDemo : AllSceneController
{



    [Header("Scene1 Components")]

    [SerializeField] private GameObject videoCanvas;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadNextScene();
        }
    }

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
        //yield return StartCoroutine(soundManager.PlayAndWait("Scene1_2", 0.4f));

       
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
