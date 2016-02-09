using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Power_S : Singleton<Power_S>
{
    // guarantee this will be always a singleton only - can't use the constructor!
    protected Power_S() { }

    //variables
    private Material highlighted_wall;
    private Material normal_wall;
    private int spawn_number = 5;
    private float Power_B_Timer = 15f;
    private float Power_C_Timer = 15f;
    private float Power_D_Timer = 15f;
    private List<GameObject> power_list = new List<GameObject>();
    private GameObject[] _Power = new GameObject[4];
    private bool[,] power_bool = new bool[21, 21];
    private int posX;
    private int posZ;

    //methods
    void Start()
    {
        highlighted_wall = Game_S.Instance.highlighted_wall;
        normal_wall = Game_S.Instance.normal_wall;
        spawn_number = Game_S.Instance.spawn_number;
        Power_B_Timer = Game_S.Instance.Power_B_Timer;
        Power_C_Timer = Game_S.Instance.Power_C_Timer;
        Power_D_Timer = Game_S.Instance.Power_D_Timer;
        _Power = Game_S.Instance._Power;
    }
    public void Register(GameObject obj)
    {
        power_list.Add(obj);
    }

    //remove object from list and destroy gameObject
    public void Delete(GameObject obj)
    {

        GetArrayPosition(obj);
        if (power_list.Remove(obj))
            Destroy(obj);
        Obstacle_S.Instance.Get_Space_Bool()[posX, posZ] = false;
        power_bool[posX, posZ] = false;
    }

    public bool[,] Get_Power_Bool()
    {
        return power_bool;
    }
    //delete gameObject and give the player a determined power
    public void TakePower(GameObject obj)
    {
        //get the position of the gameObject in the obstacle array
        GetArrayPosition(obj);
        //set the bool to false, so new gameObjects can spawn at that position
        power_bool[posX, posZ] = false;
        //give the player a power specified in the gameObject
        switch (obj.tag)
        {
            case "powerA":
                Power_A();
                break;
            case "powerB)":
                Power_B();
                break;
            case "powerC":
                Power_C();
                break;
            case "powerD":
                Power_D();
                break;
            default:
                Debug.LogError("Something went wrong");
                break;
        }
        //delete gameObject
        Delete(obj);
    }

    //spawn gameObjects specified by a given number
    public void SpawnPower()
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
            numberX = Random.Range(0, 21);
            numberY = Random.Range(0, 21);
            //if the place in the bool array is not taken, place the gameObject
            if (Obstacle_S.Instance.Get_Space_Bool()[numberX, numberY] == false && Fire_S.Instance.Get_Fire_Bool()[numberX, numberY] == false && Ice_S.Instance.Get_Ice_Bool()[numberX, numberY] == false && power_bool[numberX, numberY] == false)
            {
                Obstacle_S.Instance.Get_Space_Bool()[numberX, numberY] = true;
            }
            //else search for another spot
            else
            {
                i--;
            }
        }
        //calculate the actual position of the gameObject in the maze
        for (int i = 0; i < 21; i++)
        {
            for (int j = 0; j < 21; j++)
            {
                if (Obstacle_S.Instance.Get_Space_Bool()[i, j] == true && Fire_S.Instance.Get_Fire_Bool()[i, j] == false && Ice_S.Instance.Get_Ice_Bool()[i, j] == false && power_bool[i, j] == false)
                {
                    if (i < 10)
                    {
                        if (j < 10)
                        {
                            vector = new Vector3(i * 4 - 48.5f, 1f, j * 4 - 48.5f);
                        }
                        else if (j > 10)
                        {
                            vector = new Vector3(i * 4 - 48.5f, 1f, (20 - j) * (-4) + 48.5f);
                        }
                        else
                        {
                            vector = new Vector3(i * 4 - 48.5f, 1f, 0);
                        }
                    }
                    else if (i > 10)
                    {
                        if (j < 10)
                        {
                            vector = new Vector3((20 - i) * (-4) + 48.5f, 1f, j * 4 - 48.5f);
                        }
                        else if (j > 10)
                        {
                            vector = new Vector3((20 - i) * (-4) + 48.5f, 1f, (20 - j) * (-4) + 48.5f);
                        }
                        else
                        {
                            vector = new Vector3((20 - i) * (-4) + 48.5f, 1f, 0);
                        }
                    }
                    else
                    {
                        if (j < 10)
                        {
                            vector = new Vector3(0, 1f, j * 4 - 48.5f);
                        }
                        else if (j > 10)
                        {
                            vector = new Vector3(0, 1f, (20 - j) * (-4) + 48.5f);
                        }
                        else
                        {
                            vector = new Vector3(0, 1f, 0);
                        }
                    }
                    power_bool[i, j] = true;
                    //give the gameObject a random rotation
                    float rotation = Random.Range(0, 360);
                    qat.eulerAngles = new Vector3(0, rotation, 0);
                    //instantiate a random gameObject
                    Instantiate(_Power[Random.Range(0, _Power.Length)], vector, qat);
                }
            }
        }
    }

    //get the array position of the gameObject
    private void GetArrayPosition(GameObject obj)
    {
        if (obj.transform.position.x < 0)
        {
            posX = (int)((obj.transform.position.x + 48.5f) / 4);
        }
        else if (obj.transform.position.x > 0)
        {
            posX = (int)(20 - ((obj.transform.position.x - 48.5f) / (-4)));
        }
        else
        {
            posX = 0;
        }

        if (obj.transform.position.z < 0)
        {
            posZ = (int)((obj.transform.position.z + 48.5) / 4);
        }
        else if (obj.transform.position.z > 0)
        {
            posZ = (int)(20 - ((obj.transform.position.z - 48.5f) / (-4)));
        }
        else
        {
            posZ = 0;
        }

    }

    public void Power_A()
    {
        //add more ability to kill fires
        Player_S.Instance.Set_Abilites(1, false);
        Room_S.Instance.Set_Fire_Kills(Room_S.Instance.Get_Fire_Kills() + Room_S.Instance.Get_Temperature() + 2);
    }

    public void Power_B()
    {
        //change materials of walls
        //and ability to move the walls
        Player_S.Instance.Set_Abilites(1, false);
        Wall_S.Instance.Change_Wall_Material(highlighted_wall);
        //player loses the power after a given time
        StartCoroutine(Loose_Power_B());
    }

    public void Power_C()
    {
        //increase speed of the player
        Player_S.Instance.Set_Abilites(1, false);
        Player_S.Instance.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_WalkSpeed = 15;
        Player_S.Instance.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_RunSpeed = 20;
        //player loses the power after a given time
        StartCoroutine(Loose_Power_C());
    }

    public void Power_D()
    {
        //deactivate collision with all walls (except inner and outer walls)
        Player_S.Instance.Get_Abilites()[3] = true;
        Wall_S.Instance.Move_Through_Walls(true);
        //player loses the power after a given time

        StartCoroutine(Loose_Power_C());
    }

    IEnumerator Loose_Power_B()
    {
        yield return new WaitForSeconds(Power_B_Timer);
        Wall_S.Instance.Change_Wall_Material(normal_wall);
        Player_S.Instance.Set_Abilites(1, false);
    }

    IEnumerator Loose_Power_C()
    {
        yield return new WaitForSeconds(Power_C_Timer);
        Player_S.Instance.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_WalkSpeed = 7;
        Player_S.Instance.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_RunSpeed = 10;
        Player_S.Instance.Set_Abilites(1, false);
    }

    IEnumerator Loose_Power_D()
    {
        yield return new WaitForSeconds(Power_D_Timer);
        Wall_S.Instance.Move_Through_Walls(false);
        Player_S.Instance.Set_Abilites(1, false);
    }



}




