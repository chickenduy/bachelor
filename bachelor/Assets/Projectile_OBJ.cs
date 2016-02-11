using UnityEngine;
using System.Collections;

public class Projectile_OBJ : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.transform.LookAt(Player_S.Instance.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.Translate(new Vector3(0,0,0.05f), Space.Self);
	}
}
