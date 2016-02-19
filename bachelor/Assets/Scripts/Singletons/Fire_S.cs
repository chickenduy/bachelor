using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Fire_S : Singleton<Fire_S>
{
    // guarantee this will be always a singleton only - can't use the constructor!
    protected Fire_S() { }

    //private
    private List<GameObject> fire_list = new List<GameObject>();
    private int posX;
    private int posZ;

    //private visible
    [SerializeField]
    private GameObject _Fire;

    //getter/setter
    private bool[,] _fire_bool = new bool[25, 25];
    public bool[,] fire_bool
    {
        get
        {
            return _fire_bool;
        }
        set
        {
            _fire_bool = value;
        }
    }

    /*----------------------------------------------------------------------------------------------------*/

    //add the gameObject to the list after spawning
    public void Register(GameObject obj)
    {
        fire_list.Add(obj);
    }
    public void Delete(GameObject obj)
    {
        //converts the real position of the object to array positions
        Get_Array_Position(obj);
        //remove the object from the list
        fire_list.Remove(obj);
        //destroy object
        Destroy(obj);
        //set the array positions back to false
        Obstacle_S.Instance.space_bool[posX, posZ] = false;
        _fire_bool[posX, posZ] = false;
    }
    //calculate number of fires to spawn
    public void Calculate_Fire()
    {
        int spawn;
        if (!Room_S.Instance.wind)
        {
            //calculate number of spawns depending on temperature
            spawn = (Room_S.Instance.temperature + 2) * 3;
            //if it is less than 0 set it to 0
            if (spawn < 0)
                spawn = 0;
        }
        else
            spawn = 0;
        //calculate how many are on the map and have to spawn additionally or remove
        int test = spawn - fire_list.Count;
        //if it has to be removed than call delete
        if (test < 0)
        {
            test = -test;
            //delete until there is the right amount
            for (int i = 0; i < test; i++)
            {
                //always delete the first object in the list
                Delete(fire_list[0]);
            }
            return;
        }
        //if there is already enough fires, don't do anything
        if (test == 0)
            return;
        //call spawn function
        Spawn_Fire(test);
    }
    private void Spawn_Fire(int spawnNumber)
    {
        int numberX;
        int numberY;
        Vector3 vector;
        Quaternion qat = new Quaternion();
        //iterate through the number of spawns
        for (int i = 0; i < spawnNumber; i++)
        {
            //choose two random coordinates
            numberX = Random.Range(0, 25);
            numberY = Random.Range(0, 25);
            //if it is not in a room 
            if (!Test_In_Room(numberX, numberY))
            {
                //then test if space has already been taken by another object
                if (!Obstacle_S.Instance.space_bool[numberX, numberY]
                && !_fire_bool[numberX, numberY]
                && !Ice_S.Instance.ice_bool[numberX, numberY]
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
        //iterate through whole boolean array
        for (int i = 0; i < fire_bool.GetLength(0); i++)
        {
            for (int j = 0; j < fire_bool.GetLength(0); j++)
            {
                //if space is taken but not by another gameObject then calculate real coordinates with the array coordinates
                if (Obstacle_S.Instance.space_bool[i, j] && !_fire_bool[i, j] && !Ice_S.Instance.ice_bool[i, j] && !Power_S.Instance.power_bool[i, j])
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
                    _fire_bool[i, j] = true;
                    Instantiate(_Fire, vector, qat);
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




