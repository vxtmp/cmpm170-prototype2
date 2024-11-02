using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WispAud : MonoBehaviour
{
    private AudioSource aud;
    [SerializeField] private AudioClip sparkle;

    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
        aud.clip = sparkle;
        aud.loop = true;
        aud.Play();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
