/*
 * 작성자: 김승윤
 * 작성일: 2026.04.08
 * 역할: 보성사 씬 전용 엔딩 설정 스크립트 (암전 후 대사 출력 및 씬 전환)
 */

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
public class Scene2Ending : MonoBehaviour
{
    public Image fadeImage;          // 검은색 이미지
    // public TextMeshProUGUI subtitle; // 대사 텍스트
    public float fadeSpeed = 0.5f;   // 어두워지는 속도

    public void StartEnding()
    {
        StartCoroutine(Ending());
    }

    IEnumerator Ending()
    {
        // 알파 0 -> 1
        float alpha = 0;
        while (alpha < 1f)
        {
            alpha += Time.deltaTime * fadeSpeed;
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        // 알파 == 1 일 때
        yield return new WaitForSeconds(3.0f); // 시간 재설정 예정

        // 자막 텍스트 추가 가능
        /*
        subtitle.text = "자막 내용";
        yield return new WaitForSeconds(3.0f);
        */

        // 다음 씬 이동
        //SceneManager.LoadScene("VRScene");
    }
}
