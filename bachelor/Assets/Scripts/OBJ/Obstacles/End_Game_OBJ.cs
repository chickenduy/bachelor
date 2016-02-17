using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class End_Game_OBJ : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        SceneManager.LoadScene(0);
    }
}
