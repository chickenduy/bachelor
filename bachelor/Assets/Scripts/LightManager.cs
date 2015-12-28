using UnityEngine;
using System.Collections;

public class LightManager : MonoBehaviour {

    public bool lightswitch_Fire = false;
    public bool lightswitch_Fan = false;
    public bool lightswitch_Bath = false;
    public Light fire;
    public Light fan;
    public Light bath;


    // Use this for initialization
    void Start () {
        print(fire.enabled);
    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown("l") && fire.enabled == false)
        {
            fire.enabled = true;
            print("an");
        }
        else if (Input.GetKeyDown("l") && fire.enabled == true)
        {
            fire.enabled = false;
            print("aus");
        }

    }
}
