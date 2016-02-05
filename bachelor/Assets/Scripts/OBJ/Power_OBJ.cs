using UnityEngine;
using System.Collections;

public class Power_OBJ : MonoBehaviour
{
    void Start()
    {
        //register object in Singleton dictionary
        Power_S.Instance.Register(gameObject);
    }


}




