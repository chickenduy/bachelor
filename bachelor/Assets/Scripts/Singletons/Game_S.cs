using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game_S : Singleton<Game_S>
{
    // guarantee this will be always a singleton only - can't use the constructor!
    protected Game_S() { }



    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
   

}
