using UnityEngine;
using System.Collections;

public class Maze_Room_OBJ : MonoBehaviour
{
    public int id;

    // Use this for initialization
    void Start()
    {
        Maze_S.Instance.Register(gameObject, id);
    }

    public void OnTriggerEnter(Collider col)
    {
        Debug.Log("Entered Room " + id);
        Maze_S.Instance.Enter_Room(id);
    }
}
