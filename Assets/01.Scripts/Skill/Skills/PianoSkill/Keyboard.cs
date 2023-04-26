using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard : MonoBehaviour
{
    [SerializeField]
    private int index;

    private AudioSource audioSource;

    [SerializeField]
    private AudioClip clip;

    public Action<int> OnDown { get; set; }

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Color successColor;
    [SerializeField]
    private Color failColor;

    private Color originalColor;

    private bool isSelected = false;

    private void Awake()
    {
        audioSource = transform.parent.GetComponent<AudioSource>();
        originalColor = spriteRenderer.color;
    }
    private void OnMouseDown()
    {
        OnDown?.Invoke(index);
        PlaySound();
        isSelected = true;
    }

    private void OnMouseEnter()
    {
        if (isSelected) return;
        Color color = spriteRenderer.color;
        color -= Color.white * 0.2f;
        color.a = 1f;
        spriteRenderer.color = color;
    }

    private void OnMouseExit()
    {
        if (isSelected) return;
        spriteRenderer.color = originalColor;
    }

    public void PlaySound()
    {
        audioSource.PlayOneShot(clip);
    }

    internal void Success()
    {
        spriteRenderer.DOColor(successColor, 0.5f).SetUpdate(true);
    }

    internal void Fail()
    {
        spriteRenderer.DOColor(failColor, 0.5f).SetUpdate(true);
    }
}
