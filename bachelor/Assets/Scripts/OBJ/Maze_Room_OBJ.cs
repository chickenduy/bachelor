using UnityEngine;
using System.Collections;

public class Maze_Room_OBJ : MonoBehaviour
{
    private int id = 0;

    // Use this for initialization
    void Awake()
    {
        Check_For_ID();
    }



    public int Get_ID()
    {
        return id;
    }

    public void Check_For_ID()
    {
        if (Maze_S.Instance.Check_For_Key(id))
        {
            id++;
            Check_For_ID();
        }
        else
        {
            Maze_S.Instance.Register(gameObject, id);
        }
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.tag == "player")
        {
            Debug.Log("Entered Room " + id);
            Maze_S.Instance.Enter_Room(id);
        }

    }
}
