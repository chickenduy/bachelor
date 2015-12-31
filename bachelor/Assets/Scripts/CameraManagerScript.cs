using UnityEngine;
using System.Collections;

public class CameraManagerScript : MonoBehaviour {

    public int rayLength = 3;
    public StateManagerScript stateManager;

    // Use this for initialization
    void Start () {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, rayLength))
        {
            if (Input.GetKeyDown("e"))
            {
                print(hit.collider.tag);
                stateManager.action(hit.collider.tag);
            }
        }

    }

    void showOnGUI()
    {

    }

}
