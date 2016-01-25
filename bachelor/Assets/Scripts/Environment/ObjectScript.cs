using UnityEngine;
using System.Collections;

public class ObjectScript : MonoBehaviour {

    public GameObject _Ice;
    public GameObject _Fire;

    private Obstacles.ObstacleArray obstacles;
    private Ice.IceSpawner ice;
    private Fire.FireSpawner fire;

	// Use this for initialization
	void Start () {
        obstacles = new Obstacles.ObstacleArray(21);
        ice = new Ice.IceSpawner(_Ice);
        fire = new Fire.FireSpawner(_Fire);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SpawnObstacles(int temperature)
    {
        obstacles = fire.FireCalculator(temperature, obstacles);
        obstacles = ice.IceCalculator(temperature, obstacles);
    }


}
