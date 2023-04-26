using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSkill : SkillBase
{
    private LineSkillObject skillObject;

    protected override void OnSkillDown()
    {
        skillObject = Object.Instantiate(GameAssets.Instance.lineSkill);
        skillObject.OnEnd += OnEnd;
    }

    private void OnEnd()
    {
        skillObject.OnEnd -= OnEnd;
        
        Object.Destroy(skillObject.gameObject);
        skillObject = null;
    }
}
