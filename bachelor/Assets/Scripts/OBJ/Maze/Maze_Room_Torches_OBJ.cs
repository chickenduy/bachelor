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
        id = GetComponentInParent<Maze_Room_OBJ>().id;
        Maze_S.Instance.Register(ps, id);
        Maze_S.Instance.Register(torchlight, id);
    }

}
