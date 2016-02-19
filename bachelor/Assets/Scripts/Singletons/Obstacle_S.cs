using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Obstacle_S : Singleton<Obstacle_S>
{
    // guarantee this will be always a singleton only - can't use the constructor!
    protected Obstacle_S() { }

    //getter/setter
    private bool[,] _space_bool = new bool[25, 25];
    public bool[,] space_bool
    {
        get
        {
            return _space_bool;
        }
        set
        {
            _space_bool = value;
        }
    }

    /*----------------------------------------------------------------------------------------------------*/

    void Start()
    {
        Power_S.Instance.Spawn_Power();
        Spawn_Obstacles();
    }

    //spawn fire and ice Objects
    public void Spawn_Obstacles()
    {
        Fire_S.Instance.Calculate_Fire();
        Ice_S.Instance.Caluculate_Ice();
    }
}




