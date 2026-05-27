/*
 * 작성자: 조희연
 * 역할: UI 캔버스 띄우는 스크립트
 */

using UnityEngine;

public class UISpawner : MonoBehaviour
{

    [Header("사용할 캔버스 할당")]
    [SerializeField] private UIManager uiManager;
    [Header("NodeID")]
    [SerializeField] private string nodeID;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Start()
    {
        uiManager.HideGuide();
    }
    public void Show()
    {
        uiManager.ShowGuide(nodeID);
    }

    public void Hide()
    {
        uiManager.HideGuide();
    }
}
