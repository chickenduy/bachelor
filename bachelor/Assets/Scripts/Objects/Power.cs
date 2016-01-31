using UnityEngine;
using System.Collections;

public class Power : MonoBehaviour
{
    public class PowerSpawner
    {
        private GameObject[] power;
        private int posX;
        private int posZ;
        private int power_spawn;

        public PowerSpawner(GameObject[] obj, int num)
        {
            power = obj;
        }


        public Obstacles.ObstacleArray TakePower(Obstacles.ObstacleArray obstacles, GameObject obj, bool[] abilities)
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
            Debug.Log("Set Array to false: " + posX + "/" + posZ);
            obstacles.power[posX, posZ] = false;
            Debug.Log("Destroy Book");
            Destroy(obj);
            

            return obstacles;
        }

        public Obstacles.ObstacleArray SpawnPower(int spawnNumber, Obstacles.ObstacleArray obstacles)
        {
            int spawn = 5;
            int test = spawn - power_spawn;
            Debug.Log("Spawn "+test+" Power Books");
            if (test < 0)
            {
                GameObject[] power_temp = GameObject.FindGameObjectsWithTag("power");
                for (int i = 0; i < -test; i++)
                {
                    Destroy(power_temp[i]);
                    GetArrayPosition(power_temp[i]);
                    obstacles.space[posX, posZ] = false;
                    obstacles.power[posX, posZ] = false;
                }
                power_spawn = test;
                return obstacles;
            }
            else if(test == 0)
            {
                return obstacles;
            }
            power_spawn = spawn;
            int numberX;
            int numberY;
            Vector3 vector;
            Quaternion qat = new Quaternion();
            qat.eulerAngles = new Vector3(270, 0, 0);
            for (int i = 0; i < test; i++)
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
                                vector = new Vector3(i * 4 - 48.5f, 1f, j * 4 - 48.5f);
                            }
                            else if (j > 10)
                            {
                                vector = new Vector3(i * 4 - 48.5f, 1f, (20 - j) * (-4) + 48.5f);
                            }
                            else
                            {
                                vector = new Vector3(i * 4 - 48.5f, 1f, 0);
                            }
                        }
                        else if (i > 10)
                        {
                            if (j < 10)
                            {
                                vector = new Vector3((20 - i) * (-4) + 48.5f, 1f, j * 4 - 48.5f);
                            }
                            else if (j > 10)
                            {
                                vector = new Vector3((20 - i) * (-4) + 48.5f, 1f, (20 - j) * (-4) + 48.5f);
                            }
                            else
                            {
                                vector = new Vector3((20 - i) * (-4) + 48.5f, 1f, 0);
                            }
                        }
                        else
                        {
                            if (j < 10)
                            {
                                vector = new Vector3(0, 1f, j * 4 - 48.5f);
                            }
                            else if (j > 10)
                            {
                                vector = new Vector3(0, 1f, (20 - j) * (-4) + 48.5f);
                            }
                            else
                            {
                                vector = new Vector3(0, 1f, 0);
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

        public int Power_A(int killfire)
        {
            //add more ability to kill fires
            return killfire + 2;
        }

        public void Power_B()
        {
            //change mats of walls
            //and ability to animate to bool !state

        }

        public void Power_C()
        {
            //deactivate collision with all walls (except inner and outer walls)
        }

    }


}




