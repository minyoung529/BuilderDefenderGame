using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Enemy : MonoBehaviour
{
    public static Enemy Create(Vector3 position)
    {
        return Instantiate(Resources.Load<Enemy>("pfEnemy"), position, Quaternion.identity);
    }

    private Transform targetTransform;
    private Rigidbody2D enemyRigidbody;

    private float lookForTimer;
    private float lookForTimerMax = 0.2f;

    private void Awake()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        lookForTimerMax = Random.Range(0f, lookForTimerMax);
    }

    private void Start()
    {
        targetTransform = BuildingManager.Instance.GetHQBuilding().transform;
    }

    private void Update()
    {
        HandleMove();
        HandleTargetting();
    }

    private void HandleMove()
    {
        if (targetTransform)
        {
            Vector3 moveDir = (targetTransform.position - transform.position).normalized;
            float moveSpeed = 8f;

            enemyRigidbody.velocity = moveDir * moveSpeed;

        }
        else
        {
            enemyRigidbody.velocity = Vector3.zero;
        }
    }

    private void HandleTargetting()
    {
        lookForTimer += Time.deltaTime;

        if (lookForTimer >= lookForTimerMax)
        {
            lookForTimer = 0f;
            LookForTarget();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Building building = collision.gameObject.GetComponent<Building>();

        if (building)
        {
            HealthSytem healthSytem = building.GetComponent<HealthSytem>();
            healthSytem?.Damage(10);

            Destroy(gameObject);
        }
    }

    private void LookForTarget()
    {
        float targetMaxRadius = 20f;
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);

        foreach (Collider2D collider in collider2DArray)
        {
            Building building = collider.gameObject.GetComponent<Building>();

            if (building)
            {
                if (targetTransform == null)
                {
                    targetTransform = building.transform;
                }
                else
                {
                    if (Vector3.Distance(transform.position, targetTransform.position) > Vector3.Distance(transform.position, building.transform.position))
                    {
                        targetTransform = building.transform;
                    }
                }
            }
        }

        if (targetTransform == null)
        {
            targetTransform = BuildingManager.Instance.GetHQBuilding().transform;
        }
    }
}