using UnityEngine;
using System.Collections;

public class Maze_Room_Torches_OBJ : MonoBehaviour
{
    private int id;
    private Light torchlight;

    // Use this for initialization
    void Start()
    {
        torchlight = GetComponent<Light>();
        torchlight.enabled = false;
        id = GetComponentInParent<Maze_Room_OBJ>().id;
        Maze_S.Instance.Register(torchlight, id);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
