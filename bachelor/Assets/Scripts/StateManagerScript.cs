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

    public bool dreamState = true;


    //private variables
    private Light playerLight;


    private int temperatureIndex;
    private int lightIndex;
    private float peeIndex;
    private bool windIndex;

    // Use this for initialization
    void Start()
    {
        playerLight = player.GetComponentInChildren<Light>();
        temperatureIndex = 0;
        lightIndex = 0;
        peeIndex = 0;
        windIndex = false;

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

}