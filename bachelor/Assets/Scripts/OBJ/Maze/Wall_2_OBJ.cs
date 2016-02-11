using UnityEngine;
using System.Collections;

public class Wall_2_OBJ : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        Wall_S.Instance.Register(gameObject, gameObject.tag);
    }

}
