using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Maze_S : Singleton<Maze_S>
{
    // guarantee this will be always a singleton only - can't use the constructor!
    protected Maze_S() { }

    private Dictionary<GameObject, int> maze_room_dictionary = new Dictionary<GameObject, int>();
    private Dictionary<int, Transform> maze_position_dictionary = new Dictionary<int, Transform>();
    private Dictionary<Renderer, int> mirror_dictionary = new Dictionary<Renderer, int>();
    private Dictionary<int, List<Light>> light_dictionary = new Dictionary<int, List<Light>>();
    private Dictionary<int, List<ParticleSystem>> particle_dictionary = new Dictionary<int, List<ParticleSystem>>();

    public Material mirror;
    public Material[] rooms = new Material[4];

    public bool[] room_discovered = new bool[4];

    // Use this for initialization
    void Awake()
    {

    }

    public void Register(GameObject obj, int id)
    {
        maze_room_dictionary.Add(obj, id);
        maze_position_dictionary.Add(id, obj.transform);
    }
    public void Register(Renderer ren, int id)
    {
        mirror_dictionary.Add(ren, id);
    }

    public void Register(Light lightobj, int id)
    {
        if (light_dictionary.ContainsKey(id))
            light_dictionary[id].Add(lightobj);
        else
        {
            List<Light> l_list = new List<Light>();
            l_list.Add(lightobj);
            light_dictionary.Add(id, l_list);
        }
    }

    public void Register(ParticleSystem ps, int id)
    {
        if (particle_dictionary.ContainsKey(id))
            particle_dictionary[id].Add(ps);
        else
        {
            List<ParticleSystem> ps_list = new List<ParticleSystem>();
            ps_list.Add(ps);
            particle_dictionary.Add(id, ps_list);
        }
    }

    public void Turn_On_Light(int id)
    {
        foreach (Light light in light_dictionary[id])
            light.enabled = true;
    }

    public void Turn_On_Fire(int id)
    {
        foreach (ParticleSystem ps in particle_dictionary[id])
        {
            ParticleSystem.EmissionModule em = ps.emission;
            em.enabled = true;
        }
    }

    public void Enter_Room(int id, GameObject obj)
    {
        Material mat = rooms[id];
        foreach (KeyValuePair<Renderer, int> mirror in mirror_dictionary)
        {
            if (mirror.Value == id)
            {
                mirror.Key.material = mat;
                mirror.Key.gameObject.GetComponent<MirrorReflection>().enabled = false;
            }
        }
        Turn_On_Light(id);
        Turn_On_Fire(id);
        room_discovered[id] = true;
    }

    public void Enter_Room_Final(int id, GameObject obj)
    {
        obj.GetComponent<BoxCollider>().enabled = false;
        Turn_On_Light(id);
        Turn_On_Fire(id);
    }

    public int Get_ID(GameObject obj)
    {
        return maze_room_dictionary[obj];
    }

    public bool Check_For_Key(int id)
    {
        if (maze_room_dictionary.ContainsValue(id))
            return true;
        return false;
    }

    public bool Get_Discovered(int id)
    {
        return room_discovered[id];
    }

    public void Teleport_To_Room(int id)
    {
        Player_S.Instance.gameObject.transform.position = maze_position_dictionary[id].position;
    }

}
