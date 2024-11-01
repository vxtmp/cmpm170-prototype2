using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wisp : MonoBehaviour
{
    [SerializeField]
    private bool DEBUG_FLAG = true;
    //private bool isVisible = false;
    private float speed = 1.0f;

    private Rigidbody rb;
    [SerializeField]
    private float softCapVelocity = 10.0f;
    [SerializeField]
    private float hardCapVelocity = 20.0f;
    


    // Start is called before the first frame update
    void Start()
    {
        this.rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        randomMovementForce();
        applySoftAndHardVelocityCaps();

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

    public void avoidBehavior() // call on nearby cat (collision with larger child trigger volume
    {
        // apply a high force vector in direction away from cat, with a slight random angle direction
        //rb.AddForce((transform.position - cat.transform.position).normalized * speed * 10.0f);
        rb.AddForce((transform.position - GameObject.Find("Player").transform.position).normalized * speed * 10.0f);

    }
    public void caught() // call on cat collision to main object.
    {
        if (DEBUG_FLAG) Debug.Log("caught!");
        this.gameObject.SetActive(false);

    }

    void randomMovementForce()
    {
        // if velocity magnitude is greater than soft cap * 1.1 then return
        if (rb.velocity.magnitude > softCapVelocity * 1.1f)
        {
            return;
        }
        // else apply a random small random direction force
        rb.AddForce(new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f)) * speed);
    }

    void applySoftAndHardVelocityCaps()
    {
        // if rigidbody velocity is over the hardcap. set it to hard cap
        if (rb.velocity.magnitude > hardCapVelocity)
        {
            rb.velocity = rb.velocity.normalized * hardCapVelocity;
        }
        // if rigidbody velocity is over the soft cap, reduce it by a factor
        if (rb.velocity.magnitude > softCapVelocity)
        {
            //rb.velocity = rb.velocity.normalized * softCapVelocity;
            // slowly reduce velocity with a curve
            rb.velocity = rb.velocity.normalized * Mathf.Lerp(rb.velocity.magnitude, softCapVelocity, Time.deltaTime);
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
