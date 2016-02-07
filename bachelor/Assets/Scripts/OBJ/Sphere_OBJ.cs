using UnityEngine;
using System.Collections;

public class Sphere_OBJ : MonoBehaviour {

	
    public void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            Debug.Log("HIT PLAYER");
            col.transform.position = new Vector3(0, 0, 0);
        }
    }
}
