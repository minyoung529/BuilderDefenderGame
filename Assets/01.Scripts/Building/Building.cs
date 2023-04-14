using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    private HealthSytem healthSystem;
    private BuildingTypeSO buildingType;
    private Transform buildingDemolishBtn;
    private Transform buildingRepairBtn;

    private void Awake()
    {
        buildingDemolishBtn = transform.Find("pfBuildingDemolishBtn");
        buildingRepairBtn = transform.Find("pfRepairBtn");

        HideBuildingDemolishBtn();
        HideBuildingRepairBtn();
    }
    private void Start()
    {
        healthSystem = GetComponent<HealthSytem>();
        buildingType = GetComponent<BuildingTypeHolder>().buildingType;

        healthSystem.OnDied += HealthSytem_OnDied;
        healthSystem.OnDamaged += HealthSystem_OnDamaged;
        healthSystem.OnHealed += HealthSystem_OnHealed; ;
        healthSystem.SetHealthAmountMax(buildingType.healthAmountMax, true);
    }

    private void HealthSystem_OnHealed(object sender, System.EventArgs e)
    {
        if(healthSystem.IsFullHealthAmount())
        {
            HideBuildingRepairBtn();
        }
    }

    private void HealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        SoundManager.Instance.PlaySound(Sound.BuildingDamaged);
        ShowBuildingRepairBtn();
        CinemachineShake.Instance.ShakeCemera(7f, 0.15f);
        ChromaticAberrationEffect.Instance.SetWeight(1f);
    }

    private void HealthSytem_OnDied(object sender, System.EventArgs e)
    {
        CinemachineShake.Instance.ShakeCemera(10f, 0.2f);

        Instantiate(GameAssets.Instance.pfBuildingDestroyedParticles, transform.position, Quaternion.identity);
        ChromaticAberrationEffect.Instance.SetWeight(1f);
        SoundManager.Instance.PlaySound(Sound.BuildingDestroyed);
        Destroy(gameObject);
    }

    private void OnMouseEnter()
    {
        ShowBuildingDemolishBtn();
    }


    private void OnMouseExit()
    {
        HideBuildingDemolishBtn();
    }

    private void ShowBuildingDemolishBtn()
    {
        buildingDemolishBtn?.gameObject?.SetActive(true);
    }

    private void HideBuildingDemolishBtn()
    {
        buildingDemolishBtn?.gameObject?.SetActive(false);
    }

    private void ShowBuildingRepairBtn()
    {
        buildingRepairBtn?.gameObject?.SetActive(true);
    }

    private void HideBuildingRepairBtn()
    {
        buildingRepairBtn?.gameObject?.SetActive(false);
    }
}
