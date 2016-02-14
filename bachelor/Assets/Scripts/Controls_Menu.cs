using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Controls_Menu : MonoBehaviour {

	public void Continue()
    {
        SceneManager.LoadScene(2);
    }
}
