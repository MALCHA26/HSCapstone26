/*
 * 역할: (테스트용) AlphaChange → Spread 애니메이션 → MaterialFade 순서 실행
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
