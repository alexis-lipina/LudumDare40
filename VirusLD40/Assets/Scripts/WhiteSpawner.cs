using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteSpawner : MonoBehaviour
{
    public static System.Random rng = new System.Random();

    [SerializeField] private float spawnTime;
    [SerializeField] private float initialSpawnTime;
    [SerializeField] private GameObject whitePrefab;
    private List<Transform> spawnLocations;

    public void Awake()
    {
        spawnLocations = new List<Transform>((GetComponentsInChildren<Transform>()));
        InvokeRepeating("SpawnWhite", initialSpawnTime, spawnTime);
    }

    private void SpawnWhite()
    {
        Instantiate(whitePrefab, spawnLocations[rng.Next(spawnLocations.Count)]);
    }
}
