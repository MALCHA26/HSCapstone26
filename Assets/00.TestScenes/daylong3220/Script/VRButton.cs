using UnityEngine;

public class VRButton : MonoBehaviour
{
    [SerializeField] private VoiceListener voi;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        voi = GameObject.Find("[BuildingBlock] Dictation").GetComponent<VoiceListener>();
    }
    private void OnTriggerEnter(Collider other)
    {
        //마스코드 터치시 그 대상의 태그를 따로 넣어주세요. 그러면 stt가 실행되며 추가적으로 ai 요청이 발생할 것입니다.
        if (other.CompareTag("Player") || other.name.Contains("Hand") || other.name.Contains("Controller"))
        {
            Debug.Log("오큘러스 컨트롤러 터치 감지!");
            voi.more(); 
        }
    }
}
