/*
 * 작성자: 손다혜
 * 작성일: 2026.04.11
 * 역할: Scene1 영상 재생
 */

using UnityEngine;
using System;

public class VideoPlayer : MonoBehaviour
{
    [SerializeField] private UnityEngine.Video.VideoPlayer videoPlayer;

    public Action onComplete;

    private void Awake()
    {
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    private void OnVideoEnd(UnityEngine.Video.VideoPlayer vp)
    {
        onComplete?.Invoke();
    }

    private void OnDestroy()
    {
        videoPlayer.loopPointReached -= OnVideoEnd;
    }
}
