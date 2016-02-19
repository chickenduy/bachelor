using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Maze_S : Singleton<Maze_S>
{
    // guarantee this will be always a singleton only - can't use the constructor!
    protected Maze_S() { }

    //variables

    //private
    private Transform wake_position_bed;
    private Transform wake_position_couch;
    private Transform maze_position;
    private Dictionary<GameObject, int> maze_room_dictionary = new Dictionary<GameObject, int>();
    private Dictionary<int, Transform> respawn_dictionary = new Dictionary<int, Transform>();
    private Dictionary<int, Transform> maze_position_dictionary = new Dictionary<int, Transform>();
    private Dictionary<int, List<Light>> light_dictionary = new Dictionary<int, List<Light>>();
    private Dictionary<int, List<ParticleSystem>> particle_dictionary = new Dictionary<int, List<ParticleSystem>>();
    private Dictionary<Renderer, int> mirror_dictionary = new Dictionary<Renderer, int>();

    //private visible
    [SerializeField]
    private Material[] rooms = new Material[4];

    //getter/setter
    private bool[] _room_discovered = new bool[4];
    public bool[] room_discovered
    {
        get
        {
            return _room_discovered;
        }

    }

    //getter/setter visible
    [SerializeField]
    private Material _mirror;
    public Material mirror
    {
        get
        {
            return _mirror;
        }
    }

    /*----------------------------------------------------------------------------------------------------*/

    //methods
    //register important spawn points
    public void Register(int id, Transform trans, string tag)
    {
        if (tag == "Respawn")
            respawn_dictionary.Add(id, trans);
        else if (tag == "Wake Position Bed")
            wake_position_bed = trans;
        else if (tag == "Wake Position Couch")
            wake_position_couch = trans;
        else if (tag == "Maze Position")
            maze_position = trans;
        else
            throw new System.Exception("Something wrong in Spawn_S/Register");
    }
    //register room with id
    //assign the ids manually
    public void Register(GameObject obj, int id)
    {
        //add the room to a list
        maze_room_dictionary.Add(obj, id);
        //add the position to list
        maze_position_dictionary.Add(id, obj.transform);
    }
    //register renderer
    public void Register(Renderer ren, int id)
    {
        //add the mirror renderer to a list
        mirror_dictionary.Add(ren, id);
    }
    //register light
    public void Register(Light lightobj, int id)
    {
        //if the id is already existing
        if (light_dictionary.ContainsKey(id))
            //add the light object to the list 
            light_dictionary[id].Add(lightobj);
        //otherwise
        else
        {
            //create a new list with that id
            List<Light> l_list = new List<Light>();
            //and add that light to the list
            l_list.Add(lightobj);
            //then add the list to the dictionary
            light_dictionary.Add(id, l_list);
        }
    }
    //register particles system
    public void Register(ParticleSystem ps, int id)
    {
        //if the id is already existing
        if (particle_dictionary.ContainsKey(id))
            //add the particle system to the list 
            particle_dictionary[id].Add(ps);
        //otherwise
        else
        {
            //create a new list with that id
            List<ParticleSystem> ps_list = new List<ParticleSystem>();
            //and add that light to the list
            ps_list.Add(ps);
            //then add the list to the dictionary
            particle_dictionary.Add(id, ps_list);
        }
    }
    //first spawn 
    public void Initial_Spawn()
    {
        //choose a random room
        int i = Random.Range(0, respawn_dictionary.Count);
        Vector3 pos = new Vector3(respawn_dictionary[i].position.x, respawn_dictionary[i].position.y + 10, respawn_dictionary[i].position.z);
        //spawn the player in a random room
        Player_S.Instance.transform.position = pos;
    }

    //spawn player to a random discovered room
    public void Respawn_Player()
    {
        //if there is no room discovered (there should always be at least 1 because of initial spawn)
        if (!room_discovered[0] && !room_discovered[1] && !room_discovered[2] && !room_discovered[3])
        {
            throw new System.Exception("No Room discovered yet - Error in Maze_S/Respawn_Player");
        }
        //create a new list
        List<int> random_room = new List<int>();
        //add all the rooms that have been discovered
        for (int id = 0; id < room_discovered.Length; id++)
        {
            if (room_discovered[id])
            {
                random_room.Add(id);
            }
        }
        //pick one of the discovered rooms
        int number = Random.Range(0, random_room.Count);
        //spawn player to that room
        Player_S.Instance.transform.position = respawn_dictionary[random_room[number]].position;
    }
    //set maze position to a random discoverd room
    public void Random_Room_Position()
    {
        //create a new list
        List<int> random_room = new List<int>();
        //add all discovered rooms
        for (int i = 0; i < room_discovered.Length; i++)
        {
            if (room_discovered[i])
            {
                random_room.Add(i);
            }
        }
        //pick one of the discovered rooms
        int number = Random.Range(0, random_room.Count);
        //change position of maze_position to that room
        maze_position.position = respawn_dictionary[random_room[number]].position;
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
        if (Player_S.Instance.dream_state)
        {
            //save the current position of player in maze_position gameobject
            maze_position.position = Player_S.Instance.gameObject.transform.position;
            //check if player went to sleep on the couch
            if (Player_S.Instance.couch)
            {
                //checks if fireplace is on
                if (Object_S.Instance.fireplace)
                    //then decrease temperature because he is awake
                    Room_S.Instance.Temperature_Lower();
                //set the spawn the player at the couch
                Player_S.Instance.gameObject.transform.position = wake_position_couch.position;
            }
            //otherwise spawn the player at the bed
            else
                Player_S.Instance.gameObject.transform.position = wake_position_bed.position;
            //set dream_state to false and diasable fog and blur and motion
            Player_S.Instance.dream_state = false;
            Camera_S.Instance.fog.enabled = false;
            Camera_S.Instance.blur.enabled = false;
            Camera_S.Instance.motion.enabled = false;
            //start wake up animation depending on the couch
            Camera_S.Instance.Wake_Up_Anim(Player_S.Instance.couch);
            //not sleeping on the couch anymore
            Player_S.Instance.couch = false;
            //reset the ability to do a reality check
            Player_S.Instance.reality_check = false;
        }
        //if player is awake and going to sleep
        else if (!Player_S.Instance.dream_state)
        {
            //check if player goes to sleep on the couch
            if (Player_S.Instance.couch)
                //checks if fire is on
                if (Object_S.Instance.fireplace)
                    //then increase temperature because sleeping next to open fire
                    Room_S.Instance.Temperature_Higher();
            //set position of player to the saved position before
            Player_S.Instance.gameObject.transform.position = maze_position.transform.position;
            //set dream_state to true and enable fog, blur and motion
            Player_S.Instance.dream_state = true;
            Camera_S.Instance.fog.enabled = true;
            Camera_S.Instance.blur.enabled = true;
            Camera_S.Instance.motion.enabled = true;
            //start going to sleep sequence
            Camera_S.Instance.Go_To_Sleep_Anim(Player_S.Instance.couch);
            //reset the ability to do a reality check
            Player_S.Instance.reality_check = false;

        }
    }
    //function called if not waking up willingly
    public void Wake_Sleep_Hit()
    {
        Player_S.Instance.invincible = true;
        //check if player went to sleep on the couch
        if (Player_S.Instance.couch)
            //checks if fire is on
            if (Object_S.Instance.fireplace)
                //then decrease temperature because he is awake
                Room_S.Instance.Temperature_Lower();
        //set random room position
        Random_Room_Position();
        //set dream_state to false and diasable fog, blur and motion
        Player_S.Instance.dream_state = false;
        Camera_S.Instance.fog.enabled = false;
        Camera_S.Instance.blur.enabled = false;
        Camera_S.Instance.motion.enabled = false;
        //start wake up animation depending on the couch
        Camera_S.Instance.Wake_Up_Anim(Player_S.Instance.couch);
        //set position of player to position in room
        Player_S.Instance.gameObject.transform.position = wake_position_bed.position;
        //not sleeping on the couch anymore
        Player_S.Instance.couch = false;
        //reset the ability to do a reality check
        Player_S.Instance.reality_check = false;

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
    //function when room is entered
    public void Enter_Room(int id, GameObject obj)
    {
        //get the material of a room
        Material mat = rooms[id];
        //iterate through all mirror parts
        foreach (KeyValuePair<Renderer, int> mirror in mirror_dictionary)
        {
            if (mirror.Value == id)
            {
                //change the material of the material
                mirror.Key.material = mat;
                mirror.Key.gameObject.GetComponent<MirrorReflection>().enabled = false;
            }
        }
        //turn on all the lights in the room
        Turn_On_Light(id);
        //turn on all the fires in the room
        Turn_On_Fire(id);
        //set the discovered flag to true
        room_discovered[id] = true;
        //disable the collider on the room
        obj.GetComponent<BoxCollider>().enabled = false;
    }
    //function when final room is entered
    public void Enter_Room_Final(int id, GameObject obj)
    {
        //turn on all the lights in the room
        Turn_On_Light(id);
        //turn on all fires in the room
        Turn_On_Fire(id);
        //disable collider on the room
        obj.GetComponent<BoxCollider>().enabled = false;
    }
    //get the id of the room
    public int Get_ID(GameObject obj)
    {
        return maze_room_dictionary[obj];
    }
}
