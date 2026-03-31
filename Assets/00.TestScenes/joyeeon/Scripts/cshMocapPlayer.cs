using UnityEngine;
using Photon.Pun;

public class cshMocapPlayer : MonoBehaviourPun
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (photonView.IsMine) {

            GameObject client = GameObject.Find("Client - OptiTrack");
            GetComponent<OptitrackSkeletonAnimator>().StreamingClient = client.GetComponent<OptitrackStreamingClient>();
        }
    }

    // Update is called once per frame
    
}
