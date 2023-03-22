using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceNearbyOverlay : MonoBehaviour
{
    private ResourceGeneratorData generatorData;
    TextMeshPro text;

    private void Awake()
    {
        text = GetComponentInChildren<TextMeshPro>();
        Hide();
    }

    private void Update()
    {
        if (generatorData == null) return;

        int nearbyResourceAmount = ResourceGenerator.GetNearbyResourceAmount(generatorData, transform.position - transform.localPosition);
        float percent = Mathf.RoundToInt(nearbyResourceAmount / (float)generatorData.maxResourceAmount * 100f);
        text.SetText(percent + "%");
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        generatorData = null;
    }

    public void Show(ResourceGeneratorData data)
    {
        transform.Find("icon").GetComponent<SpriteRenderer>().sprite = data.resourceType.sprite;
        generatorData = data;
        gameObject.SetActive(true);
    }
}
