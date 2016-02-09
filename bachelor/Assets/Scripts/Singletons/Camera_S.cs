using UnityEngine;
using System.Collections;

public class Camera_S : Singleton<Camera_S>
{
    // guarantee this will be always a singleton only - can't use the constructor!
    protected Camera_S() { }

    public int rayLength = 3;
    public MonoBehaviour fog;
    public MonoBehaviour blur;

    public AudioClip audio_clip;
    private AudioSource audio_source;

    private Camera cam;

    private Camera_OBJ cam_anim;

    void Start()
    {
        cam = gameObject.GetComponent<Camera>();
        audio_source = GetComponent<AudioSource>();
        audio_source.clip = audio_clip;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            audio_source.Play();
            RaycastHit hit;
            Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, rayLength))
            {
                Debug.Log("Pressed E and hit: " + hit.collider.tag + " or " + hit.collider.name);
                Player_S.Instance.Use(hit.collider);
            }
        }
    }

    public void Register(Camera_OBJ obj)
    {
        cam_anim = obj;
    }

    public void Wake_Up_Anim()
    {
        if (Player_S.Instance.Get_Dream_State() == false)
        {
            Background_Music_S.Instance.Disable_Background_Music();
            Player_S.Instance.gameObject.SetActive(false);

            cam_anim.Enable_Camera();
            cam_anim.Play_Anim();
            cam_anim.Play_Couroutine();
        }

        if (Player_S.Instance.Get_Dream_State() == true)
        {
            Background_Music_S.Instance.Enable_Background_Music();
            Player_S.Instance.gameObject.SetActive(true);
            cam_anim.Disable_Camera();
        }
    }

    public void Disable_Camera()
    {
        cam.enabled = false;
    }
    public void Enable_Camera()
    {
        cam.enabled = true;
    }


}
