using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Maze_S : Singleton<Maze_S>
{
    // guarantee this will be always a singleton only - can't use the constructor!
    protected Maze_S() { }

    private Dictionary<GameObject, int> maze_room_dictionary = new Dictionary<GameObject, int>();
    private Dictionary<Renderer, int> mirror_dictionary = new Dictionary<Renderer, int>();
    private Dictionary<Light, int> light_dictionary = new Dictionary<Light, int>();

    public Material mirror;
    public Material[] rooms = new Material[4];

    // Use this for initialization
    void Start()
    {

    }

    public void Register(GameObject obj, int id)
    {
        maze_room_dictionary.Add(obj, id);
    }
    public void Register(Renderer ren, int id)
    {
        mirror_dictionary.Add(ren, id);
    }

    public void Register(Light lightobj, int id)
    {
        light_dictionary.Add(lightobj, id);
    }

    public void Enter_Room(int id)
    {
        Material mat = rooms[id];
        foreach (KeyValuePair<Renderer, int> mirror in mirror_dictionary)
        {
            if (mirror.Value == id)
            {
                mirror.Key.material = mat;
            }
        }
        foreach (KeyValuePair<Light, int> light in light_dictionary)
        {
            if (light.Value == id)
            {
                light.Key.enabled = true;
            }
        }
    }

}
