using UnityEngine;
using System.Collections;

public class AnimationManagerScript : MonoBehaviour {

    public GameObject lightSwitch;
    public GameObject lightSwitchb;
    public GameObject fanSwitch;

    public GameObject fanLight;
    public GameObject bathroomLight;

    public GameObject bathroomDoor;

    public StateManagerScript stateManager;

    private Animator lightSwitchAnimator;
    private Animator lightSwitchbAnimator;
    private Animator fanSwitchAnimator;

    private Animator fanLightAnimator;
    private Animator bathroomLightAnimator;

    private Animator bathroomDoorAnimator;

    void Awake()
    {
        lightSwitchAnimator = lightSwitch.GetComponent<Animator>();
        lightSwitchbAnimator = lightSwitchb.GetComponent<Animator>();
        fanSwitchAnimator = fanSwitch.GetComponent<Animator>();

        fanLightAnimator = fanLight.GetComponent<Animator>();
        bathroomLightAnimator = bathroomLight.GetComponent<Animator>();

        bathroomDoorAnimator = bathroomDoor.GetComponent<Animator>();
    }


    public void turnLightSwitch(bool lightSwitchOn)
    {
        if(lightSwitchOn)
        {
            lightSwitchAnimator.SetTrigger("Activate");
            fanLightAnimator.SetTrigger("Activate");
        }
        else
        {
            lightSwitchAnimator.SetTrigger("Deactivate");
            fanLightAnimator.SetTrigger("Deactivate");
        }
    }

    public void turnLightSwitchb(bool lightSwitchOn)
    {
        if (lightSwitchOn)
        {
            lightSwitchbAnimator.SetTrigger("Activate");
            bathroomLightAnimator.SetTrigger("Activate");
        }
        else
        {
            lightSwitchbAnimator.SetTrigger("Deactivate");
            bathroomLightAnimator.SetTrigger("Deactivate");
        }
    }

    public void turnFanSwitch(bool fanSwitchOn)
    {
        if (fanSwitchOn)
        {
            fanSwitchAnimator.SetTrigger("Activate");
        }
        else
        {
            fanSwitchAnimator.SetTrigger("Deactivate");
        }
    }

    public void openDoor(bool open)
    {
        if(open)
        {
            bathroomDoorAnimator.SetTrigger("Open");
        }
        else
        {
            bathroomDoorAnimator.SetTrigger("Close");
        }
    }
    

}
