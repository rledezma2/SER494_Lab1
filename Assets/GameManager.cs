using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Player")]
    public Transform player;

    [Header("Prefabs")]
    public GameObject healthPackPrefab;
    public GameObject hazardPrefab;
    
    [Header("Settings")]
    public float minSpawnInterval = 5f;
    public float maxSpawnInterval = 10f;
    public int minItems = 5;
    public int maxItems = 15;
    public float minDistanceFromPlayer = 5f;
    
    [Header("Spawn Locations")]
    public Transform[] spawnPoints;
    
    
    
    // Internal tracking
    private List<GameObject> spawnedItems = new List<GameObject>();
    private float nextSpawnTime;
    
    void Start()
    {
        for (int i = 0; i < minItems; i++)
        {
            SpawnItem();
        }
        
        SetNextSpawnTime();
    }
    
    void Update()
    {
        spawnedItems.RemoveAll(item => item == null);
        
        if (spawnedItems.Count < minItems)
        {
            SpawnItem();
        }
        
        if (Time.time >= nextSpawnTime && spawnedItems.Count < maxItems)
        {
            SpawnItem();
            SetNextSpawnTime();
        }
    }
    
    void SpawnItem()
    {
        if (spawnedItems.Count >= maxItems)
            return;
            
        Transform spawnPoint = GetValidSpawnPoint();
        
        
        GameObject prefabToSpawn = Random.value > 0.5f ? healthPackPrefab : hazardPrefab;
        
       
        GameObject newItem = Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);
        spawnedItems.Add(newItem);
        
        if (newItem.GetComponent<DestroyOnPickup>() == null)
        {
            newItem.AddComponent<DestroyOnPickup>();
        }
    }
    
    Transform GetValidSpawnPoint()
    {
        List<Transform> validPoints = new List<Transform>();
        
        foreach (Transform point in spawnPoints)
        {
            float distanceToPlayer = Vector3.Distance(point.position, player.position);
            if (distanceToPlayer >= minDistanceFromPlayer)
            {
                validPoints.Add(point);
            }
        }
        
        if (validPoints.Count > 0)
        {
            return validPoints[Random.Range(0, validPoints.Count)];
        }
        
        return null;
    }
    
    void SetNextSpawnTime()
    {
        nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
    }
    
    
}

public class DestroyOnPickup : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}