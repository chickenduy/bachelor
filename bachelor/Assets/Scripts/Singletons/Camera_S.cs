using UnityEngine;
using System.Collections;

public class Camera_S : Singleton<Camera_S>
{
    // guarantee this will be always a singleton only - can't use the constructor!
    protected Camera_S() { }

    //private
    private Vector3 water_projectile_position;
    private AudioSource audio_source;
    private Camera player_camera;
    private Camera_OBJ cam_wake_bed;
    private Camera_OBJ cam_sleep_bed;
    private Camera_OBJ cam_wake_couch;
    private Camera_OBJ cam_sleep_couch;

    //private visible
    [SerializeField]
    private int rayLength = 3;
    [SerializeField]
    private AudioClip click_sound_effect;
    [SerializeField]
    private GameObject _Water_Projectile;

    //getter/setter visible
    [SerializeField]
    private MonoBehaviour _fog;
    public MonoBehaviour fog
    {
        get
        {
            return _fog;
        }
    }
    [SerializeField]
    private MonoBehaviour _blur;
    public MonoBehaviour blur
    {
        get
        {
            return _blur;
        }
    }
    [SerializeField]
    private MonoBehaviour _motion;
    public MonoBehaviour motion
    {
        get
        {
            return _motion;
        }
    }

    /*----------------------------------------------------------------------------------------------------*/

    void Start()
    {
        player_camera = GetComponent<Camera>();
        audio_source = GetComponent<AudioSource>();
        audio_source.clip = click_sound_effect;
    }

    // Update is called once per frame
    void Update()
    {
        //waits for a Press E
        if (Input.GetKeyDown(KeyCode.E))
        {
            //plays a click sound
            audio_source.Play();
            //send out a ray
            RaycastHit hit;
            Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
            //if ray hits
            if (Physics.Raycast(ray, out hit, rayLength))
            {
                //Use the hit object
                Debug.Log("Pressed E and hit: " + hit.collider.tag + " or " + hit.collider.name);
                Player_S.Instance.Use(hit.collider);
            }
        }
        //waits for left mouse button click
        //only if he is in the dream and has the ability to shoot fire
        if (Input.GetKeyDown(KeyCode.Mouse0) && Player_S.Instance.dream_state && Room_S.Instance.killfire > 0)
        {
            //create a water projectile with the players rotation
            water_projectile_position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
            Instantiate(_Water_Projectile, water_projectile_position, gameObject.transform.rotation);
        }
    }

    //register the animation cameras
    public void Register(Camera_OBJ obj, string name)
    {
        if (name == "Camera Wake Bed")
            cam_wake_bed = obj;
        else if (name == "Camera Sleep Bed")
            cam_sleep_bed = obj;
        else if (name == "Camera Sleep Couch")
            cam_sleep_couch = obj;
        else if (name == "Camera Wake Couch")
            cam_wake_couch = obj;
    }
    //animate waking up
    public void Wake_Up_Anim(bool couch)
    {
        //freeze player movement so he can't run around in the room
        Player_S.Instance.Stop_Movement();
        //disable background music (set volume to 0/ so you can hear the radio) 
        Background_Music_S.Instance.Disable_Background_Music();
        //disable player camera
        player_camera.enabled = false;
        //enable animation camera;
        //animate waking up depending on the couch
        if (!couch)
        {
            cam_wake_bed.Enable_Camera();
            cam_wake_bed.Play_Anim("Wake");
        }
        else
        {
            cam_wake_couch.Enable_Camera();
            cam_wake_couch.Play_Anim("Wake");
        }
        //start coroutine in 4 seconds (length of wake up animation)
        StartCoroutine(Enable_Disable_Wake(4f, couch));
    }
    //animate going to sleep
    public void Go_To_Sleep_Anim(bool couch)
    {
        //freeze player movement so he can't run around in the maze
        Player_S.Instance.Stop_Movement();
        //disable player camera
        player_camera.enabled = false;
        //animate going to sleep depending on the couch
        if (!couch)
        {
            cam_sleep_bed.Enable_Camera();
            cam_sleep_bed.Play_Anim("Sleep");
        }
        else
        {
            cam_sleep_couch.Enable_Camera();
            cam_sleep_couch.Play_Anim("Sleep");
        }
        //start coroutine in 4 seconds (length of going to sleep animation)
        StartCoroutine(Enable_Disable_Sleep(4f, couch));
    }
    //disable player camera
    public void Disable_Camera()
    {
        player_camera.enabled = false;
    }
    //enable player camera
    public void Enable_Camera()
    {
        player_camera.enabled = true;
    }
    //coroutine
    private IEnumerator Enable_Disable_Wake(float waitTime, bool couch)
    {
        yield return new WaitForSeconds(waitTime);
        //enable player camera after waitTime seconds
        player_camera.enabled = true;
        //also disable animation camera depending on which object used for sleeping
        if (couch)
        {
            cam_wake_bed.Disable_Camera();
        }
        else
        {
            cam_wake_couch.Disable_Camera();
        }
        //unfreeze the player movement
        Player_S.Instance.invincible = false;
        Player_S.Instance.Resume_Movement();
    }
    private IEnumerator Enable_Disable_Sleep(float waitTime, bool couch)
    {
        yield return new WaitForSeconds(waitTime);
        //enable player camera after waitTime seconds
        player_camera.enabled = true;
        //also disable animation camera
        if (!couch)
        {
            cam_sleep_bed.Disable_Camera();
        }
        else
        {
            cam_sleep_couch.Disable_Camera();
        }
        //enable background music if player is going to sleep
        Background_Music_S.Instance.Enable_Background_Music();
        //unfreeze the player moveemnt
        Player_S.Instance.invincible = false;
        Player_S.Instance.Resume_Movement();
    }
    public IEnumerator Become_Lucid(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //disable camera scripts
        blur.enabled = false;
        motion.enabled = false;
    }
}
