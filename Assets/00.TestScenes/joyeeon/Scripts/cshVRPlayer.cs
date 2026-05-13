using UnityEngine;
using Photon.Pun;

public class cshVRPlayer : MonoBehaviourPun
{
    private void Awake()
    {
        GameObject VRInstance = GameObject.Find("VRInstanceData");
        VRInstance.SetActive(true);

        for(int i = 0; i<VRInstance.transform.childCount; i++)
        {
            VRInstance.transform.GetChild(i).gameObject.SetActive(true);
        }
      
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!photonView.IsMine) return;
        
          /*  Transform leftAnchor = transform.Find("LeftHandAnchor");
            Transform rightAnchor = transform.Find("RightHandAnchor");

            CartController cartcontroller = FindObjectOfType<CartController>();
            if (cartcontroller != null) {
                cartcontroller.leftHandAnchor = leftAnchor;
                cartcontroller.rightHandAnchor = rightAnchor;
                Debug.Log("수레에 손 앵커 등록 완료");
            }
*/
            GameObject[] Camera = GameObject.FindGameObjectsWithTag("Camera");
            foreach(GameObject cam in Camera)
            {
                 cam.SetActive(false);
            }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine) return;

        // 키보드 E 누르면 씬 전환 (테스트용)
        if (Input.GetKeyDown(KeyCode.E))
        {
            string targetScene = "Scene3-LightEffect";
            DontDestroyOnLoad(gameObject);
            PhotonNetwork.LoadLevel(targetScene);
        }
        //photonView.RPC("addScore", RpcTarget.AllBuffered);
    }
    //[PunRPC]
    //public void addScore()
    //{
    //    Debug.Log("get Score!");
    //}
}
