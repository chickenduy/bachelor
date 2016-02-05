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

    public void Register(int id, GameObject obj, Animator anim)
    {
        if (object_dictionary.ContainsValue(id))
        {
            id = id + 1;
            Register(id, obj, anim);
        }
        else
        {
            object_dictionary.Add(obj, id);
            object_animation.Add(id, anim);
        }
    }





}
