using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstaclesOld : MonoBehaviour
{
    public GameObject obstaclePrefab;
    private float posX;
    private float posY;
    private float posZ;
    private Vector3[] positions = new Vector3[10];

    int count;

    // Awake is called when the script instance is being loaded
    void Awake()
    {

    }

    //Start is called before the first frame update
    void Start()
    {
        // get position of obstacle
        posX = obstaclePrefab.transform.position.x + 10;
        posY = obstaclePrefab.transform.position.y + 0.5f;
        posZ = obstaclePrefab.transform.position.z + 10;


        positions[0] = new Vector3(posX, posY, posZ);
        positions[1] = new Vector3(posX, posY, posZ+5);
        positions[2] = new Vector3(posX, posY, posZ+10);
        positions[3] = new Vector3(posX, posY, posZ+15);
        positions[4] = new Vector3(posX, posY, posZ+20);
        positions[5] = new Vector3(posX, posY, posZ+25);
        positions[6] = new Vector3(posX, posY, posZ+30);
        positions[7] = new Vector3(posX, posY, posZ+35);
        positions[8] = new Vector3(posX, posY, posZ+40);
        positions[9] = new Vector3(posX, posY, posZ+45);

        Random.InitState(5);

        for (int i = 0; i < 10; i++) 
        {
            StartCoroutine(Spawner(obstaclePrefab, positions[i]));
            //Debug.Log(i);
        }
        Debug.Log("end of Start()");
    }

    // Update is called once per frame
    void Update()
    {
            
    }

    IEnumerator Spawner(GameObject prefab, Vector3 pos)
    {
        //Debug.Log((40+pos.z)/5);

        // float x;
        // float y = 0;

        while (true)
        {
            // x = Random.Range(0.1f, 5.0f);

            // y=y + x;

            // yield return new WaitForSeconds(x);
            // Instantiate(prefab, pos, Quaternion.identity);

            // Debug.Log(y + " " + ((40+pos.z)/5));

            // wait random amount of time, then spawn obstacle
            yield return new WaitForSeconds(Random.Range(0.1f, 5.0f));
            Instantiate(prefab, pos, Quaternion.identity);

            // Debug.Log(y + " " + ((40+pos.z)/5));

            // yield return new WaitForSeconds(1.0f);
            // Debug.Log(Random.value);

            // Debug.Log(Random.Range(0.1f, 1.0f));
        }
    }

    private void spawnObstacle(GameObject prefab, Vector3 pos)
    {
        Instantiate(prefab, pos, Quaternion.identity);
    }
}
