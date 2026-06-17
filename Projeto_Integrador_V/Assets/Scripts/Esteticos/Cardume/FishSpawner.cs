using UnityEngine;
using System.Collections;

public class FishSpawner : MonoBehaviour
{
    public GameObject fishPrefab;
    public GameObject leaderPrefab;

    public int minFish = 3;
    public int maxFish = 10;

    public float spawnDistance = 20f;
    public float spread = 3f;

    public float spawnInterval = 4f; // ⬅️ tempo entre spawns

    public Camera cam;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnSchool();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnSchool()
    {
        int amount = Random.Range(minFish, maxFish + 1);

        Vector3 spawnPos = cam.transform.position + cam.transform.forward * spawnDistance;

        GameObject leader = Instantiate(leaderPrefab, spawnPos, Quaternion.identity);

        for (int i = 0; i < amount; i++)
        {
            Vector3 offset = new Vector3(
                Random.Range(-spread, spread),
                Random.Range(-spread, spread),
                Random.Range(-spread, spread)
            );

            GameObject fish = Instantiate(fishPrefab, spawnPos + offset, Quaternion.identity);

            FishFollower follower = fish.AddComponent<FishFollower>();
            follower.leader = leader.transform;
            follower.offset = offset;
        }

        // Só destrói quando sair da câmera (SEM respawn aqui)
        leader.AddComponent<FishDespawn>();
    }
}