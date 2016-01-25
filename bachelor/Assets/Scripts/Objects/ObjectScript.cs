using UnityEngine;
using System.Collections;

public class ObjectScript : MonoBehaviour {
    public GameObject _Ice;
    public GameObject _Fire;
    public GameObject[] _Power;
    public int powerUpNumber = 10;

    private Obstacles.ObstacleArray obstacles;
    private Ice.IceSpawner ice;
    private Fire.FireSpawner fire;
    private Power.PowerSpawner power;

	// Use this for initialization
	void Start () {

        obstacles = new Obstacles.ObstacleArray(21);
        ice = new Ice.IceSpawner(_Ice);
        fire = new Fire.FireSpawner(_Fire);
        power = new Power.PowerSpawner(_Power, powerUpNumber);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SpawnObstacles(int temperature)
    {
        obstacles = fire.FireCalculator(temperature, obstacles);
        obstacles = ice.IceCalculator(temperature, obstacles);
        obstacles = power.SpawnPower(10, obstacles);
    }

    public void TakePowerUp()
    {

    }


}
