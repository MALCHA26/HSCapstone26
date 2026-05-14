using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonBlackoutManager : MonoBehaviourPun
{
    public AudioSource lightOffSound;
    public AudioSource ttsStart;
    public List<Light> sceneLights = new List<Light>();
    public List<Renderer> emissionObjects = new List<Renderer>();
    void Update()
    {
        if (!photonView.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                photonView.RPC(nameof(SyncLightOut), RpcTarget.AllBuffered);
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                photonView.RPC(nameof(SyncLightOn), RpcTarget.AllBuffered);
            }
        }
        
    }
    [PunRPC]
    void SyncLightOut()
    {
        if (lightOffSound != null) lightOffSound.Play();
        if (ttsStart != null) 
        {
            ttsStart.Play();
        }
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

    [PunRPC]
    void SyncLightOn()
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
                Color recoverColor = new Color(0.5f, 0.5f, 0.5f);
                ren.material.SetColor("_EmissionColor", recoverColor);
            }
        }
        RenderSettings.ambientLight = new Color(0.5f, 0.5f, 0.5f);
        RenderSettings.reflectionIntensity = 1f;
    }

}
