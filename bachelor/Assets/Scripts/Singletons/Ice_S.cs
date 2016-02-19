using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ice_S : Singleton<Ice_S>
{
    // guarantee this will be always a singleton only - can't use the constructor!
    protected Ice_S() { }

    //private
    private List<GameObject> ice_list = new List<GameObject>();
    private int posX;
    private int posZ;

    //private visible
    [SerializeField]
    private GameObject _Ice;

    //getter/seter
    private bool[,] _ice_bool = new bool[25, 25];
    public bool[,] ice_bool
    {
        get
        {
            return _ice_bool;
        }
        set
        {
            _ice_bool = value;
        }
    }

    /*----------------------------------------------------------------------------------------------------*/

    public void Register(GameObject obj)
    {
        //every instantiated prefab is added to the list
        ice_list.Add(obj);
    }

    public void Delete(GameObject obj)
    {
        //converts the real position of the object to array positions
        Get_Array_Position(obj);
        //remove the object from the list
        ice_list.Remove(obj);
        //destroy object
        Destroy(obj);
        //set the array positions back to false
        Obstacle_S.Instance.space_bool[posX, posZ] = false;
        _ice_bool[posX, posZ] = false;
    }

    public void Caluculate_Ice()
    {
        //calculating number of ice objects depending on the temperature in the room
        int spawn = (Room_S.Instance.temperature - 2) * (-3);
        if (spawn < 0)
            spawn = 0;
        //look how many ice objects are already on the map and calculate difference
        int test = spawn - ice_list.Count;
        //if there are more ice objects than supposed to, delete until there is the right number
        if (test < 0)
        {
            test = -test;
            for (int i = 0; i < test; i++)
            {
                //delete the first object in the list
                Delete(ice_list[0]);
            }
            return;
        }
        //if the number doesn't change, exit
        if (test == 0)
            return;
        //otherwise spawn the remaining ice objects
        Spawn_Ice(test);
    }


    private void Spawn_Ice(int spawnNumber)
    {
        int numberX;
        int numberY;
        Vector3 vector;
        Quaternion qat = new Quaternion();
        for (int i = 0; i < spawnNumber; i++)
        {
            //pick two random coordinates on a boolean grid which is layed on top of the maze
            numberX = Random.Range(0, 25);
            numberY = Random.Range(0, 25);
            //test if the object would spawn in a room
            if (!Test_In_Room(numberX, numberY))
            {
                //test if the spot is already taken by another object
                if (!Obstacle_S.Instance.space_bool[numberX, numberY]
                                && !_ice_bool[numberX, numberY]
                                && !Fire_S.Instance.fire_bool[numberX, numberY]
                                && !Power_S.Instance.power_bool[numberX, numberY])
                    //take the spot
                    Obstacle_S.Instance.space_bool[numberX, numberY] = true;
                //else repeat
                else
                    i--;
            }
            //else repeat
            else
                i--;
        }
        //iterate throught the whole grid
        for (int i = 0; i < 25; i++)
        {
            for (int j = 0; j < 25; j++)
            {
                //if the spot on the grid is taken but not by another object
                if (Obstacle_S.Instance.space_bool[i, j]
                    && !_ice_bool[i, j]
                    && !Fire_S.Instance.fire_bool[i, j]
                    && !Power_S.Instance.power_bool[i, j])
                {
                    if (i < 13)
                    {
                        if (j < 13)
                            vector = new Vector3((i * 4 - 48) - 0.5f, 2.3f, (j * 4 - 48) - 0.5f);
                        else if (j > 13)
                            vector = new Vector3((i * 4 - 48) - 0.5f, 2.3f, (j * 4 - 48) + 0.5f);
                        else
                            vector = new Vector3((i * 4 - 48) - 0.5f, 2.3f, 0);
                    }
                    else if (i > 13)
                    {
                        if (j < 13)
                            vector = new Vector3((i * 4 - 48) + 0.5f, 2.3f, (j * 4 - 48) - 0.5f);
                        else if (j > 13)
                            vector = new Vector3((i * 4 - 48) + 0.5f, 2.3f, (j * 4 - 48) + 0.5f);
                        else
                            vector = new Vector3((i * 4 - 48) + 0.5f, 2.3f, 0);
                    }
                    else
                    {
                        if (j < 13)
                            vector = new Vector3(0, 2.3f, (j * 4 - 48) - 0.5f);
                        else if (j > 13)
                            vector = new Vector3(0, 2.3f, (j * 4 - 48) + 0.5f);
                        else
                            vector = new Vector3(0, 2.3f, 0);
                    }
                    _ice_bool[i, j] = true;
                    Instantiate(_Ice, vector, qat);
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

    //don't let the objects spawn inside a room
    private bool Test_In_Room(int i, int j)
    {
        if ((i >= 12 && i <= 13 && j <= 4 && j >= 1) || //room1
           (i == 12 && j <= 24 && j >= 21) || //exitroom
           (i <= 6 && i >= 4 && j <= 20 && j >= 18) || //room2
           (i <= 4 && i >= 2 && j <= 5 && j >= 3) || //room0
           (i == 23 && j <= 23 && j >= 22) || //room3
           (i >= 10 && i <= 14 && j >= 10 && j <= 14)) //midroom
            return true;
        else
            return false;
    }
}




