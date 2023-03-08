using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollison : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // If player collider collides with an Obstacle, then restart level
        if (collision.collider.CompareTag("Obstacle"))
        {    
            Debug.Log(collision.collider.name);

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    // private void OnTriggerEnter(Collider collider)
    // {
    //     // If player collider collides with an Obstacle, then restart level
    //     if (collider.CompareTag("Obstacle"))
    //     {    
    //         Debug.Log(collider.name);

    //         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //     }
    // }

    private void OnTriggerExit(Collider collider)
    {
        // If player collider exits PlayerBounds, then restart level
        if (collider.CompareTag("PlayerBounds"))
        {    
            Debug.Log(collider.name);

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
