/*
 * 작성자: 김승윤(수정 반영)
 * 역할: 카트와 문 충돌하면 포톤 플레이어 정리 후 씬 이동 
 */
using UnityEngine;
using UnityEngine.SceneManagement; 
using Photon.Pun; 

public class Scene3Changer : MonoBehaviour
{
    public string nextSceneName = "Scene3-2"; // 이동할 씬 이름
    public string targetTag = "Cart";
    private OVRScreenFade screenFade3;

    void Start()
    {
        screenFade3 = FindFirstObjectByType<OVRScreenFade>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            if (screenFade3 != null)
            {
                screenFade3.FadeOut();
            }

            // 씬을 넘어가기 전에 기존 멀티플레이어 오브젝트 정리
            DestroyLocalPlayer();

            // 씬 이동
            ChangeScene();
        }
    }

    private void DestroyLocalPlayer()
    {
        GameObject player = GameObject.Find("VRPlayer(Clone)");

        if (player != null)
        {
            PhotonView photonView = player.GetComponent<PhotonView>();

            if (photonView != null && photonView.IsMine)
            {
                PhotonNetwork.Destroy(player);
                PhotonNetwork.LeaveRoom();
            }
        }
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
