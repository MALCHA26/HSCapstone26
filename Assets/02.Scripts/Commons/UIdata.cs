/*
 * 작성자: 조희연
 * 역할: UI 관리 스크립터블 오브젝트
 */

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UIdata", menuName = "Scriptable Objects/UIdata")]
public class UIdata : ScriptableObject
{
    [System.Serializable]
    public struct InteractionNode
    {
        public string nodeID;           

        [Header("가이드 UI 설정")]
        public string guideText;        
        public Color highlightColor;    
    }

    [Space(10)]
    [Tooltip("각 분기별 인터랙션 가이드 데이터 리스트")]
    public List<InteractionNode> interactionNodes = new List<InteractionNode>();


    public InteractionNode GetInteraction(string id)
    {
        return interactionNodes.Find(x => x.nodeID == id);
    }
}
