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
