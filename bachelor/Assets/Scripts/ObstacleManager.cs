using UnityEngine;
using System.Collections;
using UnityEditor;

public class ObstacleManager : MonoBehaviour
{
    public StateManager _StateManager;

    public GameObject _Fire;
    public GameObject _Ice;
    public GameObject _WaterFall;

    public int fireSpawn;
    public int iceSpawn;
    public int waterFallSpawn;
    public int[,,] obstacleArray;
    public GameObject[] movingWalls;
    public bool[] movingWallState = new bool[26];

    void Start()
    {
        obstacleArray = new int[21, 21, 5];
        movingWalls = GameObject.FindGameObjectsWithTag("moving wall");
        for (int i = 0; i < movingWallState.Length; i++)
        {
            movingWallState[i] = false;
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown("c"))
        {
            Walls();
        }

    }

    public void Fire(int temperature)
    {

        int spawn = (temperature + 2) * 3;
        if (spawn < 0)
        {
            spawn = 0;
        }
        int test = spawn - fireSpawn;
        if (test < 0)
        {
            GameObject[] fire = GameObject.FindGameObjectsWithTag("fire");
            for (int i = 0; i < -test; i++)
            {
                Destroy(fire[i]);
            }
            fireSpawn = spawn;
            return;
        }
        if (test == 0)
        {
            return;
        }
        SpawnFire(test);
        fireSpawn = spawn;
    }

    public void Ice(int temperature)
    {
        int spawn = (temperature - 2) * (-3);
        if (spawn < 0)
        {
            spawn = 0;
        }
        int test = spawn - iceSpawn;
        if (test < 0)
        {
            GameObject[] ice = GameObject.FindGameObjectsWithTag("ice");
            for (int i = 0; i < -test; i++)
            {
                Destroy(ice[i]);
            }
            iceSpawn = spawn;
            return;
        }
        if (test == 0)
        {
            return;
        }
        SpawnIce(test);
        iceSpawn = spawn;
    }

    public void Waterfall(float pee)
    {
        int spawn = (int)(pee * 10);
        if (pee < 0.3)
        {
            GameObject[] waterfall = GameObject.FindGameObjectsWithTag("waterfall");
            for (int i = 0; i < waterfall.Length; i++)
            {
                Destroy(waterfall[i]);
            }
            waterFallSpawn = 0;
            return;
        }
        SpawnWaterFall(spawn);

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
            if (obstacleArray[numberX, numberY, 0] == 0 && obstacleArray[numberX, numberY, 1] == 0)
            {
                obstacleArray[numberX, numberY, 0] = 1;
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
                if (obstacleArray[i, j, 0] == 1 && obstacleArray[i, j, 1] == 0)
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
                    obstacleArray[i, j, 1] = 1;
                    Instantiate(_Fire, vector, qat);
                }
            }
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
            if (obstacleArray[numberX, numberY, 0] == 0 && obstacleArray[numberX, numberY, 1] == 0)
            {
                obstacleArray[numberX, numberY, 0] = 2;
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
                if (obstacleArray[i, j, 0] == 2 && obstacleArray[i, j, 1] == 0)
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
                    obstacleArray[i, j, 1] = 2;
                    Instantiate(_Ice, vector, qat);
                }
            }
        }
    }

    private void SpawnWaterFall(int spawnNumber)
    {
        int numberX;
        int numberY;

        Vector3 vector;
        Quaternion qat = new Quaternion();

        for (int i = 0; i < spawnNumber; i++)
        {
            numberX = Random.Range(0, 21);
            numberY = Random.Range(0, 21);
            if (obstacleArray[numberX, numberY, 0] == 0 && obstacleArray[numberX, numberY, 1] == 0)
            {
                obstacleArray[numberX, numberY, 0] = 3;
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
                if (obstacleArray[i, j, 0] == 3 && obstacleArray[i, j, 1] == 0)
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
                    obstacleArray[i, j, 1] = 2;
                    Instantiate(_WaterFall, vector, qat);
                }
            }
        }
    }

    public void Walls()
    {
        int random;
        int number = Random.Range(0, 27);
        int[] randomArray = new int[number];
        for (int i = 0; i < number; i++)
        {
            random = Random.Range(0, 26);
            if (ArrayUtility.Contains(randomArray, random))
            {
                
                i--;
            }
            else
            {
                randomArray[i] = random;
            }
        }

        for (int i = 0; i < number; i++)
        {
            if(movingWallState[randomArray[i]] == true)
            {
                movingWalls[randomArray[i]].GetComponent<Animator>().SetTrigger("State1");
                movingWallState[randomArray[i]] = false;
            }
            else
            {
                movingWalls[randomArray[i]].GetComponent<Animator>().SetTrigger("State2");
                movingWallState[randomArray[i]] = true;
            }
        }


    }

    private float roundNumber(float floatNumber)
    {
        if (floatNumber < 0)
            return Mathf.Ceil(floatNumber / 0.5f) * 0.5f;
        else if (floatNumber > 0)
            return Mathf.Floor(floatNumber / 0.5f) * 0.5f;
        else
            return 0.5f;
    }


}




