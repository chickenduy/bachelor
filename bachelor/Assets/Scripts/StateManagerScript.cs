using UnityEngine;
using System.Collections;

public class StateManagerScript : MonoBehaviour
{

    //public variables
    public GameObject _Player;

    public GameObject _FirePlaceLight;
    public GameObject _Window;
    public GameObject _CeilingLight;


    public GameObject _Fire;
    public GameObject _Ice;

    public AnimationManagerScript animationManager;
    public PlayerScript playerManager;


    //private variables
    //int default parameter is 0
    private Light playerLight;
    private Light ceilingLight;
    private Light firePlaceLight;

    private int temperatureIndex;
    private int lightIndex;
    //private float peeIndex;
    private bool windIndex;

    private bool dreamState = true;
    private bool lightSwitchOn;
    private bool lightSwitchBOn;
    private bool fanSwitchOn;
    private bool bathroomDoorOpen;

    private int fireSpawn;
    private int iceSpawn;
    private int[,,] obstacleArray;


    // Use this for initialization
    void Start()
    {
        playerLight = _Player.GetComponentInChildren<Light>();
        ceilingLight = _CeilingLight.GetComponent<Light>();
        obstacleArray = new int[21, 21, 5];

    }

    // Update is called once per frame
    void Update()
    {
    }


    private void playerLightOut()
    {
        playerLight.enabled = false;
    }

    private void playerLightOn()
    {
        playerLight.enabled = true;
    }

    private void checkDreamState()
    {
        if (dreamState == false)
        {
            playerLightOut();
        }
        else if (dreamState == true)
        {
            playerLightOn();
        }
    }





    public void action(string tag)
    {
        if (tag == "lightswitch")
        {
            turnLightSwitch(lightSwitchOn);
            animationManager.turnLightSwitch(lightSwitchOn);
        }
        if (tag == "fanswitch")
        {
            fanSwitchOn = !fanSwitchOn;
            animationManager.turnFanSwitch(fanSwitchOn);
        }
        if (tag == "lightswitchb")
        {
            lightSwitchBOn = !lightSwitchBOn;
            animationManager.turnLightSwitchb(lightSwitchBOn);
        }

        if (tag == "bathroomdoor")
        {
            bathroomDoorOpen = !bathroomDoorOpen;
            animationManager.openDoor(bathroomDoorOpen);
        }
    }

    private void turnLightSwitch(bool state)
    {
        if (!state)
        {
            lightSwitchOn = true;
            lightIndex--;
        }
        else
        {
            lightSwitchOn = false;
            lightIndex++;
        }
    }

    private void turnFanSwitch(bool state)
    {
        if (state)
        {
            state = false;
            windIndex = false;
        }
        else
        {
            state = true;
            windIndex = true;
        }
    }

    //saves position in maze, teleports to room and back to maze; sets dreamState
    public void WakeSleep()
    {
        if (dreamState == true)
        {
            playerManager.mazePosition.transform.position = transform.position;
            playerManager.transform.position = playerManager.roomPosition.transform.position;
            dreamState = false;
        }
        else
        {
            playerManager.transform.position = playerManager.mazePosition.transform.position;
            dreamState = true;
        }
        checkDreamState();
    }

    //spawn fire with reservation of place
    //1 for Fire
    //2 for Ice
    //3 for 

    private void spawnFire(int spawn)
    {
        int numberX;
        int numberY;
        Vector3 vector;
        Quaternion qat = new Quaternion();

        for (int i = 0; i < spawn; i++)
        {
            numberX = Random.Range(0, 21);
            numberY = Random.Range(0, 21);
            if (obstacleArray[numberX, numberY, 0] == 0 && obstacleArray[numberX, numberY, 1] == 0)
            {
                obstacleArray[numberX, numberY, 0] = 1;
            }
            else
            {
                i++;
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
    private void spawnIce(int spawn)
    {
        int numberX;
        int numberY;
        Vector3 vector;
        Quaternion qat = new Quaternion();

        for (int i = 0; i < spawn; i++)
        {
            numberX = Random.Range(0, 21);
            numberY = Random.Range(0, 21);
            if (obstacleArray[numberX, numberY, 0] == 0 && obstacleArray[numberX, numberY, 1] == 0)
            {
                obstacleArray[numberX, numberY, 0] = 2;
            }
            else
            {
                i++;
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


















    //spawn fire obstacles
    //public void spawnFire()
    //{
    //    int spawn;
    //    if (temperatureIndex >= 1)
    //        spawn = 20;
    //    else if (temperatureIndex == 0)
    //        spawn = 10;
    //    else
    //        spawn = 5;

    //    print("inside setBoolArrayTrue");
    //    print(spawn);
    //    //two arrays for X and Y coordinates
    //    int[] x = new int[spawn];
    //    int[] z = new int[spawn];
    //    bool isin = false;
    //    int number;

    //    for (int i = 0; i < spawn; i++)
    //    {
    //        print(i);
    //        number = Random.Range(-12, 12);
    //        //test if random number is in X array
    //        for (int j = 0; j > i; j++)
    //        {
    //            if (x[j] == number)
    //            {
    //                isin = true;
    //            }
    //        }
    //        //assign number if it doesn't exist
    //        if (!isin)
    //        {
    //            x[i] = number;
    //        }
    //        //repeat else
    //        else
    //        {
    //            i++;
    //        }
    //    }

    //    for (int i = 0; i < spawn; i++)
    //    {
    //        number = Random.Range(-12, 12);
    //        //test if random number is in Y array
    //        for (int j = 0; j > i; j++)
    //        {
    //            if (z[j] == number) isin = true;
    //        }
    //        //assign number if it doesn't exist
    //        if (!isin)
    //        {
    //            z[i] = number;
    //        }
    //        //repeat else
    //        else
    //        {
    //            i++;
    //        }
    //    }

    //    Vector3 vec;

    //    //create fires with the coordinates
    //    for (int i = 0; i < spawn; i++)
    //    {
    //        if (x[i] >= 0)
    //        {
    //            if (z[i] >= 0)
    //            {
    //                vec = new Vector3(x[i] * 4 + 0.5f, 2.3f, z[i] * 4 + 0.5f);
    //            }

    //            else
    //            {
    //                vec = new Vector3(x[i] * 4 + 0.5f, 2.3f, z[i] * 4 - 0.5f);
    //            }
    //        }
    //        else
    //        {
    //            if (z[i] >= 0)
    //            {
    //                vec = new Vector3(x[i] * 4 - 0.5f, 2.3f, z[i] * 4 + 0.5f);
    //            }
    //            else
    //            {
    //                vec = new Vector3(x[i] * 4 - 0.5f, 2.3f, z[i] * 4 - 0.5f);
    //            }
    //        }
    //        Quaternion qat = new Quaternion();
    //        Instantiate(fire, vec, qat);
    //    }

    //}
}