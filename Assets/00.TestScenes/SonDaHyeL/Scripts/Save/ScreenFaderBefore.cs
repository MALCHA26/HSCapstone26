using UnityEngine;
using System.Collections;

public class ScreenFaderBefore : MonoBehaviour
{
    [SerializeField] Material glassMaterial;
    [SerializeField] Material[] materials;
    [SerializeField] float fadeDuration = 2.0f;

    MeshRenderer m_mesh;
    int state = 0; 

    void Start()
    {
        m_mesh = GetComponent<MeshRenderer>();
        SetAlpha(0f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int nextState = (state + 1) % (materials.Length + 1);
            StartCoroutine(FadeAndChange(nextState));
            state = nextState;
        }
    }

    IEnumerator FadeAndChange(int nextState)
    {
        // 1. Fade Out (사라짐)
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float a = 1f - (t / fadeDuration);
            SetAlpha(a);
            yield return null;
        }

        SetAlpha(0f);

        // 2. 머티리얼 교체
        if (nextState == 0)
            m_mesh.material = glassMaterial;
        else
            m_mesh.material = materials[nextState - 1];

        // 3. Fade In (나타남)
        t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float a = t / fadeDuration;
            SetAlpha(a);
            yield return null;
        }

        SetAlpha(1f);
    }

    void SetAlpha(float a)
    {
        Color c = m_mesh.material.color;
        c.a = a;
        m_mesh.material.color = c;
    }
}