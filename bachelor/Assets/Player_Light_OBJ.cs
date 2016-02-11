using UnityEngine;
using System.Collections;

public class Player_Light_OBJ : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        Player_S.Instance.Register(gameObject);
    }
}
