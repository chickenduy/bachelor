using UnityEngine;
using System.Collections;

public class Maze_Room_Torches_OBJ : MonoBehaviour
{
    private int id;

    // Use this for initialization
    void Start()
    {
        Light torchlight = GetComponent<Light>();
        torchlight.enabled = false;
        id = GetComponentInParent<Maze_Room_OBJ>().Get_ID();
        Maze_S.Instance.Register(torchlight, id);
    }

}
