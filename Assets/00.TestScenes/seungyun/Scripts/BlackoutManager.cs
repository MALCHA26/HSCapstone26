using System.Collections.Generic;
using UnityEngine;

public class BlackoutManager : MonoBehaviour
{
    public AudioSource lightOffSound;
    public AudioSource tts1;
    public AudioSource tts2;

    public List<Light> sceneLights = new List<Light>();
    public List<Renderer> emissionObjects = new List<Renderer>();
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartBlackOut();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            StartLightOn();
            if (tts1 != null)
            {
                tts1.Play();
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            StartBlackOut();
            if (tts2 != null)
            {
                tts2.Play();
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartLightOn();
        }
    }
    void StartBlackOut()
    {
        if (lightOffSound != null) lightOffSound.Play();
        foreach (Light light in sceneLights)
        {
            if (light != null) light.enabled = false;
        }
        foreach (Renderer ren in emissionObjects)
        {
            if (ren != null)
            {
                ren.material.SetColor("_EmissionColor", Color.black);
            }
        }
        RenderSettings.ambientLight = Color.black;
        RenderSettings.reflectionIntensity = 0f;
    }
    void StartLightOn()
    {
        if (lightOffSound != null) lightOffSound.Play();

        foreach (Light light in sceneLights)
        {
            if (light != null) light.enabled = true;
        }
        foreach (Renderer ren in emissionObjects)
        {
            if (ren != null)
            {
                ren.material.EnableKeyword("_EMISSION");
                Color recoverColor = new Color(0.5f, 0.5f, 0.5f);
                ren.material.SetColor("_EmissionColor", recoverColor);
            }
        }
        RenderSettings.ambientLight = new Color(0.25f, 0.25f, 0.25f);
        RenderSettings.reflectionIntensity = 1f;
    }

}