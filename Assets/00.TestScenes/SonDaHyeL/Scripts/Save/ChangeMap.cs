/*
 * 작성자: 손다혜
 * 작성일: 2026.04.21
 * 역할: 전환 연출 순서 제어
 */
using UnityEngine;
using System.Collections;

public class ChangeMap : MonoBehaviour
{
    [SerializeField] private AlphaChange alphaChange;
    [SerializeField] private MaterialFade materialFade;

    private void Start()
    {
        StartCoroutine(RunSequence());
    }

    private IEnumerator RunSequence()
    {
        alphaChange.Play();
        yield return new WaitForSeconds(alphaChange.Duration);

        bool fadeDone = false;
        materialFade.onComplete = () => fadeDone = true;
        materialFade.Play();
        yield return new WaitUntil(() => fadeDone);
    }
}
