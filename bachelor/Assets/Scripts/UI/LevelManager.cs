using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    public class L_Manager
    {
        private Camera active_camera;

        public L_Manager(Camera game_camera)
        {
            active_camera = game_camera;
        }

        public void ChangeScene(int scene)
        {
            active_camera.enabled = false;
            SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        }

        public void ChangeSceneBack(int scene)
        {
            SceneManager.UnloadScene(scene);
            active_camera.enabled = true;
        }
    }
}
