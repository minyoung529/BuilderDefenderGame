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

        MusicManager.Instance.VolumeDown();
    }

    private void ResetSkill()
    {
        MusicManager.Instance.VolumeUp();

        skillObject.OnEnd -= ResetSkill;

        EndSkill();

        Object.Destroy(skillObject.gameObject);
        skillObject = null;
    }
}
