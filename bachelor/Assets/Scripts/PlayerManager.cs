﻿using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

    //public variables
    public CameraManager _CameraManager;
    public StateManager _StateManager;
    public ObstacleManager _ObstacleManager;

    public Transform _SpawnPoints;
    public Transform _RoomPositionPoint;
    public Transform _MazePositionPoint;

    //private variables
    private Transform[] spawnPoint; //all spawn points

    // Use this for initialization
    void Start () {
        spawnPoint = _SpawnPoints.GetComponentsInChildren<Transform>();
        _RoomPositionPoint = _RoomPositionPoint.GetComponent<Transform>();
        _MazePositionPoint = _MazePositionPoint.GetComponent<Transform>();
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
        if (Input.GetKeyDown("y"))
        {
            _StateManager.temperatureIndex++;
        }
        if (Input.GetKeyDown("x"))
        {
            _StateManager.temperatureIndex--;
        }
    }

    //respawn player in a random spawn point
    private void Respawn()
    {
        int i = Random.Range(0, spawnPoint.Length);
        transform.position = spawnPoint[i].transform.position;
    }

    //saves position in maze, teleports to room and back to maze; sets dreamState
    public void WakeSleep()
    {
        if (_StateManager.dreamState == true)
        {
            _MazePositionPoint.transform.position = transform.position;
            transform.position = _RoomPositionPoint.transform.position;
            _StateManager.dreamState = false;
            _CameraManager.fog.enabled = false;
            _CameraManager.blur.enabled = false;
        }
        else
        {
            transform.position = _MazePositionPoint.transform.position;
            _StateManager.dreamState = true;
            _ObstacleManager.fire(_StateManager.temperatureIndex);
            _ObstacleManager.ice(_StateManager.temperatureIndex);


            //print(_StateManager.temperatureIndex);
            print("firespawn: " + _ObstacleManager.fireSpawn);
            print("iceSpawn: " + _ObstacleManager.iceSpawn);
        }
        _StateManager.CheckDreamState();
    }


}
