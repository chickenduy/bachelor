using UnityEngine;
using System.Collections;

public class Background_Music_S : Singleton<Background_Music_S>
{
    // guarantee this will be always a singleton only - can't use the constructor!
    protected Background_Music_S() { }

    private AudioSource audio_source;
    public AudioClip audio_clip;

    // Use this for initialization
    void Start()
    {
        audio_source = GetComponent<AudioSource>();
        audio_source.clip = audio_clip;
        audio_source.loop = true;
        audio_source.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
