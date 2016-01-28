using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class QuitGame : MonoBehaviour {
    private PauseMenu pause_menu;
	// Use this for initialization
	void Start () {
        pause_menu = FindObjectOfType<PauseMenu>();
        print(pause_menu);
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
