using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float chargeDuration = 0.0f;
    private float chargeSpeed = 10.0f;
    public const float MAX_CHARGE_DURATION = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void startCharging()
    {

    }
    void pounceForward()
    {

    }
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, 0, 0.1f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0, 0, -0.1f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-0.1f, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(0.1f, 0, 0);
        }
        // while mouse held down, charge up
        if (Input.GetMouseButton(0))
        {
            startCharging();
        }
        // when mouse released, pounce forward
        if (Input.GetMouseButtonUp(0))
        {
            pounceForward();
        }
    }

    // on trigger enter. check if the object has the tag "Enemy"
    // if it does, 
}
