using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSkill : SkillBase
{
    private readonly float SLOW_TIME = 1f;
    private readonly float ICE_TIME = 2f;
    private readonly float RADIUS = 8.5f;

    private IceSkillObject skillObject;

    private List<Enemy> enemies = new List<Enemy>();

    protected override void OnSkillDown()
    {
        skillObject = Object.Instantiate(GameAssets.Instance.iceSkill, UtilClass.GetMouseWorldPosition(), Quaternion.identity);

        Vector3 circleScale = Vector3.one * RADIUS * 2f;
        skillObject.transform.Find("Circle").localScale = circleScale;

        skillObject.onEnterEnemy += OnEnterEnemy;
        skillObject.onExitEnemy += OnExitEnemy;

        SkillManager.Instance.StartCoroutine(EndCoroutine());
    }

    private void OnEnterEnemy(Enemy enemy)
    {
        // 찾을 수 없다면 넣어준다
        if (enemies.Find(x => x == enemy) == null)
        {
            enemies.Add(enemy);
            enemy.Ice(SLOW_TIME, ICE_TIME, GetIsSkilling);
        }
    }

    private bool GetIsSkilling()
    {
        return IsSkilling;
    }

    private void OnExitEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);
        enemy.exitIceBox = true;
    }

    private IEnumerator EndCoroutine()
    {
        yield return new WaitForSeconds(SLOW_TIME + ICE_TIME);
        ResetData();
        EndSkill();
    }

    private void ResetData()
    {
        skillObject.onEnterEnemy -= OnEnterEnemy;
        skillObject.onExitEnemy -= OnExitEnemy;

        Object.Destroy(skillObject.gameObject);
        skillObject = null;

        enemies.Clear();
    }
}
