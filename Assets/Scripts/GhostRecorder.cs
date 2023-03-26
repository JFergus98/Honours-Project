using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostRecorder : MonoBehaviour
{
    [SerializeField]
    private GhostScriptableObject ghostScrObj;

    private bool record;
    private int index;
    private float timer;
    private float time;
    private int maxRecordings = 11;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // If GameManager exists, then start recording
        if (GameObject.Find("GhostManager")) {
            record = true;
            Debug.Log("Recording has started");
        }else{
            record = false;
        }
        
        // If recording
        if (record) {
            // Add new ghost to the start of the list
            ghostScrObj.ghosts.Insert(0, new Ghost());

            // If List size is greater than max recordings, then remove excess items at end of list
            if (ghostScrObj.ghosts.Count > maxRecordings) {
                ghostScrObj.ghosts.RemoveRange(maxRecordings, ghostScrObj.ghosts.Count-maxRecordings);
            }
        }else{
            // Delete recordings
            ghostScrObj.ghosts.Clear();
        }

        // If List size is greater than max recordings, then remove excess items at end of list
        if (ghostScrObj.ghosts.Count > maxRecordings) {
            ghostScrObj.ghosts.RemoveRange(maxRecordings, ghostScrObj.ghosts.Count-maxRecordings);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        // Set time and timer to 0
        timer = 0;
        time = 0;
    }

    // LateUpdate is called after all Update functions have been called
    private void LateUpdate()
    {
        // Start timer and increment time
        timer += Time.deltaTime;
        time += Time.deltaTime;

        // If recording and it is time to record
        if (record && timer >= 1/ghostScrObj.recordFrequency) {

            // Get current time, position and rotation data
            GhostData data = new GhostData(time, this.transform.position, this.transform.rotation);
            
            // Add data to the new first ghost in the list
            ghostScrObj.ghosts[0].ghostData.Add(data);

            //Debug.Log("Recorded: " + data); // Testing

            // Reset timer
            timer = 0;
        }
    }
}
