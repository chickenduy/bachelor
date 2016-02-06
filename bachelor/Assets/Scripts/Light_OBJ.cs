using UnityEngine;
using System.Collections;


public class Light_OBJ : MonoBehaviour
{
    private int id;
    private Light room_light;

    // Use this for initialization
    void Start()
    {
        Light room_light = GetComponent<Light>();
        room_light.enabled = false;
        id = GetComponentInParent<Switch_OBJ>().Get_ID();
        Object_S.Instance.Register(id, room_light);
    }

}
