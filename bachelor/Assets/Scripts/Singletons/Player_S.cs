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

    //variables
    private bool dream_state = true;
    private bool[] abilities = new bool[4];
    private bool lighter = false;
    private bool drinked = false;
    public bool[] pictures = new bool[4];
    private bool key;
    public bool invincible = false;

    private bool is_dead;
    private Scene pause_menu;
    private GameObject player_light;
    private bool sleep_on_couch;

    //methods
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        Initial_Spawn();
    }

    void Update()
    {
        if (is_dead || Input.GetKeyDown("r"))
        {
            is_dead = false;
            Respawn();
        }
        if (Input.GetKeyDown("t"))
        {
            if (dream_state == true)
            {
                Wake_Sleep();
                Check_Dream_State();
            }
        }
        if (Input.GetKeyDown("i"))
        {
            User_Interface_S.Instance.Print_OBJ();
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
            Obstacle_S.Instance.SpawnObstacles();
        }
    }

    //action use
    public void Use(Collider col)
    {
        switch (col.tag)
        {
            case "couch":
                User_Interface_S.Instance.Show_Info_Panel("Going to sleep on the couch");
                sleep_on_couch = true;
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
                Obstacle_S.Instance.Take_Power(col.transform.parent.gameObject);
                break;
            case "powerB":
                User_Interface_S.Instance.Show_Info_Panel("Picking up Power B");
                Obstacle_S.Instance.Take_Power(col.transform.parent.gameObject);
                break;
            case "powerC":
                User_Interface_S.Instance.Show_Info_Panel("Picking up Power C");
                Obstacle_S.Instance.Take_Power(col.transform.parent.gameObject);
                break;
            case "powerD":
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
                lighter = true;
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
            case "toilet":
                Room_S.Instance.Use_Toilet();
                break;
            case "room0":
                if (Maze_S.Instance.Get_Discovered(0))
                {
                    Maze_S.Instance.Respawn_Player(0);
                    User_Interface_S.Instance.Show_Info_Panel("You found a room.");
                }
                break;
            case "room1":
                if (Maze_S.Instance.Get_Discovered(1))
                {
                    Maze_S.Instance.Respawn_Player(1);
                    User_Interface_S.Instance.Show_Info_Panel("You found a room.");
                }
                break;
            case "room2":
                if (Maze_S.Instance.Get_Discovered(2))
                {
                    Maze_S.Instance.Respawn_Player(2);
                    User_Interface_S.Instance.Show_Info_Panel("You found a room.");
                }
                break;
            case "room3":
                if (Maze_S.Instance.Get_Discovered(3))
                {
                    Maze_S.Instance.Respawn_Player(3);
                    User_Interface_S.Instance.Show_Info_Panel("You found a room.");
                }
                break;
            case "fire":
                Fire_S.Instance.Kill_Fire(col.gameObject);
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
                key = true;
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
                    Object_S.Instance.Use_Object(col.gameObject);
                else
                    //interface telling you need electricity
                    return;
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
        player_light.SetActive(dream_state);
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


    public bool[] Get_Abilites()
    {
        return abilities;
    }

    public bool Get_Dream_State()
    {
        return dream_state;
    }

    public bool Get_Lighter()
    {
        return lighter;
    }

    public bool Get_Key()
    {
        return key;
    }

    public bool Get_Drinked()
    {
        return drinked;
    }

    public bool[] Get_Pictures()
    {
        return pictures;
    }
    public void Set_Abilites(int id, bool state)
    {
        abilities[id] = state;
    }

    public void Set_Dream_State(bool state)
    {
        dream_state = state;
    }
    public bool Get_Sleep_On_Couch()
    {
        return sleep_on_couch;
    }

    public void Set_Sleep_On_Couch(bool state)
    {
        sleep_on_couch = state;
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
