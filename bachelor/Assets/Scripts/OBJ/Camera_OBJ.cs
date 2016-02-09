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
    }

    // Update is called once per frame
    void Update()
    {
        if(Player_S.Instance.Get_Dream_State() == false)
        {
            if (!played)
            {
                Background_Music_S.Instance.Disable_Background_Music();
                cam.enabled = true;
                played = true;
                Camera_S.Instance.Disable_Camera();
                anim.SetBool("state", played);
                StartCoroutine(Wake_Up(4.5f));
            }
        }
        if(Player_S.Instance.Get_Dream_State() == true)
        {
            Background_Music_S.Instance.Enable_Background_Music();
            Camera_S.Instance.Enable_Camera();
            played = false;
            anim.SetBool("state", played);

        }

    }

    IEnumerator Wake_Up(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Camera_S.Instance.Enable_Camera();
        cam.enabled = false;
    }


}
