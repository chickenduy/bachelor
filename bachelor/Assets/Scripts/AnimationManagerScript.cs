using UnityEngine;
using System.Collections;

public class AnimationManagerScript : MonoBehaviour {

    public GameObject lightSwitch;
    public GameObject fanSwitch;
    public GameObject fanLight;

    public StateManagerScript stateManager;

    private Animator lightSwitchAnimator;
    private Animator fanSwitchAnimator;
    private Animator fanLightAnimator;

    void Awake()
    {
        lightSwitchAnimator = lightSwitch.GetComponent<Animator>();
        fanSwitchAnimator = fanSwitch.GetComponent<Animator>();
        fanLightAnimator = fanLight.GetComponent<Animator>();
    }


    public void turnLightSwitch(bool lightSwitchOn)
    {
        if(lightSwitchOn)
        {
            lightSwitchAnimator.SetTrigger("Activate");
            fanLightAnimator.SetTrigger("Activate");
        }
        if (!lightSwitchOn)
        {
            lightSwitchAnimator.SetTrigger("Deactivate");
            fanLightAnimator.SetTrigger("Deactivate");
        }
    }

    public void turnFanSwitch(bool fanSwitchOn)
    {
        if (fanSwitchOn)
        {
            fanSwitchAnimator.SetTrigger("Activate");
        }
        if (!fanSwitchOn)
        {
            fanSwitchAnimator.SetTrigger("Deactivate");
        }
    }

    public void turnLight(bool light)
    {
        if (light)
        {
            fanLightAnimator.SetTrigger("Activcate");
        }
        else
        {
            fanLightAnimator.SetTrigger("Deactivate");
        }
    }
 
}
