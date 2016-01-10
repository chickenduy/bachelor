using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManagerScript : MonoBehaviour
{
    public Canvas menu;

    public GameObject startMenu;

    public GameObject optionsMenu;


    public Button play;
    public Button options;
    public Button quit;

    public Button graphics;
    public Button sound;
    public Button controls;

    public GameObject resolution;
    public GameObject volume;
    public GameObject screen;


    private Slider volumeControl;
    private Dropdown resolutionControl;
    private Dropdown screenOptions;
    // Use this for initialization
    void Start()
    {
        Screen.SetResolution(2560,1440,true);
        Screen.fullScreen = false;

        //menu = menu.GetComponent<Canvas>();

        //startMenu = startMenu.GetComponent<GameObject>();
        //optionsMenu = optionsMenu.GetComponent<GameObject>();

        //play = play.GetComponent<Button>();
        //options = options.GetComponent<Button>();
        //quit = quit.GetComponent<Button>();

        volumeControl = volume.GetComponentInChildren<Slider>();
        resolutionControl = resolution.GetComponentInChildren<Dropdown>();
        screenOptions = screen.GetComponentInChildren<Dropdown>();

        optionsMenu.SetActive(false);
        resolution.SetActive(false);
        volume.SetActive(false);
        screen.SetActive(false);

        volumeControl.value = 100;
        resolutionControl.value = 2;
        screenOptions.value = 0;
    }

    public void openGraphics()
    {
        startMenu.SetActive(false);
        optionsMenu.SetActive(true);
        resolution.SetActive(true);
        volume.SetActive(false);
        screen.SetActive(true);
    }

    public void openSound()
    {
        startMenu.SetActive(false);
        optionsMenu.SetActive(true);
        resolution.SetActive(false);
        volume.SetActive(true);
        screen.SetActive(false);
    }

    public void openControls()
    {
        startMenu.SetActive(false);
        optionsMenu.SetActive(true);
        resolution.SetActive(false);
        volume.SetActive(false);
        screen.SetActive(false);
    }

    public void backButton(){
        startMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void setDefault()
    {

    }

    public void startGame()
    {
        SceneManager.LoadScene(1);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void setVolume()
    {
        //AudioListener.volume = volumeControl.value;
    }

    public void setResolution()
    {
        if (resolutionControl.value == 0)
        {
            Screen.SetResolution(2560, 1440,true);
        }
        else if (resolutionControl.value == 1)
        {
            Screen.SetResolution(1920, 1080, true);
        }
        else if(resolutionControl.value == 2)
        {
            Screen.SetResolution(1280, 720, true);
        }
        else
        {
            print("ERROR");
            throw new System.Exception();
        }
    }

    public void setScreen()
    {
        if (screenOptions.value == 0)
        {
            Screen.fullScreen = true;
        }
        else if (screenOptions.value == 1)
        {
            Screen.fullScreen = false;
        }
        else
        {
            print("ERROR");
            throw new System.Exception();
        }
    }

    public void openStartMenu()
    {
        optionsMenu.SetActive(false);
        startMenu.SetActive(true);
    }
}
