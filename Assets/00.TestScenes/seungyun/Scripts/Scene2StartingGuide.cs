/*
 * 작성자: 김승윤
 * 작성일: 2026.06.02
 * 역할: 데모용 씬2 가이드 캔버스 비활성화 컨트롤 스크립트
 */
using System.Collections;
using UnityEngine;

public class Scene2StartingGuide : MonoBehaviour
{
    private float delayTime = 10f;
    public GameObject scene2Guide;
    void Start()
    {
        StartCoroutine(CanvasOff());
    }

    private IEnumerator CanvasOff()
    {
        // 대기
        yield return new WaitForSeconds(delayTime);

        scene2Guide.SetActive(false);
    }
}
