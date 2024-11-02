using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WispAvoid : MonoBehaviour
{
    [SerializeField]
    private GameObject parentWisp; // set to parent object that's holding this object that's only holding a trigger volume.
    private Wisp parentWispScript;
    
    // Start is called before the first frame update
    void Start()
    {
        parentWispScript = parentWisp.GetComponent<Wisp>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool isPlayer(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.name == "Player")
        {
            return true;
        }
        return false;
    }

    // on enter trigger, call avoidBehavior() on parentWispScript
    private void OnTriggerEnter(Collider other)
    {
        if (isPlayer(other))
        {
            parentWispScript.avoidBehavior();
        }
    }
}
