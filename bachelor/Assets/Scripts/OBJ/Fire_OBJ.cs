using UnityEngine;
using System.Collections;

public class Fire_OBJ : MonoBehaviour
{
    void Start()
    {
        Fire_S.Instance.Register(gameObject);
    }


}




