using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wall_S : Singleton<Wall_S>
{
    // guarantee this will be always a singleton only - can't use the constructor!
    protected Wall_S() { }

    public int wall_timer;
    public int wall_Final_Timer;

    //variables
    private Dictionary<GameObject, int> wall_dictionary = new Dictionary<GameObject, int>();
    private Dictionary<int, Animator> wall_animator = new Dictionary<int, Animator>();
    private GameObject wall_2;
    private List<Animator> wall_dictionary2 = new List<Animator>();


    //methods
    void Start()
    {
        //move walls randomly in a given time interval
        InvokeRepeating("Move_Wall", wall_timer, wall_timer);
    }



    public void Register(int id, GameObject obj, Animator anim)
    {
        if (wall_dictionary.ContainsValue(id))
            Debug.LogError(obj + " ID already exists!");
        else
        {
            wall_dictionary.Add(obj, id);
            if (anim != null)
                wall_animator.Add(id, anim);
        }
    }

    public void Register(GameObject obj, string tag)
    {
        if (tag == "final wall")
            wall_dictionary2.Add(obj.GetComponent<Animator>());
        else
            wall_2 = obj;
    }



    public int Get_ID(GameObject obj)
    {
        return wall_dictionary[obj];
    }

    public Animator Get_Animator(GameObject obj)
    {
        return wall_animator[wall_dictionary[obj]];
    }

    //move the all walls with tag moving wall randomly
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
                    wall_animator[id].SetBool("move", !wall_animator[id].GetBool("move"));
            }
        }
    }

    //change material of all walls with tag moving wall
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

    //move a single wall
    public void Move_Highlighted_Wall(int id)
    {
        wall_animator[id].SetBool("move", !wall_animator[id].GetBool("move"));
    }

    //disable collision between player and walls
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
            Debug.Log("Key: " + obj.Key + " - Value: " + obj.Value + " -  Animator: " + wall_animator[obj.Value]);
    }

    public bool Check_For_ID(int id)
    {
        if (wall_dictionary.ContainsValue(id))
            return true;
        return false;
    }

    private void Move_Final_Walls()
    {
        int number = 0;
        foreach (Animator wall in wall_dictionary2)
        {
            number = Random.Range(0, 2);
            if (number == 0)
                wall.SetBool("state", !wall.GetBool("state"));
        }
    }

    public void Destroy_Wall_2()
    {
        Destroy(wall_2);
        Object_S.Instance.Delete_Main_Picture();
        InvokeRepeating("Move_Final_Walls", 0, wall_Final_Timer);
    }
}
