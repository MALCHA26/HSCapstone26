/*
 * 작성자: 손다혜
 * 작성일: 2026.04.10
 * 역할: Screen 머테리얼 효과 제어
 */

using UnityEngine;
using System;
using System.Collections;

public class TextureChange : MonoBehaviour
{
    [Header("Materials")]
    [SerializeField] private Material[] materials;

    [Header("Settings")]
    [SerializeField] private float fadeDuration = 2f;
    [SerializeField] private string pageFlipSound = "PageFlip";

    [Header("Sounds")]
    [SerializeField] private string[] sounds;

    public Action onFadeInComplete;
    public Action onFadeOutComplete;

    private MeshRenderer m_mesh;
    private int currentIndex = 0;

    private void Awake()
    {
        m_mesh = GetComponent<MeshRenderer>();
        if (materials.Length > 0)
        {
            m_mesh.material = materials[0];
            SetAlpha(0f);
        }
    }

    public IEnumerator PlaySequence()
    {
        bool fadeIn = true; // 홀수 음원 후 FadeIn, 짝수 음원 후 FadeOut
        int matIndex = 0;

        for (int i = 0; i < sounds.Length; i++)
        {
            // 나레이션 재생 + 종료 대기
            if (!string.IsNullOrEmpty(sounds[i]))
                yield return StartCoroutine(SoundManager.Instance.PlayAndWait(sounds[i], 0.4f));

            if (fadeIn)
            {
                // PageFlip + FadeIn + 머테리얼 교체
                SoundManager.Instance.Play(pageFlipSound, 1f);
                if (matIndex < materials.Length)
                    m_mesh.material = materials[matIndex++];
                yield return StartCoroutine(Fade(0f, 1f));
                onFadeInComplete?.Invoke();
            }
            else
            {
                // FadeOut
                yield return StartCoroutine(Fade(1f, 0f));
                onFadeOutComplete?.Invoke();
            }

            fadeIn = !fadeIn;
        }

        currentIndex = matIndex;
    }

    // 단일 FadeIn (외부 호출용)
    public void PlayNext()
    {
        if (currentIndex >= materials.Length)
        {
            Debug.LogWarning("[TextureChange] 더 이상 재생할 머테리얼이 없습니다.");
            return;
        }

        m_mesh.material = materials[currentIndex];
        currentIndex++;
        StartCoroutine(FadeInCoroutine());
    }

    // 단일 FadeOut (외부 호출용)
    public void FadeOut()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeInCoroutine()
    {
        yield return StartCoroutine(Fade(0f, 1f));
        onFadeInComplete?.Invoke();
    }

    private IEnumerator FadeOutCoroutine()
    {
        yield return StartCoroutine(Fade(1f, 0f));
        onFadeOutComplete?.Invoke();
    }

    private IEnumerator Fade(float from, float to)
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            SetAlpha(Mathf.Lerp(from, to, t / fadeDuration));
            yield return null;
        }
        SetAlpha(to);
    }

    private void SetAlpha(float a)
    {
        Color c = m_mesh.material.color;
        c.a = a;
        m_mesh.material.color = c;
    }
}
