using UnityEngine;
using System.Collections;

public class Power : MonoBehaviour
{
    public class PowerSpawner
    {
        private GameObject[] power;

        private int powerSpawn;
        private int posX;
        private int posZ;

        public PowerSpawner(GameObject[] obj, int num)
        {
            power = obj;
            powerSpawn = num;
        }


        private Obstacles.ObstacleArray TakePower(Obstacles.ObstacleArray obstacles)
        {
            return obstacles;
        }

        public Obstacles.ObstacleArray SpawnPower(int spawnNumber, Obstacles.ObstacleArray obstacles)
        {
            int numberX;
            int numberY;
            Vector3 vector;
            Quaternion qat = new Quaternion();
            qat.eulerAngles = new Vector3(270, 0, 0);
            for (int i = 0; i < spawnNumber; i++)
            {
                numberX = Random.Range(0, 21);
                numberY = Random.Range(0, 21);
                if (obstacles.space[numberX, numberY] == false && obstacles.fire[numberX, numberY] == false && obstacles.ice[numberX, numberY] == false && obstacles.power[numberX, numberY] == false)
                {
                    obstacles.space[numberX, numberY] = true;
                }
                else
                {
                    i--;
                }
            }
            for (int i = 0; i < 21; i++)
            {
                for (int j = 0; j < 21; j++)
                {
                    if (obstacles.space[i, j] == true && obstacles.fire[i, j] == false && obstacles.ice[i, j] == false && obstacles.power[i, j] == false)
                    {
                        if (i < 10)
                        {
                            if (j < 10)
                            {
                                vector = new Vector3(i * 4 - 48.5f, 2.3f, j * 4 - 48.5f);
                            }
                            else if (j > 10)
                            {
                                vector = new Vector3(i * 4 - 48.5f, 2.3f, (20 - j) * (-4) + 48.5f);
                            }
                            else
                            {
                                vector = new Vector3(i * 4 - 48.5f, 2.3f, 0);
                            }
                        }
                        else if (i > 10)
                        {
                            if (j < 10)
                            {
                                vector = new Vector3((20 - i) * (-4) + 48.5f, 2.3f, j * 4 - 48.5f);
                            }
                            else if (j > 10)
                            {
                                vector = new Vector3((20 - i) * (-4) + 48.5f, 2.3f, (20 - j) * (-4) + 48.5f);
                            }
                            else
                            {
                                vector = new Vector3((20 - i) * (-4) + 48.5f, 2.3f, 0);
                            }
                        }
                        else
                        {
                            if (j < 10)
                            {
                                vector = new Vector3(0, 2.3f, j * 4 - 48.5f);
                            }
                            else if (j > 10)
                            {
                                vector = new Vector3(0, 2.3f, (20 - j) * (-4) + 48.5f);
                            }
                            else
                            {
                                vector = new Vector3(0, 2.3f, 0);
                            }
                        }
                        obstacles.power[i, j] = true;
                        Instantiate(power[Random.Range(0, power.Length)], vector, qat);
                    }
                }
            }
            return obstacles;
        }
        private void GetArrayPosition(GameObject obj)
        {
            if (obj.transform.position.x < 0)
            {
                posX = (int)((obj.transform.position.x + 48.5f) / 4);
            }
            else if (obj.transform.position.x > 0)
            {
                posX = (int)(20 - ((obj.transform.position.x - 48.5f) / (-4)));
            }
            else
            {
                posX = 0;
            }

            if (obj.transform.position.z < 0)
            {
                posZ = (int)((obj.transform.position.z + 48.5) / 4);
            }
            else if (obj.transform.position.z > 0)
            {
                posZ = (int)(20 - ((obj.transform.position.z - 48.5f) / (-4)));
            }
            else
            {
                posZ = 0;
            }

        }


    }


}




