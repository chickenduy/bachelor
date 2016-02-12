using UnityEngine;
using System.Collections;

public class Toilet_OBJ : MonoBehaviour
{

    private int id = 0;
    private Animator anim;
    // Use this for initialization
    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        //register object in Singleton dictionary
        Check_For_ID();
    }

    public int Get_ID()
    {
        return id;
    }

    public void Check_For_ID()
    {
        if (Object_S.Instance.Check_For_ID(id))
        {
            id++;
            Check_For_ID();
        }
        else
        {
            Object_S.Instance.Register(id, gameObject);
            Object_S.Instance.Register(id, anim);
        }
    }
}
