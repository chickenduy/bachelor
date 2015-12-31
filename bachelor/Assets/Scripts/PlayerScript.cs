using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    //public variables
    public Camera playerCamera;
    public Transform playerSpawnPoints;
    public Transform roomPositionPoint;
    public Transform mazePositionPoint;
    public StateManagerScript script;

    //private variables
    private Transform[] spawnPoint; //all spawn points
    private Transform roomPosition;
    private Transform mazePosition;

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
            WakeSleep();
        }
    }

    //respawn player in a random spawn point
    private void Respawn()
    {
        int i = Random.Range(0, spawnPoint.Length);
        transform.position = spawnPoint[i].transform.position;
    }

    //saves position in maze, teleports to room and back to maze; sets dreamState
    private void WakeSleep()
    {
        if(script.dreamState == true)
        {
            mazePosition.transform.position = transform.position;
            transform.position = roomPosition.transform.position;
            script.dreamState = false;
        }
        else
        {
            transform.position = mazePosition.transform.position;
            script.dreamState = true;
        }
    }

}
