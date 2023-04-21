using DG.Tweening.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public static Enemy Create(Vector3 position)
    {
        return Instantiate(GameAssets.Instance.enemy, position, Quaternion.identity);
    }

    private Transform targetTransform;
    private Rigidbody2D enemyRigidbody;

    private float lookForTimer;
    private float lookForTimerMax = 0.2f;

    private HealthSytem healthSytem;

    private float speed = 8f;

    public bool exitIceBox = false;

    [SerializeField]
    private ParticleSystem slowParticle;
    [SerializeField]
    private ParticleSystem iceParticle;

    private void Awake()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        lookForTimerMax = Random.Range(0f, lookForTimerMax);

        healthSytem = GetComponent<HealthSytem>();
        healthSytem.OnDied += HealthSytem_OnDied;
        healthSytem.OnDamaged += HealthSytem_OnDamaged; ;
    }

    private void HealthSytem_OnDamaged(object sender, System.EventArgs e)
    {
        SoundManager.Instance.PlaySound(Sound.EnemyHit);
        CinemachineShake.Instance.ShakeCemera(5f, 0.1f);
        ChromaticAberrationEffect.Instance.SetWeight(0.5f);
    }

    private void HealthSytem_OnDied(object sender, System.EventArgs e)
    {
        SoundManager.Instance.PlaySound(Sound.EnemyDie);
        CinemachineShake.Instance.ShakeCemera(7f, 0.15f);
        ChromaticAberrationEffect.Instance.SetWeight(0.5f);
        Instantiate(GameAssets.Instance.pfEnemyDieParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void Start()
    {
        if (BuildingManager.Instance.GetHQBuilding() != null)
        {
            targetTransform = BuildingManager.Instance.GetHQBuilding().transform;
        }
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
            float moveSpeed = speed;

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
            this.healthSytem?.Damage(999);
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
            if (BuildingManager.Instance.GetHQBuilding() != null)
            {
                targetTransform = BuildingManager.Instance.GetHQBuilding().transform;
            }
        }
    }

    public void Ice(float slowDuration, float iceDuration, Func<bool> isSkilling)
    {
        StartCoroutine(IceCoroutine(slowDuration, iceDuration, isSkilling));
    }

    private IEnumerator IceCoroutine(float slowDuration, float iceDuration, Func<bool> isSkilling)
    {
        float timer = slowDuration;
        float originalSpeed = speed;

        speed /= 2f;

        slowParticle.gameObject.SetActive(true);
        slowParticle.Play();
        while (timer > 0f && isSkilling.Invoke() && !exitIceBox)
        {
            timer -= Time.deltaTime;
            yield return null;
        }

        speed = 0f;
        iceParticle.gameObject.SetActive(true);
        iceParticle.Play();
        while (isSkilling.Invoke() && !exitIceBox)
        {
            timer -= Time.deltaTime;
            yield return null;
        }


        slowParticle.gameObject.SetActive(false);
        iceParticle.gameObject.SetActive(false);
        speed = originalSpeed;
        exitIceBox = false;
    }
}
