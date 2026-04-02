using UnityEngine;

public class Tset : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TTSManager.Instance.Speak("이 대사가 바로 재생됩니다.");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
