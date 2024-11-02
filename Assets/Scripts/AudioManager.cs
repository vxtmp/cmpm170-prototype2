using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource playerAud;

    //player manager
    public Player PlayMan;

    //audio clips
    public AudioClip jump;
    public AudioClip wispSparkle;
    public AudioClip wispCaught;
    public AudioClip walk;

    //checking playing
    bool instate;
    bool toggleplay;

    // Start is called before the first frame update
    void Start()
    {
        playerAud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
