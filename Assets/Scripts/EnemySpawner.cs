using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyAI enemyPrefab;
    public PlayerController player;
    public List<Transform> patrolPoints;
    public int enemiesMaxCount = 5;
    public float delay = 5;
    public float increaseEnemiesCountDelay = 30;

    private List<Transform> spawnerPoints;
    private List<EnemyAI> enemies;

    private float timeLastSpawned;

    private void Start()
    {
        spawnerPoints = new List<Transform>(transform.GetComponentsInChildren<Transform>());
        enemies = new List<EnemyAI>();

        Invoke("IncreaseEnemiesMaxCount", increaseEnemiesCountDelay);
    }

    private void IncreaseEnemiesMaxCount()
    {
        enemiesMaxCount++;
        Invoke("IncreaseEnemiesMaxCount", increaseEnemiesCountDelay);
    }

    private void Update()
    {
        for(var i =0; i< enemies.Count; i++)
        {
            if (enemies[i].IsAlive()) continue;
            enemies.RemoveAt(i);
            i--;
        }

        if (enemies.Count >= enemiesMaxCount) return;
        if (Time.time - timeLastSpawned < delay) return;

        CreateEnemy();
    }

    private void CreateEnemy()
    {
        var enemy = Instantiate(enemyPrefab);
        enemy.transform.position = spawnerPoints[Random.Range(0, spawnerPoints.Count)].position;
        enemy.player = player;
        enemy.patrolPoints = patrolPoints;
        enemies.Add(enemy);
        timeLastSpawned = Time.time;
    }
}
