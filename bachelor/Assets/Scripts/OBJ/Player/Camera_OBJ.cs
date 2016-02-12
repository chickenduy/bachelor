using UnityEngine;
using System.Collections;

public class Camera_OBJ : MonoBehaviour
{
    private Camera cam;
    private bool played;
    private Animator anim;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        cam = GetComponent<Camera>();
        cam.enabled = false;
        Camera_S.Instance.Register(this, gameObject.tag);
    }

    //
    public void Disable_Camera()
    {
        cam.enabled = false;
    }

    //enable animation camera
    public void Enable_Camera()
    {
        cam.enabled = true;
    }

    public void Play_Anim(string trigger)
    {
        //animate depending on sleeping/waking and couch
        anim.SetTrigger(trigger);
    }
}
