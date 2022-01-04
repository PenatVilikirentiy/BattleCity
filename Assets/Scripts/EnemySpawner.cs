using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SpawnConfig
{
    public GameObject prefab;
    public List<Transform> placeToSpawn;
    public float timeToStartSpawn;
    public int amountToSpawn;
    public float spawnPeriod;
}

public class EnemySpawner : MonoBehaviour
{
    public GameObject portal;
    public float portalLifeTime;
    public float timeBetweenPortalSpawnAndEnemy;
    [Tooltip("Относительно того что заспавнил")]
    public Vector3 portalPositionOffset;
    public float timeBetweenEnemyTypes;
    public List<SpawnConfig> configs;


    private void Start()
    {
        StartCoroutine(SpawnEnemies(configs));
    }

    public IEnumerator SpawnEnemies(List<SpawnConfig> confogs)
    {
        foreach (SpawnConfig config in configs)
        {
            StartCoroutine(SpawnEnemy(config));
            yield return new WaitForSeconds(config.spawnPeriod * config.amountToSpawn + config.timeToStartSpawn + timeBetweenEnemyTypes);
        }

    }

    public IEnumerator SpawnEnemy(SpawnConfig config)
    {
        yield return new WaitForSeconds(config.timeToStartSpawn);
        for (int i = 0; i < config.amountToSpawn; i++)
        {
            int random = Random.Range(0, config.placeToSpawn.Count);
            Destroy(Instantiate(portal, config.placeToSpawn[random].position + portalPositionOffset, Quaternion.identity), portalLifeTime);
            yield return new WaitForSeconds(timeBetweenPortalSpawnAndEnemy);
            Instantiate(config.prefab, config.placeToSpawn[random].position, Quaternion.identity);
            yield return new WaitForSeconds(config.spawnPeriod);
        }
    }

}
