using System.Collections;
using UnityEngine;

public class UIChangeManager : MonoBehaviour
{

    public CanvasGroup questionUICanvasGroup;
 
    public float fadeDuration = 0.8f;

    void Start()
    {
        /*
        if (questionUICanvasGroup != null)
        {
            //questionUICanvasGroup.alpha = 0f;
            //questionUICanvasGroup.blocksRaycasts = false;
        }
        */
    }

    public void ShowQuestionUI()
    {
        StartCoroutine(FadeInQuestionUI());
    }

    private IEnumerator FadeInQuestionUI()
    {
        float counter = 0f;

        // 페이드 시작할 때 먼저 작동(상호작용)은 되도록 
        questionUICanvasGroup.blocksRaycasts = true;

        while (counter < fadeDuration)
        {
            counter += Time.deltaTime;
            questionUICanvasGroup.alpha = Mathf.Lerp(0f, 1f, counter / fadeDuration);
            yield return null;
        }

        // 확실하게 1로 고정
        questionUICanvasGroup.alpha = 1f;
    }
}