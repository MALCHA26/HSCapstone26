/*
 * 작성자: 손다혜
 * 작성일: 2026.03.22
 * 역할: UI를 이용해 전체 화면 페이드인-아웃 효과 구현
 */

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFade : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 1f;

    private bool isFading = false;

    void Awake()
    { 
        SetAlpha(1f);
    }
    public void FadeIn()
    {
        if (!isFading)
            StartCoroutine(FadeInCoroutine());
    }

    private IEnumerator FadeInCoroutine()
    {
        isFading = true;

        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            SetAlpha(1f - (t / fadeDuration));
            yield return null;
        }

        SetAlpha(0f);
        isFading = false;
    }

    public void FadeOutAndLoad(string sceneName)
    {
        if (!isFading)
            StartCoroutine(FadeOutCoroutine(sceneName));
    }

    private IEnumerator FadeOutCoroutine(string sceneName)
    {
        isFading = true;

        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            SetAlpha(t / fadeDuration);
            yield return null;
        }

        SetAlpha(1f);

        SceneManager.LoadScene(sceneName);
    }

    private void SetAlpha(float a)
    {
        Color c = fadeImage.color;
        c.a = a;
        fadeImage.color = c;
    }
}