using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private Enemy targetEnemy;
    private float lookForTimer;
    private float lookForTimerMax = 0.2f;

    private Vector3 projectileSpawnPosition;

    [SerializeField]
    private float shootTimerMax;
    private float shootTimer;

    private void Awake()
    {
        projectileSpawnPosition = transform.Find("projectileSpawnPosition").position;
    }

    private void Update()
    {
        HandleTargetting();
        HandleShooting();
    }

    private void LookForTargets()
    {
        float targetMaxRadius = 20f;

        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);

        foreach (Collider2D col in collider2DArray)
        {
            Enemy enemy = col.GetComponent<Enemy>();

            if (enemy)
            {
                if (targetEnemy == null)
                {
                    targetEnemy = enemy;
                }
                else
                {
                    if (Vector3.Distance(transform.position, enemy.transform.position) <
                        Vector3.Distance(transform.position, targetEnemy.transform.position))
                    {
                        targetEnemy = enemy;
                    }
                }
            }
        }
    }



    private void HandleTargetting()
    {
        lookForTimer += Time.deltaTime;

        if (lookForTimer >= lookForTimerMax)
        {
            lookForTimer = 0f;
            LookForTargets();
        }
    }

    private void HandleShooting()
    {
        shootTimer += Time.deltaTime;

        if(shootTimer >= shootTimerMax)
        {
            shootTimer = 0f;

            if (targetEnemy)
            {
                ArrowProjectile.Create(projectileSpawnPosition, targetEnemy);
            }
        }
    }
}
