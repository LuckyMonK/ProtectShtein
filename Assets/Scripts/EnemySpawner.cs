using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private int numberOfWawes;
    [SerializeField] private int numberInWawe;

    [SerializeField] private float timeDelay;

    private Bounds bounds;
    void Start()
    {
        bounds = GetComponent<Collider>().bounds;
        StartCoroutine(SpawnEnemies());

    }

    private IEnumerator SpawnEnemies() {
        for (int i = 0; i < numberOfWawes; i++) {
            yield return new WaitForSeconds(timeDelay);
            for(int j = 0; j < numberInWawe; j++)
                Instantiate(enemyPrefab, RandomPointInBounds(), Quaternion.identity);
            yield return new WaitForSeconds(1f);
        }
    }

    private Vector3 RandomPointInBounds()
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }
}
