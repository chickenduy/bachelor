using UnityEngine;
using System.Collections;

public class Obstacles : MonoBehaviour{


        private Ice ice_spawner;
        private Fire fire_spawner;
        private Power power_spawner;

        public bool[,] space;
        public bool[,] ice;
        public bool[,] fire;
        public bool[,] power;

        public Obstacles(int size, GameObject ice_obj, GameObject fire_obj, GameObject[] power_obj, int num)
        {
            space = new bool[size, size];
            ice = new bool[size, size];
            fire = new bool[size, size];
            power = new bool[size, size];
            ice_spawner = new Ice(ice_obj);
            fire_spawner = new Fire(fire_obj);
            power_spawner = new Power(power_obj, num);
        }

    }




