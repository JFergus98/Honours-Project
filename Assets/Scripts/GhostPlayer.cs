using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPlayer : MonoBehaviour
{
    [SerializeField] private GhostScriptableObject ghostScrObj;
    [SerializeField] private GameObject ghostPrefab;

    private List<GameObject> ghostObjects;
    private List<int> indexs;
    private float time;
    private float offset = 0.0f;

    // Start is called before the first frame update
    private void Start()
    {
        ghostObjects = new List<GameObject>();
        indexs = new List<int>();

        time = offset;

        Debug.Log("ghostScrObj.ghostDataList.Count" + ghostScrObj.ghosts.Count);

        if (ghostScrObj.ghosts.Count > 1)
        {
            for (int i = 1; i < ghostScrObj.ghosts.Count; i++)
            {
                ghostObjects.Insert(0, Instantiate(ghostPrefab, this.transform));

                indexs.Insert(0, 0);

                Debug.Log("Created new ghost object");
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        // Increment time
        time += Time.deltaTime;

        if (ghostScrObj.isReplay) {

            UpdateGhostObjects();
        }
    }

    // Update the ghost objects based on the current time
    private void UpdateGhostObjects()
    {
        for (int i = 1; i < ghostScrObj.ghosts.Count; i++) {

            //int index = indexs[i-1];
            
            if (indexs[i-1]+1 < ghostScrObj.ghosts[i].ghostData.Count) {

                GhostData currGhostData = ghostScrObj.ghosts[i].ghostData[indexs[i-1]];
                GhostData nextGhostData = ghostScrObj.ghosts[i].ghostData[indexs[i-1]+1];
                    
                if (time > currGhostData.timeStamp && time < nextGhostData.timeStamp) {
                    
                    float interpolationFactor = (time - currGhostData.timeStamp)/(nextGhostData.timeStamp - currGhostData.timeStamp);
                    
                    ghostObjects[i-1].transform.position = Vector3.Lerp(currGhostData.position, nextGhostData.position, interpolationFactor);
                    ghostObjects[i-1].transform.rotation = Quaternion.Lerp(currGhostData.rotation, nextGhostData.rotation, interpolationFactor);
                }

                else if (time == nextGhostData.timeStamp) {
                    ghostObjects[i-1].transform.position = currGhostData.position;
                    ghostObjects[i-1].transform.rotation = currGhostData.rotation;

                    indexs[i-1]++;
                }

                if (time > nextGhostData.timeStamp) {
                    indexs[i-1]++;
                }
            }
            else if (ghostObjects[i-1].activeSelf)
            {
                ghostObjects[i-1].SetActive(false);
            }
        }
    }
}
