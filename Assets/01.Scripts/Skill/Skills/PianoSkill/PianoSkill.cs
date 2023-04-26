using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoSkill : SkillBase
{
    private PianoSkillObject skillObject;

    protected override void OnSkillDown()
    {
        skillObject = Object.Instantiate(GameAssets.Instance.pianoSkill);
        skillObject.OnEnd += ResetSkill;
    }

    private void ResetSkill()
    {
        skillObject.OnEnd -= ResetSkill;

        Object.Destroy(skillObject.gameObject);
        skillObject = null;

        EndSkill();
    }
}
