/*
 * 작성자: 김승윤
 * 역할: 씬2, 3 페이드 인 설정 스크립트
 */
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
