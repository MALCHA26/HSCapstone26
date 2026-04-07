using UnityEngine;
using Photon.Pun;

public class cshVRPlayer : MonoBehaviourPun
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!photonView.IsMine)
        {
           // GameObject[] Camera = GameObject.FindGameObjectsWithTag("Camera");
           //foreach(GameObject cam in Camera)
           // {
           //     cam.SetActive(false);
           // }
        }
    }

    // Update is called once per frame
    //void Update()
    //{
    //    photonView.RPC("addScore", RpcTarget.AllBuffered);
    //}
    //[PunRPC]
    //public void addScore()
    //{
    //    Debug.Log("get Score!");
    //}
}
