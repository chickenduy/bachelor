using UnityEngine;
using System.Collections;

public class Spawns_OBJ : MonoBehaviour
{
    // Use this for initialization
    private int id = 0;
    void Start()
    {
        //register object in Singleton dictionary
        Check_For_ID();
    }
    private void Check_For_ID()
    {
        if (gameObject.tag == "Respawn")
        {
            if (Spawns_S.Instance.Check_For_ID(id))
            {
                id++;
                Check_For_ID();
            }
            else
            {
                Spawns_S.Instance.Register(id, gameObject.transform, gameObject.tag);
            }
        }
        else
        {
            Spawns_S.Instance.Register(id, gameObject.transform, gameObject.tag);
        }
    }
}
