using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceGeneratorOverlay : MonoBehaviour
{
    [SerializeField] private ResourceGenerator generator;

    private void Start()
    {
        ResourceGeneratorData data = generator.GetResourceGeneratorData();

        transform.Find("icon").GetComponent<SpriteRenderer>().sprite = data.resourceType.sprite;
        transform.Find("text").GetComponent<TextMeshPro>().SetText(generator.GetAmountGeneratedPerSecond().ToString("F1"));
    }

    private void Update()
    {
        transform.Find("bar").localScale = new Vector3(1f - generator.GetTimerNormalized(), 1, 1);
    }
}