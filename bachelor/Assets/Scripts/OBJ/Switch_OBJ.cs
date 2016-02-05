using UnityEngine;
using System.Collections;

public class Switch_OBJ : MonoBehaviour
{
    private int id = 0;
    // Use this for initialization
    void Start()
    {
        //register object in Singleton dictionary
        Object_S.Instance.Register(id, gameObject, gameObject.GetComponent<Animator>());
    }


}
