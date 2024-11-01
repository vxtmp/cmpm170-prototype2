using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wisp : MonoBehaviour
{
    //private bool isVisible = false;
    private float speed = 1.0f;
    private float maxDistance = 10.0f;
    private float distanceTravelled = 0.0f;

    private Rigidbody rb;
    private float softCapVelocity = 10.0f;
    private float hardCapVelocity = 20.0f;
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

    void avoidBehavior() // call on nearby cat (collision with larger child trigger volume
    {

    }
    void caught() // call on cat collision to main object.
    {

    }

    void defaultMovement()
    {
        // apply a random small random direction force
        rb.AddForce(new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f)) * speed);
    }

    void attenuateVelocity()
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
}
