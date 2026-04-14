/*
 * 작성자: 손다혜
 * 작성일: 2026.03.21
 * 역할: UV 값을 이용한 머테리얼 텍스쳐 회전 효과 구현
 */

using UnityEngine;

public class MoveTexture : MonoBehaviour
{
    [SerializeField] Vector2 m_offset = Vector2.zero;
    MeshRenderer m_mesh = null;
    void Start()
    {
        m_mesh = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        m_mesh.material.mainTextureOffset += m_offset * Time.deltaTime;
    }
}
