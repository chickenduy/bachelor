using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject _Main_Menu;
    public GameObject _Quit_Menu;

    public void Start_Game()
    {
        SceneManager.LoadScene(1);
    }

    public void Main_Menu()
    {
        _Main_Menu.SetActive(true);
        _Quit_Menu.SetActive(false);
    }
    public void Quit_Menu()
    {
        _Quit_Menu.SetActive(true);
        _Main_Menu.SetActive(false);
    }

    public void Quit_Game()
    {
        Application.Quit();
    }







}

