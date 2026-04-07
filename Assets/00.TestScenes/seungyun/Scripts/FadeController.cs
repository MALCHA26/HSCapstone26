/*
 * 작성자: 김승윤
 * 작성일: 2026.03.20
 * 역할: 씬 전환 효과 설정을 위한 스크립트
 */

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // 씬 이동용

public class FadeController : MonoBehaviour
{
    private Image fadeImage; // 씬 전환 효과를 위한 검은색 이미지
    public float fadeSpeed = 0.5f; // 페이드 전환 효과 속도

    // 효과 시작 확인 변수 (true: 작동, false: 멈춤)
    private bool fadeIn = false;
    private bool fadeOut = false;
    private string nextScene = ""; // 다음 씬 이름 

    private void Awake()
    {
        fadeImage = GetComponent<Image>();
    }

    private void Start()
    {
        // 씬 초기 설정 (검은 화면)
        Color color = fadeImage.color;
        color.a = 1f;
        fadeImage.color = color;

        // 페이드 인 효과 제공
        fadeIn = true;
    }

    private void Update()
    {
        Color color = fadeImage.color;

        // 페이드 인 호출 시
        if (fadeIn)
        {
            color.a -= Time.deltaTime * fadeSpeed;

            if (color.a <= 0f)
            {
                color.a = 0f;
                fadeIn = false;
            }
        }

        // 페이드 아웃 호출 시
        if (fadeOut)
        {
            color.a += Time.deltaTime * fadeSpeed;

            if (color.a >= 1f)
            {
                color.a = 1f;
                fadeOut = false;

                // 다음 씬으로 이동
                SceneManager.LoadScene(nextScene);
            }
        }
        fadeImage.color = color;
    }

    /// <summary>
    /// 페이드 아웃한 뒤, 지정한 다음 씬으로 이동
    /// </summary>
    /// <param name="sceneName">이동할 씬 이름</param>
    public void ChangeScene(string sceneName)
    {
        nextScene = sceneName;
        fadeOut = true;
    }
}