using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    //Spawn Points
    public Transform playerSpawnPoints;
    public bool respawn = false;
    private Transform[] spawnPoint;
    private bool respawnToggle = false;

    public Transform wakePositionPoint;
    public Transform sleepPositionPoint;
    private Transform wakePosition;
    private Transform formerPosition;
    private bool awake = false;



    // Use this for initialization
    void Start () {
        spawnPoint = playerSpawnPoints.GetComponentsInChildren<Transform>();
        wakePosition = wakePositionPoint;
        formerPosition = sleepPositionPoint;
        print(wakePosition.transform.position);
        print(formerPosition.transform.position);

    }

    // Update is called once per frame
    void Update () {
        if(respawnToggle != respawn)
        {
            Respawn();
            respawn = false;
        }
        else
        {
            respawnToggle = respawn;
        }

        if (Input.GetKeyDown("t"))
        {
            WakeSleep();
        }


    }

    private void Respawn()
    {
        int i = Random.Range(0, spawnPoint.Length);
        transform.position = spawnPoint[i].transform.position;
    }

    private void WakeSleep()
    {
        if(awake == false)
        {
            sleepPositionPoint.transform.position = transform.position;
            formerPosition.transform.position = sleepPositionPoint.transform.position;
            transform.position = wakePosition.transform.position;
            awake = true;
        }
        else
        {

            transform.position = formerPosition.transform.position;
            awake = false;
        }
    }
}
