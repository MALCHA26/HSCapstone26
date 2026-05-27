/*
 * 작성자: 손다혜
 * 작성일: 2026.03.22
 * 수정일: 2026.04.28
 * 역할: OVRScreenFade를 이용한 전체 화면 페이드인-아웃 효과 구현
 */

using UnityEngine;

public class SceneFade : MonoBehaviour
{
    public float FadeDuration => OVRScreenFade.instance != null ? OVRScreenFade.instance.fadeTime : 1f;

    public void FadeIn()
    {
        OVRScreenFade.instance?.FadeIn();
    }

    public void FadeOut()
    {
        OVRScreenFade.instance?.FadeOut();
    }
}
