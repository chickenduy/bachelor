using UnityEngine;
using System.Collections;

public class Picture_OBJ : MonoBehaviour
{
    private int _id = 0;
    public int id
    {
        get
        {
            return _id;
        }

    }
    private Animator anim = null;

    // Use this for initialization
    void Awake()
    {
        //register object in Singleton dictionary
        Check_For_ID();
    }

    public int Get_ID()
    {
        return _id;
    }

    public void Check_For_ID()
    {
        if (gameObject.tag == "main picture")
        {
            Object_S.Instance.Register(gameObject, "main picture");
        }
        else if (Object_S.Instance.Check_For_ID(_id))
        {
            _id++;
            Check_For_ID();
        }
        else
        {
            Object_S.Instance.Register(_id, gameObject);
            Object_S.Instance.Register(_id, anim);
        }
    }

}
