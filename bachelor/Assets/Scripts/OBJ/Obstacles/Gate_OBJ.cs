using UnityEngine;
using System.Collections;

public class Gate_OBJ : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Object_S.Instance.Register(gameObject, "gate");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
