using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TooltipUI : MonoBehaviour
{
    public static TooltipUI Instance { get; private set; }

    [SerializeField]
    private RectTransform canvasRectTransform;

    private RectTransform backgroundRectTransform;
    private TextMeshProUGUI textMeshPro;

    private RectTransform rectTransform;

    private bool isWaitDelay = false;

    private void Awake()
    {
        Instance = this;

        backgroundRectTransform = transform.Find("Background").GetComponent<RectTransform>();
        textMeshPro = transform.Find("Text").GetComponent<TextMeshProUGUI>();

        rectTransform = GetComponent<RectTransform>();

        Hide();
    }

    private void Update()
    {
        HandleFollowMouse();
    }

    private void SetText(string tooltipText)
    {
        textMeshPro.SetText(tooltipText);
        textMeshPro.ForceMeshUpdate();

        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        Vector2 padding = new Vector2(16, 16);

        backgroundRectTransform.sizeDelta = textSize + padding;
    }

    private void HandleFollowMouse()
    {
        Vector2 anchoredPos = Input.mousePosition / canvasRectTransform.localScale.x;

        // right check
        if (anchoredPos.x + backgroundRectTransform.rect.width > canvasRectTransform.rect.width)
        {
            anchoredPos.x = canvasRectTransform.rect.width - backgroundRectTransform.rect.width;
        }

        if (anchoredPos.y + backgroundRectTransform.rect.height > canvasRectTransform.rect.height)
        {
            anchoredPos.y = canvasRectTransform.rect.height - backgroundRectTransform.rect.height;
        }

        if (anchoredPos.x < 0)
            anchoredPos.x = 0;

        if (anchoredPos.y < 0)
            anchoredPos.y = 0;

        rectTransform.anchoredPosition = anchoredPos;
    }

    public void Show(string tooltipText, bool isDelay = false)
    {
        if (isWaitDelay)
        {
            StopAllCoroutines();
            isWaitDelay = false;
            Hide();
            return;
        }

        gameObject.SetActive(true);
        SetText(tooltipText);
        HandleFollowMouse();

        if(isDelay)
        {
            StartCoroutine(DelayCoroutine());
        }
    }

    private IEnumerator DelayCoroutine()
    {
        isWaitDelay = true;
        yield return new WaitForSeconds(2f);
        Hide();
        isWaitDelay = false;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
