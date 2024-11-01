using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wisp : MonoBehaviour
{
    //private bool isVisible = false;
    private float speed = 1.0f;
    private float maxDistance = 10.0f;
    private float distanceTravelled = 0.0f;

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
}
