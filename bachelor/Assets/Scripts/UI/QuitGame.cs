using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class QuitGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ReturnToMenu()
    {
        SceneManager.UnloadScene(3);
    }

    public void QuitTheGame()
    {
        Application.Quit();
    }
}
