using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class QuitGame : MonoBehaviour
{
    private GameObject pause_menu;
    private Camera pause_camera;
    private GameObject main_menu;
    private Camera main_menu_camera;
    private Camera quit_camera;

    // Use this for initialization
    void Start()
    {
        //if called from Main Menu -> Pause Menu is null
        pause_menu = GameObject.Find("PauseScene");
        if (pause_menu != null)
        {
            pause_camera = pause_menu.GetComponentInChildren<Camera>();
        }
        print(pause_menu);
        //if called from Pause Menu -> Main Menu is null
        main_menu = GameObject.Find("MainMenuScene");
        if (main_menu != null)
        {
            main_menu_camera = main_menu.GetComponentInChildren<Camera>();
        }
        print(main_menu);
        quit_camera = GetComponentInChildren<Camera>();
    }        


    // Update is called once per frame
    void Update()
    {

    }

    public void ReturnToMenu()
    {
        if (pause_camera != null)
        {
            pause_camera.enabled = true;
            quit_camera.enabled = false;
        }
        else if (main_menu_camera != null)
        {
            main_menu_camera.enabled = true;
            quit_camera.enabled = false;
        }
        SceneManager.UnloadScene(3);
    }

    public void QuitTheGame()
    {
        Application.Quit();
    }
}
