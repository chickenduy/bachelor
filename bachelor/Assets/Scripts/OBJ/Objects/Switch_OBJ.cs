using UnityEngine;
using System.Collections;

public class Switch_OBJ : MonoBehaviour
{
    private int _id = 0;
    public int id
    {
        get
        {
            return _id;
        }
    }
    private Animator anim;

    // Use this for initialization
    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        //register object in Singleton dictionary
        Check_For_ID();
    }

    public void Check_For_ID()
    {
        if (Object_S.Instance.Check_For_ID(_id))
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
