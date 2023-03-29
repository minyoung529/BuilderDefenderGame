using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    private Enemy targetEnemy;
    private Vector3 lastMoveDir;
    private float timeToDie = 2f;

    private void Update()
    {
        timeToDie -= Time.deltaTime;

        if(timeToDie < 0f)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 moveDir;

        if (targetEnemy != null)
        {
            moveDir = (targetEnemy.transform.position - transform.position).normalized;
        }

        else
        {
            moveDir = lastMoveDir;
        }

        float moveSpeed = 20f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, 0, UtilClass.GetAngleFromVector(moveDir));
        lastMoveDir = moveDir;
    }

    private void SetTarget(Enemy target)
    {
        targetEnemy = target;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();

        if (enemy)
        {
            int damageAmount = 10;
            enemy.GetComponent<HealthSytem>().Damage(damageAmount);

            Destroy(gameObject);
        }
    }

    public static ArrowProjectile Create(Vector3 position, Enemy enemy)
    {
        ArrowProjectile temp = Instantiate(Resources.Load<ArrowProjectile>("pfArrowProjectile"), position, Quaternion.identity);
        temp.SetTarget(enemy);

        return temp;
    }
}
