using UnityEngine;
using System.Collections;

public class CameraManagerScript : MonoBehaviour {
    public GameObject lightSwitch;
    public int rayLength = 10;

    private bool isOn = false;

    Rect showAction = new Rect(100, 25, Screen.width / 2, Screen.height / 2);

    // Use this for initialization
    void Start () {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, out hit, rayLength))
        {
            if(hit.collider.tag == "lightswitch")
            {
                lightSwitch.GetComponent<Animation>().Play("SwitchOn");
            }
        }

    }

}
