using UnityEngine;
using System.Collections;

public class AnimationManagerScript : MonoBehaviour {

    public GameObject lightSwitch;
    public StateManagerScript stateManager;



    public void turnLightSwitch(bool lightSwitchOn)
    {
        if (!lightSwitchOn)
        {
            print("turn lights on");
            lightSwitch.GetComponent<Animation>().Play("TurnLightOn");
            stateManager.lightSwitchOn = !stateManager.lightSwitchOn;
        }
        else
        {
            print("turn lights off");
            lightSwitch.GetComponent<Animation>().Play("TurnLightOff");
            stateManager.lightSwitchOn = !stateManager.lightSwitchOn;
        }
    }
    	
}
