using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.Effects;

public class Player_S : Singleton<Player_S>
{
    // guarantee this will be always a singleton only - can't use the constructor!
    protected Player_S() { }

    //private
    private bool drinked = false;
    private GameObject player_light;
    private Animator hands;
    private Camera fpscam;

    //getter/setter
    private bool _dream_state = true;
    public bool dream_state
    {
        get
        {
            return _dream_state;
        }
        set
        {
            _dream_state = value;
        }
    }
    private bool[] _abilities = new bool[4];
    public bool[] abilities
    {
        get
        {
            return _abilities;
        }
        set
        {
            _abilities = value;
        }
    }
    private bool _lighter = false;
    public bool lighter
    {
        get
        {
            return _lighter;
        }
    }
    private bool[] _pictures = new bool[4];
    public bool[] pictures
    {
        get
        {
            return _pictures;
        }
        set
        {
            _pictures = value;
        }
    }
    private bool _key;
    public bool key
    {
        get
        {
            return _key;
        }

    }
    private bool _invincible = false;
    public bool invincible
    {
        get
        {
            return _invincible;
        }
        set
        {
            _invincible = value;
        }
    }
    private bool _couch;
    public bool couch
    {
        get
        {
            return _couch;
        }
        set
        {
            _couch = value;
        }
    }
    private bool _reality_check;
    public bool reality_check
    {
        get
        {
            return _reality_check;
        }
        set
        {
            _reality_check = value;
        }
    }

    /*----------------------------------------------------------------------------------------------------*/

    //methods
    void Start()
    {
        fpscam = GetComponentInChildren<Camera>();
        hands = GetComponentInChildren<Animator>();
        DontDestroyOnLoad(gameObject);
        Initial_Spawn();
    }

