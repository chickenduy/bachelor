using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.UnloadScene(2);
    }

    public void SaveMenu()
    {
    }

    public void OptionsMenu()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Additive);

    }

    public void QuitGame()
    {
            SceneManager.LoadScene(3, LoadSceneMode.Additive);

    }
}
