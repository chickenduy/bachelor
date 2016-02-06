using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wall_OBJ : MonoBehaviour {

    private int id = 0;
    private Animator anim;
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        //register object in Singleton dictionary
        Check_For_ID();
    }

    private void Check_For_ID()
    {
        if (Wall_S.Instance.Check_For_ID(id))
        {
            id++;
            Check_For_ID();
        }
        else
        {
            Wall_S.Instance.Register(id, gameObject, anim);
        }
    }


}
