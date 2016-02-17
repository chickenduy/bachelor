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

    public void Create_Object(GameObject obj, Vector3 pos, Quaternion rot, string tag)
    {
        //Register(obj, tag);
        Instantiate(obj, pos, rot);
    }

    //Delete a gameObject
    public void Delete(GameObject obj)
    {
        switch (obj.tag)
        {
            case "fire":
                Fire_S.Instance.Delete(obj);
                break;
            case "ice":
                Ice_S.Instance.Delete(obj);
                break;
            case "powerA":
                Power_S.Instance.Delete(obj);
                break;
            default:
                Debug.LogError("Something went wrong in Object_S/Delete");
                break;
        }

    }

    //spawn all Objects
    public void SpawnObstacles()
    {
        Fire_S.Instance.Calculate_Fire();
        Ice_S.Instance.Caluculate_Ice();
    }

    public void SpawnPowers()
    {
        Power_S.Instance.SpawnPower();
    }
    //take book
    public void Take_Power(GameObject obj)
    {
        Power_S.Instance.TakePower(obj);
    }

    public void Kill_Fire(GameObject obj)
    {
        Fire_S.Instance.Delete(obj);
    }

}




