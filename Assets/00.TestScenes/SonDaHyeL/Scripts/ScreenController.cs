/*
 * 작성자: 손다혜
 * 작성일: 2026.03.23
 * 역할: 씬1 내부의 스크린을 전체적으로 컨트롤 함. 머테리얼 변경 사이에 페이드인-아웃 효과를 추가
 */

using UnityEngine;

public class ScreenController : MonoBehaviour
{
    [Header("Screen Settings")]
    [SerializeField] private GameObject screenPrefab;
    [SerializeField] private Material glassMaterial;
    [SerializeField] private Material[] materials;
    [SerializeField] private float fadeDuration = 2f;
    [SerializeField] private AudioClip audioClip;

    private ScreenFader screenFaderInstance;

    public void ActivateScreen()
    {
        if (screenFaderInstance == null)
        {
            GameObject screenObj = Instantiate(screenPrefab);
            screenFaderInstance = screenObj.GetComponent<ScreenFader>();
            screenFaderInstance.Initialize(glassMaterial, materials, fadeDuration, audioClip);
        }

        screenFaderInstance.StartSequence();
    }
}