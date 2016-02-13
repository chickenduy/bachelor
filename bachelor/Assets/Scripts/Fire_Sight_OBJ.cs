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
        wait_shoot = 3;
        shooting_interval = 3;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.LookAt(Player_S.Instance.transform);

        RaycastHit hit;
        Ray ray = new Ray(gameObject.transform.position, Player_S.Instance.transform.position - gameObject.transform.position);
        if (Physics.Raycast(ray, out hit, 25f))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                if (!Player_S.Instance.invincible)
                {
                    if (shooting_interval <= wait_shoot)
                    {
                        Instantiate(bullet, gameObject.transform.position, gameObject.transform.rotation);
                        wait_shoot = 0;
                    }
                    else
                    {
                        wait_shoot += Time.deltaTime;
                    }

                }
            }
        }
    }

}
