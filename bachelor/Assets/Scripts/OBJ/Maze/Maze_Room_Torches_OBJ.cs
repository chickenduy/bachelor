using UnityEngine;
using System.Collections;

public class Maze_Room_Torches_OBJ : MonoBehaviour
{
    private int id;
    private Light torchlight;
    private ParticleSystem ps;
    // Use this for initialization
    void Start()
    {
        torchlight = GetComponentInChildren<Light>();
        ps = GetComponentInChildren<ParticleSystem>();
        ParticleSystem.EmissionModule em = ps.emission;
        em.enabled = false;
        torchlight.enabled = false;
        if (gameObject.tag == "exit_room")
            id = GetComponentInParent<Maze_Room_Final_OBJ>().id;
        else
            id = GetComponentInParent<Maze_Room_OBJ>().id;
        Maze_S.Instance.Register(ps, id);
        Maze_S.Instance.Register(torchlight, id);
    }

    private GameObject Get_Parent(GameObject obj, string tag)
    {
        GameObject parent = obj.transform.parent.gameObject;
        if (parent.tag == "exit_room")
            return parent;
        else
            Get_Parent(parent, tag);
        return null;
    }

}
