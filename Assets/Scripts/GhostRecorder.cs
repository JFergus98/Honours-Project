using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostRecorder : MonoBehaviour
{
    [SerializeField]
    private GhostScriptableObject ghost;
    private int index;
    private float timer;
    private float time;

    private int maxRecordings = 11;

    // Start is called before the first frame update
    void Awake()
    {
        if (ghost.isRecord) {
            
            ghost.ghostDataList.Insert(0, new GhostDataList());
            if (ghost.ghostDataList.Count > maxRecordings) {
                ghost.ghostDataList.RemoveRange(maxRecordings, ghost.ghostDataList.Count-maxRecordings);
            }

            timer = 0;
            time = 0;
        }
    }

    void LateUpdate()
    {
        // Start timer and increment time
        timer += Time.deltaTime;
        time += Time.deltaTime;

        // If recording and it is time to record
        if (ghost.isRecord && timer >= 1/ghost.recordFrequency) {

            // Get current time, position and rotation data
            GhostData data = new GhostData(time, this.transform.position, this.transform.rotation);
            
            // Add data to the list
            ghost.ghostDataList[0].ghostData.Add(data);

            //Debug.Log("Recorded: " + data.position);
            // Reset timer
            timer = 0;
        }
    }
}
