using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJump : MonoBehaviour
{

    [SerializeField]
    private GameObject playerObj;
    private Player playerScript;

    // maintain a list of gameobjects that are near the player (through on enter triggers and exit triggers)
    private List<GameObject> nearbyObjects = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        playerScript = this.playerObj.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // on trigger enter
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            // add the wall to the list of nearby objects
            nearbyObjects.Add(other.gameObject);
        }
    }
}
