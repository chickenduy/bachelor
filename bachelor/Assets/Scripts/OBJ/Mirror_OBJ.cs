using UnityEngine;
using System.Collections;

public class Mirror_OBJ : MonoBehaviour
{
    private int id;

    // Use this for initialization
    void Start()
    {
        id = GetComponentInParent<Maze_Room_OBJ>().id;
        Maze_S.Instance.Register(gameObject.GetComponent<Renderer>(), id);
        gameObject.GetComponent<Renderer>().material = Maze_S.Instance.mirror;
    }


}
