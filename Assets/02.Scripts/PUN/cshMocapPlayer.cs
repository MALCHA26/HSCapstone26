using UnityEngine;
using Photon.Pun;

public class cshMocapPlayer : MonoBehaviourPun
{
    public GameObject[] MRUKInstance;
    //public MocapSceneChanger MocapSceneChanger;
    public string targetScene = "Scene3-LightEffect";
    public GameObject VRplayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        //MocapSceneChanger = GameObject.Find("MocapSceneChanger").GetComponent<MocapSceneChanger>();
        if (photonView.IsMine) {

            //GameObject client = transform.Find("Client - OptiTrack");
            //GetComponent<OptitrackSkeletonAnimator>().StreamingClient = client.GetComponent<OptitrackStreamingClient>();
            //
           
        }
    }
    private void Update()
    {
        if (photonView.IsMine)
        {
            if(Input.GetKeyDown(KeyCode.E)){
                photonView.RPC("LoadmyNextScene", RpcTarget.AllBuffered, targetScene);
            }
        }
    }
    [PunRPC]
    public void LoadmyNextScene(string targetScene)
    {
        Debug.Log("rpc Called");
        DontDestroyOnLoad(gameObject);
        VRplayer = GameObject.Find("VRPlayer(Clone)");
        DontDestroyOnLoad(VRplayer);
        PhotonNetwork.LoadLevel(targetScene);
        
    }

}
