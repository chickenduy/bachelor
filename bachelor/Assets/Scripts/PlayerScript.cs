using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {

    //public variables
    public CameraScript _Camera_Manager;
    public ObjectScript _Object_Manager;

    public Transform[] _Respawn_Position;
    public Transform _Wake_Position;
    public Transform _Maze_Position;

    public bool dream_state = true;
    public GameObject _Fire;

    //private variables
    private bool abilties;
    private bool is_dead;
    private Scene pause_menu;

    private LevelManager.L_Manager lvl_manager;
    private AnimationManager.A_Manager a_manager;
    private StateManager.S_Manager s_manager;
    private LightingManager.L_Manager l_manager;

    private Spawns.Respawn spawn_points;
    private Spawns.WakeSleep wake_sleep_position;


    // Use this for initialization
    void Start () {
        spawn_points = new Spawns.Respawn(_Respawn_Position);
        wake_sleep_position = new Spawns.WakeSleep(_Wake_Position, _Maze_Position);
        lvl_manager = new LevelManager.L_Manager(GetComponentInChildren<Camera>());
        a_manager = new AnimationManager.A_Manager();
        s_manager = new StateManager.S_Manager();
        l_manager = new LightingManager.L_Manager(_Fire);

    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown("r"))
        {
            Debug.Log("Respawn");
            transform.position = spawn_points.RespawnPlayer();
        }
        if (Input.GetKeyDown("t"))
        {
            transform.position = wake_sleep_position.Wake_Sleep(gameObject, this, _Camera_Manager);
            CheckDreamState();
        }
        if (Input.GetKeyDown("i"))
        {
            print(FindObjectOfType(typeof(StateManager)));
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause_menu = SceneManager.GetSceneByName("Pause");
            if (pause_menu.name == null)
            {
                Debug.Log("Change Scene");

                SceneManager.LoadScene(2, LoadSceneMode.Additive);
                GetComponentInChildren<Camera>().enabled = false;
            }
        }
    }


    //action use
    public void Use(Collider col)
    {
        if (col.gameObject.name == "Switch Light Bathroom")
        {
            a_manager.Switch_Light_Bathroom(s_manager.switch_light_bathroom);
            s_manager.switch_fan = s_manager.Switch_Light_Bathroom(s_manager.switch_fan);
        }
        if (col.gameObject.name == "Switch Light")
        {
            Debug.Log(s_manager.switch_light);
            a_manager.Switch_Light(s_manager.switch_light);
            s_manager.switch_light = s_manager.Switch_Light(s_manager.switch_light);
            Debug.Log(s_manager.switch_light);
        }
        if (col.gameObject.name == "Switch Fan")
        {
            a_manager.Switch_Fan(s_manager.switch_fan);
            s_manager.switch_fan = s_manager.Switch_Fan(s_manager.switch_fan);
        }
        if (col.gameObject.tag == "bathroomdoor")
        {
            a_manager.Door_Bathroom(s_manager.door);
            s_manager.door = s_manager.Door(s_manager.door);
        }
        if (col.gameObject.tag == "exit")
        {
            //ChangeScene(3);
        }
        if (col.gameObject.tag == "drawer")
        {
            a_manager.Drawer(s_manager.drawer, s_manager.lighter);
            s_manager.drawer = s_manager.Drawer(s_manager.drawer);
        }
        if (col.gameObject.name == "Toilet")
        {
            a_manager.Toilet(s_manager.toilet);
            s_manager.toilet = s_manager.Toilet(s_manager.toilet);
        }
        if (col.gameObject.tag == "window")
        {
            a_manager.Window(s_manager.window);
            s_manager.window = s_manager.Window(s_manager.window);
        }
        if (col.gameObject.name == "Logs")
        {
            a_manager.Fireplace(s_manager.fireplace, s_manager.lighter);
            s_manager.fireplace = s_manager.Fireplace(s_manager.fireplace);
        }
        if (col.gameObject.name == "Lighter")
        {
            s_manager.lighter = true;
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "power")
        {
            //either using tags or using name
            if (col.gameObject.name == "BookOpenA(Clone)")
            {
                Debug.Log("Picked Up " + col.gameObject.name);
                _Object_Manager.TakePower(col.gameObject);
            }
            else if (col.gameObject.name == "BookOpenB(Clone)")
            {
                Debug.Log("Picked Up " + col.gameObject.name);
                _Object_Manager.TakePower(col.gameObject);
            }
            else if (col.gameObject.name == "BookOpenC(Clone)")
            {
                Debug.Log("Picked Up " + col.gameObject.name);
                _Object_Manager.TakePower(col.gameObject);
            }
        }

    }

    public void CheckDreamState()
    {
        l_manager.PlayerLight(dream_state);
    }




}
