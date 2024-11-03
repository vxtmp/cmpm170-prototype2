using System.Collections.Generic;
using UnityEngine;

public class WispAvoid : MonoBehaviour
{

    private bool DEBUG_FLAG = false;
    //[SerializeField] private GameObject parentWisp; // set to parent object that's holding this object that's only holding a trigger volume.
    [SerializeField] private Wisp parentWispScript;

    public List<GameObject> nearbyObjects;

    [SerializeField] private float playerAvoidForce = 10.0f;
    [SerializeField] private float wallAvoidForce = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        nearbyObjects = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < nearbyObjects.Count; i++)
        {
            Vector3 closestPoint = nearbyObjects[i].GetComponent<Collider>().ClosestPoint(this.transform.position);
            parentWispScript.applyForceAwayFromNearbyObj(closestPoint);

        }
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
        if (other.gameObject.CompareTag("Player") ||
            other.gameObject.CompareTag("Wall") ||
            other.gameObject.CompareTag("Wisp"))
        {
            nearbyObjects.Add(other.gameObject);
        }
        // print out all objects
        if (DEBUG_FLAG)
        {
            foreach (GameObject obj in nearbyObjects)
            {
                Debug.Log(obj.name);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        nearbyObjects.Remove(other.gameObject);
        // print out all objects
        if (DEBUG_FLAG)
        {

            foreach (GameObject obj in nearbyObjects)
            {
                Debug.Log(obj.name);
            }
        }
    }
}
