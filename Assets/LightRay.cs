using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRay : MonoBehaviour
{

    [SerializeField]
    private bool DEBUG_FLAG = true;

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
        if (other.gameObject.CompareTag("Wisp") || other.gameObject.name == "wisp")
        {
            Wisp wisp = other.gameObject.GetComponent<Wisp>();
            if (wisp != null)
            {
                if (DEBUG_FLAG)
                    Debug.Log("wisp entered light. making visible");
                wisp.makeVisible();
            } else
            {
                if (DEBUG_FLAG)
                    Debug.Log("wisp == null");
            }
            //other.gameObject.gameObject.GetComponent<Wisp>().makeInvisible();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Wisp") || other.gameObject.name == "wisp")
        {
            Wisp wisp = other.gameObject.GetComponent<Wisp>();
            if (wisp != null)
            {
                if (DEBUG_FLAG)
                    Debug.Log("wisp exited light. making invisible");
                wisp.makeInvisible();
            }
            else
            {
                if (DEBUG_FLAG)
                    Debug.Log("wisp == null");
            }
            //other.gameObject.gameObject.GetComponent<Wisp>().makeVisible();
        }
    }
}
