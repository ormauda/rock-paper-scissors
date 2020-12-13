using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] float spinSpeed = 500f;
    [SerializeField] Enemy[] enemyPrefabs;

    [SerializeField] float enemyLaunchSpeed = 2f;
    [SerializeField] float enemySpeedIncreaseRate = 0.02f;
    [SerializeField] [Range(0, 1)] float enemySpeedRandomnessFactor = 0.1f;


    [SerializeField] float baseSpawnDelayInSeconds = 2f;
    [SerializeField] float spawnDelayProgressionFactor = 200f;
    [SerializeField] [Range(0, 1)] float spawnDelayRandomnessFactor = 0.1f;

    // Cached references
    Clock clock;

    private void Start()
    {
        clock = FindObjectOfType<Clock>();
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        SpinAroundWorldOrigin();
    }

    private void SpinAroundWorldOrigin()
    {
        transform.RotateAround(Vector3.zero, Vector3.back, spinSpeed * Time.deltaTime);
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            float spawnDelay = GetRandomSpawnDelay();
            yield return new WaitForSeconds(spawnDelay);
            SpawnEnemy();
        }
    }

    private float GetRandomSpawnDelay()
    {
        var time = clock.GetTimeSinceStart();
        if (time > spawnDelayProgressionFactor)
        {
            time = spawnDelayProgressionFactor;
        }
        var baseDelay = Math.Abs(baseSpawnDelayInSeconds - time / spawnDelayProgressionFactor);
        var minSpawnDelay = baseDelay * (1 - spawnDelayRandomnessFactor);
        var maxSpawnDelay = baseDelay * (1 + spawnDelayRandomnessFactor);
        var randomDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
        return randomDelay;
    }

    private void SpawnEnemy()
    {
        var enemyPrefab = GetRandomEnemyPrefab();
        var newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        var enemyDirection = CalculateEnemyDirection();
        float enemySpeed = GetRandomEnemySpeed();

        newEnemy.Launch(enemyDirection * enemySpeed);
    }

    private float GetRandomEnemySpeed()
    {
        var time = clock.GetTimeSinceStart();
        var enemyBaseSpeed = enemyLaunchSpeed + time * enemySpeedIncreaseRate;
        var minEnemySpeed = enemyBaseSpeed * (1 - enemySpeedRandomnessFactor);
        var maxEnemySpeed = enemyBaseSpeed * (1 + enemySpeedRandomnessFactor);
        var randomSpeed = Random.Range(minEnemySpeed, maxEnemySpeed);
        return randomSpeed;
    }

    private Enemy GetRandomEnemyPrefab()
    {
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        return enemyPrefabs[randomIndex];
    }

    /*
     * caluclate the spawn direction vector based on the rotation of the spawner
     * Vector.X = SINUS of angle in RADIANS
     * Vector.Y = COSINUS of angle in RADIANS
     * 1 DEG = (1 * Mathf.Deg2Rad) RADIDANS
     * somehow the x coordinates are flipped, so multiply by -1
     */
    private Vector2 CalculateEnemyDirection()
    {
        float angle = transform.eulerAngles.z * Mathf.Deg2Rad;
        Vector2 launchDirection = new Vector2(Mathf.Sin(angle), -Mathf.Cos(angle));
        return launchDirection;
    }
}
