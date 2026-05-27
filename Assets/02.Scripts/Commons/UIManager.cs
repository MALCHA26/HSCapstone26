/*
 * 작성자: 조희연
 * 역할: UI 매니저 - UI 데이터를 관리하는 스크립트   
 */

using TMPro;
using UnityEngine;


public class UIManager : MonoBehaviour
{
    [SerializeField] private UIdata uidata;      // 스크립터블 오브젝트(SO) 연결
    private TextMeshProUGUI guideText;     // 캔버스에 이미 만들어둔 Text 오브젝트 직접 연결

    private void Awake()
    {
       guideText = GetComponentInChildren<TextMeshProUGUI>(); // 자식 오브젝트에서 TextMeshProUGUI 컴포넌트 찾기

    }

    void Start()
    {
        // 시작할 때는 글자를 비워두거나 판넬을 꺼둡니다.
        if (guideText != null) guideText.text = "";
        
    }
    public void ShowGuide(string nodeId)
    {
        if (uidata == null || guideText == null) return;

        // SO에서 ID로 데이터를 가져옴
        var data = uidata.GetInteraction(nodeId);

        if (!string.IsNullOrEmpty(data.nodeID))
        {
            guideText.text = data.guideText;       // 글자 바꾸기
            guideText.color = data.highlightColor; // 색상 바꾸기
        }
    }

    // 미션 완료하면 글자 지우기
    public void HideGuide()
    {
        if (guideText != null) guideText.text = "";
    }
}
