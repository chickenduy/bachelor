using UnityEngine;
using System.Collections;

public class Background_Music_S : Singleton<Background_Music_S>
{
    // guarantee this will be always a singleton only - can't use the constructor!
    protected Background_Music_S() { }

    private AudioSource background_audio;
    private AudioSource radio_audio;
    public AudioClip background_music;
    private bool _background_music_is_playing;

    private float max_background_volume = 0.02f;
    private float max_radio_volume = 0.04f;

    public bool background_music_is_playing
    {
        get
        {
            return _background_music_is_playing;
        }
        set
        {
            _background_music_is_playing = value;
        }
    }
    private bool _is_radio_on;
    public bool is_radio_on
    {
        get
        {
            return _is_radio_on;
        }
        set
        {
            _is_radio_on = value;
        }

    }
    public AudioClip[] radio_music = new AudioClip[15];

    // Use this for initialization
    void Start()
    {
        background_audio = GetComponent<AudioSource>();
        radio_audio.clip = background_music;
        background_audio.clip = background_music;
        background_audio.loop = true;
        radio_audio.volume = 0;
        Play();
    }

    public void Register(AudioSource a_source)
    {
        radio_audio = a_source;
    }

    public void Disable_Background_Music()
    {
        background_audio.volume = 0;
    }

    public void Enable_Background_Music()
    {
        if (!_is_radio_on)
            background_audio.volume = 1;
        else
            background_audio.volume = 0.02f;
    }

    public void Turn_Off_Radio()
    {
        _is_radio_on = false;
        radio_audio.clip = background_music;
        background_audio.clip = background_music;
        background_audio.loop = true;
        Play();

    }

    public void Turn_On_Radio()
    {
        _is_radio_on = true;
        int number = Random.Range(0, radio_music.Length);
        radio_audio.clip = radio_music[number];
        background_audio.clip = radio_music[number];
        background_audio.loop = false;
        radio_audio.volume = max_radio_volume;
        Play();
    }

    public void Play()
    {
        radio_audio.Play();
        background_audio.Play();
    }


}
