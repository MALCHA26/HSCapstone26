/*
 * 작성자: 손다혜
 * 작성일: 2026.04.22
 * 역할: 효과음 관리, 외부 호출 시 효과음 재생 
 */

using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    [Serializable]
    public class SoundClip
    {
        public string name;
        public AudioClip clip;
    }

    public static SoundManager Instance { get; private set; }

    [SerializeField] private SoundClip[] sounds;

    private AudioSource audioSource;

    private void Awake()
    {
        Instance = this;
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void Play(string soundName, float volume)
    {
        SoundClip found = Array.Find(sounds, s => s.name == soundName);
        if (found == null)
        {
            Debug.Log($"[SoundManager] '{soundName}' 효과음을 찾을 수 없습니다.");
            return;
        }

        audioSource.PlayOneShot(found.clip,volume);
    }
}
