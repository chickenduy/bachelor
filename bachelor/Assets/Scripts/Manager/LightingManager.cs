using UnityEngine;
using System.Collections;

public class LightingManager : MonoBehaviour {

	public class L_Manager
    {
        private GameObject fire;

        public Light fireplace_light;
        public Light light_main;
        public Light light_bathroom;
        public Light player_light;

        private ParticleSystem fireplace_particle;
        private ParticleSystem.EmissionModule em;

        public L_Manager(GameObject fire_obj)
        {
            fire = fire_obj;
            fireplace_light = GameObject.Find("Fireplace Light").GetComponent<Light>();
            light_main = GameObject.Find("Main Light").GetComponent<Light>();
            light_bathroom = GameObject.Find("Bathroom Light").GetComponent<Light>();
            player_light = GameObject.Find("FirstPersonCharacter").GetComponent<Light>();
            fireplace_particle = GameObject.Find("Fireplace Fire").GetComponent<ParticleSystem>();
            em = fireplace_particle.emission;

            fireplace_light.enabled = false;
            light_main.enabled = false;
            light_bathroom.enabled = false;
            em.enabled = false;
        }
        public void PlayerLight(bool dreaming)
        {
            if (dreaming)
            {
                player_light.enabled = true;
            }
            else
            {
                player_light.enabled = false;
            }
        }

        public void ManageFire(bool state)
        {
            if (state)
            {
                fireplace_light.enabled = true;
                em.enabled = true;
            }
            else
            {
                fireplace_light.enabled = false;
                em.enabled = false;
                fireplace_particle.Clear();
            }
        }
    }
}
