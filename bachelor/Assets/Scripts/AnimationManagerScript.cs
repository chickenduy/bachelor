using UnityEngine;
using System.Collections;

public class AnimationManagerScript : MonoBehaviour {

    public GameObject lightSwitch;
    public GameObject lightSwitchb;
    public GameObject fanSwitch;
    public GameObject fanBladeBody;
    public GameObject fanBlades;
    public GameObject fanLight;
    public GameObject fan;
    public GameObject bathroomLight;

    public GameObject bathroomDoor;

    public StateManagerScript stateManager;

    private Animator lightSwitchAnimator;
    private Animator lightSwitchbAnimator;
    private Animator fanSwitchAnimator;

    private Animator fanLightAnimator;
    private Animator fanBladeAnimator;
    private Animator fanBladeBodyAnimator;
    private Animator fanAnimator;

    private Animator bathroomLightAnimator;

    private Animator bathroomDoorAnimator;

    void Awake()
    {
        lightSwitchAnimator = lightSwitch.GetComponent<Animator>();
        lightSwitchbAnimator = lightSwitchb.GetComponent<Animator>();
        fanSwitchAnimator = fanSwitch.GetComponent<Animator>();

        fanAnimator = fan.GetComponent<Animator>();
        fanLightAnimator = fanLight.GetComponent<Animator>();
        fanBladeAnimator = fanBlades.GetComponent<Animator>();
        fanBladeBodyAnimator = fanBladeBody.GetComponent<Animator>();
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
            fanAnimator.SetTrigger("Activate");
        }
        else
        {
            fanSwitchAnimator.SetTrigger("Deactivate");
            fanAnimator.SetTrigger("Deactivate");

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
