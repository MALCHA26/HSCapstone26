using UnityEngine;

public class Scene32Loader : MonoBehaviour
{
    [SerializeField] private GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


        if (player != null)
        {
            float originalY = player.transform.position.y;

            // X와 Z만 0으로 맞추고, Y는 바닥 레벨 유지를 위해 기존 값 사용
            player.transform.position = new Vector3(0f, originalY, 0f);

            // 회전값 초기화
            player.transform.rotation = Quaternion.identity;

            // VR 헤드셋 정면 리센터
            if (OVRManager.display != null)
            {
                OVRManager.display.RecenterPose();
            }

        }
    }

}
