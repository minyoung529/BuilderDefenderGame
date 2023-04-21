using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillManager : MonoBehaviour
{
    private List<SkillSO> skillInfos = new List<SkillSO>();
    private List<SkillBase> skills = new List<SkillBase>();

    public IReadOnlyList<SkillSO> SkillInfos => skillInfos;
    public IReadOnlyList<SkillBase> Skills => skills;

    public static SkillManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        skillInfos = GameAssets.Instance.skillList.skillList;

        for (int i = 0; i < skillInfos.Count; i++)
        {
            SkillBase skillBase = (SkillBase)Activator.CreateInstance(Type.GetType(skillInfos[i].name));
            skillBase.Init(skillInfos[i]);
            skills.Add(skillBase);
        }
    }

    private void Update()
    {
        // 일단 마우스 금지 (나중에 기획 바꾸면 지우기)
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) return;

        foreach (SkillBase skill in skills)
        {
            if (Input.GetKeyDown(skill.SkillSO.keyCode))
            {
                skill.SkillDown();
            }

            if (Input.GetKeyUp(skill.SkillSO.keyCode))
            {
                skill.SkillUp();
            }

            skill.OnUpdate();
        }
    }
}
