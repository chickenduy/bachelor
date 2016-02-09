using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game_S : Singleton<Game_S>
{
    // guarantee this will be always a singleton only - can't use the constructor!
    protected Game_S() { }
    public Material highlighted_wall;
    public Material normal_wall;
    public int spawn_number = 5;
    public float Power_B_Timer = 15f;
    public float Power_C_Timer = 15f;
    public float Power_D_Timer = 15f;
    public int wall_timer = 20;
    public int wall_Final_Timer = 3;
    public int temperature = 0;
    public int lighting = 0;
    public float pee = 0;
    public bool wind = false;
    public int killfire = 0;
    public GameObject _Ice;
    public GameObject _Fire;
    public GameObject[] _Power  = new GameObject[4];
    public AudioClip background_music;
}
