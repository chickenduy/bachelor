using UnityEngine;
using System.Collections;

public class Maze_Room_Torches_OBJ : MonoBehaviour
{
    private int id;

    // Use this for initialization
    void Start()
    {
        Light torchlight = GetComponent<Light>();
        ParticleSystem ps = GetComponent<ParticleSystem>();
        ParticleSystem.EmissionModule em = ps.emission;
        em.enabled = false;
        torchlight.enabled = false;
        id = GetComponentInParent<Maze_Room_OBJ>().id;
        Maze_S.Instance.Register(em, id);
        Maze_S.Instance.Register(torchlight, id);
    }

}
