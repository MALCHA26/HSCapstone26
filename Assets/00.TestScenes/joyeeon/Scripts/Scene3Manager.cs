using UnityEngine;

public class Scene3Manager : MonoBehaviour
{
    public Vector3 scene3SpawnPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject playerObj = GameObject.Find("VRPlayer(Clone)");

        
        
        if (playerObj != null)
        {
            playerObj.transform.position = scene3SpawnPos;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
