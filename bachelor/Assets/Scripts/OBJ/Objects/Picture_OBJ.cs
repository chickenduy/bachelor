using UnityEngine;
using System.Collections;

public class Picture_OBJ : MonoBehaviour {
    private int id = 0;
    private Animator anim =null;

    // Use this for initialization
    void Awake()
    {
        //register object in Singleton dictionary
        Check_For_ID();
    }

    public int Get_ID()
    {
        return id;
    }

    public void Check_For_ID()
    {
        if(gameObject.tag == "main picture")
        {
            Object_S.Instance.Register(gameObject, "main picture");
        }
        else if (Object_S.Instance.Check_For_ID(id))
        {
            id++;
            Check_For_ID();
        }
        else
        {
            Object_S.Instance.Register(id, gameObject, anim);
        }
    }
}
