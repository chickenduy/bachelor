using UnityEngine;
using System.Collections;

public class Fire_Sight_OBJ : MonoBehaviour
{
    private float wait_shoot;
    public GameObject bullet;
    public float shooting_interval;
    // Use this for initialization
    void Start()
    {
        shooting_interval = 3;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.LookAt(Player_S.Instance.transform);

        RaycastHit hit;
        Ray ray = new Ray(gameObject.transform.position, Player_S.Instance.transform.position - gameObject.transform.position);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                if (!Player_S.Instance.invincible)
                {
                    Instantiate(bullet, gameObject.transform.position, gameObject.transform.rotation);
                    wait_shoot += Time.time + shooting_interval;
                }
            }
        }
    }

}
