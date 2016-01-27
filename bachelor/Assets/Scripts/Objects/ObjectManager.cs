using UnityEngine;
using System.Collections;

public class ObjectManager : MonoBehaviour
{
    public class O_Manager
    {
        //public GameObject _Ice;
        //public GameObject _Fire;
        //public GameObject[] _Power;

        //private int powerUpNumber;

        private Obstacles.ObstacleArray obstacles;
        private Ice.IceSpawner ice;
        private Fire.FireSpawner fire;
        private Power.PowerSpawner power;

        // Use this for initialization
        //void Start()
        //{
        //    obstacles = new Obstacles.ObstacleArray(21, _Ice, _Fire, _Power, powerUpNumber);
        //}

        public O_Manager(GameObject ice_obj, GameObject fire_obj, GameObject[] power_obj, int power_num)
        {
            obstacles = new Obstacles.ObstacleArray(21, ice_obj, fire_obj, power_obj, power_num);
            fire = new Fire.FireSpawner(fire_obj);
            ice = new Ice.IceSpawner(ice_obj);
            power = new Power.PowerSpawner(power_obj, power_num);
        }

        //spawn other Obstacles
        public void SpawnObstacles(int temperature)
        {
            Debug.Log("spawn");
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

    



}
