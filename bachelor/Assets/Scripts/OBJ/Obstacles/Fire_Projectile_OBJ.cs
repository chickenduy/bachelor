using UnityEngine;
using System.Collections;

public class Fire_Projectile_OBJ : MonoBehaviour
{
    public GameObject impactParticle;
    public GameObject projectileParticle;
    public GameObject[] trailParticles;
    public Vector3 impactNormal; //Used to rotate impactparticle.

    // Use this for initialization
    void Start()
    {
        gameObject.transform.LookAt(Player_S.Instance.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(new Vector3(0, 0, 0.05f), Space.Self);
    }

    void OnTriggerEnter(Collider col)
    {

        impactParticle = Instantiate(impactParticle, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal)) as GameObject;

        if (col.gameObject.tag == "Player")
        {
            Destroy(projectileParticle);
            Destroy(impactParticle);
            Destroy(gameObject);
            Maze_S.Instance.Wake_Sleep_Hit();
            Player_S.Instance.Check_Dream_State();
            return;
        }

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
