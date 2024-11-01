using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRay : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // check collision trigger, if the object has the tag Wisp, then call make visible function.
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wisp") || other.gameObject.name == "Wisp")
        {
            Wisp wisp = other.gameObject.GetComponent<Wisp>();
            if (wisp != null)
            {
                wisp.makeInvisible();
            } else
            {
                Debug.Log("wisp == null");
            }
            //other.gameObject.gameObject.GetComponent<Wisp>().makeInvisible();
        }
    }

    void makeVisible()
    {

    }
}
