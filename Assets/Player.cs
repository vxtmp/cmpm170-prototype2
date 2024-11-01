using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private const bool DEBUG_FLAG = false;

    private bool isCharging = false;
    private float currentCharge = 0.0f;
    private float chargeSpeed = 1.0f;
    [SerializeField]
    private const float mouseSensitivity = 500f;
    [SerializeField]
    private const float MOVESPEED_MULTIPLIER = 3.0f;
    [SerializeField]
    private const float POUNCE_BASE_DISTANCE = 500.0f;
    [SerializeField]
    private const float MAX_CHARGE = 2.0f;

    [SerializeField]
    private GameObject pawsCanvasObject;

    public Transform playerBody; // Link this to the Player object if camera is attached to the player

    private float xRotation = 0f;
    private float yRotation = 0f;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        this.rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // if velocity < a certain value then disable the pawsCanvasObject
        if (rb.velocity.magnitude < 1.0f)
        {
            pawsCanvasObject.SetActive(false);
        }
        else
        {
            pawsCanvasObject.SetActive(true);
        }
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
            //transform.position += transform.forward * POUNCE_BASE_DISTANCE * currentCharge;
            // apply forces to rb
            rb.AddForce(transform.forward * POUNCE_BASE_DISTANCE * currentCharge * MOVESPEED_MULTIPLIER, ForceMode.Impulse);
            currentCharge = 0;
        }

    }

    // abstract each WASD movement direction into a function
    // call these functions in FixedUpdate
    // this is to ensure that the player moves at a consistent speed regardless of framerate
    void moveLeft() {
        // move left relative to the current facing of the camera
        catMove(transform.right * -1);
    }
    void moveBackward()
    {
        catMove(transform.forward * -1);
    }
    void moveRight()
    {
        catMove(transform.right);
    }
    void moveForward()
    {
        // move forward relative
        catMove(transform.forward);
    }

    void catMove(Vector3 direction)
    {
        // flatten one of the axis to prevent flying or moving into the ground
        direction.y = 0;
        //transform.position += direction * Time.deltaTime * MOVESPEED_MULTIPLIER;
        // set velocity in direction
        rb.velocity = direction * MOVESPEED_MULTIPLIER;
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
        // while spacebar held down
        if (Input.GetKey(KeyCode.Space) && isCharging == false)
        {
            isCharging = true;
            startCharging();
            if (DEBUG_FLAG)
                Debug.Log("space held down. charging");
        } else if (!Input.GetKey(KeyCode.Space) && isCharging == true)
        {
            pounceForward();
            isCharging = false;
            if (DEBUG_FLAG)
                Debug.Log("space released. pouncing forward");
        }
        rotatePlayer();
    }

    // need to move the camera with mouse
    // use Input.GetAxis("Mouse X") and Input.GetAxis("Mouse Y") to get mouse movement
    // rotate the player object based on mouse movement.
    void rotatePlayer()
    {
        //Get mouse movement
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        ////// Adjust vertical rotation and clamp it to prevent over-rotation
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        yRotation += mouseX;


        // Apply rotations:
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
