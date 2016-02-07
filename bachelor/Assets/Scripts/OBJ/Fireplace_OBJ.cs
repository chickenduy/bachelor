using UnityEngine;
using System.Collections;

public class Fireplace_OBJ : MonoBehaviour {

    private int id = 0;
    private Animator anim;
    private Light firelight;

    private ParticleSystem fire;
    private ParticleSystem.EmissionModule em;
    // Use this for initialization
    void Awake()
    {

        fire = gameObject.GetComponentInChildren<ParticleSystem>();
        em = fire.emission;
        em.enabled = false;
        firelight = gameObject.GetComponentInChildren<Light>();
        anim = gameObject.GetComponentInParent<Animator>();
        //register object in Singleton dictionary
        Check_For_ID();
    }

    public int Get_ID()
    {
        return id;
    }

    public void Check_For_ID()
    {
        if (Object_S.Instance.Check_For_ID(id))
        {
            id++;
            Check_For_ID();
        }
        else
        {
            Object_S.Instance.Register(id, gameObject, anim);
            Object_S.Instance.Register(id, firelight);
            Object_S.Instance.Register(fire);
        }
    }
}
