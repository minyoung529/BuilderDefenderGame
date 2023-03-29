using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    private Dictionary<ResourceTypeSO, int> resourceAmountDict;

    public static ResourceManager Instance { get; private set; }

    public event EventHandler OnResourceAmountChanged;

    [SerializeField]
    private List<ResourceAmount> startingResourceAmountList;

    private void Awake()
    {
        Instance = this;

        resourceAmountDict = new Dictionary<ResourceTypeSO, int>();
        ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);

        foreach (ResourceTypeSO resourceType in resourceTypeList.list)
        {
            resourceAmountDict[resourceType] = 0;
        }

        foreach(ResourceAmount amount in  startingResourceAmountList)
        {
            AddResource(amount.resourceType, amount.amount);
        }
    }

    public void AddResource(ResourceTypeSO resourceType, int amount)
    {
        resourceAmountDict[resourceType] += amount;
        OnResourceAmountChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveResource(ResourceTypeSO resourceType, int amount)
    {
        resourceAmountDict[resourceType] -= amount;
    }

    public int GetResourceAmount(ResourceTypeSO resourceType)
    {
        return resourceAmountDict[resourceType];
    }

    public bool CanAfford(ResourceAmount[] resourceCosts)
    {
        foreach(ResourceAmount cost in resourceCosts)
        {
            if (resourceAmountDict[cost.resourceType] < cost.amount)
            {
                return false;
            }
        }

        return true;
    }

    public void SpendResources(ResourceAmount[] resourceCosts)
    {
        foreach (ResourceAmount cost in resourceCosts)
        {
            resourceAmountDict[cost.resourceType] -= cost.amount;
        }
    }
}
