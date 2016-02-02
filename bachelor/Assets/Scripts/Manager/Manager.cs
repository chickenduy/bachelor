using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {

    public GameObject player;

    public LevelManager.L_Manager lvl_manager;
    public AnimationManager.A_Manager a_manager;
    public StateManager.S_Manager s_manager;
    public LightingManager.L_Manager l_manager;
    public ObjectManager.O_Manager o_manager;
    public Spawns.Respawn spawn_points;
    public Spawns.WakeSleep wake_sleep_position;

    public GameObject _Rain;
    public GameObject _Ice;
    public GameObject _Fire;
    public GameObject[] _Power;

    private Transform[] respawn_pos;
    private Transform wake_pos;
    private Transform maze_pos;

    public Material highlighted_wall;
    public Material normal_wall;
    public GameObject[] walls;
    public GameObject[] all_walls;

    // Use this for initialization
    void Start () {
        lvl_manager = new LevelManager.L_Manager(GetComponentInChildren<Camera>());
        a_manager = new AnimationManager.A_Manager();
        s_manager = new StateManager.S_Manager();
        l_manager = new LightingManager.L_Manager(_Fire);
        o_manager = new ObjectManager.O_Manager(_Ice, _Fire, _Power, 5);

        GameObject[] spawns = GameObject.FindGameObjectsWithTag("Respawn");
        respawn_pos = new Transform[spawns.Length];
        for(int i = 0; i<spawns.Length; i++)
        {
            respawn_pos[i] = spawns[i].transform;
        }
        spawn_points = new Spawns.Respawn(respawn_pos);

        wake_pos = GameObject.Find("Wake Position").transform;
        maze_pos = GameObject.Find("Maze Position").transform;

        wake_sleep_position = new Spawns.WakeSleep(wake_pos, maze_pos);

        walls = GameObject.FindGameObjectsWithTag("moving wall");
        all_walls = new GameObject[walls.Length+1];
        all_walls[0] = GameObject.Find("Walls");
        for(int i = 1; i < all_walls.Length; i++)
        {
            all_walls[i] = walls[i-1];
        }
    }

    void Update()
    {
        Check_For_Rain();

    }
	
    public void Switch_Light_Main()
    {
        s_manager.light = a_manager.Switch_Light_Main(s_manager.switch_light_main, s_manager.light);
        s_manager.switch_light_main = s_manager.Switch_Light(s_manager.switch_light_main);
        l_manager.light_main.enabled = s_manager.switch_light_main;
    }

    public void Switch_Light_Bathroom()
    {
        a_manager.Switch_Light_Bathroom(s_manager.switch_light_bathroom);
        s_manager.switch_light_bathroom = s_manager.Switch_Light_Bathroom(s_manager.switch_light_bathroom);
        l_manager.light_bathroom.enabled = s_manager.switch_light_bathroom;

    }

    public void Switch_Fan()
    {
        s_manager.wind =  a_manager.Switch_Fan(s_manager.switch_fan);
        s_manager.switch_fan = s_manager.Switch_Fan(s_manager.switch_fan);
    }
    
    public void Door()
    {
        a_manager.Door(s_manager.door);
        s_manager.door = s_manager.Door(s_manager.door);
    }

    public void Drawer()
    {
        a_manager.Drawer(s_manager.drawer, s_manager.lighter);
        s_manager.drawer = s_manager.Drawer(s_manager.drawer);
    }

    public void Toilet()
    {
        a_manager.Toilet(s_manager.toilet);
        s_manager.toilet = s_manager.Toilet(s_manager.toilet);
    }

    public void Window()
    {
        s_manager.temperature = a_manager.Window(s_manager.window,s_manager.temperature);
        s_manager.window = s_manager.Window(s_manager.window);
    }

    public void Logs()
    {
        if (s_manager.lighter)
        {
            if (!s_manager.fireplace)
            {
                l_manager.ManageFire(true);
                s_manager.temperature =  a_manager.Fireplace(s_manager.lighter,s_manager.temperature);
                s_manager.fireplace = s_manager.Fireplace(s_manager.fireplace);
            }
            else
            {
                s_manager.temperature = a_manager.Fireplace(s_manager.lighter, s_manager.temperature);
                l_manager.ManageFire(false);
                s_manager.fireplace = s_manager.Fireplace(s_manager.fireplace);
            }
        }
        else
        {
            //GUI NEED LIGHTER
        }
    }

    public void KillFire(GameObject obj)
    {
        if(s_manager.kill_fires > 0)
        {
            Destroy(obj);
            s_manager.kill_fires--;
        }
    }

    public void Lighter(GameObject obj)
    {
        s_manager.lighter = true;
        Destroy(obj);
    }

    public void Waterbottle(GameObject obj)
    {
        s_manager.waterbottle = true;
        Destroy(obj);
    }

    public bool[] TakePower(GameObject obj, bool[] abilities)
    {
        //Object disappears and you get power = true
        o_manager.Take_Power(obj, abilities);
        
        if(abilities[0])
        {
            //show moving walls for X seconds
            o_manager.Get_Power(highlighted_wall, walls);
            StartCoroutine(Lose_PowerA(15f));
        }
        if(abilities[1])
        {
            //give more Fire Killing
            s_manager.kill_fires = o_manager.Get_Power(s_manager.kill_fires);
        }
        if (abilities[2])
        {
            player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_WalkSpeed = 20;
            StartCoroutine(Lose_PowerC(15f));
            //move through walls
        }
        if (abilities[3])
        {
            Physics.IgnoreCollision(player.GetComponent<CharacterController>(), all_walls[0].GetComponent<MeshCollider>(),true);
            for (int i = 1; i < all_walls.Length; i++)
            {
                Physics.IgnoreCollision(player.GetComponent<CharacterController>(), all_walls[i].GetComponent<BoxCollider>(),true);
            }
            StartCoroutine(Lose_PowerD(5f));
        }


        return abilities;
    }

    public void PlayerLight(bool dream_state)
    {
        l_manager.PlayerLight(dream_state);
    }

    public Vector3 Wake_Sleep(PlayerScript player_script, CameraScript player_camera)
    {
        PlayerLight(!player.GetComponent<PlayerScript>().dream_state);
        _Rain.SetActive(false);
        Debug.Log("Trying to spawn");
        Debug.Log(s_manager.temperature);
        o_manager.SpawnObstacles(s_manager.temperature);
        return wake_sleep_position.Wake_Sleep(player, player_script, player_camera, _Rain);
    }

    public void Drink()
    {
        InvokeRepeating("Load_Pee", 0, 30f);
    }

    private void Load_Pee()
    {
        s_manager.pee = s_manager.pee + 0.1f;
    }

    private void Check_For_Rain()
    {
        //disable Rain while in Room
        _Rain.GetComponent<Renderer>().enabled = player.GetComponent<PlayerScript>().dream_state;
        if (s_manager.pee > 0.5f)
        {
            _Rain.SetActive(true);
            if (s_manager.pee > 0.5f && s_manager.pee < 0.7f)
            {
                player.GetComponentInChildren<EllipsoidParticleEmitter>().minEmission = 3125;
                player.GetComponentInChildren<EllipsoidParticleEmitter>().maxEmission = 3125;
            }
            else if (s_manager.pee > 0.6f && s_manager.pee < 0.8f)
            {
                player.GetComponentInChildren<EllipsoidParticleEmitter>().minEmission = 6250;
                player.GetComponentInChildren<EllipsoidParticleEmitter>().maxEmission = 6250;
            }
            else if (s_manager.pee > 0.7f && s_manager.pee < 0.9f)
            {
                player.GetComponentInChildren<EllipsoidParticleEmitter>().minEmission = 12500;
                player.GetComponentInChildren<EllipsoidParticleEmitter>().maxEmission = 12500;
            }
            else if (s_manager.pee > 0.8f && s_manager.pee < 1f)
            {
                player.GetComponentInChildren<EllipsoidParticleEmitter>().minEmission = 25000;
                player.GetComponentInChildren<EllipsoidParticleEmitter>().maxEmission = 25000;
            }
            else if (s_manager.pee > 0.9f)
            {
                player.GetComponentInChildren<EllipsoidParticleEmitter>().minEmission = 50000;
                player.GetComponentInChildren<EllipsoidParticleEmitter>().maxEmission = 50000;
            }
        }

    }

    IEnumerator Lose_PowerA(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        for (int i = 0; i < walls.Length; i++)
        {
            Material mat = normal_wall;
            walls[i].GetComponent<Renderer>().material = mat;
        }
        player.GetComponent<PlayerScript>().abilities[0] = false;

    }

    IEnumerator Lose_PowerD(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Physics.IgnoreCollision(player.GetComponent<CharacterController>(), all_walls[0].GetComponent<MeshCollider>(), false);
        for (int i = 1; i < all_walls.Length; i++)
        {
            Physics.IgnoreCollision(player.GetComponent<CharacterController>(), all_walls[i].GetComponent<BoxCollider>(), false);
        }
    }
    IEnumerator Lose_PowerC(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_WalkSpeed = 7;

    }

}
