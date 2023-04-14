using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthSytem : MonoBehaviour
{
    public event EventHandler OnDamaged;
    public event EventHandler OnDied;
    public event EventHandler OnHealed;

    [SerializeField]
    private int healthAmountMax;
    private int healthAmount;

    public event EventHandler OnHealthAmountMaxChanged;

    private void Awake()
    {
        healthAmount = healthAmountMax;
    }

    public void Damage(int damage)
    {
        healthAmount -= damage;
        healthAmount = Mathf.Clamp(healthAmount, 0, healthAmountMax);

        OnDamaged?.Invoke(this, EventArgs.Empty);

        if (IsDead())
        {
            OnDied?.Invoke(this, EventArgs.Empty);
        }
    }

    public void Heal(int healAmount)
    {
        healthAmount += healAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, healthAmountMax);
        OnHealed?.Invoke(this, EventArgs.Empty);
    }

    public void HealFull()
    {
        healthAmount = healthAmountMax;
        OnHealed?.Invoke(this, EventArgs.Empty);
    }

    [ContextMenu("DIE")]
    public void Die()
    {
        OnDied?.Invoke(this, EventArgs.Empty);
    }

    [ContextMenu("DAMAGED")]
    public void DamagedHalf()
    {
        Damage(healthAmountMax / 2);
    }

    public int GetHealthAmountMax()
    {
        return healthAmountMax;
    }

    public bool IsDead()
    {
        return healthAmount == 0;
    }

    public int GetHealthAmount()
    {
        return healthAmount;
    }

    public float GetHealthAmountNormalized()
    {
        return (float)healthAmount / healthAmountMax;
    }

    public bool IsFullHealthAmount()
    {
        return healthAmount == healthAmountMax;
    }

    public void SetHealthAmountMax(int healthAmountMax, bool update)
    {
        this.healthAmountMax = healthAmountMax;

        if (update)
        {
            healthAmount = healthAmountMax;
        }

        OnHealthAmountMaxChanged?.Invoke(this, EventArgs.Empty);
    }
}
