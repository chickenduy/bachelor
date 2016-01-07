using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    //public variables
    public Camera playerCamera;
    public Transform playerSpawnPoints;
    public Transform roomPositionPoint;
    public Transform mazePositionPoint;
    public StateManagerScript stateManagerScript;

    //private variables
    private Transform[] spawnPoint; //all spawn points
    public Transform roomPosition;
    public Transform mazePosition;

    // Use this for initialization
    void Start () {
        spawnPoint = playerSpawnPoints.GetComponentsInChildren<Transform>();
        roomPosition = roomPositionPoint;
        mazePosition = mazePositionPoint;
        
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown("r"))
        {
            Respawn();
        }
        if (Input.GetKeyDown("t"))
        {
            stateManagerScript.WakeSleep();
        }

        if (Input.GetKeyDown("z"))
        {
            playerCamera.fieldOfView = 30;
        }
        if (Input.GetKeyUp("z"))
        {
            playerCamera.fieldOfView = 60;
        }
        if (Input.GetKeyDown("q"))
        {
            stateManagerScript.LightFire(true);
        }

    }


    //respawn player in a random spawn point
    private void Respawn()
    {
        int i = Random.Range(0, spawnPoint.Length);
        transform.position = spawnPoint[i].transform.position;
    }

    

}
