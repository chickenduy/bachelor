using UnityEngine;
using System.Collections;

public class ActionManager : MonoBehaviour
{

    public StateManager _StateManager;
    public AnimationManager _AnimationManager;

    public GameObject _Lighter;

    void Start()
    {
    }

    public void Use(string tag)
    {
        if (tag == "lightswitch")
        {
            _AnimationManager.TurnLightSwitch(_StateManager.lightSwitchOn);
        }
        if (tag == "fanswitch")
        {
            _AnimationManager.TurnFanSwitch(_StateManager.fanSwitchOn);
        }
        if (tag == "lightswitchb")
        {
            _AnimationManager.TurnLightSwitchBathroom(_StateManager.lightSwitchBathroomOn);
        }
        if (tag == "bathroomdoor")
        {
            _AnimationManager.OpenDoor(_StateManager.doorOpen);
        }

        if (tag == "window")
        {
            _AnimationManager.OpenWindow(_StateManager.windowOpen);
        }
        if (tag == "logs")
        {
            _AnimationManager.LightFire(_StateManager.firePlaceOn);
        }
        if (tag == "lighter")
        {
            _StateManager.haveLighter = true;
            Destroy(_Lighter);
        }
        if (tag == "drawer")
        {
            _AnimationManager.OpenDrawer(_StateManager.drawerOpen);
        }
        if (tag == "bed")
        {
            //playerManager.WakeSleep();
        }
        if(tag == "toilet")
        {
            _AnimationManager.OpenToilet(_StateManager.toiletOpen);
        }
    }
}