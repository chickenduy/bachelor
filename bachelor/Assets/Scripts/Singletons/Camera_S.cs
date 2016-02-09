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

    void Start()
    {
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


}
