using UnityEngine;
using System.Collections;

public class ActionScript : MonoBehaviour
{

    public StateScript _StateManager;
    public AnimationScript _AnimationManager;

    public GameObject _Lighter;

    void Start()
    {
    }

    public void Use(Collider col)
    {
        if (col.gameObject.name == "Switch Light Bathroom")
        {
            _AnimationManager.TurnLightSwitchBathroom(_StateManager.switchLightBathroomOn);
        }
        if (col.gameObject.name == "Switch Light")
        {
            _AnimationManager.TurnLightSwitch(_StateManager.switchLightOn);
        }
        if (col.gameObject.name == "Switch Fan")
        {
            _AnimationManager.TurnFanSwitch(_StateManager.switchFanOn);
        }
        if (col.gameObject.tag == "bathroomdoor")
        {
            _AnimationManager.OpenDoor(_StateManager.doorOpen);
        }
        if (col.gameObject.tag == "drawer")
        {
            _AnimationManager.OpenDrawer(_StateManager.drawerOpen);
        }
        if (col.gameObject.name == "Toilet")
        {
            _AnimationManager.OpenToilet(_StateManager.toiletOpen);
        }
        if (col.gameObject.tag == "window")
        {
            _AnimationManager.OpenWindow(_StateManager.windowOpen);
        }
        if (col.gameObject.name == "Logs")
        {
            _AnimationManager.LightFire(_StateManager.firePlaceOn);
        }
        if (col.gameObject.name == "Lighter")
        {
            _StateManager.haveLighter = true;
            Destroy(col.gameObject);
        }
        if(col.gameObject.tag == "power")
        {
            //either using tags or using name
            if (col.gameObject.name == "BookOpenA(Clone)")
            {
                Debug.Log("Picked Up Book A");
                Destroy(col.gameObject);
            }
            else if (col.gameObject.name == "BookOpenB(Clone)")
            {
                Debug.Log("Picked Up Book A");
                Destroy(col.gameObject);
            }
            else if (col.gameObject.name == "BookOpenC(Clone)")
            {
                Debug.Log("Picked Up Book C");
                Destroy(col.gameObject);
            }
        }
    }

}