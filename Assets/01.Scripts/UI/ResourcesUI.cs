using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesUI : MonoBehaviour
{
    private ResourceTypeListSO resourceTypeList;

    private Dictionary<ResourceTypeSO, Transform> resourceTransformDict;

    private void Awake()
    {
        resourceTypeList = GameAssets.Instance.resourceTypeList;
        resourceTransformDict = new Dictionary<ResourceTypeSO, Transform>();

        Transform resourceTemplate = transform.Find("ResourceTemplate");
        resourceTemplate.gameObject.SetActive(false);

        int index = 0;
        foreach (ResourceTypeSO resourceType in resourceTypeList.list)
        {
            Transform resourceTrnasform = Instantiate(resourceTemplate, transform);
            resourceTrnasform.gameObject.SetActive(true);

            float offsetAmount = -160f;
            resourceTrnasform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);

            resourceTrnasform.Find("Icon").GetComponent<Image>().sprite = resourceType.sprite;

            resourceTransformDict[resourceType] = resourceTrnasform;

            index++;
        }
    }

    private void Start()
    {
        ResourceManager.Instance.OnResourceAmountChanged += UpdateResourceAmount;
    }

    private void UpdateResourceAmount(object sender = null, EventArgs args = null)
    {
        foreach (ResourceTypeSO resourceType in resourceTypeList.list)
        {
            int resourceAmount = ResourceManager.Instance.GetResourceAmount(resourceType);
            resourceTransformDict[resourceType].Find("Text").GetComponent<TMP_Text>().text = resourceAmount.ToString();
        }
    }
}
