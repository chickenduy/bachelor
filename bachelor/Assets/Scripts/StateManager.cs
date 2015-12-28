using UnityEngine;
using System.Collections;

public class StateManager : MonoBehaviour {

    public GameObject firePlace;
    public GameObject window;
    public GameObject ceilingLight;
    public GameObject ceilingFan;

    public GameObject torches;
    private Light[] torch;

    private int temperatureIndex;
    private int lightIndex;
    private float peeIndex;
    private bool windIndex;


    // Use this for initialization
    void Start() {
        torch = torches.GetComponentsInChildren<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("k"))
        {
            torchesOut();
        }
    }

    private void torchesOut()
    {
        for(int i = 0; i<torch.Length; i++)
        {
            torch[i].enabled = false;
        }
        print("unlit");
    }
}
