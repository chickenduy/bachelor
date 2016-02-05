using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawns_S : Singleton<Spawns_S>
{
    // guarantee this will be always a singleton only - can't use the constructor!
    protected Spawns_S() { }

    //variables
    private Transform wake_position;
    private Transform maze_position;
    private Dictionary<int, Transform> respawn_dictionary = new Dictionary<int, Transform>();

    //methods
    public void Register(int id, Transform trans, string tag)
    {

        if (tag == "respawn")
        {
            if (!respawn_dictionary.ContainsKey(id))
            {
                respawn_dictionary.Add(id, trans);
            }
            else
            {
                Register(id + 1, trans, tag);
            }
        }
        else if (tag == "wake")
            wake_position = trans;
        else if (tag == "sleep")
            maze_position = trans;
        else
            Debug.LogError("SPAWN - Wrong Tag");

    }

    public void RespawnPlayer()
    {
        int i = Random.Range(0, respawn_dictionary.Count);
        Player_S.Instance.transform.position = respawn_dictionary[i].transform.position;
    }

    //saves position in maze, teleports to room and back to maze; sets dreamState
    public void Wake_Sleep()
    {
        if (Player_S.Instance.dream_state)
        {
            maze_position.position = Player_S.Instance.transform.position;
            Player_S.Instance.dream_state = false;
            Camera_S.Instance.fog.enabled = false;
            Camera_S.Instance.blur.enabled = false;
            Player_S.Instance.transform.position = wake_position.transform.position;
        }

        else
        {
            Player_S.Instance.dream_state = true;
            Player_S.Instance.transform.position = maze_position.transform.position;
        }
    }
}