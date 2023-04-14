using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollison : MonoBehaviour
{
    private PauseMenu pauseMenu;

    private void Start()
    {
        pauseMenu = GameObject.Find("PauseMenuCanvas").GetComponent<PauseMenu>();

        // Debug.Log("pauseMenu " + pauseMenu);  // Testing
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If player collider collides with an Obstacle, then restart level
        if (collision.collider.CompareTag("Obstacle"))
        {    
            // Debug.Log(collision.collider.name);  // Testing

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        // If player collider enters the Finish Bounds, then end Level
        if (collider.CompareTag("FinishBounds"))
        {    
            // Debug.Log(collider.name); // Testing

            pauseMenu.LevelCompleted();
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        // If player collider exits the Player Bounds, then restart level
        if (collider.CompareTag("PlayerBounds"))
        {    
            // Debug.Log(collider.name); // Testing

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
        }
    }
}
