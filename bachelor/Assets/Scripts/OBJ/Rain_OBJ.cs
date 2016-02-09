using UnityEngine;
using System.Collections;

public class Rain_OBJ : MonoBehaviour
{

    private float minEmission;
    private float maxEmission;

    // Update is called once per frame
    void Update()
    {
        Check_For_Rain();
    }


    private void Check_For_Rain()
    {
        //disable Rain while in Room
        gameObject.GetComponent<Renderer>().enabled = Player_S.Instance.Get_Dream_State();
        if (Room_S.Instance.Get_Pee() > 0.5f)
        {
            if (Room_S.Instance.Get_Pee() >= 0.5f && Room_S.Instance.Get_Pee() < 0.7f)
            {
                minEmission = 3125;
                maxEmission = 3125;
            }
            else if (Room_S.Instance.Get_Pee() >= 0.6f && Room_S.Instance.Get_Pee() < 0.8f)
            {
                minEmission = 6250;
                maxEmission = 6250;
            }
            else if (Room_S.Instance.Get_Pee() >= 0.7f && Room_S.Instance.Get_Pee() < 0.9f)
            {
                minEmission = 12500;
                maxEmission = 12500;
            }
            else if (Room_S.Instance.Get_Pee() >= 0.8f && Room_S.Instance.Get_Pee() < 1f)
            {
                minEmission = 25000;
                maxEmission = 25000;
            }
            else if (Room_S.Instance.Get_Pee() >= 0.9f)
            {
                minEmission = 50000;
                maxEmission = 50000;
            }

        }
        else
        {
            minEmission = 0;
            maxEmission = 0;
        }
        gameObject.GetComponentInChildren<EllipsoidParticleEmitter>().minEmission = minEmission;
        gameObject.GetComponentInChildren<EllipsoidParticleEmitter>().maxEmission = maxEmission;

    }
}
