/*
 * 작성자: 김승윤
 * 작성일: 2026.04.25
 * 역할: 전등 깜빡임 효과 구현
 */
using UnityEngine;

public class LightController : MonoBehaviour
{
    private Light tableLight;
    private Material lampMaterial;

    public float minIntensity = 5f;  // 최저 밝기
    public float maxIntensity = 25f; // 최고 밝기 
    public float blinkingSpeed = 0.1f; // 깜빡임 간격

    private float timer;

    void Start()
    {
        tableLight = GetComponent<Light>();
        MeshRenderer renderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= blinkingSpeed)
        {
            float newIntensity = Random.Range(minIntensity, maxIntensity);
            tableLight.intensity = newIntensity;

            // Emission 색상 밝기, 라이트 강도 동기화
            if (lampMaterial != null)
            {
                lampMaterial.SetColor("_EmissionColor", Color.white * (newIntensity / maxIntensity));
                lampMaterial.EnableKeyword("_EMISSION"); // Emission 활성화 확인
            }

            timer = 0f;
        }
    }
}