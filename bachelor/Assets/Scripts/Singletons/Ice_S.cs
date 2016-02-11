using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ice_S : Singleton<Ice_S>
{
    // guarantee this will be always a singleton only - can't use the constructor!
    protected Ice_S() { }

    private List<GameObject> ice_list = new List<GameObject>();
    private bool[,] ice_bool = new bool[21, 21];
    private int posX;
    private int posZ;
    private GameObject _Ice;

    void Start()
    {
        _Ice = Game_S.Instance._Ice;
    }

    public void Register(GameObject obj)
    {
        ice_list.Add(obj);
    }

    public void Delete(GameObject obj)
    {
        Get_Array_Position(obj);
        if (ice_list.Remove(obj))
            Destroy(obj);
        else
            Debug.LogError("Something went wrong in Ice_S/Delete");
        Obstacle_S.Instance.Get_Space_Bool()[posX, posZ] = false;
        ice_bool[posX, posZ] = false;
    }


    public bool[,] Get_Ice_Bool()
    {
        return ice_bool;
    }

    public void Caluculate_Ice()
    {
        int spawn = (Room_S.Instance.temperature - 2) * (-3);
        if (spawn < 0)
            spawn = 0;
        int test = spawn - ice_list.Count;
        if (test < 0)
        {
            test = -test;
            for (int i = 0; i < test; i++)
            {
                Delete(ice_list[0]);
            }
            return;
        }
        if (test == 0)
            return;
        SpawnIce(test);
        return;
    }
    public void Clear(GameObject obj)
    {
        foreach (GameObject ice in ice_list)
        {
            Destroy(ice);
            ice_list.Remove(ice);
        }
    }

    private void SpawnIce(int spawnNumber)
    {
        int numberX;
        int numberY;
        Vector3 vector;
        Quaternion qat = new Quaternion();
        for (int i = 0; i < spawnNumber; i++)
        {
            numberX = Random.Range(0, 21);
            numberY = Random.Range(0, 21);
            if (!Obstacle_S.Instance.Get_Space_Bool()[numberX, numberY]
                && !ice_bool[numberX, numberY]
                && !Fire_S.Instance.Get_Fire_Bool()[numberX, numberY]
                && !Power_S.Instance.Get_Power_Bool()[numberX, numberY])
                Obstacle_S.Instance.Get_Space_Bool()[numberX, numberY] = true;
            else
                i--;
        }
        for (int i = 0; i < 21; i++)
        {
            for (int j = 0; j < 21; j++)
            {
                if (Obstacle_S.Instance.Get_Space_Bool()[i, j]
                    && !ice_bool[i, j]
                    && !Fire_S.Instance.Get_Fire_Bool()[i, j]
                    && !Power_S.Instance.Get_Power_Bool()[i, j])
                {
                    if (i < 10)
                    {
                        if (j < 10)
                            vector = new Vector3(i * 4 - 48.5f, 2.3f, j * 4 - 48.5f);
                        else if (j > 10)
                            vector = new Vector3(i * 4 - 48.5f, 2.3f, (20 - j) * (-4) + 48.5f);
                        else
                            vector = new Vector3(i * 4 - 48.5f, 2.3f, 0);
                    }
                    else if (i > 10)
                    {
                        if (j < 10)
                            vector = new Vector3((20 - i) * (-4) + 48.5f, 2.3f, j * 4 - 48.5f);
                        else if (j > 10)
                            vector = new Vector3((20 - i) * (-4) + 48.5f, 2.3f, (20 - j) * (-4) + 48.5f);
                        else
                            vector = new Vector3((20 - i) * (-4) + 48.5f, 2.3f, 0);
                    }
                    else
                    {
                        if (j < 10)
                            vector = new Vector3(0, 2.3f, j * 4 - 48.5f);
                        else if (j > 10)
                            vector = new Vector3(0, 2.3f, (20 - j) * (-4) + 48.5f);
                        else
                            vector = new Vector3(0, 2.3f, 0);
                    }
                    ice_bool[i, j] = true;
                    Instantiate(_Ice, vector, qat);
                }
            }
        }
    }


    private void Get_Array_Position(GameObject obj)
    {
        if (obj.transform.position.x < 0)
            posX = (int)((obj.transform.position.x + 48.5f) / 4);
        else if (obj.transform.position.x > 0)
            posX = (int)(20 - ((obj.transform.position.x - 48.5f) / (-4)));
        else
            posX = 0;

        if (obj.transform.position.z < 0)
            posZ = (int)((obj.transform.position.z + 48.5) / 4);
        else if (obj.transform.position.z > 0)
            posZ = (int)(20 - ((obj.transform.position.z - 48.5f) / (-4)));
        else
            posZ = 0;
    }





}




