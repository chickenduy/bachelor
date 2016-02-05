using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wall_OBJ : MonoBehaviour {

    private int id = 0;

    void Start()
    {
        //register object in Singleton dictionary
        Wall_S.Instance.Register(id, gameObject, gameObject.GetComponent<Animator>());
    }




}
