/*
 * 작성자: 손다혜
 * 최초 작성일: 2026.03.23 
 * 수정일 : 2026.03.30
 * 역할: Scene1 전체 제어, 이벤트 발생 시 다음 Scene 로드
 */

using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private SceneFade sceneFade;
    [SerializeField] private string nextSceneName = "Scene2";

    void Start()
    {
        if (sceneFade != null)
        {
            sceneFade.FadeIn();
        }
    }

    public void LoadNextScene()
    {
        if (sceneFade != null)
        {
            sceneFade.FadeOutAndLoad(nextSceneName);
        }
        else
        {
            Debug.LogWarning("SceneFade가 SceneController에 연결되지 않았습니다.");
        }
    }
}