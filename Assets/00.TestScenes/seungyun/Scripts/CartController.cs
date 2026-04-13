/*
 * 작성자: 김승윤
 * 작성일: 2026.04.14
 * 역할: 컨트롤러 그립 입력을 감지하여 수레 이동 처리 스크립트
 */

using UnityEngine;

public class CartController : MonoBehaviour
{
    public Transform leftHandAnchor; // 왼쪽 컨트롤러 앵커
    public Transform rightHandAnchor; // 오른쪽 컨트롤러 앵커
    public Transform leftHandleGrabPoint; // 왼쪽 손잡이 위치
    public Transform rightHandleGrabPoint; // 오른쪽 손잡이 위치
    // public Transform handleGrabPoint; // 손잡이 한 개일 경우

    public float grabDistance = 0.3f; // 손잡이 인식 거리
    public float moveSpeed = 30f; // 수레 이동 속도

    private Rigidbody rb;
    private bool isGrabbed = false; // 현재 잡고 있는지 여부
    private float fixedHeight; // 잡은 순간 수레 높이 고정값
    private Vector3 lastControllerPos; // 이전 프레임 컨트롤러 위치
    private Transform activeController; // 현재 잡고 있는 컨트롤러

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        /* 손잡이 한 개일 경우
        float dist = Vector3.Distance(hand.position, handleGrabPoint.position);
        if (dist <= grabDistance)
        */

        // 그립 버튼 입력 감지
        bool leftGrip = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch);
        bool rightGrip = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch);

        // 그립 버튼 눌렀을 때
        if (!isGrabbed && (leftGrip || rightGrip))
        {
            Transform hand = leftGrip ? leftHandAnchor : rightHandAnchor;

            float distL = Vector3.Distance(hand.position, leftHandleGrabPoint.position);
            float distR = Vector3.Distance(hand.position, rightHandleGrabPoint.position);

            if (distL <= grabDistance || distR <= grabDistance)
            {
                isGrabbed = true;
                rb.isKinematic = true;
                activeController = hand;
                lastControllerPos = hand.position;
                fixedHeight = transform.position.y;
            }
        }

        // 그립 버튼 모두 떼었을 때
        if (isGrabbed && !leftGrip && !rightGrip)
        {
            isGrabbed = false;
            rb.isKinematic = false;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            activeController = null;
        }
    }

    void FixedUpdate()
    {
        if (!isGrabbed) return;

        // 컨트롤러 이동량만큼 수레 이동
        Vector3 delta = activeController.position - lastControllerPos;
        delta.y = 0; // 수직 이동 무시

        // 수레 위치 업데이트
        transform.position = new Vector3
            (transform.position.x + delta.x * moveSpeed * Time.fixedDeltaTime, 
            fixedHeight, transform.position.z + delta.z * moveSpeed * Time.fixedDeltaTime);

        lastControllerPos = activeController.position;
    }
}