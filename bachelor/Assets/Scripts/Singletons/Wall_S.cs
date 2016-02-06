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

    public int wall_timer = 20;

    //methods
    void Start()
    {
        InvokeRepeating("Move_Wall", wall_timer, wall_timer);
    }

    public void Register(int id, GameObject obj, Animator anim)
    {
        if (wall_dictionary.ContainsValue(id))
        {
            Debug.LogError(obj + " ID already exists!");
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
        int number;
        int id;
        foreach (KeyValuePair<GameObject, int> wall in wall_dictionary)
        {
            id = wall.Value;
            if (wall.Key.tag == "moving wall")
            {
                number = Random.Range(0, 2);
                if (number == 0)
                {
                    wall_animator[id].SetBool("move", !wall_animator[id].GetBool("move"));
                }
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


    public void Print_Dictionary()
    {
        foreach (KeyValuePair<GameObject, int> obj in wall_dictionary)
        {
            Debug.Log("Key: " + obj.Key + " - Value: " + obj.Value + " -  Animator: " + wall_animator[obj.Value]);
        }
    }

    public bool Check_For_ID(int id)
    {
        if (wall_dictionary.ContainsValue(id))
        {
            return true;
        }
        return false;
    }
}
