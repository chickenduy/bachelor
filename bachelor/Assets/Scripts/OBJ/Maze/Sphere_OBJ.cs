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
            Player_S.Instance.Check_Dream_State();
            User_Interface_S.Instance.Show_Info_Panel("Rolling Sphere woke you up!");
        }
        if (col.tag == "ice")
        {
            Ice_S.Instance.Delete(col.gameObject);
        }
    }


}
