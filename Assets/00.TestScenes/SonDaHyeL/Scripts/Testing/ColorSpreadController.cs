using UnityEngine;

public class ColorSpreadController : MonoBehaviour
{
    public Material mat;
    public float speed = 1.0f;

    private float progress = 1f;   // 초기값을 1로 설정
    private bool spreading = false;

    void Update()
    {
        // 스페이스바 입력 확인
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("[ColorSpread] Space pressed.");

            if (mat == null)
            {
                Debug.LogError("[ColorSpread] Material is NULL! Inspector에서 Material 연결 필요.");
                return;
            }

            spreading = true;
            progress = 1f;  // 시작값 1
            mat.SetFloat("_Progress", progress);

            Debug.Log("[ColorSpread] Spread started. Progress initialized to 1.");
        }

        // 확산 진행 (감소 방향)
        if (spreading)
        {
            progress -= Time.deltaTime * speed;
            progress = Mathf.Clamp01(progress);
            mat.SetFloat("_Progress", progress);

            Debug.Log("[ColorSpread] Spreading... Progress: " + progress);

            // 완료 확인
            if (progress <= 0f)
            {
                Debug.Log("[ColorSpread] Spread complete (reached 0).");
                spreading = false;
            }
        }
    }
}