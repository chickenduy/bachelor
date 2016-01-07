using UnityEngine;
using System.Collections;

public class AnimationManagerScript : MonoBehaviour {

    public GameObject _LightSwitch;
    public GameObject _LightSwitchb;
    public GameObject _FanSwitch;
    public GameObject _Fan;
    public GameObject _Window;
    public GameObject _Drawer;
    public GameObject _Lighter;
    public GameObject _Fireplace;


    public GameObject bathroomDoor;

    public StateManagerScript stateManager;

    private Animator lightSwitchAnimator;
    private Animator lightSwitchbAnimator;
    private Animator fanSwitchAnimator;

    private Animator fanAnimator;

    private Animator bathroomDoorAnimator;

    private Animator windowAnimator;

    private Animator drawerAnimator;
    private Animator lighterAnimator;

    private Animator fireLightAnimator;

    void Awake()
    {
        lightSwitchAnimator = _LightSwitch.GetComponent<Animator>();
        lightSwitchbAnimator = _LightSwitchb.GetComponent<Animator>();
        fanSwitchAnimator = _FanSwitch.GetComponent<Animator>();

        fanAnimator = _Fan.GetComponent<Animator>();

        bathroomDoorAnimator = bathroomDoor.GetComponent<Animator>();

        windowAnimator = _Window.GetComponent<Animator>();

        drawerAnimator = _Drawer.GetComponent<Animator>();
        lighterAnimator = _Lighter.GetComponent<Animator>();
        fireLightAnimator = _Fireplace.GetComponentInChildren<Animator>();
    }


    public void turnLightSwitch(bool lightSwitchOn)
    {
        if(lightSwitchOn)
        {
            lightSwitchAnimator.SetTrigger("Activate");
        }
        else
        {
            lightSwitchAnimator.SetTrigger("Deactivate");
        }
    }

    public void turnLightSwitchB(bool lightSwitchBOn)
    {
        if (lightSwitchBOn)
        {
            lightSwitchbAnimator.SetTrigger("Activate");
        }
        else
        {
            lightSwitchbAnimator.SetTrigger("Deactivate");
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

    public void openDoor(bool doorOpen)
    {
        if(doorOpen)
        {
            bathroomDoorAnimator.SetTrigger("Open");
        }
        else
        {
            bathroomDoorAnimator.SetTrigger("Close");
        }
    }

    public void openWindow(bool windowOpen)
    {
        if (windowOpen)
        {
            windowAnimator.SetTrigger("Open");
        }
        else
        {
            windowAnimator.SetTrigger("Close");
        }
    }

    public void openDrawer(bool drawerOpen)
    {
        if (drawerOpen)
        {
            drawerAnimator.SetTrigger("Open");
            if (GameObject.FindWithTag("lighter"))
                lighterAnimator.SetTrigger("Open");
        }
        else
        {
            drawerAnimator.SetTrigger("Close");
            if (GameObject.FindWithTag("lighter"))
                lighterAnimator.SetTrigger("Close");

        }
    }

    public void lightFire(bool fireOn)
    {
        if (fireOn)
        {
            fireLightAnimator.SetTrigger("Activate");
            fireLightAnimator.SetBool("isOn", true);
        }
        else
        {
            fireLightAnimator.SetTrigger("Deactivate");
        }
    }


}
