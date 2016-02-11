using UnityEngine;
using System.Collections;

public class Fan_OBJ : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Object_S.Instance.Register(gameObject, "fan");
	}
}
