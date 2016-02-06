using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Object_S : Singleton<Object_S>
{
    // guarantee this will be always a singleton only - can't use the constructor!
    protected Object_S() { }

    //variables
    private Dictionary<GameObject, int> object_dictionary = new Dictionary<GameObject, int>();
    private Dictionary<int, Animator> object_animation = new Dictionary<int, Animator>();
    private Dictionary<int, Light> object_light = new Dictionary<int, Light>();
    private GameObject fan;

    public void Register(int id, GameObject obj, Animator anim)
    {
        if (object_dictionary.ContainsValue(id))
        {
            Debug.LogError(obj + " ID already exists!");
        }
        else
        {
            object_dictionary.Add(obj, id);
            if (anim != null)
                object_animation.Add(id, anim);
        }
    }

    public void Register(int id, Light light)
    {
        object_light.Add(id, light);
    }

    public void Register(GameObject obj)
    {
        fan = obj;
    }


    public void Use_Object(GameObject obj)
    {
        int id = object_dictionary[obj];
        Debug.Log(id);
        Animator anim = object_animation[id];
        anim.SetBool("state", !anim.GetBool("state"));
        if (object_light.ContainsKey(id))
        {
            object_light[id].enabled = !object_light[id].isActiveAndEnabled;
        }
        if(obj.tag == "fan")
        {
            fan.GetComponent<Animator>().SetBool("state", !fan.GetComponent<Animator>().GetBool("state"));
        }
    }

    public int Get_ID(GameObject obj)
    {
        return object_dictionary[obj];
    }

    public bool Check_For_ID(int id)
    {
        if (object_dictionary.ContainsValue(id))
        {
            return true;
        }
        return false;
    }

    public void Print_Dictionary()
    {
        foreach (KeyValuePair<GameObject, int> obj in object_dictionary)
        {
            Debug.Log("Key: " + obj.Key.name + " - Value: " + obj.Value + " "+object_animation[obj.Value]);
        }
    }

    public void Print_Dictionary_A()
    {
        foreach (KeyValuePair<int, Animator> anim in object_animation)
        {
            Debug.Log("Key: " + anim.Key + " - Value: " + anim.Value);
        }
    }



}
