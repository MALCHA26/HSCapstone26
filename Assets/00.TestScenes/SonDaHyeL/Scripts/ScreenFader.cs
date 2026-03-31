/*
 * 작성자: 손다혜
 * 작성일: 2026.03.22
 * 역할: Paper와 인쇄기 상호작용 시 머테리얼 변화 효과와 스크린의 페이드인-아웃 효과를 구현. 
 */

using UnityEngine;
using System.Collections;

public class ScreenFader : MonoBehaviour
{
    private Material glassMaterial;
    private Material[] materials;
    private float fadeDuration;
    private AudioClip clip;            

    private MeshRenderer m_mesh;
    private AudioSource audioSource;

    public ObjectSpawn spawner;

    void Awake()
    {
        m_mesh = GetComponent<MeshRenderer>();
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    // 초기화
    public void Initialize(Material glass, Material[] mats, float duration, AudioClip audioClip)
    {
        glassMaterial = glass;
        materials = mats;
        fadeDuration = duration;
        clip = audioClip;

        m_mesh.material = glassMaterial;
        SetAlpha(0f);
    }

    // Paper 충돌로 시작
    public void StartSequence()
    {
        StartCoroutine(FadeAudioMaterialLoop());
    }

    private IEnumerator FadeAudioMaterialLoop()
    {
        // 모든 머티리얼 순회
        for (int i = 0; i < materials.Length; i++)
        {
            // 1. 머티리얼 교체
            m_mesh.material = materials[i];

            // 2. Fade In + 오디오 동시 재생
            float t = 0f;
            if (clip != null)
                audioSource.PlayOneShot(clip);  // Fade In과 동시에 재생

            while (t < fadeDuration)
            {
                t += Time.deltaTime;
                SetAlpha(t / fadeDuration);
                yield return null;
            }
            SetAlpha(1f);

            // 3. 오디오 종료까지 대기
            if (clip != null)
                yield return new WaitForSeconds(clip.length);

            // 4. Fade Out
            t = 0f;
            while (t < fadeDuration)
            {
                t += Time.deltaTime;
                SetAlpha(1f - t / fadeDuration);
                yield return null;
            }
            SetAlpha(0f);
        }

        // 모든 머티리얼 순회 후 글래스로 복귀
        m_mesh.material = glassMaterial;
        SetAlpha(0f);

        spawner.Spawn();

    }

    private void SetAlpha(float a)
    {
        Color c = m_mesh.material.color;
        c.a = a;
        m_mesh.material.color = c;
    }
}