    void Update()
    {

        if (fpscam.transform.eulerAngles.x > 60 && fpscam.transform.eulerAngles.x <= 70)
        {
            if (_reality_check == false && _dream_state == true)
            {
                hands.SetTrigger("reality_check");
                _reality_check = true;
                StartCoroutine(Camera_S.Instance.Become_Lucid(7f));
            }

            else if (_reality_check == false && _dream_state == false)
            {
                hands.SetTrigger("reality_check_fail");
                _reality_check = true;
            }
        }
        if (Input.GetKeyDown("t"))
        {
            if (_dream_state == true)
            {
                Wake_Sleep();
                Check_Dream_State();
            }
        }
        if (Input.GetKeyDown("i"))
        {
            Room_S.Instance.killfire = 10;
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
        switch (col.tag)
        {
            case "radio":
                if (Background_Music_S.Instance.is_radio_on)
                    Background_Music_S.Instance.Turn_Off_Radio();
                else
                    Background_Music_S.Instance.Turn_On_Radio();
                break;
            case "couch":
                User_Interface_S.Instance.Show_Info_Panel("Going to sleep on the couch");
                couch = true;
                Wake_Sleep();
                Check_Dream_State();
                break;
            case "bed":
                User_Interface_S.Instance.Show_Info_Panel("Going to sleep on the bed");
                Wake_Sleep();
                Check_Dream_State();
                break;
            case "powerA":
                User_Interface_S.Instance.Show_Info_Panel("Picking up Power A");
                User_Interface_S.Instance.Destroyed();
                Obstacle_S.Instance.Take_Power(col.transform.parent.gameObject);
                break;
            case "powerB":
                User_Interface_S.Instance.Destroyed();
                User_Interface_S.Instance.Show_Info_Panel("Picking up Power B");
                Obstacle_S.Instance.Take_Power(col.transform.parent.gameObject);
                break;
            case "powerC":
                User_Interface_S.Instance.Destroyed();
                User_Interface_S.Instance.Show_Info_Panel("Picking up Power C");
                Obstacle_S.Instance.Take_Power(col.transform.parent.gameObject);
                break;
            case "powerD":
                User_Interface_S.Instance.Destroyed();
                User_Interface_S.Instance.Show_Info_Panel("Picking up Power D");
                Obstacle_S.Instance.Take_Power(col.transform.parent.gameObject);
                break;
            case "moving wall":
                if (abilities[1])
                    Wall_S.Instance.Move_Highlighted_Wall(Wall_S.Instance.Get_ID(col.gameObject));
                break;
            case "switch":
                User_Interface_S.Instance.Show_Info_Panel("Flip Switch");
                Object_S.Instance.Use_Object(col.gameObject);
                break;
            case "fan":
                User_Interface_S.Instance.Show_Info_Panel("Flip Fan Switch");
                Room_S.Instance.wind = true;
                Object_S.Instance.Use_Object(col.gameObject);
                break;
            case "door":
                User_Interface_S.Instance.Show_Info_Panel("Opened the Door");

                Object_S.Instance.Use_Object(col.transform.parent.gameObject);
                break;
            case "drawer":
                Object_S.Instance.Use_Object(col.transform.parent.gameObject);
                break;
            case "lighter":
                Destroy(col.gameObject);
                _lighter = true;
                break;
            case "window":
                Object_S.Instance.Use_Object(col.transform.parent.gameObject);
                break;
            case "logs":
                Object_S.Instance.Light_Fireplace(col.transform.parent.gameObject);
                break;
            case "bottle":
                //Destroy(col.gameObject);
                Room_S.Instance.Drink();
                break;
            case "toilet lid":
                Object_S.Instance.Use_Object(col.transform.parent.gameObject);
                if (lighter)
                    User_Interface_S.Instance.Show_Info_Panel("Lit up the Fireplace.");
                else
                    User_Interface_S.Instance.Show_Info_Panel("You need something to light the fire.");
                break;
            case "room0":
                if (Maze_S.Instance.room_discovered[0])
                {
                    Maze_S.Instance.Respawn_Player(0);
                    User_Interface_S.Instance.Show_Info_Panel("You found a room.");
                }
                break;
            case "room1":
                if (Maze_S.Instance.room_discovered[1])
                {
                    Maze_S.Instance.Respawn_Player(1);
                    User_Interface_S.Instance.Show_Info_Panel("You found a room.");
                }
                break;
            case "room2":
                if (Maze_S.Instance.room_discovered[2])
                {
                    Maze_S.Instance.Respawn_Player(2);
                    User_Interface_S.Instance.Show_Info_Panel("You found a room.");
                }
                break;
            case "room3":
                if (Maze_S.Instance.room_discovered[3])
                {
                    Maze_S.Instance.Respawn_Player(3);
                    User_Interface_S.Instance.Show_Info_Panel("You found a room.");
                }
                break;
            case "picture":
                Object_S.Instance.Touch_Picture(col.gameObject);
                if (pictures[0] && pictures[1] && pictures[2] && pictures[3])
                    User_Interface_S.Instance.Next_Quest();
                break;
            case "hiddenwall":
                if (pictures[0] && pictures[1] && pictures[2] && pictures[3] && key)
                {
                    Wall_S.Instance.Destroy_Wall_2();
                    User_Interface_S.Instance.Destroyed();
                    User_Interface_S.Instance.Next_Quest();
                    User_Interface_S.Instance.Show_Info_Panel("You found the hidden room");
                }
                if (!pictures[0] || !pictures[1] || !pictures[2] || !pictures[3])
                {
                    User_Interface_S.Instance.Show_Info_Panel("Something seems off.");
                }
                if (pictures[0] && pictures[1] && pictures[2] && pictures[3] && !key)
                {
                    User_Interface_S.Instance.Show_Info_Panel("You need a key for that.");
                }
                break;
            case "key":
                Destroy(col.gameObject);
                User_Interface_S.Instance.Destroyed();
                _key = true;
                if (pictures[0] && pictures[1] && pictures[2] && pictures[3])
                    User_Interface_S.Instance.Next_Quest();
                break;
            case "e_lever":
                Object_S.Instance.Use_Object(col.gameObject);
                Room_S.Instance.electricity = true;
                User_Interface_S.Instance.Next_Quest();
                Wall_S.Instance.Destroy_Final_Walls();
                //destroy lazers
                break;
            case "lever":
                if (Room_S.Instance.electricity)
                {
                    Object_S.Instance.Use_Object(col.gameObject);
                    Object_S.Instance.Open_Gate();
                }
                else
                    User_Interface_S.Instance.Show_Info_Panel("You need a elictricity for that.");
                break;

            default:
                Debug.Log("hit nothing");
                break;
        }
    }

    public void Register(GameObject obj)
    {

        player_light = obj;
    }

    public void Drink()
    {
        Room_S.Instance.Drink();
    }


    public void Check_Dream_State()
    {
        if (Room_S.Instance.wind)
            player_light.SetActive(false);
        else
            player_light.SetActive(_dream_state);

    }

    public FireLight Get_Firelight()
    {
        return player_light.GetComponentInChildren<FireLight>();
    }
    public void Respawn()
    {
        Maze_S.Instance.Respawn_Player();
    }

    private void Initial_Spawn()
    {
        Maze_S.Instance.Initial_Spawn();
    }

    public void Wake_Sleep()
    {
        Maze_S.Instance.Wake_Sleep();
    }



    public bool Get_Drinked()
    {
        return drinked;
    }



    public void Stop_Movement()
    {
        GetComponent<FirstPersonController>().m_RunSpeed = 0;
        GetComponent<FirstPersonController>().m_WalkSpeed = 0;
    }
    public void Resume_Movement()
    {
        GetComponent<FirstPersonController>().m_RunSpeed = 8;
        GetComponent<FirstPersonController>().m_WalkSpeed = 5;
    }

}
