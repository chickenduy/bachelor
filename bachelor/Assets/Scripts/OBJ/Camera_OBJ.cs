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
        anim = gameObject.GetComponent<Animator>();
        cam = gameObject.GetComponent<Camera>();
        cam.enabled = false;
        Camera_S.Instance.Register(this);
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

    public void Play_Anim(string trigger, bool state)
    {
        //animate depending on sleeping/waking and couch
        anim.SetTrigger(trigger);
        anim.SetBool("couch", state);
    }

}
