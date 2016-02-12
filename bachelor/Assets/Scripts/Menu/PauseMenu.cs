using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class PauseMenu : MonoBehaviour {
    private GameObject player_cam;
    private Camera player_camera;
    private Camera pause_camera;
    private FirstPersonController m_mouselook;

	// Use this for initialization
	void Start () {
        player_cam = GameObject.Find("FirstPersonCharacter");
        player_camera = player_cam.GetComponent<Camera>();
        m_mouselook = FindObjectOfType<FirstPersonController>();
        pause_camera = GetComponentInChildren<Camera>();

        player_cam.GetComponent<GUIScript>().enabled = false;
        pause_camera.enabled = true;
        player_camera.enabled = false;
        m_mouselook.m_MouseLook.SetCursorLock(false);
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void ResumeGame()
    {
        player_cam.GetComponent<GUIScript>().enabled = true;
        player_camera.enabled = true;
        pause_camera.enabled = false;
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
        GetComponentInChildren<Camera>().enabled = false;
        SceneManager.LoadScene(3, LoadSceneMode.Additive);

    }
}
