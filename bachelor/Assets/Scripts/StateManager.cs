using UnityEngine;
using System.Collections;

public class StateManager : MonoBehaviour {

    public GameObject firePlace;
    public GameObject window;
    public GameObject ceilingLight;
    public GameObject ceilingFan;

    public GameObject torches;
    private Light[] torch;
    private ParticleSystem[] torchflame;

    private int temperatureIndex;
    private int lightIndex;
    private float peeIndex;
    private bool windIndex;

    // Use this for initialization
    void Start() {
        torch = torches.GetComponentsInChildren<Light>();
        torchflame = torches.GetComponentsInChildren<ParticleSystem>();
        temperatureIndex = 0;
        lightIndex = 0;
        peeIndex = 0;
        windIndex = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("k"))
        {
            temperatureIndex = temperatureIndex - 1;
            print(temperatureIndex);
        }


        if(temperatureIndex == -1)
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
        for (int i = 0; i < torch.Length; i++)
        {
            var em = torchflame[i].emission;
            torchflame[i].Clear();
            em.enabled = false;
        }
        print("unlit");
    }
}
