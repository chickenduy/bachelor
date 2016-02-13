using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Maze_S : Singleton<Maze_S>
{
    // guarantee this will be always a singleton only - can't use the constructor!
    protected Maze_S() { }

    //variables
    private Transform wake_position;
    private Transform maze_position;
    private Dictionary<int, Transform> respawn_dictionary = new Dictionary<int, Transform>();
    private Dictionary<GameObject, int> maze_room_dictionary = new Dictionary<GameObject, int>();
    private Dictionary<int, Transform> maze_position_dictionary = new Dictionary<int, Transform>();
    private Dictionary<Renderer, int> mirror_dictionary = new Dictionary<Renderer, int>();
    private Dictionary<int, List<Light>> light_dictionary = new Dictionary<int, List<Light>>();
    private Dictionary<int, List<ParticleSystem>> particle_dictionary = new Dictionary<int, List<ParticleSystem>>();

    public Material mirror;
    public Material[] rooms = new Material[4];
    public bool[] room_discovered = new bool[4];

    // Use this for initialization
    void Awake()
    {

    }



    //methods
    //register important spawn points
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

    //register room
    public void Register(GameObject obj, int id)
    {
        maze_room_dictionary.Add(obj, id);
        maze_position_dictionary.Add(id, obj.transform);
    }

    //register renderer
    public void Register(Renderer ren, int id)
    {
        mirror_dictionary.Add(ren, id);
    }

    //register light
    public void Register(Light lightobj, int id)
    {
        if (light_dictionary.ContainsKey(id))
            light_dictionary[id].Add(lightobj);
        else
        {
            List<Light> l_list = new List<Light>();
            l_list.Add(lightobj);
            light_dictionary.Add(id, l_list);
        }
    }

    //register particles system
    public void Register(ParticleSystem ps, int id)
    {
        if (particle_dictionary.ContainsKey(id))
            particle_dictionary[id].Add(ps);
        else
        {
            List<ParticleSystem> ps_list = new List<ParticleSystem>();
            ps_list.Add(ps);
            particle_dictionary.Add(id, ps_list);
        }
    }

    public void Initial_Spawn()
    {
        int i = Random.Range(0, respawn_dictionary.Count);
        Player_S.Instance.transform.position = respawn_dictionary[i].position;
    }

    //spawn player to a random discovered room
    public void Respawn_Player()
    {
        if (!room_discovered[0] && !room_discovered[1] && !room_discovered[2] && !room_discovered[3])
        {
            Debug.Log("No Room discovered yet");
            return;
        }
        int i = Random.Range(0, respawn_dictionary.Count);
        if (room_discovered[i])
            Player_S.Instance.transform.position = respawn_dictionary[i].position;
        else
            Respawn_Player();
    }

    //spawn player to a specific room
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

    //check if the id is present in respawn_dictionary
    public bool Check_For_ID(int id)
    {
        if (respawn_dictionary.ContainsKey(id))
            return true;
        else
            return false;
    }

    //enable light with id
    public void Turn_On_Light(int id)
    {
        foreach (Light light in light_dictionary[id])
            light.enabled = true;
    }

    //enable fire with id
    public void Turn_On_Fire(int id)
    {
        foreach (ParticleSystem ps in particle_dictionary[id])
        {
            ParticleSystem.EmissionModule em = ps.emission;
            em.enabled = true;
        }
    }

    //function is called when room is entered
    public void Enter_Room(int id, GameObject obj)
    {
        Material mat = rooms[id];
        foreach (KeyValuePair<Renderer, int> mirror in mirror_dictionary)
        {
            if (mirror.Value == id)
            {
                mirror.Key.material = mat;
                mirror.Key.gameObject.GetComponent<MirrorReflection>().enabled = false;
            }
        }
        Turn_On_Light(id);
        Turn_On_Fire(id);
        room_discovered[id] = true;
    }

    //function is called when final room is entered
    public void Enter_Room_Final(int id, GameObject obj)
    {
        obj.GetComponent<BoxCollider>().enabled = false;
        Turn_On_Light(id);
        Turn_On_Fire(id);
    }

    //get the id of the room
    public int Get_ID(GameObject obj)
    {
        return maze_room_dictionary[obj];
    }

    //check if key has been picked up
    public bool Check_For_Key(int id)
    {
        if (maze_room_dictionary.ContainsValue(id))
            return true;
        return false;
    }

    //checks if room #id has been discovered
    public bool Get_Discovered(int id)
    {
        return room_discovered[id];
    }

}
