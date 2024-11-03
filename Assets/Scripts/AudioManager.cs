using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource aud;

    //player manager
    public Player PlayMan;

    //audio clips
    public AudioClip jump;
    public AudioClip wispCaught;
    public AudioClip walk;

    //checking playing
    bool instate = false;
    bool toggleplay = false;
    bool walking = false;

    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayMan.isCharging)
        {
            aud.clip = jump;
            aud.loop = false;
            instate = true;
            aud.Play();
        }

        if (PlayMan.walking && !toggleplay && instate)
        {
            aud.clip = walk;
            aud.loop = true;
            instate = true;
            walking = true;
            aud.Play();
            toggleplay = true;
        }
        else if (!PlayMan.walking && walking)
        {
            aud.Stop();
            toggleplay = false;
            walking = false;
            instate = false;
        }
    }
}
