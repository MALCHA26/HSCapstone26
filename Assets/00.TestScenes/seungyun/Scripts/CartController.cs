/*
 * 작성자: 김승윤
 * 작성일: 2026.04.14
 * 역할: 컨트롤러 그립 입력을 감지하여 수레 이동 처리 스크립트
 */

using Photon.Realtime;
using System.Collections;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class CartController : MonoBehaviour
{
    public Transform leftHandAnchor; // 왼쪽 컨트롤러 위치
    public Transform rightHandAnchor; // 오른쪽 컨트롤러 위치
    public Transform leftHandlePoint; // 왼쪽 손잡이 기준
    public Transform rightHandlePoint; // 오른쪽 손잡이 기준
 
    public float grabDistance = 0.3f; // 손잡이 인식 거리
    public float moveSpeed = 10f; // 수레 이동 속도

    private Rigidbody rb;
    private bool isGrabbed = false; // 잡고 있는지 여부
    private Vector3 grabOffset; // 손과 수레 사이 거리 
    private Transform activeController; // 잡고 있는 컨트롤러
    public GameObject butterflies;
    public GameObject cartGrabPos;
    public AudioSource cartSound;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        butterflies.SetActive(false);
        StartCoroutine(FindMyHandsRoutine());
    }

    //수정:조희연
    private IEnumerator FindMyHandsRoutine()
    {
        // 손을 찾을 때까지 계속 반복 (Update를 대체하는 효율적인 방법)
        while (leftHandAnchor == null || rightHandAnchor == null)
        {
            // 씬에 있는 모든 VR 플레이어를 찾음
            GameObject playerObj = GameObject.Find("VRPlayer(Clone)");
            if (playerObj != null)
            {
                cshVRPlayer player = playerObj.GetComponent<cshVRPlayer>();
                leftHandAnchor = player.transform.Find("[VR] Camera Rig/TrackingSpace/LeftHandAnchor");
                rightHandAnchor = player.transform.Find("[VR] Camera Rig/TrackingSpace/RightHandAnchor");
            }

            // 아직 내 캐릭터가 씬에 로드되지 않았다면 0.5초 대기 후 다시 시도
            yield return new WaitForSeconds(0.5f);
        }
    }

    void Update()
    {
        // 손이 아직 연결 안 됐으면 수레 로직 정지 (에러 방지)
        if (leftHandAnchor == null || rightHandAnchor == null) return;
        HandleInput();
        if (isGrabbed && activeController != null)
        {
            MoveCartInUpdate();
        }
    }

    // 그립 버튼 입력 및 상태 관리
    private void HandleInput()
    {
        // 컨트롤러 그립 입력 값 가져오기
        bool leftGrip = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch);
        bool rightGrip = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch);

        if (!isGrabbed)
        {
            if (leftGrip)
            {
                TryGrab(leftHandAnchor);
            }
            else if (rightGrip)
            {
                TryGrab(rightHandAnchor);
            }
        }
        else
        {
            // 잡고 있는 손의 버튼을 떼면 놓기
            bool gripping = (activeController == leftHandAnchor) ? leftGrip : rightGrip;
            if (!gripping)
            {
                ReleaseCart();
            }
        }
    }
    private void TryGrab(Transform hand)
    {
        // 컨트롤러와 손잡이 사이 거리 계산
        float distL = Vector3.Distance(hand.position, leftHandlePoint.position);
        float distR = Vector3.Distance(hand.position, rightHandlePoint.position);

        // 잡기 성공
        if (distL <= grabDistance || distR <= grabDistance)
        {
            isGrabbed = true;
            cartGrabPos.SetActive(false);
            butterflies.SetActive(true);

            cartSound.Play();
            activeController = hand;

            // 수레 위치 계산
            Vector3 handPosXZ = new Vector3(hand.position.x, 0, hand.position.z);
            Vector3 cartPosXZ = new Vector3(transform.position.x, 0, transform.position.z);
            grabOffset = cartPosXZ - handPosXZ;

            rb.isKinematic = false; 
            rb.useGravity = true;
        }
    }

    private void ReleaseCart()
    {
        isGrabbed = false;
        activeController = null;
    }


    //수정:조희연
    private void MoveCartInUpdate()
    {
        // 목표 위치 계산
        Vector3 targetPos = activeController.position + grabOffset;
        //Vector3 nextPosition = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * moveSpeed);

        // y축 고정
        //nextPosition.y = transform.position.y;
        //rb.MovePosition(nextPosition);

        targetPos.y = transform.position.y;
        Vector3 moveDirection = (targetPos - transform.position);
        rb.linearVelocity = moveDirection * moveSpeed;

    }
}