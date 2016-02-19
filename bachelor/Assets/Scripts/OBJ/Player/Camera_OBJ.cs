using UnityEngine;
using System.Collections;

public class Camera_OBJ : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

        Camera_S.Instance.Register(this, gameObject.name);
    }

    //disable animation camera
    public void Disable_Camera()
    {
        gameObject.GetComponent<Camera>().enabled = false;
        gameObject.GetComponent<AudioListener>().enabled = false;
        Player_S.Instance.GetComponentInChildren<AudioListener>().enabled = true;
    }

    //enable animation camera
    public void Enable_Camera()
    {
        gameObject.GetComponent<Camera>().enabled = true;
        gameObject.GetComponent<AudioListener>().enabled = true;
        Player_S.Instance.GetComponentInChildren<AudioListener>().enabled = false;
    }

    public void Play_Anim(string trigger)
    {
        //animate depending on sleeping/waking and couch
        gameObject.GetComponent<Animator>().SetTrigger(trigger);
    }
}
