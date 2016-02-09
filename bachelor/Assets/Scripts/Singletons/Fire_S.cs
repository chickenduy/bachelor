using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Fire_S : Singleton<Fire_S>
{
    // guarantee this will be always a singleton only - can't use the constructor!
    protected Fire_S() { }

    private List<GameObject> fire_list = new List<GameObject>();
    private bool[,] fire_bool = new bool[21, 21];
    private GameObject _Fire;
    private int posX;
    private int posZ;

    void Start()
    {
        _Fire = Game_S.Instance._Fire;
    }

    public void Register(GameObject obj)
    {
        fire_list.Add(obj);
    }

    public bool[,] Get_Fire_Bool()
    {
        return fire_bool;
    }

    public void Delete(GameObject obj)
    {
        Debug.Log("Destroy Fire at: X" + posX + "/Z" + posZ);
        GetArrayPosition(obj);
        fire_list.Remove(obj);
        Destroy(obj);
        Obstacle_S.Instance.Get_Space_Bool()[posX, posZ] = false;
        fire_bool[posX, posZ] = false;
    }

    public void Clear()
    {
        foreach (GameObject fire in fire_list)
        {
            fire_list.Remove(fire);
            Destroy(fire);
        }
    }

    //calculate number of fires to spawn
    public void Calculate_Fire()
    {
        if (Room_S.Instance.Get_Pee() < 1.0)
        {
            int spawn = (Room_S.Instance.Get_Temperature() + 2) * 3;
            if (spawn < 0)
            {
                spawn = 0;
            }
            int test = spawn - fire_list.Count;

            if (test < 0)
            {
                test = -test;
                for (int i = 0; i < test; i++)
                {
                    Delete(fire_list[0]);
                }
                return;
            }
            if (test == 0)
            {
                return;
            }
            SpawnFire(test);
        }
        else
            Clear();

    }

    private void SpawnFire(int spawnNumber)
    {
        int numberX;
        int numberY;
        Vector3 vector;
        Quaternion qat = new Quaternion();
        for (int i = 0; i < spawnNumber; i++)
        {
            numberX = Random.Range(0, 21);
            numberY = Random.Range(0, 21);
            if (Obstacle_S.Instance.Get_Space_Bool()[numberX, numberY] == false && fire_bool[numberX, numberY] == false && Ice_S.Instance.Get_Ice_Bool()[numberX, numberY] == false && Power_S.Instance.Get_Power_Bool()[numberX, numberY] == false)
            {
                Obstacle_S.Instance.Get_Space_Bool()[numberX, numberY] = true;
            }
            else
            {
                i--;
            }
        }
        for (int i = 0; i < 21; i++)
        {
            for (int j = 0; j < 21; j++)
            {
                if (Obstacle_S.Instance.Get_Space_Bool()[i, j] == true && fire_bool[i, j] == false && Ice_S.Instance.Get_Ice_Bool()[i, j] == false && Power_S.Instance.Get_Power_Bool()[i, j] == false)
                {
                    if (i < 10)
                    {
                        if (j < 10)
                        {
                            vector = new Vector3(i * 4 - 48.5f, 2.3f, j * 4 - 48.5f);
                        }
                        else if (j > 10)
                        {
                            vector = new Vector3(i * 4 - 48.5f, 2.3f, (20 - j) * (-4) + 48.5f);
                        }
                        else
                        {
                            vector = new Vector3(i * 4 - 48.5f, 2.3f, 0);
                        }
                    }
                    else if (i > 10)
                    {
                        if (j < 10)
                        {
                            vector = new Vector3((20 - i) * (-4) + 48.5f, 2.3f, j * 4 - 48.5f);
                        }
                        else if (j > 10)
                        {
                            vector = new Vector3((20 - i) * (-4) + 48.5f, 2.3f, (20 - j) * (-4) + 48.5f);
                        }
                        else
                        {
                            vector = new Vector3((20 - i) * (-4) + 48.5f, 2.3f, 0);
                        }
                    }
                    else
                    {
                        if (j < 10)
                        {
                            vector = new Vector3(0, 2.3f, j * 4 - 48.5f);
                        }
                        else if (j > 10)
                        {
                            vector = new Vector3(0, 2.3f, (20 - j) * (-4) + 48.5f);
                        }
                        else
                        {
                            vector = new Vector3(0, 2.3f, 0);
                        }
                    }
                    fire_bool[i, j] = true;
                    Instantiate(_Fire, vector, qat);
                }
            }
        }
    }
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

    public void Kill_Fire(GameObject obj)
    {
        if (Room_S.Instance.Get_Fire_Kills() > 0)
        {
            Room_S.Instance.Use_Fire();
            Delete(obj);
        }
    }



}




