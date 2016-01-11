using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadRoom : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
