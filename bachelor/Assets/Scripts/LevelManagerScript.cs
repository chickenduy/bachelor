using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("k"))
        {
            Application.LoadLevelAdditive("Maze");
            //SceneManager.LoadSceneAsync("Maze");
            print("load");
        }
	}
}
