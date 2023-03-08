using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField]private sbyte direction = 1;
    [SerializeField]private float speed = 30.0f;
    //[SerializeField]private float outOfBounds = 11.5f;
    private Rigidbody rb;

    // Awake is called when the script instance is being loaded
    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        
        rb.velocity = new Vector3(speed*direction, 0, 0);
        
        //rb.AddForce(-speed, 0, 0, ForceMode.VelocityChange);
    }

    // Update is called once per frame
    void Update()
    {
        // // if gameObject's position is out of bounds, then destroy gameObject
        // if(transform.position.x < -outOfBounds || transform.position.x > outOfBounds){
        //     Destroy(this.gameObject);
        //     Debug.Log("Obsticle destroyed");
        // }
    }

    private void OnTriggerExit(Collider collider)
    {
        Debug.Log("test");
        // If player collider collides with an Obstacle, then restart level
        if (collider.CompareTag("ObstacleBounds")) {    
            Debug.Log(collider.name);
            Destroy(this.gameObject);
            Debug.Log("Obsticle destroyed");
        }
    }
}
