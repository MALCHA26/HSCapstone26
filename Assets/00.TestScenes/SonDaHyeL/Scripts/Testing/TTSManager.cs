using UnityEngine;
using Meta.XR.BuildingBlocks.AIBlocks;

public class TTSManager : MonoBehaviour
{
    public static TTSManager Instance { get; private set; }

    [SerializeField] private TextToSpeechAgent ttsAgent;

    private void Awake()
    {
        Instance = this;
    }

    public void Speak(string text)
    {
        if (ttsAgent == null)
        {
            Debug.LogError("[TTS] TextToSpeechAgent is not assigned.");
            return;
        }

        ttsAgent.SpeakText(text);
    }
}