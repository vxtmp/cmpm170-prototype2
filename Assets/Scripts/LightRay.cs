using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class LightRay : MonoBehaviour
{

    [SerializeField]
    private bool DEBUG_FLAG = true;
    [SerializeField] private Bloom bloom;

    // Start is called before the first frame update
    void Start()
    {
        PostProcessVolume volume = GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out bloom);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool isWisp(Collider other)
    {
        if (other.gameObject.CompareTag("Wisp") || other.gameObject.name == "wisp")
        {
            return true;
        }
        return false;
    }
    bool isPlayer(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.name == "Player")
        {
            return true;
        }
        return false;
    }

    // check collision trigger, if the object has the tag Wisp, then call make visible function.
    void OnTriggerEnter(Collider other)
    {
        if (isWisp(other))
        {
            Wisp wisp = other.gameObject.GetComponent<Wisp>();
            if (wisp != null)
            {
                if (DEBUG_FLAG) Debug.Log("wisp entered light. making visible");
                wisp.makeVisible();
            } else
            {
                if (DEBUG_FLAG) Debug.Log("wisp == null");
            }
            return;
        }
        if (isPlayer(other))
        {
            // enable light post processing effect here.
            bloom.active = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (isWisp(other))
        {
            Wisp wisp = other.gameObject.GetComponent<Wisp>();
            if (wisp != null)
            {
                if (DEBUG_FLAG) Debug.Log("wisp exited light. making invisible");
                wisp.makeInvisible();
            }
            else
            {
                if (DEBUG_FLAG) Debug.Log("wisp == null");
            }
            return;
        }
        if (isPlayer(other))
        {
            // disable light post processing effecth ere
            bloom.active = false;
        }
    }
}
