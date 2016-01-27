using UnityEngine;
using System.Collections;

public class ObjectScript : MonoBehaviour
{
    public GameObject _Ice;
    public GameObject _Fire;
    public GameObject[] _Power;
    public int powerUpNumber = 10;

    private Obstacles.ObstacleArray obstacles;
    private Ice.IceSpawner ice;
    private Fire.FireSpawner fire;
    private Power.PowerSpawner power;

    // Use this for initialization
    void Start()
    {
        obstacles = new Obstacles.ObstacleArray(21, _Ice, _Fire, _Power, powerUpNumber);
    }

    //spawn other Obstacles
    public void SpawnObstacles(int temperature)
    {
        obstacles = fire.FireCalculator(temperature, obstacles);
        obstacles = ice.IceCalculator(temperature, obstacles);
        obstacles = power.SpawnPower(10, obstacles);
    }

    //take book
    public void TakePower(GameObject obj)
    {
        Debug.Log("Take Power");
        obstacles = power.TakePower(obstacles, obj);
        return;
    }



}
