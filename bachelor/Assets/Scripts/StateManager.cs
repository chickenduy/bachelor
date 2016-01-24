using UnityEngine;
using System.Collections;

public class StateManager : MonoBehaviour
{
    public LightingManager _LightingManager;

    public bool dreamState;
    public bool lightSwitchOn;
    public bool lightSwitchBathroomOn;
    public bool fanSwitchOn;
    public bool doorOpen;
    public bool drawerOpen;
    public bool windowOpen;
    public bool haveLighter;
    public bool firePlaceOn;
    public bool toiletOpen;


    public int temperatureIndex = 0;
    public int lightIndex = 0;
    public float peeIndex = 0;
    public bool windIndex = false;

    // Use this for initialization
    void Start()
    {
        dreamState = true;
    }

    void Update()
    {

    }

    public void CheckDreamState()
    {
        _LightingManager.PlayerLight(dreamState);
    }

    //private void TturnLightSwitch(bool state)
    //{
    //    if (!state)
    //    {
    //        lightSwitchOn = true;
    //        lightIndex--;
    //        ceilingLight.enabled = true;
    //    }
    //    else
    //    {
    //        lightSwitchOn = false;
    //        lightIndex++;
    //        ceilingLight.enabled = false;
    //    }
    //    animationManager.turnLightSwitch(lightSwitchOn);
    //}


    //private void TurnLightSwitchB(bool state)
    //{
    //    if (!state)
    //    {
    //        lightSwitchBOn = true;
    //        lightIndex--;
    //        bathroomLight.enabled = true;
    //    }
    //    else
    //    {
    //        lightSwitchBOn = false;
    //        lightIndex++;
    //        bathroomLight.enabled = false;
    //    }
    //    animationManager.TurnLightSwitchBathroom(lightSwitchBOn);
    //}

    //private void TurnFanSwitch(bool state)
    //{
    //    if (!state)
    //    {
    //        fanSwitchOn = true;
    //        windIndex = true;
    //    }
    //    else
    //    {
    //        fanSwitchOn = false;
    //        windIndex = false;
    //    }
    //    animationManager.TurnFanSwitch(fanSwitchOn);
    //}

    //private void OpenDoor(bool state)
    //{
    //    if (!state)
    //    {
    //        bathroomDoorOpen = true;
    //    }
    //    else
    //    {
    //        bathroomDoorOpen = false;
    //    }
    //    animationManager.OpenDoor(bathroomDoorOpen);
    //}

    //private void OpenWindow(bool state)
    //{
    //    if (!state)
    //    {
    //        windowOpen = true;
    //        temperatureIndex--;
    //    }
    //    else
    //    {
    //        windowOpen = false;
    //        temperatureIndex++;
    //    }
    //    animationManager.openWindow(windowOpen);
    //}

    //public void LightFire(bool haveLighter)
    //{
    //    if (haveLighter)
    //    {
    //        if (firePlaceOn)
    //        {
    //            em.enabled = false;
    //            firePlaceOn = false;
    //            animationManager.lightFire(firePlaceOn);
    //            firePlaceFire.Clear();
    //            temperatureIndex--;

    //        }
    //        else
    //        {
    //            em.enabled = true;
    //            firePlaceOn = true;
    //            animationManager.lightFire(firePlaceOn);
    //            temperatureIndex++;
    //        }
    //    }
    //    else
    //    {
    //        em.enabled = false;
    //        firePlaceOn = false;
    //        firePlaceFire.Clear();
    //    }
    //}

    //private void OpenDrawer(bool state)
    //{
    //    if (!state)
    //    {
    //        drawerOpen = true;
    //    }
    //    else
    //    {
    //        drawerOpen = false;
    //    }
    //    animationManager.openDrawer(drawerOpen);
    //}

    //private void CheckChangeInTemp()
    //{

    //}


}