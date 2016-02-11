using UnityEngine;
using System.Collections;

public class Ice_OBJ : MonoBehaviour
{
    void Start()
    {
        Ice_S.Instance.Register(gameObject);
    }


}




