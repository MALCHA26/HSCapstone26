/*
 * 작성자: 손다혜
 * 작성일: 2026.03.22
 * 역할: UI를 이용해 전체 화면 페이드인-아웃 효과 구현
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneFade : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 1f;

    public float FadeDuration => fadeDuration;

    private bool isFading = false;

    void Awake()
    {
        SetAlpha(1f);
    }

    public void FadeIn()
    {
        if (!isFading)
            StartCoroutine(FadeCoroutine(1f, 0f));
    }

    public void FadeOut()
    {
        if (!isFading)
            StartCoroutine(FadeCoroutine(0f, 1f));
    }

    private IEnumerator FadeCoroutine(float from, float to)
    {
        isFading = true;

        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            SetAlpha(Mathf.Lerp(from, to, t / fadeDuration));
            yield return null;
        }

        SetAlpha(to);
        isFading = false;
    }

    private void SetAlpha(float a)
    {
        Color c = fadeImage.color;
        c.a = a;
        fadeImage.color = c;
    }
}