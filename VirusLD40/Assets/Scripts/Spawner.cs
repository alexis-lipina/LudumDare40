using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public static System.Random rng = new System.Random();

    [SerializeField] private float spawnTime;
    [SerializeField] private float initialSpawnTime;
    [SerializeField] private GameObject prefab;
    [SerializeField] private Vector2 randomRange;
    [SerializeField] private int maxSpawned;
    private List<Transform> spawnLocations;

    public void Awake()
    {
        spawnLocations = new List<Transform>(GetComponentsInChildren<Transform>());
        spawnLocations.Remove(transform);
        InvokeRepeating("SpawnWhite", initialSpawnTime, spawnTime);
    }

    private void SpawnWhite()
    {
        if (GameObject.FindGameObjectsWithTag(prefab.tag).Length < maxSpawned)
        {
            Transform spawnTrans = spawnLocations[rng.Next(spawnLocations.Count)];
            Vector3 position = new Vector3((float)(spawnTrans.position.x + (rng.NextDouble() * 2 * randomRange.x)), (float)(spawnTrans.position.y + (rng.NextDouble() * 2 * randomRange.y)), 0);
            Instantiate(prefab, position, Quaternion.identity);
        }
    }
}
