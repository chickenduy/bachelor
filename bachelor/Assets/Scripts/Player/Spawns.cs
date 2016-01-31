using UnityEngine;
using System.Collections;

public class Spawns : MonoBehaviour {

    public class Respawn
    {
        public Transform[] respawn_positions;

        public Respawn(Transform[] spawns)
        {
            respawn_positions = spawns;
        }

        public Vector3 RespawnPlayer()
        {
            int i = Random.Range(0, respawn_positions.Length);
            return respawn_positions[i].transform.position;
        }
    }	

    public class WakeSleep
    {
        public Transform wake_position;
        public Transform maze_position;

        public WakeSleep(Transform wake, Transform maze)
        {
            wake_position = wake;
            maze_position = maze;
        }

        

        //saves position in maze, teleports to room and back to maze; sets dreamState
        public Vector3 Wake_Sleep(GameObject player, PlayerScript script, CameraScript camera, GameObject rain)
        {
            if (script.dream_state)
            {
                rain.SetActive(false);
                maze_position.position = player.GetComponent<Transform>().position;
                script.dream_state = false;
                camera.fog.enabled = false;
                camera.blur.enabled = false;
                return wake_position.transform.position;
            }
            else
            {
                rain.SetActive(true);
                script.dream_state = true;
                return maze_position.transform.position;
            }
        }
    }
}
