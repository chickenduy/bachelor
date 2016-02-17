using UnityEngine;
using System.Collections;

public class Maze_Room_Final_OBJ : MonoBehaviour
{
    public int id;

    // Use this for initialization
    void Awake()
    {
        Maze_S.Instance.Register(gameObject, id);
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player" && Player_S.Instance.key)
        {
            Debug.Log("Entered Room " + id);
            Maze_S.Instance.Enter_Room_Final(id, gameObject);
        }
        else
            Debug.Log("Entering Room " + id + " failed but " + col + " entered");
    }
}
