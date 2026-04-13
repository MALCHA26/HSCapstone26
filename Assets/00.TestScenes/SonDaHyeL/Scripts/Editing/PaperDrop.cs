/*
 * 작성자: 손다혜
 * 작성일: 2026.04.12
 * 역할: Scene1 종이 애니매이션 구현 
 */

using UnityEngine;
using System;
using System.Collections;

public class PaperDrop : MonoBehaviour
{
    [SerializeField] private Paper paper;

    public Action onComplete;

    public void Drop()
    {
        StartCoroutine(DropCoroutine());
    }

    private IEnumerator DropCoroutine()
    {
        Animator anim = paper.GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogWarning("[PaperDrop] Animator가 없습니다.");
            onComplete?.Invoke();
            yield break;
        }

        anim.Play("FallPaper", 0, 0f);
        yield return null;

        while (anim.GetCurrentAnimatorStateInfo(0).IsName("FallPaper") &&
               anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            yield return null;
        }

        anim.Play("MovingPaperAnimation", 0, 0f);
        onComplete?.Invoke();
    }
}