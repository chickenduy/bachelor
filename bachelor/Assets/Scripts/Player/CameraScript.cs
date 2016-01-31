using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
    private PlayerScript _PlayerScript;

    public int rayLength = 3;
    public MonoBehaviour fog;
    public MonoBehaviour blur;

    // Use this for initialization
    void Start()
    {
        _PlayerScript = GetComponentInParent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);

        

        if (Input.GetKeyDown(KeyCode.E))
        {
            


            if (Physics.Raycast(ray, out hit, rayLength))
            {
                Debug.Log("Pressed E and hit: " + hit.collider.tag +" or " + hit.collider.name);
                _PlayerScript.Use(hit.collider);
            }
        }
    }


}
