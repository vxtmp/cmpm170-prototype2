using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float currentCharge = 0.0f;
    private float chargeSpeed = 1.0f;
    public const float MAX_CHARGE = 2.0f;
    public const float POUNCE_BASE_DISTANCE = 5.0f;

    public float mouseSensitivity = 100f;
    public Transform playerBody; // Link this to the Player object if camera is attached to the player

    private float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void startCharging()
    {
        // play charge animation
        // start increasing charge
        currentCharge += chargeSpeed * Time.deltaTime;
        if (currentCharge > MAX_CHARGE)
        {
            currentCharge = MAX_CHARGE;
        }

    }
    void pounceForward()
    {
        // move forward by POUNCE_BASE_DISTANCE * currentCharge
        if (currentCharge > 0)
        {
            transform.position += transform.forward * POUNCE_BASE_DISTANCE * currentCharge;
            currentCharge = 0;
        }

    }

    // abstract each WASD movement direction into a function
    // call these functions in FixedUpdate
    // this is to ensure that the player moves at a consistent speed regardless of framerate
    void moveLeft() { 
    }
    void moveBackward()
    {

    }
    void moveRight()
    {

    }
    void moveForward()
    {

    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            moveForward();
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveBackward();
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveLeft();
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveRight();
        }
        // while mouse held down, charge up
        if (Input.GetMouseButton(0))
        {
            startCharging();
        }
        // when mouse released, pounce forward
        if (Input.GetMouseButtonUp(0))
        {
            pounceForward();
        }
    }

    // need to move the camera with mouse
    // use Input.GetAxis("Mouse X") and Input.GetAxis("Mouse Y") to get mouse movement
    // rotate the player object based on mouse movement.
    
    void rotatePlayer()
    {
        // Get mouse movement
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Adjust vertical rotation and clamp it to prevent over-rotation
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Apply rotations: rotate camera around X and player around Y
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
