using UnityEngine;
using System.Collections;

public class AnimationManager: MonoBehaviour
{
    public LightingManager _LightingManager;
    public StateManager _StateManager;

    public GameObject _LightSwitch;
    public GameObject _LightSwitchBathroom;
    public GameObject _FanSwitch;
    public GameObject _Fan;
    public GameObject _Window;
    public GameObject _Drawer;
    public GameObject _Lighter;
    public GameObject _Fireplace;
    public GameObject _BathroomDoor;

    public GameObject _MovingWall1;


    private Animator lightSwitchAnimator;
    private Animator lightSwitchbAnimator;
    private Animator fanSwitchAnimator;

    private Animator fanAnimator;

    private Animator bathroomDoorAnimator;

    private Animator windowAnimator;

    private Animator drawerAnimator;

    private Animator fireAnimator;

    private Animator movingWallAnimator;

    void Start()
    {
        lightSwitchAnimator = _LightSwitch.GetComponent<Animator>();
        lightSwitchbAnimator = _LightSwitchBathroom.GetComponent<Animator>();
        fanSwitchAnimator = _FanSwitch.GetComponent<Animator>();

        fanAnimator = _Fan.GetComponent<Animator>();

        bathroomDoorAnimator = _BathroomDoor.GetComponent<Animator>();

        windowAnimator = _Window.GetComponent<Animator>();

        drawerAnimator = _Drawer.GetComponent<Animator>();
        fireAnimator = _Fireplace.GetComponentInChildren<Animator>();

        movingWallAnimator = _MovingWall1.GetComponent<Animator>();
    }


    public void TurnLightSwitch(bool state)
    {
        if (!state)
        {
            lightSwitchAnimator.SetTrigger("Activate");
            _LightingManager._Main.enabled = true;
            _StateManager.lightSwitchOn = true;
        }
        else
        {
            lightSwitchAnimator.SetTrigger("Deactivate");
            _LightingManager._Main.enabled = false;
            _StateManager.lightSwitchOn = false;
        }
    }

    public void TurnLightSwitchBathroom(bool state)
    {
        if (!state)
        {
            lightSwitchbAnimator.SetTrigger("Activate");
            _LightingManager._Bathroom.enabled = true;
            _StateManager.lightSwitchBathroomOn = true;
        }
        else
        {
            lightSwitchbAnimator.SetTrigger("Deactivate");
            _LightingManager._Bathroom.enabled = false;
            _StateManager.lightSwitchBathroomOn = false;
        }
    }

    public void TurnFanSwitch(bool state)
    {
        if (!state)
        {
            fanSwitchAnimator.SetTrigger("Activate");
            fanAnimator.SetTrigger("Activate");
            _StateManager.fanSwitchOn = true;
            _StateManager.windIndex = true;
        }
        else
        {
            fanSwitchAnimator.SetTrigger("Deactivate");
            fanAnimator.SetTrigger("Deactivate");
            _StateManager.fanSwitchOn = false;
            _StateManager.windIndex = false;
        }
    }

    public void OpenDoor(bool state)
    {
        if (!state)
        {
            bathroomDoorAnimator.SetTrigger("Open");
            _StateManager.doorOpen = true;
        }
        else
        {
            bathroomDoorAnimator.SetTrigger("Close");
            _StateManager.doorOpen = false;
        }
    }

    public void OpenWindow(bool state)
    {
        if (!state)
        {
            windowAnimator.SetTrigger("Open");
            _StateManager.windowOpen = true;
            _StateManager.temperatureIndex--;
        }
        else
        {
            windowAnimator.SetTrigger("Close");
            _StateManager.windowOpen = false;
            _StateManager.temperatureIndex++;
        }
    }

    public void OpenDrawer(bool state)
    {
        if (!state)
        {
            drawerAnimator.SetTrigger("Open");
            _StateManager.drawerOpen = true;
        }
        else
        {
            drawerAnimator.SetTrigger("Close");
            _StateManager.drawerOpen = false;
        }
    }

    public void LightFire(bool state)
    {
        if (_StateManager.haveLighter)
        {
            if (!state)
            {
                _LightingManager._FirePlace.enabled = true;
                _LightingManager.ManageFire(_LightingManager._FirePlace.enabled);
                fireAnimator.SetTrigger("Activate");
                fireAnimator.SetBool("isOn", true);
                _StateManager.firePlaceOn = true;
                _StateManager.temperatureIndex++;
            }
            else
            {
                fireAnimator.SetTrigger("Deactivate");
                _LightingManager._FirePlace.enabled = false;
                _LightingManager.ManageFire(_LightingManager._FirePlace.enabled);
                _StateManager.firePlaceOn = false;
                _StateManager.temperatureIndex--;

            }
        }
        else
        {
            //You need a lighter
        }
        
    }

    public void MoveWall()
    {
        if (Input.GetKeyDown("q"))
        {
            movingWallAnimator.SetTrigger("State1");
        }
        else if (Input.GetKeyDown("1"))
        {
            movingWallAnimator.SetTrigger("State2");
        }
    }


}
