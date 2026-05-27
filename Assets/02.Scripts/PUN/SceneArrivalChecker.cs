using Photon.Pun;
using UnityEngine;

public class SceneArrivalChecker : MonoBehaviour
{
    void Start()
    {
        Debug.Log($"[도착확인] {PhotonNetwork.NickName} 가 Scene3 도착");
        Debug.Log($"[도착확인] 현재 방 인원: {PhotonNetwork.CurrentRoom.PlayerCount}명");
        Debug.Log($"[도착확인] 방장 여부: {PhotonNetwork.IsMasterClient}");

        // 방에 있는 모든 플레이어 목록 출력
        foreach (var player in PhotonNetwork.CurrentRoom.Players.Values)
        {
            Debug.Log($"[도착확인] 플레이어: {player.NickName} / ActorNumber: {player.ActorNumber}");
        }
    }
}
