using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillBase
{
    protected float curCoolingTime;

    private SkillSO skillSO;

    private bool isSkilling = false;

    #region Property
    public SkillSO SkillSO => skillSO;
    public bool CanSkill => curCoolingTime > CoolingTime;
    public bool IsSkilling => isSkilling;
    private float CoolingTime => skillSO.coolingTime;
    #endregion

    public virtual void OnUpdate()
    {
        CalculateCoolingTime();
    }

    public void SkillDown()
    {
        if (!CanSkill || isSkilling) return;

        isSkilling = true;
        OnSkillDown();

        // 누를 때 끝나는 스킬이면 끝낸다.
        if (skillSO.isDownSkill && skillSO.isFast)
        {
            EndSkill();
        }

        Debug.Log($"{GetType()} Down!");
    }

    public void SkillUp()
    {
        if (isSkilling && skillSO.isDownSkill)
        {
            OnSkillUp();
            Debug.Log($"{GetType()} Up!");
        }
    }

    protected virtual void OnSkillDown() { }
    protected virtual void OnSkillUp() { }

    protected void EndSkill()
    {
        isSkilling = false;
        curCoolingTime = 0f;
    }

    private void CalculateCoolingTime()
    {
        if (isSkilling) return;

        if (curCoolingTime < CoolingTime)
        {
            curCoolingTime += Time.deltaTime;
        }
    }

    internal void Init(SkillSO skillSO)
    {
        this.skillSO = skillSO;
        curCoolingTime = CoolingTime + 1f;
        isSkilling = false;
    }

    public float NormalizedValue()
    {
        return Mathf.Clamp01(curCoolingTime / CoolingTime);
    }
}

