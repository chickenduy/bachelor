using UnityEngine;
using System.Collections;


public class Light_OBJ : MonoBehaviour
{
    private int id;
    private Light lightobj;

    // Use this for initialization
    void Start()
    {
        lightobj  = GetComponent<Light>();
        lightobj.enabled = false;
        id = GetComponentInParent<Switch_OBJ>().id;
        //register light into lighting_dictionary
        Object_S.Instance.Register(id, lightobj);
    }

}
