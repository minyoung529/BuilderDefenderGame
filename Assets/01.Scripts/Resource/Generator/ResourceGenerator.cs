using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    private float timer = 0f;
    private float timerMax;

    private BuildingTypeSO buildingType;
    private ResourceGeneratorData resourceGeneratorData;

    private void Awake()
    {
        buildingType = GetComponent<BuildingTypeHolder>().buildingType;
        resourceGeneratorData = buildingType.resourceGeneratorData;
        timerMax = buildingType.resourceGeneratorData.timerMax;
        timer = timerMax;
    }

    private void Start()
    {
        int nearByResourceAmount = GetNearbyResourceAmount(resourceGeneratorData, transform.position);

        if (nearByResourceAmount == 0)  // 비활성화
        {
            transform.Find("ResourceGeneratorOverlay").gameObject.SetActive(false);
            transform.GetComponentInChildren<Animator>().enabled = false;
            enabled = false;
        }
        else
        {
            timerMax = (resourceGeneratorData.timerMax / 2) +
            resourceGeneratorData.timerMax *
            (1 - (float)nearByResourceAmount / resourceGeneratorData.maxResourceAmount);
        }
    }

    public static int GetNearbyResourceAmount(ResourceGeneratorData data, Vector3 position)
    {
        int nearByresourceAmount = 0;
        Collider2D[] cols = Physics2D.OverlapCircleAll(position, data.resourceDetectRadius);

        foreach (Collider2D col in cols)
        {
            ResourceNode resourceNode = col.GetComponent<ResourceNode>();

            if (resourceNode)
            {
                if (resourceNode.resourceType == data.resourceType)
                    nearByresourceAmount++;
            }
        }

        return Mathf.Clamp(nearByresourceAmount, 0, data.maxResourceAmount);
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            timer = timerMax;
            ResourceManager.Instance.AddResource(buildingType.resourceGeneratorData.resourceType, 1);
        }
    }

    public ResourceGeneratorData GetResourceGeneratorData()
    {
        return resourceGeneratorData;
    }

    public float GetTimerNormalized()
    {
        return timer / timerMax;
    }

    public float GetAmountGeneratedPerSecond()
    {
        return 1 / timerMax;
    }
}
