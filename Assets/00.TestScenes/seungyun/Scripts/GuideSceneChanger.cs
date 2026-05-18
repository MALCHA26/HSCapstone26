using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GuideSceneChanger : MonoBehaviour
{
    public string nextSceneName = "Scene1"; // 이동할 씬 이름
    private OVRScreenFade screenFadeOut;
    private bool isChanging = false;
    void Start()
    {
        screenFadeOut = FindFirstObjectByType<OVRScreenFade>();
    }
    public void TriggerSceneChange()
    {
        if (isChanging) return;
        isChanging = true;

        StartCoroutine(ExitGuideScene());
    }
    IEnumerator ExitGuideScene()
    {
        yield return new WaitForSeconds(2.0f);

        if (screenFadeOut != null)
        {
            screenFadeOut.FadeOut();
            yield return new WaitForSeconds(1.0f);
        }
        SceneManager.LoadScene(nextSceneName);

    }
}
