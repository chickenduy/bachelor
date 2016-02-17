using UnityEngine;
using System.Collections;

public class Camera_OBJ : MonoBehaviour
{
    private Camera cam;
    private bool played;
    private Animator anim;
    private AudioListener listener;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        cam = GetComponent<Camera>();
        listener = GetComponent<AudioListener>();
        listener.enabled = false;
        Disable_Camera();
        Camera_S.Instance.Register(this, gameObject.name);

    }

    //disable animation camera
    public void Disable_Camera()
    {
        cam.enabled = false;
        listener.enabled = false;
        Player_S.Instance.GetComponentInChildren<AudioListener>().enabled = true;
    }

    //enable animation camera
    public void Enable_Camera()
    {
        cam.enabled = true;
        listener.enabled = true;
        Player_S.Instance.GetComponentInChildren<AudioListener>().enabled = false;
    }

    public void Play_Anim(string trigger)
    {
        //animate depending on sleeping/waking and couch
        anim.SetTrigger(trigger);
    }
}
