using UnityEngine;
using System.Collections;

public class Obstacles : MonoBehaviour{

	public class ObstacleArray
    {
        private Ice.IceSpawner ice_spawner;
        private Fire.FireSpawner fire_spawner;
        private Power.PowerSpawner power_spawner;

        public bool[,] space;
        public bool[,] ice;
        public bool[,] fire;
        public bool[,] power;

        public ObstacleArray(int size, GameObject ice_obj, GameObject fire_obj, GameObject[] power_obj, int num)
        {
            space = new bool[size, size];
            ice = new bool[size, size];
            fire = new bool[size, size];
            power = new bool[size, size];
            ice_spawner = new Ice.IceSpawner(ice_obj);
            fire_spawner = new Fire.FireSpawner(fire_obj);
            power_spawner = new Power.PowerSpawner(power_obj, num);
        }

    }



}
