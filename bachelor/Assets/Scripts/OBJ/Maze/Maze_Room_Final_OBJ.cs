using UnityEngine;
using System.Collections;

public class Maze_Room_Final_OBJ : MonoBehaviour
{
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
        if (col.tag == "Player" && Room_S.Instance.electricity)
        {
            Maze_S.Instance.Enter_Room_Final(id, gameObject);
        }
        else
            Debug.Log("Entering Final Room " + id + " failed but " + col + " entered");
    }
}
