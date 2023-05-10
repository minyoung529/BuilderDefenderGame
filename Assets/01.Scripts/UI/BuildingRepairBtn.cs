using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingRepairBtn : MonoBehaviour
{
    [SerializeField]
    private HealthSytem healthSytem;

    [SerializeField]
    private ResourceTypeSO goldResourceType;

    private void Awake()
    {
        Button b = GetComponentInChildren<Button>();
        b.onClick.AddListener(OnPressed);

        if(healthSytem == null)
        {
            healthSytem = transform.parent.GetComponent<HealthSytem>();
        }
    }

    private void OnPressed()
    {
        int missingHealth = healthSytem.GetHealthAmountMax() - healthSytem.GetHealthAmount();
        int repairCost = missingHealth / 2;

        ResourceAmount[] resources = new ResourceAmount[]
        {
            new ResourceAmount{resourceType=goldResourceType, amount=repairCost }
        };

        if(ResourceManager.Instance.CanAfford(resources))
        {
            ResourceManager.Instance.SpendResources(resources);
        }
        else
        {
            TooltipUI.Instance.Show("Cannot afford repair cost!", true);
        }

        healthSytem.HealFull();
    }
}
