using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Obstacle_S : Singleton<Obstacle_S>
{
    // guarantee this will be always a singleton only - can't use the constructor!
    protected Obstacle_S() { }

    //variables
    public bool[,] space_bool = new bool[21, 21];

    //methods
    public void Create_Object(GameObject obj, Vector3 pos, Quaternion rot, string tag)
    {
        //Register(obj, tag);
        Instantiate(obj, pos, rot);
    }

    public void SpawnObstacles()
    {
        Debug.Log("Spawn Fire/Ice/Power");
        Fire_S.Instance.Calculate_Fire();
        Ice_S.Instance.Caluculate_Ice();
        Power_S.Instance.SpawnPower();
    }

    //take book
    public void Take_Power(GameObject obj)
    {
        Power_S.Instance.TakePower(obj);
    }

}




