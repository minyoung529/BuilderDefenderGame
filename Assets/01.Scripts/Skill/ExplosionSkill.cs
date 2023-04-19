using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ExplosionSkill : SkillBase
{
    private readonly float CHARGING_TIME = 1f;
    private readonly float RADIUS = 5.5f;
    private Transform skillObject;

    protected override void OnSkillDown()
    {
        skillObject = Object.Instantiate(GameAssets.Instance.explosionSkill, UtilClass.GetMouseWorldPosition(), Quaternion.identity);

        Vector3 circleScale = Vector3.one * RADIUS * 2f;

        skillObject.Find("Circle").localScale = circleScale;
        skillObject.Find("Radius").DOScale(circleScale, CHARGING_TIME).OnComplete(Explosion);
    }

    private void Explosion()
    {
        skillObject.Find("Circle").Find("Collider").gameObject.SetActive(true);

        SkillManager.Instance.StartCoroutine(DestroySkillObject());
    }

    private IEnumerator DestroySkillObject()
    {
        yield return null;
        skillObject.Find("Circle").Find("Outline").gameObject.SetActive(false);
        skillObject.Find("Radius").gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);

        skillObject = null;
        Object.Destroy(skillObject);
    }
}
