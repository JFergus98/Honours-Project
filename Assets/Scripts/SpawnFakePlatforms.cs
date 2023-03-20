using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFakePlatforms : MonoBehaviour
{
    [SerializeField]private SeedScriptableObject seedScrObj;

    [SerializeField]private int NoOfRows;
    [SerializeField]private List<PlatformRow> platformList = new List<PlatformRow>();

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

        // int Col = platformList.Count / NoOfRows;

        // for (int i = 0; i < platformList.Count; i += Col) {
        //     int index = Random.Range(i, i + Col);
        //     platformList[index].GetComponent<Collider>().enabled = true;
        //     Debug.Log(index);
        // }

        foreach (PlatformRow row in platformList) {
            int index = Random.Range(0, row.count());
                row.getElementAt(index).GetComponent<Collider>().enabled = true;
                Debug.Log("fake: " + index);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
