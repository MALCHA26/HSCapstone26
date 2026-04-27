using UnityEngine;
using System;
using System.Collections;

public class Snap : MonoBehaviour
{
    private Animator anim;

    private bool isTriggered = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isTriggered) return;

        if (other.CompareTag("PRINTER"))
        {
            isTriggered = true;
            anim.Play("Into", 0, 0f);
        }
    }
}