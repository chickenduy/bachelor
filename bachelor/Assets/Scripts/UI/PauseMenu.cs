using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    private Camera player_camera;
	// Use this for initialization
	void Start () {
        player_camera = GameObject.Find("FPSController").GetComponentInChildren<Camera>();
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
        player_camera.enabled = true;
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
