using UnityEngine;

public class VR3DObjectTouch : MonoBehaviour
{
    [Header("테스트용 마우스 클릭 허용 여부")]
    [SerializeField] private bool enableMouseTest = true;
    [SerializeField] private VoiceListener voi;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        voi = GameObject.Find("[BuildingBlock] Dictation").GetComponent<VoiceListener>();
    }

    // 1. [실제 VR 환경] 컨트롤러가 3D 오브젝트 영역에 들어왔을 때 작동
    private void OnTriggerEnter(Collider other)
    {
        // 메타 XR 리그의 컨트롤러나 손 오브젝트 이름/태그를 필터링합니다.
        if (other.name.Contains("Hand") || other.name.Contains("Controller") || other.CompareTag("Player"))
        {
            Debug.Log($"[VR 터치] {other.name} 가 오브젝트에 닿았습니다!");
            voi.more();
        }
    }

    // 2. [집에서 테스트용] 장비가 없을 때 마우스로 이 3D 오브젝트를 클릭하면 작동
    private void OnMouseDown()
    {
        if (enableMouseTest)
        {
            Debug.Log("[PC 테스트] 마우스로 3D 오브젝트를 클릭하여 more()를 실행합니다.");
            voi.more();
        }
    }

}