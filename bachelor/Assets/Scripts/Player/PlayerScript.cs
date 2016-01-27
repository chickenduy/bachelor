using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {

    //public variables
    public CameraScript _Camera_Manager;
    public ObjectManager _Object_Manager;
    public Manager _Manager;

    public bool dream_state = true;

    //private variables
    private bool abilties;
    private bool is_dead;
    private Scene pause_menu;

    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown("r"))
        {
            Debug.Log("Respawn");
            transform.position = _Manager.spawn_points.RespawnPlayer();
        }
        if (Input.GetKeyDown("t"))
        {
            transform.position = _Manager.Wake_Sleep(gameObject, this, _Camera_Manager);
            Check_Dream_State();
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
        if (col.gameObject.name == "Switch Light")
        {
            _Manager.Switch_Light_Main();
        }
        if (col.gameObject.name == "Switch Light Bathroom")
        {
            _Manager.Switch_Light_Bathroom();
        }
        if (col.gameObject.name == "Switch Fan")
        {
            _Manager.Switch_Fan();
        }
        if (col.gameObject.tag == "bathroomdoor")
        {
            _Manager.Door();
        }
        if (col.gameObject.tag == "exit")
        {
            //ChangeScene(3);
        }
        if (col.gameObject.tag == "drawer")
        {
            _Manager.Drawer();
        }
        if (col.gameObject.name == "Toilet")
        {
            _Manager.Toilet();
        }
        if (col.gameObject.tag == "window")
        {
            _Manager.Window();
        }
        if (col.gameObject.name == "Logs")
        {
            _Manager.Logs();
        }
        if (col.gameObject.name == "Lighter")
        {
            _Manager.Lighter(col.gameObject);
        }
        if(col.gameObject.name == "Waterbottle")
        {
            _Manager.Waterbottle(col.gameObject);
        }
        if (col.gameObject.tag == "power")
        {
            //either using tags or using name
            if (col.gameObject.name == "BookOpenA(Clone)")
            {
                Debug.Log("Picked Up " + col.gameObject.name);
            }
            else if (col.gameObject.name == "BookOpenB(Clone)")
            {
                Debug.Log("Picked Up " + col.gameObject.name);
            }
            else if (col.gameObject.name == "BookOpenC(Clone)")
            {
                Debug.Log("Picked Up " + col.gameObject.name);
            }
        }

    }

    public void Check_Dream_State()
    {
        _Manager.PlayerLight(dream_state);
    }

}
