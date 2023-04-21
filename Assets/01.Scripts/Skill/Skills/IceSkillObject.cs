using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSkillObject : MonoBehaviour
{
    public Action<Enemy> onEnterEnemy;
    public Action<Enemy> onExitEnemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;

        onEnterEnemy?.Invoke(collision.GetComponent<Enemy>());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;

        onExitEnemy?.Invoke(collision.GetComponent<Enemy>());
    }
}
