using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingOrder : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private bool runOnce;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        float precisionMultiplier = 5f;
        spriteRenderer.sortingOrder = (int)(- (transform.position.y-transform.localPosition.y) * precisionMultiplier);

        if(runOnce)
        {
            Destroy(this);
        }
    }
}
