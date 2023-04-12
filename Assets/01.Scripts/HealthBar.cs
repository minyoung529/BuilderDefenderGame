using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private HealthSytem healthSystem;

    private Transform barTransform;

    private void Awake()
    {
        barTransform = transform.Find("Bar");
    }

    private void Start()
    {
        healthSystem.OnDamaged += HealthSystem_OnDamaged;
        healthSystem.OnHealed += HealthSystem_OnHealed; ;
        UpdateBarVisible();
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
