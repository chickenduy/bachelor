using UnityEngine;
using System.Collections;

public class Wall_Final_OBJ : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        Wall_S.Instance.Register(gameObject, gameObject.tag);
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            col.transform.position = new Vector3(0, 10, 0);
        }
    }

}
