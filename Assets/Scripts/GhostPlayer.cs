using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPlayer : MonoBehaviour
{
    [SerializeField]
    private GhostScriptableObject ghostScrObj;
    [SerializeField]
    private GameObject ghostPrefab;

    private List<GameObject> ghostObjects;
    private List<int> indexs;
    private float time;
    private const float offset = 0.0f;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Initialise lists
        ghostObjects = new List<GameObject>();
        indexs = new List<int>();

        Debug.Log("ghostScrObj.ghostDataList.Count " + ghostScrObj.ghosts.Count);

        // If there are more than one ghosts in the ghost scriptable object
        if (ghostScrObj.ghosts.Count > 1)
        {
            // For each ghost excluding the new one
            for (int i = 1; i < ghostScrObj.ghosts.Count; i++)
            {
                // Instatiate ghost object
                ghostObjects.Insert(0, Instantiate(ghostPrefab, this.transform));

                // added new idex to idex list
                indexs.Insert(0, 0);

                Debug.Log("Created new ghost object");
            }
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        // Initialise time
        time = offset;
    }

    // Update is called once per frame
    private void Update()
    {
        // Increment time
        time += Time.deltaTime;

        // Update Ghost Objects' position and rotation
        UpdateGhostObjects();     
    }

    // Update the ghost objects' position and rotation, based on the current time
    private void UpdateGhostObjects()
    {
        // For each ghost excluding the new one
        for (int i = 1; i < ghostScrObj.ghosts.Count; i++)
        {
            // If current index is less that current Ghost's list of data's count
            if (indexs[i-1]+1 < ghostScrObj.ghosts[i].ghostData.Count)
            {
                // Initialise varables for the currnet and next ghost data
                GhostData currGhostData = ghostScrObj.ghosts[i].ghostData[indexs[i-1]];
                GhostData nextGhostData = ghostScrObj.ghosts[i].ghostData[indexs[i-1]+1];
                
                // If time is greater than current ghost data's timestamp and less than next ghost data's timestamp
                if (time > currGhostData.timeStamp && time < nextGhostData.timeStamp)
                {
                    // Set interpolation factor
                    float interpolationFactor = (time - currGhostData.timeStamp)/(nextGhostData.timeStamp - currGhostData.timeStamp);
                    
                    // Set ghost objects position and rotation to a linear interopolation between current and next ghost data postion and rotation based on interpolation factor
                    ghostObjects[i-1].transform.position = Vector3.Lerp(currGhostData.position, nextGhostData.position, interpolationFactor);
                    ghostObjects[i-1].transform.rotation = Quaternion.Lerp(currGhostData.rotation, nextGhostData.rotation, interpolationFactor);
                }

                // Else if time equals ghost data's timestamp
                else if (time == nextGhostData.timeStamp)
                {
                    // Set ghost objects position and rotation to current ghost data postion and rotation
                    ghostObjects[i-1].transform.position = currGhostData.position;
                    ghostObjects[i-1].transform.rotation = currGhostData.rotation;
                    
                    // Increment current index
                    indexs[i-1]++;
                }

                // If time is greater than next ghost data's timestamp, then increment current index
                if (time > nextGhostData.timeStamp)
                {
                    indexs[i-1]++;
                }
            }
            // Else if no more data for ghost and ghost still active, then deactive ghost object
            else if (ghostObjects[i-1].activeSelf)
            {
                ghostObjects[i-1].SetActive(false);
            }
        }
    }
}
