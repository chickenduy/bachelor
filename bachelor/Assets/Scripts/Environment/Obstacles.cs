using UnityEngine;
using System.Collections;

public class Obstacles : MonoBehaviour{

	public class ObstacleArray
    {
        public bool[,] space;
        public bool[,] ice;
        public bool[,] fire;
        public bool[,] power;

        public ObstacleArray(int size)
        {
            space = new bool[size, size];
            ice = new bool[size, size];
            fire = new bool[size, size];
            power = new bool[size, size];
        }

    }



}
