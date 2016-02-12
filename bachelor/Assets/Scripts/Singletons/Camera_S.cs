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
    private Camera_OBJ cam_wake_bed;
    private Camera_OBJ cam_sleep_bed;

    void Start()
    {
        cam = GetComponent<Camera>();
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

    public void Register(Camera_OBJ obj, string tag)
    {
        if (tag == "cam_wake_bed")
            cam_wake_bed = obj;
        else if (tag == "cam_sleep_bed")
            cam_sleep_bed = obj;
    }

    public void Wake_Up_Anim()
    {
        //disable background music
        Background_Music_S.Instance.Disable_Background_Music();
        //disable player camera
        cam.enabled = false;
        //enable animation camera;
        cam_wake_bed.Enable_Camera();
        //animate waking up
        cam_wake_bed.Play_Anim("Wake");
        //start coroutine in 4 seconds (length of wake up animation)
        StartCoroutine(Enable_Disable_Wake_Bed(4f));
    }

    public void Go_To_Sleep_Anim()
    {
        //disable player camera
        cam.enabled = false;
        //enable animation camera
        cam_sleep_bed.Enable_Camera();
        //animate going to sleep
        cam_sleep_bed.Play_Anim("Sleep");
        //start coroutine in 4 seconds (length of going to sleep animation)
        StartCoroutine(Enable_Disable_Sleep_Bed(4f));
    }

    //disable player camera
    public void Disable_Camera()
    {
        cam.enabled = false;
    }
    //enable player camera
    public void Enable_Camera()
    {
        cam.enabled = true;
    }

    //coroutine
    private IEnumerator Enable_Disable_Wake_Bed(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //enable player camera after waitTime seconds
        cam.enabled = true;
        //also disable animation camera
        cam_wake_bed.Disable_Camera();
        //enable background music if player is going to sleep
    }
    private IEnumerator Enable_Disable_Sleep_Bed(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //enable player camera after waitTime seconds
        cam.enabled = true;
        //also disable animation camera
        cam_sleep_bed.Disable_Camera();
        //enable background music if player is going to sleep
        Background_Music_S.Instance.Enable_Background_Music();

    }




}
