using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private HealthSytem healthSystem;

    [SerializeField]
    private Transform seperatorContainer;

    private Transform barTransform;

    private void Awake()
    {
        barTransform = transform.Find("Bar");
    }

    private void Start()
    {
        seperatorContainer = transform.Find("SeperatorContainer");
        ConstructHealthBarSeperators();

        healthSystem.OnDamaged += HealthSystem_OnDamaged;
        healthSystem.OnHealed += HealthSystem_OnHealed; ;
        healthSystem.OnHealthAmountMaxChanged += HealthSystem_OnHealthAmountMaxChanged;
        UpdateBarVisible();
        ConstructHealthBarSeperators();
    }

    private void ConstructHealthBarSeperators()
    {
        Transform template = seperatorContainer.Find("SeperatorTemplate");
        template.gameObject.SetActive(false);

        foreach(Transform seperator in seperatorContainer)
        {
            if (seperator == template) continue;
            Destroy(seperator.gameObject);
        }

        int healthAmountPerSeperate = 10;
        float barSize = 4f;
        float barOneHealthAmountSize = barSize / healthSystem.GetHealthAmountMax();
        int healthSeperatorCount = Mathf.FloorToInt(healthSystem.GetHealthAmountMax() / healthAmountPerSeperate);

        for (int i = 1; i < healthSeperatorCount; i++)
        {
            Transform seperateTransform = Instantiate(template, seperatorContainer);
            seperateTransform.gameObject.SetActive(true);
            seperateTransform.localPosition = new Vector3(barOneHealthAmountSize * i * healthAmountPerSeperate, 0, 0);
        }
    }

    private void HealthSystem_OnHealthAmountMaxChanged(object sender, System.EventArgs e)
    {
        ConstructHealthBarSeperators();
    }

    private void HealthSystem_OnHealed(object sender, System.EventArgs e)
    {
        UpdateBar();
        UpdateBarVisible();
    }

    private void HealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        UpdateBar();
    }

    private void UpdateBar()
    {
        UpdateBarVisible();
        barTransform.localScale = new Vector3(healthSystem.GetHealthAmountNormalized(), 1f, 1f);
    }

    private void UpdateBarVisible()
    {
        if (healthSystem.IsFullHealthAmount())
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
