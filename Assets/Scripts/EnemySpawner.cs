using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] float spinSpeed = 500f;
    [SerializeField] Enemy[] enemyPrefabs;
    [SerializeField] float enemyLaunchSpeed = 20f;
    [SerializeField] float minSpawnDelayInSeconds = 1f;
    [SerializeField] float maxSpawnDelayInSeconds = 2f;

    private void Start()
    {
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
            var randomDelay = Random.Range(minSpawnDelayInSeconds, maxSpawnDelayInSeconds);
            yield return new WaitForSeconds(randomDelay);
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        var enemyPrefab = GetRandomEnemyPrefab();
        var newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        var enemyDirection = CalculateEnemyDirection();
        newEnemy.Launch(enemyDirection * enemyLaunchSpeed);
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
