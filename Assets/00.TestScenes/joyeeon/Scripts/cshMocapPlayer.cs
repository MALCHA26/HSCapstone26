using UnityEngine;
using Photon.Pun;

public class cshMocapPlayer : MonoBehaviourPun
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
            FindAnyObjectByType<OptitrackStreamingClient>().gameObject.SetActive(true);
    }
}
