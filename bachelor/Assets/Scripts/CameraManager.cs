using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{
    public ActionManager _ActionManager;

    public int rayLength = 3;
    public MonoBehaviour fog;
    public MonoBehaviour blur;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, rayLength))
        {
            if (Input.GetKeyDown("e"))
            {
                print(hit.collider.tag);
                _ActionManager.Use(hit.collider.tag);
            }
        }

    }


}
