using UnityEngine;

public class Scene32Loader : MonoBehaviour
{
    [SerializeField] private GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


        if (player != null)
        {
            // 위치를 (0, 0, 0)으로 초기화
            player.transform.position = Vector3.zero;

            // 회전값도 회전하지 않은 정면 상태(0, 0, 0)로 초기화
            player.transform.rotation = Quaternion.identity;

        }
    }

}
