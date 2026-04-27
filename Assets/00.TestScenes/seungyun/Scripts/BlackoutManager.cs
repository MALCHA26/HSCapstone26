using System.Collections.Generic;
using UnityEngine;

public class BlackoutManager : MonoBehaviour
{
    public AudioSource lightOffSound;
    public List<Light> sceneLights = new List<Light>();
    public List<Renderer> emissionObjects = new List<Renderer>();
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            lightOffSound.Play();
            StartBlackout();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            lightOffSound.Play();
            StartLightOn();
        }
    }
    void StartBlackout()
    {
        foreach (Light light in sceneLights)
        {
            if (light != null) light.enabled = false;
        }
        foreach (Renderer ren in emissionObjects)
        {
            if (ren != null)
            {
                ren.material.SetColor("_EmissionColor", Color.black);
                ren.material.DisableKeyword("_EMISSION");
            }
        }
        RenderSettings.ambientLight = Color.black;
        RenderSettings.reflectionIntensity = 0f;
    }
    void StartLightOn()
    {
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
