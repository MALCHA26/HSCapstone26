/*
 * 작성자: 손다혜
 * 작성일: 2026.04.06
 * 역할: TTS 기능 제어 
 */

using UnityEngine;
using System;
using Meta.XR.BuildingBlocks.AIBlocks;

public class TTSManager : MonoBehaviour
{
    public static TTSManager Instance { get; private set; }

    [SerializeField] private TextToSpeechAgent ttsAgent;

    public Action onSpeakComplete;

    private void Awake()
    {
        Instance = this;
    }

    public void Speak(string text, Action onComplete = null)
    {
        if (ttsAgent == null)
        {
            Debug.LogError("[TTS] TextToSpeechAgent is not assigned.");
            onComplete?.Invoke();
            return;
        }

        onSpeakComplete = onComplete;
        ttsAgent.SpeakText(text);
    }

    //재생 완료시 종료 신호 전달
    public void OnSpeakFinished()
    {
        onSpeakComplete?.Invoke();
        onSpeakComplete = null;
    }
}