using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private const bool DEBUG_FLAG = false;

    public bool isCharging = false;
    public bool walking = false;
    private float currentCharge = 0.0f;
    private float chargeSpeed = 1.0f;
    private const float mouseSensitivity = 500f;
    private const float MOVESPEED_MULTIPLIER = 3.0f;
    private const float POUNCE_BASE_DISTANCE = 200.0f;
    private const float MAX_CHARGE = 2.0f;
    private float MIDAIR_DIRECTION_INFLUENCE = 0.05f;

    [SerializeField]
    private GameObject pawsCanvasObject;

    public Transform playerBody; // Link this to the Player object if camera is attached to the player

    private float xRotation = 0f;
    private float yRotation = 0f;

    private Rigidbody rb;


    void displayPawsIfApplicable()
    {
        // if velocity < a certain value then disable the pawsCanvasObject
        float pawThreshold = transform.forward.magnitude * MOVESPEED_MULTIPLIER;
        if (rb.velocity.magnitude <= pawThreshold)
        {
            pawsCanvasObject.SetActive(false);
        }
        else
        {
            pawsCanvasObject.SetActive(true);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        this.rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        displayPawsIfApplicable();
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
        if (currentCharge > 0 && isGrounded())
        {
            //transform.position += transform.forward * POUNCE_BASE_DISTANCE * currentCharge;
            // apply forces to rb
            rb.AddForce(transform.forward * POUNCE_BASE_DISTANCE * currentCharge * MOVESPEED_MULTIPLIER, ForceMode.Impulse);
            currentCharge = 0;
        }

    }
    bool isGrounded()
    {
        // check if the player is grounded
        return Physics.Raycast(transform.position, Vector3.down, 0.30f);
    }

    void catMove(Vector3 direction)
    {
        // flatten one of the axis to prevent flying or moving into the ground
        direction.y = 0;
        //transform.position += direction * Time.deltaTime * MOVESPEED_MULTIPLIER;
        // set velocity in direction
        if (isGrounded())
        {
            rb.velocity = direction * MOVESPEED_MULTIPLIER;
        } else
        {
            rb.AddForce (direction * MOVESPEED_MULTIPLIER * MIDAIR_DIRECTION_INFLUENCE, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        float forwardSpeed = 0.0f;
        float rightSpeed = 0.0f;
        if (Input.GetKey(KeyCode.W))
        {
            forwardSpeed++;
        }
        if (Input.GetKey(KeyCode.A))
        {
            rightSpeed--;
        }
        if (Input.GetKey(KeyCode.S))
        {
            forwardSpeed--;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rightSpeed++;
        }
        if (forwardSpeed == 0.0f && rightSpeed == 0.0f)
        {
            walking = false;
        }
        else
        {
            Vector3 direction = (transform.forward * forwardSpeed + transform.right * rightSpeed);
            direction.y = 0;
            direction.Normalize();
            catMove(direction);
            walking = true;
        }
        if (Input.GetKey(KeyCode.Space) && isCharging == false)
        {
            isCharging = true;
            startCharging();
            if (DEBUG_FLAG)
                Debug.Log("space held down. charging");
        }
        else if (!Input.GetKey(KeyCode.Space) && isCharging == true)
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
