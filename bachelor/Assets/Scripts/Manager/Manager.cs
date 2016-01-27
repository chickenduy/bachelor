using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {

    public LevelManager.L_Manager lvl_manager;
    public AnimationManager.A_Manager a_manager;
    public StateManager.S_Manager s_manager;
    public LightingManager.L_Manager l_manager;
    public ObjectManager.O_Manager o_manager;
    public Spawns.Respawn spawn_points;
    public Spawns.WakeSleep wake_sleep_position;

    public GameObject _Ice;
    public GameObject _Fire;
    public GameObject[] _Power;

    private Transform[] respawn_pos;
    private Transform wake_pos;
    private Transform maze_pos;

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
    }
	
    public void Switch_Light_Main()
    {
        a_manager.Switch_Light_Main(s_manager.switch_light_main);
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
        a_manager.Switch_Fan(s_manager.switch_fan);
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
        a_manager.Window(s_manager.window);
        s_manager.window = s_manager.Window(s_manager.window);
    }

    public void Logs()
    {
        if (s_manager.lighter)
        {
            if (!s_manager.fireplace)
            {
                l_manager.ManageFire(true);
                a_manager.fireplace.SetTrigger("Activate");
                s_manager.fireplace = true;
            }
            else
            {
                a_manager.fireplace.SetTrigger("Deactivate");
                l_manager.ManageFire(true);
                s_manager.fireplace = false;
            }
        }
        else
        {
            //GUI NEED LIGHTER
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

    public void PlayerLight(bool dream_state)
    {
        l_manager.PlayerLight(dream_state);
    }

    public Vector3 Wake_Sleep(GameObject player, PlayerScript player_script, CameraScript player_camera)
    {
        Debug.Log("Trying to spawn");
        o_manager.SpawnObstacles(s_manager.temperature);
        return wake_sleep_position.Wake_Sleep(player, player_script, player_camera);
    }
}
