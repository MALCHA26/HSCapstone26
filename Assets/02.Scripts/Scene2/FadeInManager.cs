using UnityEngine;
using System.Collections;

public class FadeInManager : MonoBehaviour
{
    private OVRScreenFade screenFade3;

    void Start()
    {
        screenFade3= FindFirstObjectByType<OVRScreenFade>();
        StartCoroutine(ProcessEnding());

    }

    // Update is called once per frame
    IEnumerator ProcessEnding()
    {
        if (screenFade3 != null)
        {
            screenFade3.FadeIn();
            yield return new WaitForSeconds(1.0f); // 설정 시간만큼 대기
        }
    }
}
