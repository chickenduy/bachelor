using UnityEngine;
using System.Collections;

public class Water_Projectile_OBJ : MonoBehaviour
{
    public GameObject impactParticle;
    public GameObject projectileParticle;
    public GameObject[] trailParticles;
    public Vector3 impactNormal; //Used to rotate impactparticle.

    // Use this for initialization
    void Start()
    {
        Room_S.Instance.Use_Fire();
        Physics.IgnoreCollision(gameObject.GetComponent<BoxCollider>(), Player_S.Instance.GetComponent<CharacterController>());
        gameObject.transform.Translate(new Vector3(0, 0, 2f), Space.Self);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > 10 || transform.position.y < -3)
            Destroy(gameObject);
        gameObject.transform.Translate(new Vector3(0, 0, 0.25f), Space.Self);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "water_projectile") return;
        if (col.tag == "fire")
            Destroy(col.gameObject);

        impactParticle = Instantiate(impactParticle, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal)) as GameObject;

        //foreach (GameObject trail in trailParticles)
        //{
        //    GameObject curTrail = transform.Find(projectileParticle.name + "/" + trail.name).gameObject;
        //    curTrail.transform.parent = null;
        //    Destroy(curTrail, 3f);
        //}

        Destroy(projectileParticle, 2f);
        Destroy(impactParticle, 3f);
        Destroy(gameObject);
    }
}
