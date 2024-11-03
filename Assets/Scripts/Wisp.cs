using UnityEngine;

public class Wisp : MonoBehaviour
{
    [SerializeField]
    private GameObject wispPool;

    private WispSpawner wispPoolScript;

    [SerializeField]
    private bool DEBUG_FLAG = true;
    //private bool isVisible = false;
    private float speed = 1.0f;

    private Rigidbody rb;
    [SerializeField]
    private GameObject playerObj;
    [SerializeField]
    private float SOFTCAP_VELOCITY = 10.0f;
    [SerializeField]
    private float HARDCAP_VELOCITY = 100.0f;
    [SerializeField]
    private float AVOID_FORCE = 10.0f;
    private Vector3 dodgeDirection = new Vector3 (-1, 0, 0);

    private bool isAvoidingNearbyCat = false;



    // Start is called before the first frame update
    void Start()
    {
        this.rb = GetComponent<Rigidbody>();
        wispPoolScript = wispPool.GetComponent<WispSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAvoidingNearbyCat)
        {
            avoidBehavior(playerObj.transform.position);
        } else
        {
            randomMovementForce();
        }
        applySoftAndHardVelocityCaps(); // truncates to hard. lerps to soft.

    }

    public void startAvoidingCat()
    {
        isAvoidingNearbyCat = true;
    }

    public void stopAvoidingCat()
    {
        isAvoidingNearbyCat = false;
    }

    // Should only be called once per spawn either in Start() or in WispSpawner script when spawning new wisp
    // out of the object pool.
    // Determines a fixed dodge vector
    public void reinitializeDodgeVector()
    {
        dodgeDirection = new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f));
        //normalize the dodge direction
        dodgeDirection.Normalize();
    }

    // Calculate relative dodge direction vector
    public Vector3 calculateRelativeDodgeVector(Vector3 catToWisp)
    {
        // Normalize catToWisp to define the primary dodge direction
        catToWisp.Normalize();

        // Choose an arbitrary "up" vector to define the dodge direction
        Vector3 up = Vector3.up;

        // Calculate a perpendicular dodge direction
        Vector3 dodgePerpendicular = Vector3.Cross(catToWisp, up);

        // If dodgePerpendicular is very small (catToWisp was nearly aligned with up),
        // use another direction (e.g., Vector3.right) to avoid zero vectors
        if (dodgePerpendicular.sqrMagnitude < 0.001f)
        {
            dodgePerpendicular = Vector3.Cross(catToWisp, Vector3.right);
        }

        dodgePerpendicular.Normalize(); // Ensure consistent magnitude
        if (DEBUG_FLAG) Debug.Log("Consistent Dodge Perpendicular Vector: " + dodgePerpendicular);

        return dodgePerpendicular;
    }
    public void makeVisible()
    {
        //isVisible = true;
        // enable this object's mesh renderer component
        this.gameObject.GetComponent<MeshRenderer>().enabled = true;
    }

    public void makeInvisible()
    {
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    public float calculateAvoidForce (float distance)
    {
        if (DEBUG_FLAG) Debug.Log("distance is " + distance + " force is " + AVOID_FORCE / distance);
        return AVOID_FORCE / distance;
    }
    public void avoidBehavior(Vector3 catPosition)
    {
        // set a dodge direction such that we're moving in the dodgeDirection vector direction relative to the angle
        // between the cat and the wisp
        // get the vector from cat to wisp
        Vector3 catToWisp = transform.position - catPosition;
        float avoidForce = calculateAvoidForce(catToWisp.magnitude);
        // calculate the relative dodge vector
        Vector3 relativeDodgeVector = calculateRelativeDodgeVector(catToWisp);
        // apply dodge vector
        rb.AddForce(relativeDodgeVector * speed * avoidForce);
    }
    public void caught() // call on cat collision to main object.
    {
        if (DEBUG_FLAG) Debug.Log("caught!");
        wispPoolScript.ReturnWisp(this.gameObject);
    }

    void randomMovementForce()
    {
        // if velocity magnitude is greater than soft cap * 1.1 then return
        if (rb.velocity.magnitude > SOFTCAP_VELOCITY * 1.1f)
        {
            return;
        }
        // else apply a random small random direction force
        rb.AddForce(new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f)) * speed);
    }

    void applySoftAndHardVelocityCaps()
    {
        // if rigidbody velocity is over the hardcap. set it to hard cap
        if (rb.velocity.magnitude > HARDCAP_VELOCITY)
        {
            rb.velocity = rb.velocity.normalized * HARDCAP_VELOCITY;
        }
        // if rigidbody velocity is over the soft cap, reduce it by a factor
        if (rb.velocity.magnitude > SOFTCAP_VELOCITY)
        {
            //rb.velocity = rb.velocity.normalized * softCapVelocity;
            // slowly reduce velocity with a curve
            rb.velocity = rb.velocity.normalized * Mathf.Lerp(rb.velocity.magnitude, SOFTCAP_VELOCITY, Time.deltaTime);
        }
    }
    // on collision with player, call caught()  
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.name == "Player")
        {
            caught();
        }
    }
}
