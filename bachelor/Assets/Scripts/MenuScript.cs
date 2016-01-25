using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject optionsMenu;
    public GameObject exitMenu;

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
        volumeControl = volume.GetComponentInChildren<Slider>();
        resolutionControl = resolution.GetComponentInChildren<Dropdown>();
        screenOptions = screen.GetComponentInChildren<Dropdown>();

        optionsMenu.SetActive(false);
        exitMenu.SetActive(false);

        resolution.SetActive(false);
        volume.SetActive(false);
        screen.SetActive(false);

        volumeControl.value = 100;
        resolutionControl.value = 2;
        screenOptions.value = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            SceneManager.LoadScene(1);
        }
        if (Input.GetKeyDown("q"))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown("o"))
        {
            OpenGraphicsOptions();
        }
    }

    public void OpenStartMenu()
    {
        optionsMenu.SetActive(false);
        startMenu.SetActive(true);
        exitMenu.SetActive(false);
    }

    public void OpenGraphicsOptions()
    {
        startMenu.SetActive(false);
        optionsMenu.SetActive(true);

        resolution.SetActive(true);
        volume.SetActive(false);
        screen.SetActive(true);
    }

    public void OpenSoundOptions()
    {
        resolution.SetActive(false);
        volume.SetActive(true);
        screen.SetActive(false);
    }

    public void OpenControlOptions()
    {
        resolution.SetActive(false);
        volume.SetActive(false);
        screen.SetActive(false);
    }

    public void BackButton(){
        startMenu.SetActive(true);
        optionsMenu.SetActive(false);
        exitMenu.SetActive(false);

    }

    public void SetDefault()
    {

    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        startMenu.SetActive(false);
        exitMenu.SetActive(true);
    }

    public void SetVolume()
    {
        AudioListener.volume = volumeControl.value;
    }

    public void SetResolution()
    {
        if (resolutionControl.value == 0)
        {
            Screen.SetResolution(2560, 1440, Screen.fullScreen);
        }
        else if (resolutionControl.value == 1)
        {
            Screen.SetResolution(1920, 1080, Screen.fullScreen);
        }
        else if(resolutionControl.value == 2)
        {
            Screen.SetResolution(1280, 720, Screen.fullScreen);
        }
        else
        {
            print("ERROR");
            throw new System.Exception();
        }
    }

    public void SetScreen()
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

    public void PressYes()
    {
        Application.Quit();
    }
}
