using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SaveLoad : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SaveGame()
    {

    }

    public void LoadGame()
    {

    }

    public void ReturnToMenu()
    {
        SceneManager.UnloadScene(1);
    }
}
