using UnityEngine;

public class StartTTSController : MonoBehaviour
{
    float time = 2.0f;
    float currentTime = 0.0f;
    bool isPlayed = false;
    public AudioSource startTTS;
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime >= time)
        {
            startTTS.Play();
            isPlayed = true;
        }
    }
}
