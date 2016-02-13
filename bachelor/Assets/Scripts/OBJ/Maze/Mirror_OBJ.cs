using UnityEngine;
using System.Collections;

public class Mirror_OBJ : MonoBehaviour
{
    private int _id;
    public int id
    {
        get
        {
            return _id;
        }
    }

    // Use this for initialization
    void Start()
    {
        _id = GetComponentInParent<Maze_Room_OBJ>().id;
        gameObject.GetComponent<Renderer>().material = Maze_S.Instance.mirror;
        Maze_S.Instance.Register(gameObject.GetComponent<Renderer>(), _id);

    }


}
