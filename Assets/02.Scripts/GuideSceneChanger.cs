/*
 * 작성자: 김승윤
 * 작성일: 2026.05.19
 * 역할: 가이드 씬 전환 스크립트
 */
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GuideSceneChanger : MonoBehaviour
{
    public bool prac1= false;
    public bool prac2 = false;
    public bool prac3 = false;
    public string nextSceneName = "Scene1"; // 이동할 씬 이름
    private OVRScreenFade screenFadeOut;
    private bool isChanging = false;
    void Start()
    {
        screenFadeOut = FindFirstObjectByType<OVRScreenFade>();
    }
    void Update()
    {
        if (prac1 && prac2 && prac3)
        {
            TriggerSceneChange();
        }
    }
    public void SetPrac1True()
    {
        prac1 = true;
    }
    public void SetPrac2True()
    {
        prac2 = true;
    }

    public void SetPrac3True()
    {
        prac3 = true;
    }

    public void TriggerSceneChange()
    {
        if (isChanging) return;
        isChanging = true;

        StartCoroutine(ExitGuideScene());
    }
    IEnumerator ExitGuideScene()
    {
        yield return new WaitForSeconds(1.0f);

        if (screenFadeOut != null)
        {
            screenFadeOut.FadeOut();
            yield return new WaitForSeconds(1.0f);
        }
        SceneManager.LoadScene(nextSceneName);

    }
}
