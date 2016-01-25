using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {

    //public variables
    public CameraScript _CameraManager;
    public StateScript _StateManager;
    public ObstacleScript _ObstacleManager;

    public Transform _SpawnPoints;
    public Transform _RoomPositionPoint;
    public Transform _MazePositionPoint;

    public bool isDead;

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
            _StateManager.CheckDreamState();
            WakeSleep();
        }
        if (Input.GetKeyDown("+"))
        {
            _StateManager.temperatureIndex++;
        }
        if (Input.GetKeyDown("#"))
        {
            _StateManager.temperatureIndex--;
        }
        if (Input.GetKeyDown("o"))
        {
            _StateManager.peeIndex = 0.2f;
        }
        if (Input.GetKeyDown("l"))
        {
            _StateManager.peeIndex = 0.65f;
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
            _ObstacleManager.Fire(_StateManager.temperatureIndex);
            _ObstacleManager.Ice(_StateManager.temperatureIndex);
            _ObstacleManager.Waterfall(_StateManager.peeIndex);

            //print(_StateManager.temperatureIndex);
            print("firespawn: " + _ObstacleManager.fireSpawn);
            print("iceSpawn: " + _ObstacleManager.iceSpawn);
        }
        _StateManager.CheckDreamState();
    }


}
