using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class PauseMenu : MonoBehaviour {
    private Camera player_Camera;

	// Use this for initialization
	void Start () {
        player_Camera = GameObject.Find("FPSController").GetComponentInChildren<Camera>(); ;
        player_Camera.enabled = false;
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
        player_Camera.enabled = true;
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
        gameObject.SetActive(false);
        SceneManager.LoadScene(3, LoadSceneMode.Additive);

    }
}
