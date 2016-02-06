using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wall_S : Singleton<Wall_S>
{
    // guarantee this will be always a singleton only - can't use the constructor!
    protected Wall_S() { }

    //variables
    private Dictionary<GameObject, int> wall_dictionary = new Dictionary<GameObject, int>();
    private Dictionary<int, Animator> wall_animator = new Dictionary<int, Animator>();

    //methods
    void Start()
    {
        InvokeRepeating("Move_Wall", 20f, 20f);
    }

    public void Register(int id, GameObject obj, Animator anim)
    {
        if (wall_dictionary.ContainsValue(id))
        {
            id = id + 1;
            Register(id, obj, anim);
        }
        else
        {
            wall_dictionary.Add(obj, id);
            if (anim != null)
            {
                wall_animator.Add(id, anim);
            }
        }
    }

    public int Get_ID(GameObject obj)
    {
        return wall_dictionary[obj];
    }

    public Animator Get_Animator(GameObject obj)
    {
        return wall_animator[wall_dictionary[obj]];
    }

    public void Move_Wall()
    {
        for (int i = 0; i < wall_dictionary.Count; i++)
        {
            int number = Random.Range(0, 2);
            if (number == 0)
            {
                wall_animator[i].SetBool("move", !wall_animator[i].GetBool("move"));
            }

        }
    }

    public void Change_Wall_Material(Material highlighted_wall)
    {

        foreach (KeyValuePair<GameObject, int> wall in wall_dictionary)
        {
            if (wall.Key.tag == "moving wall")
            {
                Material mat = highlighted_wall;
                wall.Key.GetComponent<Renderer>().material = mat;
            }
        }
    }

    public void Move_Highlighted_Wall(int id)
    {
        wall_animator[id].SetBool("move", !wall_animator[id].GetBool("move"));
    }

    public void Move_Through_Walls(bool power)
    {
        foreach (KeyValuePair<GameObject, int> wall in wall_dictionary)
        {
            if (wall.Key.tag == "moving wall")
                Physics.IgnoreCollision(Player_S.Instance.GetComponent<CharacterController>(), wall.Key.GetComponent<BoxCollider>(), power);
            else
                Physics.IgnoreCollision(Player_S.Instance.GetComponent<CharacterController>(), wall.Key.GetComponent<MeshCollider>(), power);
        }
    }



}
