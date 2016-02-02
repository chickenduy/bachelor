using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerScript : MonoBehaviour {

    //public variables
    public CameraScript _Camera_Manager;
    public Manager _Manager;

    public bool dream_state = true;

    //private variables
    public bool[] abilities = new bool[4];
    private bool is_dead;
    private Scene pause_menu;

    // Use this for initialization
    void Start () {
        InvokeRepeating("Walls", 30f, 30f);
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(_Manager);
    }

    // Update is called once per frame
    void Update () {
        if (is_dead || Input.GetKeyDown("r"))
        {
            is_dead = false;
            Debug.Log("Respawn");
            transform.position = _Manager.spawn_points.RespawnPlayer();
        }
        if (Input.GetKeyDown("t"))
        {
            transform.position = _Manager.Wake_Sleep(this, _Camera_Manager);
            //Check_Dream_State();
        }
        if (Input.GetKeyDown("i"))
        {
            print(FindObjectOfType(typeof(StateManager)));
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //pause_menu = SceneManager.GetSceneByName("Pause");
            //if (pause_menu.name == null)
            //{
            //    Debug.Log("Change Scene");
            //    SceneManager.LoadScene(2, LoadSceneMode.Additive);
            //}
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
        }


        

    }

    //action use
    public void Use(Collider col)
    {
        if (col.gameObject.name == "Switch Light")
        {
            _Manager.Switch_Light_Main();
            return;
        }
        if (col.gameObject.name == "Switch Light Bathroom")
        {
            _Manager.Switch_Light_Bathroom();
            return;
        }
        if (col.gameObject.name == "Switch Fan")
        {
            _Manager.Switch_Fan();
            return;
        }
        if (col.gameObject.tag == "bathroomdoor")
        {
            _Manager.Door();
            return;
        }
        if (col.gameObject.tag == "exit")
        {
            //ChangeScene(3);
        }
        if (col.gameObject.tag == "drawer")
        {
            _Manager.Drawer();
            return;
        }
        if (col.gameObject.name == "Toilet")
        {
            _Manager.Toilet();
            return;
        }
        if (col.gameObject.tag == "window")
        {
            _Manager.Window();
            return;
        }
        if (col.gameObject.name == "Logs")
        {
            _Manager.Logs();
            return;
        }
        if (col.gameObject.name == "Lighter")
        {
            _Manager.Lighter(col.gameObject);
            return;
        }
        if(col.gameObject.name == "Waterbottle")
        {
            _Manager.Waterbottle(col.gameObject);
            return;
        }
        if (col.gameObject.tag == "power")
        {
            //either using tags or using name
            _Manager.TakePower(col.gameObject, abilities);
            return;
        }
        if(col.gameObject.tag == "fire")
        {
            _Manager.KillFire(col.gameObject);
            return;
        }
        if(col.gameObject.tag == "moving wall")
        {
            if (abilities[0])
            {
                _Manager.a_manager.Wall(col.gameObject);
                return;
            }
        }

    }

    public void Check_Dream_State()
    {
        _Manager.PlayerLight(dream_state);
    }

    private void Walls()
    {
        _Manager.a_manager.Walls();
    }

}
