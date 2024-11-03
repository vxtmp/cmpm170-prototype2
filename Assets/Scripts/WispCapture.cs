using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WispCapture : MonoBehaviour
{
    [SerializeField] private Wisp parentWispScript;
    [SerializeField] private AudioSource aud;
    [SerializeField] private AudioClip caught;

    // Start is called before the first frame update
    void Start()
    {
        aud = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();

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
            aud.clip = caught;
            parentWispScript.caught();
        }
    }
}
