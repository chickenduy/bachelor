using UnityEngine;
using System.Collections;

public class Spawns_OBJ : MonoBehaviour
{
    // Use this for initialization
    private int id = 0;
    void Start()
    {        
        //register object in Singleton dictionary
        Spawns_S.Instance.Register(id, transform, gameObject.tag);
    }
}
