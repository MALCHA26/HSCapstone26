using UnityEngine;
using System.Collections;

public class Scene32FadeIn : MonoBehaviour
{
    private OVRScreenFade screenFade4;

    void Start()
    {
        screenFade4 = FindFirstObjectByType<OVRScreenFade>();
        StartCoroutine(ProcessEnding());

    }

    // Update is called once per frame
    IEnumerator ProcessEnding()
    {
        if (screenFade4 != null)
        {
            screenFade4.FadeIn();
            yield return new WaitForSeconds(1.0f); // 설정 시간만큼 대기
        }
    }
}
