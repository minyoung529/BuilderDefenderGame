using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    new private BoxCollider2D collider;

    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
        collider.enabled = false;
    }

    public void Attack()
    {
        ActiveCollider();
        Shake();
    }

    private void ActiveCollider()
    {
        collider.enabled = true;
    }

    private void Shake()
    {
        transform.DOShakePosition(1f);
    }
}
