using System.Collections;
using UnityEngine;

public class Wisp : MonoBehaviour
{
    private GameObject wispSpawner;
    private GameObject playerObj;

    private WispSpawner wispPoolScript;

    [SerializeField]
    private bool DEBUG_FLAG = true;
    //private bool isVisible = false;
    [SerializeField] private Mesh bunny;
    private float speed = 0.3f;

    private Rigidbody rb;
    private MeshFilter meshFilter;

    private const float SOFTCAP_VELOCITY = 2.0f;
    private const float HARDCAP_VELOCITY = 20.0f;

    private const float AVOIDANCE_FORCE_CAP = 5.0f;
    private const float AVOIDANCE_FORCE_SCALAR = 1.0f;

    private const float COHESION_FORCE_SCALAR = 0.0f;

    private const float ALIGNMENT_FORCE_SCALAR = 1.0f;

    private float time_elapsed_since_last_vector = 0.0f;
    private float vector_duration = 0.0f;
    private float MAX_VECTOR_DURATION = 3.0f;
    private Vector3 currentVector;



    // Start is called before the first frame update
    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player")!;
        wispSpawner = GameObject.FindGameObjectWithTag("WispSpawner")!;
        wispPoolScript = wispSpawner.GetComponent<WispSpawner>()!;

        this.rb = GetComponent<Rigidbody>();
        this.meshFilter = GetComponent<MeshFilter>();
        vector_duration = Random.Range(0.5f, MAX_VECTOR_DURATION);
        currentVector = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        currentVector.Normalize();

        //reinitializeDodgeVector();
    }

    float getCappedForceBasedOnDistance (Vector3 nearbyObjPosition)
    {
        Vector3 distance = nearbyObjPosition - this.transform.position;
        float distanceMagnitude = distance.magnitude;
        float force = AVOIDANCE_FORCE_SCALAR / distanceMagnitude;
        if (force > AVOIDANCE_FORCE_CAP)
        {
            force = AVOIDANCE_FORCE_CAP;
        }
        return force;
    }

    public void applyForceAwayFromNearbyObj(Vector3 nearbyObjPosition)
    {
        // get the direction vector from this object to the nearby object
        Vector3 direction = nearbyObjPosition - this.transform.position;
        // apply a force in the opposite direction
        rb.AddForce(-direction.normalized * getCappedForceBasedOnDistance(nearbyObjPosition));
    }

    // Update is called once per frame
    void Update()
    {
        updateVector(); // update the random direction vector every random duration 0.5s to 3.0s to feel more random.

        randomMovementForce();
        applySoftAndHardVelocityCaps(); // truncates to hard. lerps to soft.

    }

    void updateVector()
    {
        time_elapsed_since_last_vector += Time.deltaTime;
        if (time_elapsed_since_last_vector > vector_duration)
        {
            time_elapsed_since_last_vector = 0.0f;
            vector_duration = Random.Range(0.5f, MAX_VECTOR_DURATION);
            currentVector = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
            currentVector.Normalize();
        }
    }
    public void makeVisible()
    {
        //isVisible = true;
        // enable this object's mesh renderer component
        this.gameObject.GetComponent<MeshRenderer>().enabled = true;
        // set layer to default
        this.gameObject.layer = 0;
    }

    public void makeInvisible()
    {
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        // set layer to InvisWisp
        this.gameObject.layer = 8;
    }
    public void caught() // call on cat collision to main object.
    {
        if (DEBUG_FLAG) Debug.Log("caught!");
        setColorToRed();
        returnWispAfterDelay(3.0f);
    }

    public void setColorToRed()
    {
        this.meshFilter.mesh = bunny;
        this.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
        this.rb.useGravity = true;
        Debug.Log(this.meshFilter.mesh);
    }

    public void resetColor()
    {
        this.meshFilter.mesh = Resources.GetBuiltinResource<Mesh>("Sphere.fbx");
        this.gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
        this.rb.useGravity = false;
        Debug.Log(this.meshFilter.mesh);
    }

    // create a coroutine to return the wisp after a delay
    public void returnWispAfterDelay(float delay)
    {
        StartCoroutine(returnWispAfterDelayCoroutine(delay));
    }
    public IEnumerator returnWispAfterDelayCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        resetColor();
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
        //rb.AddForce(new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f)) * speed);
        // apply force in direction of currentVector
        rb.AddForce(currentVector * speed);
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
