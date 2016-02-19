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
    private GameObject gate;

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

    //register gameObject into dictionary
    public void Register(int id, GameObject obj)
    {
        if (object_dictionary.ContainsValue(id))
            throw new System.Exception(obj + " ID already exists! ERROR in Object_S/Register");
        else
            object_dictionary.Add(obj, id);
    }
    //register animator
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
        else if (tag == "gate")
            gate = obj;
        else
            throw new System.Exception("Other Tag on Object, ERROR in Object_S/Register");
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
            //if wind is on lower temperature
            if (Room_S.Instance.wind)
                Room_S.Instance.Temperature_Lower();
            else
                Room_S.Instance.Temperature_Higher();
        }
    }
    public GameObject Get_Child_Object(GameObject obj, string name)
    {
        GameObject grandchild;
        //search through all the children
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            //get the child of the object
            grandchild = obj.transform.GetChild(i).gameObject;
            //if the object is the object searched for then return
            if (grandchild.name == name)
            {
                return grandchild;
            }
        }
        throw new System.Exception("Error in Object_S/Get_Child_Object: couldn't find child object");
    }
    public void Light_Fireplace(GameObject obj)
    {
        //get id of gameObject
        int id = object_dictionary[obj];
        //if the player has the lighter
        if (Player_S.Instance.lighter)
        {
            //if the fireplace is not on
            if (!fireplace)
            {
                //enable the emission module
                em.enabled = true;
                //enable the light
                object_light[id].enabled = true;
                //set fireplace to on
                fireplace = true;
                //raise room temperature 
                Room_S.Instance.Temperature_Higher();
                //animate the fire
                object_animation[id].SetBool("state", fireplace);
            }
            //if fireplace is already on
            else
            {
                //disable emission module
                em.enabled = false;
                //kill all flames at once
                ps.Clear();
                //set fireplace to false
                fireplace = false;
                //lower room temperature
                Room_S.Instance.Temperature_Lower();
                //animate fire
                object_animation[id].SetBool("state", fireplace);
            }
        }
    }
    //animate the opening of the gate
    public void Open_Gate()
    {
        gate.GetComponent<Animator>().SetBool("state", true);
    }
    //get the id of the game object
    public int Get_ID(GameObject obj)
    {
        return object_dictionary[obj];
    }
    //check for existing id in the object dictionary
    public bool Check_For_ID(int id)
    {
        if (object_dictionary.ContainsValue(id))
            return true;
        return false;
    }
    public void Touch_Picture(GameObject obj)
    {
        //animate the picture
        obj.GetComponentInParent<Animator>().SetBool("state", true);
        //disable the collider on it
        obj.GetComponent<BoxCollider>().enabled = false;
        //set the picture array to true
        Player_S.Instance.pictures[pictures] = true;
        //add 1 to the amount of pictures found
        pictures++;
    }

    public void Delete_Main_Picture()
    {
        //delete all main pictures
        foreach (GameObject picture in main_picture)
            Destroy(picture);
        main_picture.Clear();
    }
}
