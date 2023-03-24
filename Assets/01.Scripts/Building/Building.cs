using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    private HealthSytem healthSystem;
    private BuildingTypeSO buildingType;

    private void Start()
    {
        healthSystem = GetComponent<HealthSytem>();
        buildingType = GetComponent<BuildingTypeHolder>().buildingType;

        healthSystem.OnDied += HealthSytem_OnDied;
        healthSystem.SetHealthAmountMax(buildingType.healthAmountMax, true);
    }

    private void HealthSytem_OnDied(object sender, System.EventArgs e)
    {
        Destroy(gameObject);
    }
}
