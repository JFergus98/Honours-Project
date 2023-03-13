using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPlayer : MonoBehaviour
{
    [SerializeField]
    private GhostScriptableObject ghost;
    private float time;
    private float offset = 0.0f;
    private List<int> index;
    private List<int> nextindex;

    private List<GameObject> ghostObjects;
    
    [SerializeField]
    private GameObject ghostPrefab;

    // Start is called before the first frame update
    void Start()
    {
        ghostObjects = new List<GameObject>();

        time = offset;
        index = new List<int>();
        nextindex = new List<int>();

        Debug.Log(ghost.ghostDataList.Count);

        if (ghost.ghostDataList.Count > 1)
        {
            for (int i = 1; i < ghost.ghostDataList.Count; i++)
            {
                ghostObjects.Insert(0, Instantiate(ghostPrefab, this.transform));

                index.Insert(0, 0);
                nextindex.Insert(0, 0);

                Debug.Log("created ghost");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (ghost.isReplay) {

            for (int i = 1; i < ghost.ghostDataList.Count; i++) {

                if (nextindex[i-1] < ghost.ghostDataList[i].ghostData.Count) {
                    
                    
                    if (time > ghost.ghostDataList[i].ghostData[index[i-1]].timeStamp && time < ghost.ghostDataList[i].ghostData[index[i-1]+1].timeStamp) {
                        
                        float interpolationFactor = (time - ghost.ghostDataList[i].ghostData[index[i-1]].timeStamp)/(ghost.ghostDataList[i].ghostData[index[i-1]+1].timeStamp - ghost.ghostDataList[i].ghostData[index[i-1]].timeStamp);
                        
                        ghostObjects[i-1].transform.position = Vector3.Lerp(ghost.ghostDataList[i].ghostData[index[i-1]].position, ghost.ghostDataList[i].ghostData[index[i-1]+1].position, interpolationFactor);
                        ghostObjects[i-1].transform.rotation = Quaternion.Lerp(ghost.ghostDataList[i].ghostData[index[i-1]].rotation, ghost.ghostDataList[i].ghostData[index[i-1]+1].rotation, interpolationFactor);

                        Debug.Log("if 2 - time :");
                    }

                    else if (time == ghost.ghostDataList[i].ghostData[nextindex[i-1]].timeStamp) {
                        ghostObjects[i-1].transform.position = ghost.ghostDataList[i].ghostData[index[i-1]].position;
                        ghostObjects[i-1].transform.rotation = ghost.ghostDataList[i].ghostData[index[i-1]].rotation;

                        nextindex[i-1]++;
                        index[i-1] = nextindex[i-1] - 1;

                        Debug.Log("if 1 - time :");
                    }

                    if (time > ghost.ghostDataList[i].ghostData[nextindex[i-1]].timeStamp) {
                        nextindex[i-1]++;
                        index[i-1] = nextindex[i-1] - 1;

                        Debug.Log("if 0 - time :");
                    }
                }
                else if (ghostObjects[i-1].activeSelf)
                {
                    ghostObjects[i-1].SetActive(false);
                }
            }
        }
    }
}
