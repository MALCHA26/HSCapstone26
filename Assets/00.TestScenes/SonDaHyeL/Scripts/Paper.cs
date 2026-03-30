/*
 * 작성자: 손다혜
 * 작성일: 2026.03.21
 * 역할: 종이가 인쇄기에 충돌 시 다음 씬 호출
 */

using UnityEngine;

public class Paper : MonoBehaviour
{
    [SerializeField] private string sceneName = "Scene2";
    [SerializeField] private SceneFade sceneFade;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PRINTER"))
        {
            Destroy(gameObject);
            sceneFade.FadeOutAndLoad(sceneName);
        }
    }
}