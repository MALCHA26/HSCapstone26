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