using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class OptionsMenu : MonoBehaviour
{
    public Animator optionsMenu;
    public bool sound;
    public bool graphics;
    public bool keyMap;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ReturnToMainMenu()
    {
        SceneManager.UnloadScene(1);
    }

    public void OpenSound()
    {
        if(sound == false)
        optionsMenu.SetTrigger("Sound");
        sound = true;
        graphics = false;
        keyMap = false;
    }

    public void OpenGraphics()
    {
        if(graphics == false)
        optionsMenu.SetTrigger("Graphics");
        sound = false;
        graphics = true;
        keyMap = false;
    }

    public void OpenKeyMap()
    {
        if(keyMap == false)
        optionsMenu.SetTrigger("KeyMap");
        sound = false;
        graphics = false;
        keyMap = true;
    }
}
