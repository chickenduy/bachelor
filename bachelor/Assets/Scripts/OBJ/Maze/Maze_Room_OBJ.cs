using UnityEngine;
using System.Collections;

public class Maze_Room_OBJ : MonoBehaviour
{
    //assign room id over the inspector
    [SerializeField]
    private int _id;
    public int id
    {
        get
        {
            return _id;
        }
    }


    // Use this for initialization
    void Awake()
    {
        Maze_S.Instance.Register(gameObject, id);
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            Maze_S.Instance.Enter_Room(id, gameObject);
        }
        else
            Debug.Log("Entering Room " + id + " failed but " + col + " entered");
    }
}
