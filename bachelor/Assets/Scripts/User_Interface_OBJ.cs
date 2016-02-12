using UnityEngine;
using System.Collections;

public class User_Interface_OBJ : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        User_Interface_S.Instance.Register(gameObject, gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
