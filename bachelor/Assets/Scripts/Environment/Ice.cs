using UnityEngine;
using System.Collections;

public class Ice : MonoBehaviour
{
    public class IceSpawner
    {
        private GameObject ice;
        private int iceSpawn;
        private int posX;
        private int posZ;

        public IceSpawner(GameObject obj)
        {
            ice = obj;
        }

        public Obstacles.ObstacleArray IceCalculator(int temperature, Obstacles.ObstacleArray obstacles)
        {
            int spawn = (temperature - 2) * (-3);
            if (spawn < 0)
            {
                spawn = 0;
            }
            int test = spawn - iceSpawn;
            if (test < 0)
            {
                GameObject[] ice = GameObject.FindGameObjectsWithTag("ice");
                for (int i = 0; i < -test; i++)
                {
                    Destroy(ice[i]);
                    GetArrayPosition(ice[i]);
                    obstacles.space[posX, posZ] = false;
                    obstacles.ice[posX, posZ] = false;
                }
                iceSpawn = spawn;
                Debug.Log("Too Much Ice - Destroying Ice");
                return obstacles;
            }
            if (test == 0)
            {
                Debug.Log("No Changes");
                return obstacles;
            }
            obstacles = SpawnIce(test, obstacles);
            iceSpawn = spawn;
            Debug.Log(test + " Ice spawned");
            return obstacles;
        }


        private Obstacles.ObstacleArray SpawnIce(int spawnNumber, Obstacles.ObstacleArray obstacles)
        {
            int numberX;
            int numberY;

            Vector3 vector;
            Quaternion qat = new Quaternion();

            for (int i = 0; i < spawnNumber; i++)
            {
                numberX = Random.Range(0, 21);
                numberY = Random.Range(0, 21);
                if (obstacles.space[numberX, numberY] == false && obstacles.ice[numberX, numberY] == false && obstacles.fire[numberX, numberY] == false)
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
                    if (obstacles.space[i, j] == true && obstacles.ice[i, j] == false && obstacles.fire[i, j] == false)
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
                        obstacles.ice[i, j] = true;
                        Instantiate(ice, vector, qat);
                    }
                }
            }
            return obstacles;
        }


        private void GetArrayPosition(GameObject obj)
        {
            if(obj.transform.position.x < 0)
            {
                posX = (int)((obj.transform.position.x + 48.5f) / 4);
            }
            else if(obj.transform.position.x > 0)
            {
                posX = (int)(20 - ((obj.transform.position.x - 48.5f)/(-4)));
            }
            else
            {
                posX = 0;
            }

            if(obj.transform.position.z < 0)
            {
                posZ = (int) ((obj.transform.position.z + 48.5)/4);
            }
            else if(obj.transform.position.z > 0)
            {
                posZ = (int) (20 - ((obj.transform.position.z - 48.5f)/(-4)));
            }
            else
            {
                posZ = 0;
            }

        }
    }

    


}




