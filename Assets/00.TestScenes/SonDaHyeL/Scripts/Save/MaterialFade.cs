/*
 * 역할: 머테리얼 배열을 순서대로 덮어씌우는 전환 효과
 *       Play() 호출 시 순서대로 1→2→3... 페이드 전환
 */

using UnityEngine;
using System;
using System.Collections;

public class MaterialFade : MonoBehaviour
{
    [SerializeField] private Material[] materials;
    [SerializeField] private float duration = 1f;

    public Action onComplete;

    private Renderer m_renderer;

    private void Awake()
    {
        m_renderer = GetComponent<Renderer>();
    }

    public void Play()
    {
        StartCoroutine(FadeSequence());
    }

    public IEnumerator PlayAndWait()
    {
        yield return StartCoroutine(FadeSequence());
    }

    private IEnumerator FadeSequence()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            yield return StartCoroutine(FadeTo(materials[i]));
        }

        onComplete?.Invoke();
    }

    private IEnumerator FadeTo(Material next)
    {
        // 새 머테리얼 인스턴스, 알파 0으로 시작
        Material fadeMat = new Material(next);
        Color c = fadeMat.color;
        c.a = 0f;
        fadeMat.color = c;

        // 현재 머테리얼 위에 추가
        Material[] current = m_renderer.materials;
        Material[] combined = new Material[current.Length + 1];
        current.CopyTo(combined, 0);
        combined[combined.Length - 1] = fadeMat;
        m_renderer.materials = combined;

        // 알파 0 → 1
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            c = fadeMat.color;
            c.a = Mathf.Clamp01(t / duration);
            fadeMat.color = c;
            yield return null;
        }

        // 전환 완료 후 해당 머테리얼만 남기기
        m_renderer.materials = new Material[] { next };
    }
}
