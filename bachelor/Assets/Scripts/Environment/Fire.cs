using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour
{
    public class FireSpawner
    {
        private GameObject fire;
        private int fireSpawn;
        private int posX;
        private int posZ;

        public FireSpawner(GameObject obj)
        {
            fire = obj;
        }

        //calculate number of fires to spawn
        public Obstacles.ObstacleArray FireCalculator(int temperature, Obstacles.ObstacleArray obstacles)
        {

            int spawn = (temperature + 2) * 3;
            if (spawn < 0)
            {
                spawn = 0;
            }
            int test = spawn - fireSpawn;
            if (test < 0)
            {
                GameObject[] fire = GameObject.FindGameObjectsWithTag("fire");
                for (int i = 0; i < -test; i++)
                {
                    Destroy(fire[i]);
                    GetArrayPosition(fire[i]);
                    obstacles.space[posX, posZ] = false;
                    obstacles.fire[posX, posZ] = false;
                }
                fireSpawn = spawn;
                Debug.Log("Too Much Fire - Destroying Fire");
                return obstacles;
            }
            if (test == 0)
            {
                Debug.Log("No Changes");
                return obstacles;
            }
            obstacles = SpawnFire(test, obstacles);
            fireSpawn = spawn;
            Debug.Log(test + " Fire spawned");

            return obstacles;
        }

        private Obstacles.ObstacleArray SpawnFire(int spawnNumber, Obstacles.ObstacleArray obstacles)
        {
            int numberX;
            int numberY;
            Vector3 vector;
            Quaternion qat = new Quaternion();
            for (int i = 0; i < spawnNumber; i++)
            {
                numberX = Random.Range(0, 21);
                numberY = Random.Range(0, 21);
                if (obstacles.space[numberX, numberY] == false && obstacles.fire[numberX, numberY] == false && obstacles.ice[numberX, numberY] == false)
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
                    if (obstacles.space[i, j] == true && obstacles.fire[i, j] == false && obstacles.ice[i, j] == false)
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
                        obstacles.fire[i, j] = true;
                        Instantiate(fire, vector, qat);
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




