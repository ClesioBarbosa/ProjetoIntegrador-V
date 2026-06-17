using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner3D : MonoBehaviour
{
     [Header("Prefabs")]
    public GameObject[] fishPrefabs;

    [Header("Spawn")]
    public float spawnInterval = 3f;

    public float minY = -3f;
    public float maxY = 3f;

    public float spawnX = 12f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnFish();

            timer = 0f;
        }
    }

    void SpawnFish()
    {
        GameObject prefab =
            fishPrefabs[
                Random.Range(
                    0,
                    fishPrefabs.Length
                )
            ];

        bool spawnRight =
            Random.Range(0, 2) == 0;

        Vector3 spawnPos;

        if (spawnRight)
        {
            spawnPos =
                new Vector3(
                    spawnX,
                    Random.Range(minY, maxY),
                    0f
                );
        }
        else
        {
            spawnPos =
                new Vector3(
                    -spawnX,
                    Random.Range(minY, maxY),
                    0f
                );
        }

        Instantiate(
            prefab,
            spawnPos,
            Quaternion.identity
        );
    }
}
