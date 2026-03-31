using UnityEngine;
using System.Collections;

public class ScreenFader1 : MonoBehaviour
{
    private Material glassMaterial;
    private Material[] materials;
    private float fadeDuration;

    private MeshRenderer m_mesh;
    private int state = 0;

    void Awake()
    {
        m_mesh = GetComponent<MeshRenderer>();
    }

    public void Initialize(Material glass, Material[] mats, float duration)
    {
        glassMaterial = glass;
        materials = mats;
        fadeDuration = duration;
        m_mesh.material = glassMaterial;
        SetAlpha(0f);
    }

    public void StartFadeSequence()
    {
        int nextState = (state + 1) % (materials.Length + 1);
        StartCoroutine(FadeAndChange(nextState));
        state = nextState;
    }

    private IEnumerator FadeAndChange(int nextState)
    {
        // Fade Out
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            SetAlpha(1f - t / fadeDuration);
            yield return null;
        }
        SetAlpha(0f);

        // Material 교체
        if (nextState == 0)
            m_mesh.material = glassMaterial;
        else
            m_mesh.material = materials[nextState - 1];

        // Fade In
        t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            SetAlpha(t / fadeDuration);
            yield return null;
        }
        SetAlpha(1f);
    }

    private void SetAlpha(float a)
    {
        Color c = m_mesh.material.color;
        c.a = a;
        m_mesh.material.color = c;
    }
}