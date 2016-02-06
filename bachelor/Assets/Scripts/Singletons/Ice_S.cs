using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ice_S : Singleton<Ice_S>
{
    // guarantee this will be always a singleton only - can't use the constructor!
    protected Ice_S() { }

    private List<GameObject> ice_list = new List<GameObject>();
    public bool[,] ice_bool = new bool[21, 21];
    private int posX;
    private int posZ;
    public GameObject _Ice;

    public int length = 0;

    void Update()
    {
        length = ice_list.Count;
    }

    public void Register(GameObject obj)
    {
        ice_list.Add(obj);
    }

    public void Delete(GameObject obj)
    {
        Debug.Log("Destroy Ice at: X" + posX + "/Z" + posZ);
        GetArrayPosition(obj);
        if (ice_list.Remove(obj))
        {
            Destroy(obj);
        }
        else
        {
            Debug.LogError("Can't remove ICE from list");

        }

        Obstacle_S.Instance.space_bool[posX, posZ] = false;
        ice_bool[posX, posZ] = false;
    }


    public void Caluculate_Ice()
    {
        int spawn = (Room_S.Instance.temperature - 2) * (-3);
        if (spawn < 0)
        {
            spawn = 0;
        }

        int test = spawn - ice_list.Count;
        if (test < 0)
        {
            test = -test;
            Debug.Log("Destroying " + test + " Ice");

            for (int i = 0; i < test; i++)
            {
                Delete(ice_list[0]);
            }
            return;
        }

        if (test == 0)
        {
            return;
        }
        SpawnIce(test);
        Debug.Log(test + " Ice spawned");
        return;
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
            if (Obstacle_S.Instance.space_bool[numberX, numberY] == false && ice_bool[numberX, numberY] == false && Fire_S.Instance.fire_bool[numberX, numberY] == false && Power_S.Instance.power_bool[numberX, numberY] == false)
            {
                Obstacle_S.Instance.space_bool[numberX, numberY] = true;
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
                if (Obstacle_S.Instance.space_bool[i, j] == true && ice_bool[i, j] == false && Fire_S.Instance.fire_bool[i, j] == false && Power_S.Instance.power_bool[i, j] == false)
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

                    ice_bool[i, j] = true;
                    Instantiate(_Ice, vector, qat);
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





}




