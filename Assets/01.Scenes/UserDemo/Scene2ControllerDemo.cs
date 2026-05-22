/*
 * 작성자: 손다혜
 * 작성일: 2026.04.08
 * 수정 : 2026.04.27
 * 역할: Scene1 흐름 제어
 */

using UnityEngine;
using System.Collections;

public class Scene2ControllerDemo : AllSceneController
{




    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadNextScene();
        }
    }

    protected override IEnumerator RunSequence()
    {
        yield return null;
    }

    // 씬 전환
    public override void LoadNextScene()
    {
        sceneFade.FadeOut();
        Invoke(nameof(LoadNextSceneDelayed), 3f);
    }

    private void LoadNextSceneDelayed()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
    }
}
