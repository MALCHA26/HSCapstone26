/*
 * 작성자: 김승윤
 * 작성일: 2026.05.11
 * 역할: 카트와 문 충돌하면 씬 이동 
 */
using UnityEngine;
using UnityEngine.SceneManagement; 
public class Scene3Changer : MonoBehaviour
{
    public string nextSceneName = "Scene4"; // 이동할 씬 이름
    public string targetTag = "Cart"; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            ChangeScene();
        }
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
