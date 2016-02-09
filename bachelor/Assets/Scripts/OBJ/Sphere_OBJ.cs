using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sphere_OBJ : MonoBehaviour
{
    public void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {        Debug.Log("HIT");

            Player_S.Instance.Respawn();
        }
    }


}
