using UnityEngine;
using System.Collections;

public class Radio_Music_OBJ : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Background_Music_S.Instance.Register(GetComponent<AudioSource>());
	}
	
}
