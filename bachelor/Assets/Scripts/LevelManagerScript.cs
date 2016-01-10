using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManagerScript : MonoBehaviour
{
    public Canvas startMenu;
    public Canvas optionsMenu;

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
        startMenu = startMenu.GetComponent<Canvas>();
        optionsMenu = optionsMenu.GetComponent<Canvas>();
        play = play.GetComponent<Button>();
        options = options.GetComponent<Button>();
        quit = quit.GetComponent<Button>();

        volumeControl = volume.GetComponentInChildren<Slider>();
        resolutionControl = resolution.GetComponentInChildren<Dropdown>();
        screenOptions = screen.GetComponentInChildren<Dropdown>();

        optionsMenu.enabled = false;
        resolution.SetActive(false);
        volume.SetActive(false);
        screen.SetActive(false);

        volumeControl.value = 100;
        resolutionControl.value = 2;
        screenOptions.value = 0;
    }

    public void openGraphics()
    {
        startMenu.enabled = false;
        optionsMenu.enabled = true;
        resolution.SetActive(true);
        volume.SetActive(false);
        screen.SetActive(true);
    }

    public void openSound()
    {
        startMenu.enabled = false;
        optionsMenu.enabled = true;
        resolution.SetActive(false);
        volume.SetActive(true);
        screen.SetActive(false);
    }

    public void openControls()
    {
        startMenu.enabled = false;
        optionsMenu.enabled = true;
        resolution.SetActive(false);
        volume.SetActive(false);
        screen.SetActive(false);
    }

    public void backButton(){
        startMenu.enabled = true;
        optionsMenu.enabled = false;
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
        AudioListener.volume = volumeControl.value;
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
            Screen.SetResolution(1920, 1080, true);
        }
        else if (screenOptions.value == 2)
        {
            print("not avaible yet");
        }
        else
        {
            print("ERROR");
            throw new System.Exception();
        }
    }
}
