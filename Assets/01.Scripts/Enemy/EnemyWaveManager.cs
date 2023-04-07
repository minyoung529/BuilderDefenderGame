using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyWaveManager : MonoBehaviour
{
    private enum State
    {
        WaitingToSpawnNextWave,
        SpawningWave
    }

    private float nextWaveSpawnTimer = 10f;

    private float nextEnemySpawnTimer = 1f;
    private int remainingEnemySpawnAmount;
    [SerializeField] private List<Transform> spawnPositions;
    Transform spawnPosition;

    private State state;
    private int waveNumber;

    [SerializeField]
    private Transform nextWaveSpawnPositionTransform;

    public event EventHandler OnWaveNumberChanged;

    void Start()
    {
        nextWaveSpawnTimer = 3f;
        state = State.WaitingToSpawnNextWave;
        spawnPosition = spawnPositions[Random.Range(0, spawnPositions.Count)];
        nextWaveSpawnPositionTransform.position = spawnPosition.position;
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToSpawnNextWave:
                nextWaveSpawnTimer -= Time.deltaTime;

                if (nextWaveSpawnTimer < 0f)
                {
                    SpawnWave();
                }

                break;
            case State.SpawningWave:
                if (remainingEnemySpawnAmount > 0)
                {
                    nextEnemySpawnTimer -= Time.deltaTime;

                    if (nextEnemySpawnTimer < 0f)
                    {
                        nextEnemySpawnTimer = Random.Range(0, 2f);
                        Enemy.Create(spawnPosition.position + UtilClass.GetRandomDir() * Random.Range(0, 10f));
                        remainingEnemySpawnAmount--;
                    }
                }
                else
                {
                    state = State.WaitingToSpawnNextWave;
                    spawnPosition = spawnPositions[Random.Range(0, spawnPositions.Count)];
                    nextWaveSpawnPositionTransform.position = spawnPosition.position;
                    nextWaveSpawnTimer = 10f;
                }
                break;
        }
    }

    private void SpawnWave()
    {
        remainingEnemySpawnAmount = 5 + 3 * waveNumber;

        state = State.SpawningWave;
        waveNumber++;

        OnWaveNumberChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetWaveNumber()
    {
        return waveNumber;
    }

    public float GetNextWaveSpawnTimer()
    {
        return nextWaveSpawnTimer;
    }

    public Vector3 GetSpawnPosition()
    {
        return spawnPosition.position;
    }
}
