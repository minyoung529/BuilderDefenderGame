using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactEnemyDie : MonoBehaviour
{
    [SerializeField]
    private int damageAmount = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();

        if (enemy)
        {
            DamageEnemy(enemy);
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();

        if (enemy)
        {
            DamageEnemy(enemy);
        }
    }

    private void DamageEnemy(Enemy enemy)
    {
        enemy.GetComponent<HealthSytem>().Damage(damageAmount);
    }
}
