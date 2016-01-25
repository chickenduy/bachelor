using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(4);
    }

    public void OptionsMenu()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }

    public void QuitMenu()
    {
        SceneManager.LoadScene(3, LoadSceneMode.Additive);
    }

    public void LoadGame()
    {

    }

}

