using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Object_S : Singleton<Object_S>
{
    // guarantee this will be always a singleton only - can't use the constructor!
    protected Object_S() { }

    //private
    private Dictionary<GameObject, int> object_dictionary = new Dictionary<GameObject, int>();
    private Dictionary<int, Animator> object_animation = new Dictionary<int, Animator>();
    private Dictionary<int, Light> object_light = new Dictionary<int, Light>();
    private List<GameObject> main_picture = new List<GameObject>();
    private GameObject fan;
    private ParticleSystem ps;
    private ParticleSystem.EmissionModule em;
    private int pictures;
    private bool window = false;

    //getter/setter
    private bool _fireplace = false;
    public bool fireplace
    {
        get
        {
            return _fireplace;
        }
        set
        {
            _fireplace = value;
        }
    }

    /*----------------------------------------------------------------------------------------------------*/

    //register gameObject into dictionary (also register animation)
    public void Register(int id, GameObject obj)
    {
        if (object_dictionary.ContainsValue(id))
            Debug.LogError(obj + " ID already exists!");
        else
        {
            object_dictionary.Add(obj, id);
        }
    }

    //register animator with object animation
    public void Register(int id, Animator anim)
    {
        object_animation.Add(id, anim);
    }

    //register lighting objects with its parent id
    public void Register(int id, Light light)
    {
        object_light.Add(id, light);
    }

    //TODO: change Registering two individual objects
    public void Register(GameObject obj, string tag)
    {
        if (tag == "main picture")
            main_picture.Add(obj);
        else if (tag == "fan")
            fan = obj;
        else
            Debug.Log("Other Tag on Object");
    }

    //Register a Particle System
    public void Register(ParticleSystem par)
    {
        ps = par;
        em = par.emission;
    }

    //Delete gameObject from dictionaries
    public void Delete(GameObject obj)
    {
        int id = object_dictionary[obj];
        object_dictionary.Remove(obj);
        if (object_animation.ContainsKey(id))
            object_animation.Remove(id);
        if (object_light.ContainsKey(id))
            object_light.Remove(id);
        Destroy(obj);
    }


    public void Use_Object(GameObject obj)
    {
        //get the id of the gameObject
        int id = object_dictionary[obj];
        //get the Animator of the gameObject
        Animator anim = object_animation[id];
        //Animate gameObject
        anim.SetBool("state", !anim.GetBool("state"));
        //if gameObject is window change temperature
        if (obj.tag == "window")
        {
            window = anim.GetBool("state");
            if (window)
                Room_S.Instance.Temperature_Lower();
            else
                Room_S.Instance.Temperature_Higher();
        }
        //if gameObject has a ligt, change lighting
        if (object_light.ContainsKey(id))
            object_light[id].enabled = !object_light[id].isActiveAndEnabled;
        //if gameObject is part of fan, animate the fan
        if (obj.tag == "fan")
        {
            fan.GetComponent<Animator>().SetBool("state", !fan.GetComponent<Animator>().GetBool("state"));
            Room_S.Instance.wind = fan.GetComponent<Animator>().GetBool("state");
            if (Room_S.Instance.wind)
                Room_S.Instance.Temperature_Lower();
            else
                Room_S.Instance.Temperature_Higher();
        }
    }

    public GameObject Get_Child_Object(GameObject obj, string name)
    {
        GameObject grandchild;
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            grandchild = obj.transform.GetChild(i).gameObject;
            if (grandchild.name == name)
            {
                return grandchild;
            }
        }
        Debug.LogError("Error in Object_S/Get_Child_Object: couldn't find child object");
        return null;
    }

    public void Light_Fireplace(GameObject obj)
    {
        int id = object_dictionary[obj];
        if (Player_S.Instance.lighter)
        {
            if (!fireplace)
            {
                em.enabled = true;
                object_light[id].enabled = true;
                fireplace = true;
                Room_S.Instance.Temperature_Higher();
                object_animation[id].SetBool("state", fireplace);
            }
            else
            {
                em.enabled = false;
                ps.Clear();
                fireplace = false;
                Room_S.Instance.Temperature_Lower();
                object_animation[id].SetBool("state", fireplace);
            }
        }
    }

    public int Get_ID(GameObject obj)
    {
        return object_dictionary[obj];
    }

    public bool Check_For_ID(int id)
    {
        if (object_dictionary.ContainsValue(id))
            return true;
        return false;
    }

    public void Touch_Picture(GameObject obj)
    {
        obj.GetComponentInParent<Animator>().SetBool("state", true);
        obj.GetComponent<BoxCollider>().enabled = false;
        Player_S.Instance.pictures[pictures] = true;
        pictures++;
    }

    public void Delete_Main_Picture()
    {
        foreach (GameObject picture in main_picture)
            Destroy(picture);
        main_picture.Clear();
    }


}
