using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Skill")]
public class SkillSO : ScriptableObject
{
    public float coolingTime;
    public Sprite icon;
    public string skillName;
    public KeyCode keyCode;
    public bool isDownSkill = true;
    public bool isFast = true;
}
