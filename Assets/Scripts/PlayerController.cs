using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 wallNormal;
    //sphercast
    // public float sphereRadius = 0.3f;
    // public float sphereCastDistance = 1.6f;

    // //boxcast
    // public Vector3 boxSize;
    // public Vector3 gizmoBoxSize;
    // public float maxDistance;

    // [SerializeField]private BoxCollider boxCollider;

    [SerializeField]private LayerMask groundLayerMask;
    private InputManager inputManager;
    private Rigidbody playerRb;
    private Transform groundCheck;
    private Transform orientation;
    Vector2 moveInput;
    [SerializeField]private float speed = 50f;
    [SerializeField]private float maxSpeed = 7f;
    private const float frictionForce = 0.2f;
    private const float groundCheckRadius = 0.499f;

    [SerializeField]private float jumpForce  = 5.56f;
    private bool isJumping;

    private const float coyoteTime = 0.2f;
    private float coyoteTimer;
    private const float jumpBuffer = 0.1f;
    private float jumpBufferTimer;
    private const float jumpDelay = 0.2f;
    private float jumpDelayTimer = 0;

    private RaycastHit hitInfo;

    private float maxSlopeAngle = 40;

    // Start is called before the first frame update
    void Start()
    {
        inputManager = InputManager.Instance;
        playerRb = GetComponent<Rigidbody>();
        groundCheck = this.gameObject.transform.GetChild(1);
        orientation = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Get player movement input;
        moveInput = inputManager.GetPlayerMovement();

        // If player is grounded, then reset coyete timer, else let coyete timer count down
        if (IsGrounded()) {
            coyoteTimer = coyoteTime;
        }else{
            coyoteTimer -= Time.deltaTime;
        }

        // If jump button is pressed, then reset jump buffer timer, else let jump buffer timer count down
        if (inputManager.PlayerJumped()) {
            jumpBufferTimer = jumpBuffer;
        }else{
            jumpBufferTimer -= Time.deltaTime;
        }

        // is jumping is true and jump delay timer is less than or equal to 0, then set is jumping to false, else let jump delay timer count down
        if (isJumping && jumpDelayTimer <= 0) {
			isJumping = false;
        }else{
            jumpDelayTimer -= Time.deltaTime;
        }

        // Debug.Log(playerRb.velocity.y); // Testing
        // Debug.Log("isJumping : " + isJumping); // Testing
        // if (playerRb.velocity.y < 0) Debug.Log("playerRb.velocity.y <= 0"); // Testing
        // Debug.Log(groundCheck.position); // Testing
        
        // Debug.Log("grounded: " + IsGrounded()); // Testing
        // Debug.Log(playerRb.velocity); // Testing
        // Debug.Log("OnSlope: " + OnSlope()); // Testing
        Debug.Log("IsTouchingWall: " + IsTouchingWall()); // Testing
    }

    // FixedUpdate is called once per 0.02 seconds
    private void FixedUpdate()
    {
        // Convert player move input from a Vector2 to Vector3
        Vector3 movement = new Vector3(moveInput.x, 0f, moveInput.y);

        // Combine the player movement input with the direction of the player camera
        movement = orientation.forward * movement.z + orientation.right * movement.x;
        movement.y = 0f;
        
        // If the player is on a slope, then get slope movement
        if (OnSlope()) {
            movement = GetSlopeMovement(movement);
        }
        
        movement = movement.normalized;

        if (IsTouchingWall()) {
            movement = GetWallMovement(movement);
        }


        // Move the player
        playerRb.AddForce(movement * speed, ForceMode.Force);

        // If player is grounded and there is no move input, then apply a friction force
        if (OnSlope() && Mathf.Abs(moveInput.magnitude) < 0.01f) {
            playerRb.AddForce(new Vector3(playerRb.velocity.x, playerRb.velocity.y, playerRb.velocity.z) * -frictionForce, ForceMode.Impulse);
        }else if (IsGrounded() && Mathf.Abs(moveInput.magnitude) < 0.01f) {
            playerRb.AddForce(new Vector3(playerRb.velocity.x, 0f, playerRb.velocity.z) * -frictionForce, ForceMode.Impulse);
        }

        // calculate players velocity exluding the y component
        Vector3 groundVelocity = new Vector3(playerRb.velocity.x, 0f, playerRb.velocity.z);
        
        // If players grounded velocity is greater than max speed, then limit ther velocity
        if  (groundVelocity.magnitude > maxSpeed) {
            Vector3 maxVelocity = groundVelocity.normalized * maxSpeed;
            playerRb.velocity = new Vector3(maxVelocity.x, playerRb.velocity.y, maxVelocity.z);
        }

        // If the player is not currently jumping and the coyote and jump buffer timers are positive, then the player jumps
        if (!isJumping && coyoteTimer > 0 && jumpBufferTimer > 0) {
            // Set isJumping to true
            isJumping = true;
            
            playerRb.velocity = new Vector3(playerRb.velocity.x, 0f, playerRb.velocity.z);
            // Apply upward force
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            
            // Reset timers
            jumpDelayTimer = jumpDelay;
            jumpBufferTimer = 0;
            coyoteTimer = 0;
        }

        // If player is on a slope and is grounded, then disable gravity and apply a force on the player towrds the slope
        if (OnSlope()) {
            playerRb.AddForce(-hitInfo.normal * 50f, ForceMode.Force);
            playerRb.useGravity = false;
        }else{
            playerRb.useGravity = true;
        }
    }

    // Returns true if the player object is in contact with the ground
    private bool IsGrounded()
    {
        // If player is jumping, then return false
        if (isJumping) {
            return false;
        }
        // If player is not in contact with the ground, then return true
        return Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayerMask);
        
        /**
        //bool grounded;

        //raycast
        //grounded = Physics.Raycast(transform.position, Vector3.down, 1.1f, groundLayerMask);

        //spherecast
        //grounded = Physics.SphereCast(transform.position, sphereRadius, Vector3.down, out RaycastHit hit, sphereCastDistance, groundLayerMask);

        

        // if (Physics.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, Vector3.down, groundCheck.rotation, 1f, groundLayerMask)) {
        //     grounded = true;
        // }else{
        //     grounded = false;
        // }

        // //boxcast
        // if (Physics.BoxCast(transform.position, boxSize, Vector3.down, transform.rotation, maxDistance, groundLayerMask)) {
        //     grounded = true;
        // }else{
        //     grounded = false;
        // }

        //Debug.Log("grounded: " + grounded); //testing

        //return grounded;
        */
    }

    // Returns true if the player object is in contact with a slope
    private bool OnSlope()
    {   
        // If player is jumping, then return false
        if (!IsGrounded()) {
            return false;
        }
        // if (!Physics.Raycast(transform.position, Vector3.down, 1.1f, groundLayerMask)) {
        //     return false; 
        // }
        // If player is not in contact with the ground, then return false
        // if (!Physics.SphereCast(transform.position, groundCheckRadius, Vector3.down, out hitInfo, 1f, groundLayerMask)) {
        //     return false;
        // }
        if (!Physics.BoxCast(transform.position, new Vector3(groundCheckRadius, groundCheckRadius, groundCheckRadius), Vector3.down, out hitInfo, Quaternion.identity, 0.5f, groundLayerMask)) {
            return false;
        }
        // If the ground is a not a slope, then return false
        if(hitInfo.normal == Vector3.up) {
            return false;
        }
        // Else the player is on a slope, so return true
        return true; 
    }

    // Returns movement parallel to the angle of slope
    private Vector3 GetSlopeMovement(Vector3 movement)
    {
        return Vector3.ProjectOnPlane(movement, hitInfo.normal);
    }

    // Returns true if the player object is in contact with a wall while airborn
    private bool IsTouchingWall()
    {   
        if (Physics.CheckBox(transform.position, Vector3.one * 0.6f, Quaternion.identity, groundLayerMask) && Physics.CheckCapsule(transform.position + Vector3.up * 0.5f, transform.position + Vector3.down * 0.5f, 0.6f, groundLayerMask))
        {
            return true;
        }
        return false;
    }

    private Vector3 GetWallMovement(Vector3 movement)
    {
        if (movement.x < 0 && wallNormal.normalized.x > 0) {
            movement.x = 0;
        }
        else if (movement.x > 0 && wallNormal.normalized.x < 0) {
            movement.x = 0;
        }
        if (movement.z < 0 && wallNormal.normalized.z > 0) {
            movement.z = 0;
        }
        else if (movement.z > 0 && wallNormal.normalized.z < 0) {
            movement.z = 0;
        }
        return movement;
    }

    void OnCollisionStay(Collision collision) {
        ContactPoint contact = collision.contacts[0];

        // Visualize the contact point
        Debug.DrawRay(contact.point, contact.normal, Color.red);
        
        if (new Vector2(contact.normal.x, contact.normal.z).magnitude == 1) { 
            // if (contact.normal.x >= contact.normal.z) {
            //     wallNormal = new Vector3(contact.normal.x, 0 ,0);
            // }
            // else {
            //     wallNormal = new Vector3(0, 0 ,contact.normal.z);
            // }
            
            wallNormal = new Vector3(contact.normal.x, 0 ,contact.normal.z);
        }
    }
}