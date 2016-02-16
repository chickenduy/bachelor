using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sphere_OBJ : MonoBehaviour
{
    public void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            Maze_S.Instance.Wake_Sleep_Hit();
        }
        if (col.tag == "ice")
        {
            Ice_S.Instance.Delete(col.gameObject);
        }
    }


}
