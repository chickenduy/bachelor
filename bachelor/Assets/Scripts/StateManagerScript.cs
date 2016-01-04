using UnityEngine;
using System.Collections;

public class StateManagerScript : MonoBehaviour
{

    //public variables
    public GameObject player;
    public GameObject firePlace;
    public GameObject window;
    public GameObject ceilingLight;
    public GameObject ceilingFan;
    public GameObject fire;

    public bool dreamState = true;
    public bool lightSwitchOn;
    public bool lightSwitchbOn;
    public bool fanswitchOn;
    public bool bathroomDoorOpen;

    public AnimationManagerScript animationManager;


    //private variables
    //int default parameter is 0
    private Light playerLight;
    private int temperatureIndex;
    //private int lightIndex;
    //private float peeIndex;
    //private bool windIndex;
    //bool default parameter is false
    private bool[,] fireArray;
    private bool[,] iceArray;

    // Use this for initialization
    void Start()
    {
        playerLight = player.GetComponentInChildren<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("k"))
        {
            temperatureIndex = temperatureIndex - 1;
            print(temperatureIndex);
        }
        if (Input.GetKeyDown("j"))
        {
            temperatureIndex = temperatureIndex + 1;
            print(temperatureIndex);
        }

        checkState();
    }


    private void playerLightOut()
    {
        playerLight.enabled = false;
    }

    private void playerLightOn()
    {
        playerLight.enabled = true;
    }

    private void checkState()
    {
        if (dreamState == false && playerLight.enabled)
        {
            playerLightOut();
        }
        else if (dreamState == true && !playerLight.enabled)
        {
            playerLightOn();
        }
    }

    public void action(string tag)
    {
        if (tag == "lightswitch")
        {
            lightSwitchOn = !lightSwitchOn;
            animationManager.turnLightSwitch(lightSwitchOn);
            spawnFire();
        }
        if (tag == "fanswitch")
        {
            fanswitchOn = !fanswitchOn;
            animationManager.turnFanSwitch(fanswitchOn);
        }
        if (tag == "lightswitchb")
        {
            lightSwitchbOn = !lightSwitchbOn;
            animationManager.turnLightSwitchb(lightSwitchbOn);
        }

        if (tag == "bathroomdoor")
        {
            bathroomDoorOpen = !bathroomDoorOpen;
            animationManager.openDoor(bathroomDoorOpen);
        }

    }

    //spawn fire obstacles
    public void spawnFire()
    {
        int spawn;
        if (temperatureIndex >= 1)
            spawn = 20;
        else if (temperatureIndex == 0)
            spawn = 10;
        else
            spawn = 5;

        print("inside setBoolArrayTrue");
        print(spawn);
        //two arrays for X and Y coordinates
        int[] x = new int[spawn];
        int[] z = new int[spawn];
        bool isin = false;
        int number;

        for (int i = 0; i < spawn; i++)
        {
            print(i);
            number = Random.Range(-12, 12);
            //test if random number is in X array
            for (int j = 0; j > i; j++)
            {
                if (x[j] == number)
                {
                    isin = true;
                }
            }
            //assign number if it doesn't exist
            if (!isin)
            {
                x[i] = number;
            }
            //repeat else
            else
            {
                i++;
            }
        }

        for (int i = 0; i < spawn; i++)
        {
            number = Random.Range(-12, 12);
            //test if random number is in Y array
            for (int j = 0; j > i; j++)
            {
                if (z[j] == number) isin = true;
            }
            //assign number if it doesn't exist
            if (!isin)
            {
                z[i] = number;
            }
            //repeat else
            else
            {
                i++;
            }
        }

        Vector3 vec;

        //create fires with the coordinates
        for (int i = 0; i < spawn; i++)
        {
            if (x[i] >= 0)
            {
                if (z[i] >= 0)
                {
                    vec = new Vector3(x[i] * 4 + 0.5f, 2.3f, z[i] * 4 + 0.5f);
                }

                else
                {
                    vec = new Vector3(x[i] * 4 + 0.5f, 2.3f, z[i] * 4 - 0.5f);
                }
            }
            else
            {
                if (z[i] >= 0)
                {
                    vec = new Vector3(x[i] * 4 - 0.5f, 2.3f, z[i] * 4 + 0.5f);
                }
                else
                {
                    vec = new Vector3(x[i] * 4 - 0.5f, 2.3f, z[i] * 4 - 0.5f);
                }
            }
            Quaternion qat = new Quaternion();
            Instantiate(fire, vec, qat);
        }

    }


}