﻿using UnityEngine;
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
        if (tag == "Respawn")
        {
            respawn_dictionary.Add(id, trans);
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
        //spawn player to a random room
        int i = Random.Range(0, respawn_dictionary.Count);
        Player_S.Instance.transform.position = respawn_dictionary[i].position;
    }

    //saves position in maze, teleports to room and back to maze; sets dreamState
    public void Wake_Sleep()
    {
        //if Player is dreaming and going to wake up
        if (Player_S.Instance.dream_state)
        {
            //save the current position of player in maze_position gameobject
            maze_position.position = Player_S.Instance.transform.position;
            //set dream_state to false and diasable fog and blur
            Player_S.Instance.dream_state = false;
            Camera_S.Instance.fog.enabled = false;
            Camera_S.Instance.blur.enabled = false;
            //set position of player to position in room
            Player_S.Instance.transform.position = wake_position.position;
        }
        //if player is awake and going to sleep
        else
        {
            //set dream_state to true 
            Player_S.Instance.dream_state = true;
            //set position of player to the saved position before
            Player_S.Instance.transform.position = maze_position.position;
        }
    }

    public bool Check_For_ID(int id)
    {
        if (respawn_dictionary.ContainsKey(id))
        {
            return true;
        }
        return false;
    }
}