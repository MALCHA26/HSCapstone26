using UnityEngine;

public class GaugeManagerTester : MonoBehaviour
{
    private GaugeManager gaugeManager;

    void Start()
    {
        gaugeManager = FindFirstObjectByType<GaugeManager>();
    }

    void Update()
    {
        // T 키 누르면 ProcessEnding 강제 실행
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("[테스트] ProcessEnding 강제 호출");
            gaugeManager.StartCoroutine("ProcessEnding");
        }
    }
}
