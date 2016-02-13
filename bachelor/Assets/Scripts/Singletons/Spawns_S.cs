using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;

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
            respawn_dictionary.Add(id, trans);
        else if (tag == "wake")
            wake_position = trans;
        else if (tag == "sleep")
            maze_position = trans;
        else
            Debug.LogError("Something wrong in Spawn_S/Register");
    }

    public void Respawn_Player()
    {
        //spawn player to a random room
        int i = Random.Range(0, respawn_dictionary.Count);
        Player_S.Instance.transform.position = respawn_dictionary[i].position;
    }

    public void Respawn_Player(int id)
    {
        Player_S.Instance.transform.position = respawn_dictionary[id].position;

    }

    //saves position in maze, teleports to room and back to maze; sets dreamState
    public void Wake_Sleep()
    {
        Player_S.Instance.invincible = true;
        //if Player is dreaming and going to wake up
        if (Player_S.Instance.Get_Dream_State())
        {
            //check if player goes to sleep on the couch
            if (Player_S.Instance.Get_Sleep_On_Couch())
                //checks if fire is on
                if (Object_S.Instance.Get_Fire())
                    //then decrease temperature because he is awake
                    Room_S.Instance.Temperature_Lower();
            //save the current position of player in maze_position gameobject
            maze_position.position = Player_S.Instance.gameObject.transform.position;
            //set dream_state to false and diasable fog and blur
            Player_S.Instance.Set_Dream_State(false);
            Camera_S.Instance.fog.enabled = false;
            Camera_S.Instance.blur.enabled = false;
            //start wake up sequence
            Camera_S.Instance.Wake_Up_Anim();
            //set position of player to position in room
            Player_S.Instance.gameObject.transform.position = wake_position.position;
            //not sleeping on the couch anymore
            Player_S.Instance.Set_Sleep_On_Couch(false);
        }
        //if player is awake and going to sleep
        else if (!Player_S.Instance.Get_Dream_State())
        {
            Player_S.Instance.invincible = true;
            //check if player goes to sleep on the couch
            if (Player_S.Instance.Get_Sleep_On_Couch())
                //checks if fire is on
                if (Object_S.Instance.Get_Fire())
                    //then increase temperature because sleeping next to open fire
                    Room_S.Instance.Temperature_Higher();
            //set dream_state to true 
            Player_S.Instance.Set_Dream_State(true);

            Camera_S.Instance.fog.enabled = true;

            //start going to sleep sequence
            Camera_S.Instance.Go_To_Sleep_Anim();
            //set position of player to the saved position before
            Player_S.Instance.gameObject.transform.position = maze_position.transform.position;
        }
    }

    public bool Check_For_ID(int id)
    {
        if (respawn_dictionary.ContainsKey(id))
            return true;
        else
            return false;
    }

}