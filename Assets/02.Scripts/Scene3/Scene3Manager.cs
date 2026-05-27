/*
 * 작성자: 조희연
 * 역할: 씬3 매니저 - 씬3에서 플레이어 위치 초기화
 */

using UnityEngine;

public class Scene3Manager : MonoBehaviour
{
    public Transform scene3SpawnPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject playerObj = GameObject.Find("VRPlayer(Clone)");

        if (playerObj != null && scene3SpawnPoint != null)
        { 
            playerObj.transform.position = scene3SpawnPoint.position;
            playerObj.transform.rotation = scene3SpawnPoint.rotation;

        }
        else
        {
            if (playerObj == null) Debug.LogWarning("VRPlayer(Clone)을 찾을 수 없습니다.");
            if (scene3SpawnPoint == null) Debug.LogWarning("Scene3 Spawn Point 오브젝트가 지정되지 않았습니다.");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
