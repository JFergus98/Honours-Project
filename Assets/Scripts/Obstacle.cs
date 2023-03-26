using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField]
    private sbyte direction = 1;
    [SerializeField]
    private float speed = 30.0f;
    //[SerializeField]private float outOfBounds = 11.5f;
    private Rigidbody rb;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Get the Obstacle's rigidbody component
        rb = this.GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        // Set the Obstacle Velocity
        rb.velocity = new Vector3(speed*direction, 0, 0);
        
        //rb.AddForce(-speed, 0, 0, ForceMode.VelocityChange);
    }

    // Update is called once per frame
    private void Update()
    {
        //rb.velocity = new Vector3(speed*direction, 0, 0);

        // // if gameObject's position is out of bounds, then destroy gameObject
        // if(transform.position.x < -outOfBounds || transform.position.x > outOfBounds){
        //     Destroy(this.gameObject);
        //     Debug.Log("Obsticle destroyed");
        // }
    }

    private void OnTriggerExit(Collider collider)
    {
        // If obstacle collider collides with ObstacleBounds, then destroy Obastacle
        if (collider.CompareTag("ObstacleBounds")) {    
            Destroy(this.gameObject);
            // Debug.Log(collider.name);
            // Debug.Log("Obsticle destroyed");
        }
    }
}
