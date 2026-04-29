using Photon.Pun;
using UnityEngine;

public class MocapSceneChanger : MonoBehaviour
{
    public string targetScene = "Scene3-LightEffect";

    void Update()
    {
        // 방장이고 PlayerMode가 1일 때만 작동
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                Debug.Log("[테스트] 방장이 아니라 실행 불가");
                return;
            }

            cshPlayerInfo playerInfo = FindFirstObjectByType<cshPlayerInfo>();
            if (playerInfo == null)
            {
                Debug.Log("[테스트] PlayerInfo를 찾을 수 없음");
                return;
            }

            if (playerInfo.playerInfo != 1)
            {
                Debug.Log($"[테스트] PlayerMode가 {playerInfo.playerInfo} 라 실행 불가 (1이어야 함)");
                return;
            }

            Debug.Log("[테스트] 방장 + PlayerMode 1 확인 → PhotonNetwork.LoadLevel 호출");
            PhotonNetwork.LoadLevel(targetScene);
        }
    }
}
