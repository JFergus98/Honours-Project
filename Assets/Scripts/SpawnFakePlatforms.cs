using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFakePlatforms : MonoBehaviour
{
    [SerializeField]
    private SeedScriptableObject seedScrObj;
    [SerializeField]
    private int NoOfRows;
    [SerializeField]
    private List<PlatformRow> platformList = new List<PlatformRow>();

    // Start is called before the first frame update
    private void Start()
    {
        // random seed (5 for testing)
        //Random.InitState(5);
        Random.InitState(seedScrObj.seed);

        foreach (PlatformRow row in platformList)
        {
            int index = Random.Range(0, row.count());
            row.getElementAt(index).GetComponent<Collider>().enabled = true;
            Debug.Log("fake: " + index);
        }
    }
}
