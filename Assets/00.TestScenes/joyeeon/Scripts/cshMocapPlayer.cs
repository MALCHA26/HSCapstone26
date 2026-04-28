using UnityEngine;
using Photon.Pun;

public class cshMocapPlayer : MonoBehaviourPun
{
    public GameObject[] MRUKInstance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (photonView.IsMine) {

            GameObject client = GameObject.Find("Client - OptiTrack");
            GetComponent<OptitrackSkeletonAnimator>().StreamingClient = client.GetComponent<OptitrackStreamingClient>();           
        }
    }

    // Update is called once per frame
    
}
