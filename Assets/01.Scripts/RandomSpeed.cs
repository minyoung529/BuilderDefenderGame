using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpeed : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator ??= GetComponent<Animator>();
        animator.SetFloat("Speed", Random.Range(0.6f, 1.4f));
    }
}
