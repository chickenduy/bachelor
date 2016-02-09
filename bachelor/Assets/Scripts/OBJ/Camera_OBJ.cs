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

    // Update is called once per frame
    public void Disable_Camera()
    {
        cam.enabled = false;
    }

    public void Enable_Camera()
    {
        cam.enabled = true;
    }

    public void Play_Anim()
    {
        anim.SetBool("state", true);
    }

    public void Play_Couroutine()
    {
        StartCoroutine(Wake_Up(4.5f));
    }

    IEnumerator Wake_Up(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Player_S.Instance.gameObject.SetActive(true);
        anim.SetBool("state", false);
        cam.enabled = false;
    }


}
