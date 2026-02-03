using System;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Events;

public enum SpawnWave
{
    Wave1,
    Wave2,
    Wave3,
    Wave4,
    ImFreee
}
[Serializable]
public class EnemySpawn
{
    public EnemyType enemyType;
    public int spawnPointIndex;
}
public class Spawner : MonoBehaviour
{
    public static Spawner Instance;

    public Transform[] spawnPoints;
    public SpawnWave wave;
    [SerializeField] EnemySpawn[] firstWaveEnemies;
    [SerializeField] EnemySpawn[] secondWaveEnemies;
    [SerializeField] EnemySpawn[] thirdWaveEnemies;
    [SerializeField] EnemySpawn[] forthWaveEnemies;
    [SerializeField] int currentEnemies = 0;
    bool isSpawningWave = false;
    bool allWavesCompleted = false;

    [SerializeField] UnityEvent onAllWavesCompleted;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        //else
        //{
        //    Destroy(gameObject);
        //}
    }
    public void SpawnCurrentWaveEnemies()
    {
        if (isSpawningWave) return;
        isSpawningWave = true;

        currentEnemies = 0;
        switch (wave)
        {
            case SpawnWave.Wave1:
                //Start Wave 1 spawning logic
                foreach (EnemySpawn enemySpawn in firstWaveEnemies)
                {
                    //spawno tutti dal primo punto per ora, altrimenti random?
                    GameObject g = SpawnEnemy(enemySpawn.enemyType, spawnPoints[enemySpawn.spawnPointIndex]);
                    Damageable damageable = g.GetComponent<Damageable>();
                    //damageable.onDeath.RemoveListener(EnemyDead);
                    damageable.beforeDeath += EnemyDead;
                    //damageable.onDeath.AddListener(EnemyDead);
                    //dopo aver spawnato assegno alla morte che toglie 1 a currentEnemies
                    //e va alla prossima wave
                    currentEnemies++;
                }
                wave = SpawnWave.Wave2;
                break;
            case SpawnWave.Wave2:
                //Start Wave 2 spawning logic
                foreach (EnemySpawn enemySpawn in secondWaveEnemies)
                {
                    //spawno tutti dal primo punto per ora, altrimenti random?
                    GameObject g = SpawnEnemy(enemySpawn.enemyType, spawnPoints[enemySpawn.spawnPointIndex]);
                    Damageable damageable = g.GetComponent<Damageable>();
                    //damageable.onDeath.RemoveListener(EnemyDead);
                    damageable.beforeDeath += EnemyDead;
                    //damageable.onDeath.AddListener(EnemyDead);
                    //dopo aver spawnato assegno alla morte che toglie 1 a currentEnemies
                    //e va alla prossima wave
                    currentEnemies++;
                }
                wave = SpawnWave.Wave3;
                break;
            case SpawnWave.Wave3:
                //Start Wave 3 spawning logic
                foreach (EnemySpawn enemySpawn in thirdWaveEnemies)
                {
                    //spawno tutti dal primo punto per ora, altrimenti random?
                    GameObject g = SpawnEnemy(enemySpawn.enemyType, spawnPoints[enemySpawn.spawnPointIndex]);
                    Damageable damageable = g.GetComponent<Damageable>();
                    //damageable.onDeath.RemoveListener(EnemyDead);
                    damageable.beforeDeath += EnemyDead;
                    //damageable.onDeath.AddListener(EnemyDead);
                    //dopo aver spawnato assegno alla morte che toglie 1 a currentEnemies
                    //e va alla prossima wave
                    currentEnemies++;
                }
                wave = SpawnWave.Wave4;
                break;
            case SpawnWave.Wave4:
                //Start Wave 3 spawning logic
                foreach (EnemySpawn enemySpawn in forthWaveEnemies)
                {
                    //spawno tutti dal primo punto per ora, altrimenti random?
                    GameObject g = SpawnEnemy(enemySpawn.enemyType, spawnPoints[enemySpawn.spawnPointIndex]);
                    Damageable damageable = g.GetComponent<Damageable>();
                    //damageable.onDeath.RemoveListener(EnemyDead);
                    damageable.beforeDeath += EnemyDead;
                    //damageable.onDeath.AddListener(EnemyDead);
                    //dopo aver spawnato assegno alla morte che toglie 1 a currentEnemies
                    //e va alla prossima wave
                    currentEnemies++;
                }
                wave = SpawnWave.ImFreee;
                break;

            case SpawnWave.ImFreee:
                if (allWavesCompleted) return;
                allWavesCompleted = true;
                onAllWavesCompleted?.Invoke();
                break;
        }
        isSpawningWave = false;
    }
    /// <summary>
    /// controlla la morte di un nemico, se sono tutti morti spawna la wave successiva
    /// </summary>
    public void EnemyDead()
    {
        Debug.Log("nemico muertooo");
        currentEnemies--;

        if (currentEnemies > 0)
            return;

        SpawnCurrentWaveEnemies();
    }

    public GameObject SpawnEnemy(EnemyType enemyType, Transform position)
    {
        switch (enemyType) { 
            case EnemyType.Clown:
                //Instantiate clown prefab at position
                return GameManager.Instance.CreateClownEnemy(position);
            case EnemyType.Spider:
                //Instantiate spider prefab at position
                return GameManager.Instance.CreateSpiderEnemy(position);
            case EnemyType.Feever:
                //Instantiate feeve prefab at position
                return GameManager.Instance.CreateFeeverEnemy(position);
                default:
                return null;
        }
    }
}
