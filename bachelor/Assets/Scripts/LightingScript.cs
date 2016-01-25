using UnityEngine;
using System.Collections;

public class LightingScript : MonoBehaviour {

    public GameObject _Fire;

    public Light _FirePlace;
    public Light _Main;
    public Light _Bathroom;
    public Light _Player;

    private ParticleSystem fireParticle;
    public ParticleSystem.EmissionModule em;

    // Use this for initialization
    void Start () {
        _Main.enabled = false;
        _Bathroom.enabled = false;
        _FirePlace.enabled = false;
        fireParticle = _Fire.GetComponent<ParticleSystem>();
        em = fireParticle.emission;
        em.enabled = false;
    }
	
    public void PlayerLight(bool dreaming)
    {
        if (dreaming)
        {
            _Player.enabled = true;
        }
        else
        {
            _Player.enabled = false;
        }
    }

    public void ManageFire(bool state)
    {
        if (!state)
        {
            em.enabled = false;
            fireParticle.Clear();
        }
        else
        {
            em.enabled = true;
        }
    }

}
