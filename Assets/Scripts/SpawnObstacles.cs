using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour
{
    [SerializeField]private SeedScriptableObject seedScrObj;
    [SerializeField]private float minTime = 0.1f;
    [SerializeField]private float maxTime = 5.0f;
    [SerializeField]private List<Spawner> spawnerList = new List<Spawner>();

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    private void Start()
    {
        // random seed (5 for testing)
        //Random.InitState(5);
        Random.InitState(seedScrObj.seed);

        // Run ObstacleSpawner() function for each spawner in the list
        foreach (Spawner spawner in spawnerList) {
            StartCoroutine(ObstacleSpawner(spawner.getObstaclePrefab(), spawner.getTransform()));
        }
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    // Spawns object at the transform randomly
    IEnumerator ObstacleSpawner(GameObject prefab, Transform spawner)
    {
        while (true) {
            // wait random amount of time, then spawn obstacle
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));
            Instantiate(prefab, spawner);
        }
    }
}
