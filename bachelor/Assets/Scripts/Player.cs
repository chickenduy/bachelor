using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    //Spawn Points
    public Transform playerSpawnPoints;
    public bool respawn = false;

    private Transform[] spawnPoint;
    private bool toggle = false;


	// Use this for initialization
	void Start () {
        spawnPoint = playerSpawnPoints.GetComponentsInChildren<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        if(toggle != respawn)
        {
            Respawn();
            respawn = false;
        }
        else
        {
            toggle = respawn;
        }
	
	}

    private void Respawn()
    {
        int i = Random.Range(0, spawnPoint.Length);
        transform.position = spawnPoint[i].transform.position;
    }
}
