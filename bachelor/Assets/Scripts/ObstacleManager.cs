using UnityEngine;
using System.Collections;

public class ObstacleManager : MonoBehaviour
{

    public GameObject _Fire;
    public GameObject _Ice;
    public GameObject _WaterFall;
    public GameObject _Boulder;

    public int fireSpawn;
    public int iceSpawn;
    public int waterFallSpawn;
    public int[,,] obstacleArray;

    private float rayLength = 1;


    void Start()
    {
        obstacleArray = new int[21, 21, 5];
    }

    void FixedUpdate()
    {
        RaycastHit hit1;
        RaycastHit hit2;

        Vector3 frontleft = new Vector3(_Boulder.transform.position.x-1.45f , _Boulder.transform.position.y, _Boulder.transform.position.z +1.45f);
        Vector3 frontright = new Vector3(_Boulder.transform.position.x +1.45f , _Boulder.transform.position.y, _Boulder.transform.position.z+1.45f);

        Vector3 leftfront = new Vector3(_Boulder.transform.position.x - 1.45f, _Boulder.transform.position.y, _Boulder.transform.position.z + 1.45f);
        Vector3 rightfront = new Vector3(_Boulder.transform.position.x + 1.45f, _Boulder.transform.position.y, _Boulder.transform.position.z + 1.45f);

        Vector3 rightback = new Vector3(_Boulder.transform.position.x + 1.45f, _Boulder.transform.position.y, _Boulder.transform.position.z - 1.45f);
        Vector3 leftback = new Vector3(_Boulder.transform.position.x - 1.45f, _Boulder.transform.position.y, _Boulder.transform.position.z - 1.45f);

        Vector3 backright = new Vector3(_Boulder.transform.position.x + 1.45f, _Boulder.transform.position.y, _Boulder.transform.position.z - 1.45f);
        Vector3 backleft = new Vector3(_Boulder.transform.position.x - 1.45f, _Boulder.transform.position.y, _Boulder.transform.position.z - 1.45f);

        Ray rayFRONTLEFT = new Ray(frontleft, Vector3.forward);
        Ray rayFRONTRIGHT = new Ray(frontright, Vector3.forward);

        Ray rayRIGHTFRONT = new Ray(rightfront, Vector3.right);
        Ray rayRIGHTBACK = new Ray(rightback, Vector3.right);

        Ray rayBACKRIGHT = new Ray(backright, Vector3.back);
        Ray rayBACKLEFT = new Ray(backleft, Vector3.back);

        Ray rayLEFTBACK = new Ray(leftback, Vector3.left);
        Ray rayLEFTFRONT = new Ray(leftfront, Vector3.left);

        //Debug.DrawRay(frontleft, Vector3.forward);
        //Debug.DrawRay(frontright, Vector3.forward);
        //Debug.DrawRay(backleft, Vector3.back);
        //Debug.DrawRay(backright, Vector3.back);
        //Debug.DrawRay(leftback, Vector3.left);
        Debug.DrawRay(leftfront, Vector3.left);
        Debug.DrawRay(rightback, Vector3.right);
        //Debug.DrawRay(rightfront, Vector3.right);


        if (Physics.Raycast(rayBACKRIGHT, out hit1, rayLength) && Physics.Raycast(rayFRONTRIGHT, out hit2, rayLength))
        {
            _Boulder.transform.position = new Vector3(_Boulder.transform.position.x - 0.1f, _Boulder.transform.position.y, _Boulder.transform.position.z);
            _Boulder.transform.position = new Vector3((Mathf.Round(_Boulder.transform.position.x * 10f)) / 10f, _Boulder.transform.position.y, _Boulder.transform.position.z);
        }
        else if(Physics.Raycast(rayLEFTFRONT, out hit1, rayLength) && !Physics.Raycast(rayFRONTLEFT, out hit1, rayLength) && Physics.Raycast(rayBACKLEFT, out hit1, rayLength))
        {
            _Boulder.transform.position = new Vector3(_Boulder.transform.position.x, _Boulder.transform.position.y, _Boulder.transform.position.z+0.1f);
            _Boulder.transform.position = new Vector3((Mathf.Round(_Boulder.transform.position.x * 10f)) / 10f, _Boulder.transform.position.y, _Boulder.transform.position.z);
        }



    }

    public void fire(int temperature)
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
        spawnFire(test);
        fireSpawn = spawn;
    }

    public void ice(int temperature)
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
        spawnIce(test);
        iceSpawn = spawn;
    }

    public void waterfall(float pee)
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
        spawnWaterFall(spawn);

    }

    private void spawnFire(int spawnNumber)
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

    private void spawnIce(int spawnNumber)
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

    private void spawnWaterFall(int spawnNumber)
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




