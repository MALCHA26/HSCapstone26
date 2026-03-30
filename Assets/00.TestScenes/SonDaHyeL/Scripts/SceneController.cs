/*
 * 작성자: 손다혜
 * 작성일: 2026.03.23
 * 역할: Scene1의 전체적인 제어를 수행
 */
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private SceneFade sceneFade;

    void Start()
    {
        if (sceneFade != null)
        {
            sceneFade.FadeIn(); 
        }
    }
}