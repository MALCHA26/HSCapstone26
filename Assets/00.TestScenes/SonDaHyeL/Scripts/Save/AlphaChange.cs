/*
 * 역할: Play() 호출 시 알파값을 alphaMin → alphaMax로 한 번 전환
 */

using UnityEngine;
using System.Collections;

public class AlphaChange : MonoBehaviour
{
    [SerializeField] private float alphaMin = 0f;
    [SerializeField] private float alphaMax = 1f;
    [SerializeField] private float duration = 1f;

    public float Duration => duration;

    private Material m_mat;

    private void Awake()
    {
        m_mat = GetComponent<Renderer>().material;
    }

    public void Play()
    {
        StartCoroutine(FadeCoroutine());
    }

    private IEnumerator FadeCoroutine()
    {
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            Color c = m_mat.color;
            c.a = Mathf.Lerp(alphaMin, alphaMax, t / duration);
            m_mat.color = c;
            yield return null;
        }

        Color final = m_mat.color;
        final.a = alphaMax;
        m_mat.color = final;
    }
}
