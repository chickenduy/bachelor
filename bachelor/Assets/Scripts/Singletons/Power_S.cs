using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Power_S : Singleton<Power_S>
{
    // guarantee this will be always a singleton only - can't use the constructor!
    protected Power_S() { }

    //private
    private List<GameObject> power_list = new List<GameObject>();
    private int posX;
    private int posZ;

    //private visible
    [SerializeField]
    private Material highlighted_wall;
    [SerializeField]
    private Material normal_wall;
    [SerializeField]
    private GameObject[] _Power = new GameObject[4];

    //getter/setter visible
    [SerializeField]
    private int _spawn_number = 5;
    public int spawn_number
    {
        get
        {
            return _spawn_number;
        }
        set
        {
            _spawn_number = value;
        }
    }
    [SerializeField]
    private float _timer_see = 30f;
    public float timer_see
    {
        get { return _timer_see; }
    }
    [SerializeField]
    private float _timer_speed = 15f;
    public float timer_speed
    {
        get { return _timer_speed; }
    }
    [SerializeField]
    private float _timer_go = 5f;
    public float timer_go
    {
        get { return _timer_go; }
    }

    //getter/setter
    private bool[,] _power_bool = new bool[25, 25];
    public bool[,] power_bool
    {
        get
        {
            return _power_bool;
        }
        set
        {
            _power_bool = value;
        }

    }

    /*----------------------------------------------------------------------------------------------------*/

    //methods
    void Start()
    {
        InvokeRepeating("SpawnPower", 60f, 60f);
    }

    public void Register(GameObject obj)
    {
        power_list.Add(obj);
    }

    //remove object from list and destroy gameObject
    public void Delete(GameObject obj)
    {
        //converts the real position of the object to array positions
        Get_Array_Position(obj);
        //remove the object from the list
        power_list.Remove(obj);
        //destroy object
        Destroy(obj);
        //set the array positions back to false
        Obstacle_S.Instance.space_bool[posX, posZ] = false;
        _power_bool[posX, posZ] = false;
    }

    //delete gameObject and give the player a determined power
    public void TakePower(GameObject obj)
    {
        //give the player a power specified in the gameObject
        switch (obj.tag)
        {
            case "powerA":
                Shoot_Ability();
                break;
            case "powerB":
                See_Ability();
                break;
            case "powerC":
                Speed_Ability();
                break;
            case "powerD":
                Go_Ability();
                break;
            default:
                Debug.LogError("Something went wrong");
                break;
        }
        //delete gameObject
        Delete(obj);
    }

    //spawn gameObjects specified by a given number
    public void Spawn_Power()
    {
        //determine if there are enough books on the map
        int test = spawn_number - power_list.Count;
        //if there are too many books, destroy until right amount
        if (test < 0)
        {
            test = -test;
            for (int i = 0; i < -test; i++)
            {
                Delete(power_list[0]);
            }
        }
        //if there are already enough books, don't do anything
        else if (test == 0)
        {
            return;
        }
        //declare X/Y position of the gameObjects in the map on an bool array
        int numberX;
        int numberY;
        Vector3 vector;
        Quaternion qat = new Quaternion();
        for (int i = 0; i < test; i++)
        {
            numberX = Random.Range(0, 25);
            numberY = Random.Range(0, 25);
            if (Test_In_Room(numberX, numberY))
            {
                //if the place in the bool array is not taken, place the gameObject
                if (!Obstacle_S.Instance.space_bool[numberX, numberY]
                    && !Fire_S.Instance.fire_bool[numberX, numberY]
                    && !Ice_S.Instance.ice_bool[numberX, numberY]
                    && !_power_bool[numberX, numberY])
                    Obstacle_S.Instance.space_bool[numberX, numberY] = true;
                else
                    i--;
            }

            //else search for another spot
            else
            {
                i--;
            }
        }
        //calculate the actual position of the gameObject in the maze
        for (int i = 0; i < 25; i++)
        {
            for (int j = 0; j < 25; j++)
            {
                if (Obstacle_S.Instance.space_bool[i, j]
                    && !Fire_S.Instance.fire_bool[i, j]
                    && !Ice_S.Instance.ice_bool[i, j]
                    && !_power_bool[i, j])
                {
                    if (i < 13)
                    {
                        if (j < 13)
                            vector = new Vector3((i * 4 - 48) - 0.5f, 0.5f, (j * 4 - 48) - 0.5f);
                        else if (j > 13)
                            vector = new Vector3((i * 4 - 48) - 0.5f, 0.5f, (j * 4 - 48) + 0.5f);
                        else
                            vector = new Vector3((i * 4 - 48) - 0.5f, 0.5f, 0);
                    }
                    else if (i > 13)
                    {
                        if (j < 13)
                            vector = new Vector3((i * 4 - 48) + 0.5f, 0.5f, (j * 4 - 48) - 0.5f);
                        else if (j > 13)
                            vector = new Vector3((i * 4 - 48) + 0.5f, 0.5f, (j * 4 - 48) + 0.5f);
                        else
                            vector = new Vector3((i * 4 - 48) + 0.5f, 0.5f, 0);
                    }
                    else
                    {
                        if (j < 13)
                            vector = new Vector3(0, 0.5f, (j * 4 - 48) - 0.5f);
                        else if (j > 13)
                            vector = new Vector3(0, 0.5f, (j * 4 - 48) + 0.5f);
                        else
                            vector = new Vector3(0, 0.5f, 0);
                    }
                    _power_bool[i, j] = true;
                    //give the gameObject a random rotation
                    float rotation = Random.Range(0, 360);
                    qat.eulerAngles = new Vector3(0, rotation, 0);
                    //instantiate a random gameObject
                    int number = Random.Range(0, _Power.Length);
                    Instantiate(_Power[number], vector, qat);

                }
            }
        }
    }

    //get x/z position in bool array
    private void Get_Array_Position(GameObject obj)
    {
        //get x position
        if (obj.transform.position.x < 0)
            posX = (int)((obj.transform.position.x + 48.5f) / 4);
        else if (obj.transform.position.x > 0)
            posX = (int)((obj.transform.position.x + 4.5f) / 4);
        else
            posX = 0;
        //get z position
        if (obj.transform.position.z < 0)
            posZ = (int)((obj.transform.position.z + 48.5f) / 4);
        else if (obj.transform.position.z > 0)
            posZ = (int)((obj.transform.position.z + 4.5f) / 4);
        else
            posZ = 0;
    }

    public void Shoot_Ability()
    {
        //activate countdown on Ability
        User_Interface_S.Instance.Activate_Shoot_Ability();
        //doesn't matter what happens here
        Player_S.Instance.abilities[0] = false;
        //add more ability to kill fires
        Room_S.Instance.Increase_Fire();

    }

    public void See_Ability()
    {
        //activate countdown on Ability
        User_Interface_S.Instance.Activate_See_Ability();
        //set abilities to true;
        Player_S.Instance.abilities[1] = true;
        //change materials of walls
        //and ability to move the walls
        Wall_S.Instance.Change_Wall_Material(highlighted_wall);
        //player loses the power after a given time
        StartCoroutine(Loose_See_Ability());
    }

    public void Speed_Ability()
    {
        //activate countdown on Ability
        User_Interface_S.Instance.Activate_Speed_Ability();
        //set abilities to true;
        Player_S.Instance.abilities[2] = true;
        //increase speed of the player
        Player_S.Instance.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_WalkSpeed = 15;
        Player_S.Instance.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_RunSpeed = 20;
        //player loses the power after a given time
        StartCoroutine(Loose_Speed_Ability());
    }

    public void Go_Ability()
    {
        //activate countdown on Ability
        User_Interface_S.Instance.Activate_Go_Ability();
        //set abilities to true;
        Player_S.Instance.abilities[3] = true;
        //deactivate collision with all walls (except inner and outer walls)
        Wall_S.Instance.Move_Through_Walls(true);
        //player loses the power after a given time
        StartCoroutine(Loose_Go_Ability());
    }

    private IEnumerator Loose_See_Ability()
    {
        yield return new WaitForSeconds(timer_see);
        //change wall materials back to normal
        Wall_S.Instance.Change_Wall_Material(normal_wall);
        //set abilities back to false
        Player_S.Instance.abilities[1] = false;
    }

    private IEnumerator Loose_Speed_Ability()
    {
        yield return new WaitForSeconds(timer_speed);
        //return player speed back to normal
        Player_S.Instance.Resume_Movement();
        //set abilities back to false
        Player_S.Instance.abilities[2] = false;
    }

    private IEnumerator Loose_Go_Ability()
    {
        yield return new WaitForSeconds(timer_go);
        //reactivate collision
        Wall_S.Instance.Move_Through_Walls(false);
        //set abilities back to false
        Player_S.Instance.abilities[3] = false;
    }

    private bool Test_In_Room(int i, int j)
    {
        if (i >= 10 && i <= 14 && j >= 10 && j <= 14)
            return false;
        else
            return true;
    }

}




