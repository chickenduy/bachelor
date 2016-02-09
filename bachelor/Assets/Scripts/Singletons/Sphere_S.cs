using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sphere_S : Singleton<Sphere_S>
{
    // guarantee this will be always a singleton only - can't use the constructor!
    protected Sphere_S() { }

    public float x = 0f;
    public float z = 0f;

    private Vector3 temp;
    public float speed = 0.125f;

    private Animator rock;

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1f;
        rock = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //strore the last position of the sphere
        temp = transform.position;
        //move the sphere with a specified speed in a direction
        transform.position = new Vector3(RoundNumber(transform.position.x + x * speed), RoundNumber(transform.position.y), RoundNumber(transform.position.z + z * speed));
    }

    

    //when sphere collides with a moving point
    public void Calculate_Direction(int id, bool up, bool down, bool left, bool right)
    {
        if (id == 0)
        {
            int number = 0;

            switch (Coming_From_Direction())
            {
                case 1:
                    ComingFromDown(number, up, down, left, right);
                    break;
                case 2:
                    ComingFromRight(number, up, down, left, right);
                    break;
                case 3:
                    ComingFromLeft(number, up, down, left, right);
                    break;
                case 4:
                    ComingFromUp(number, up, down, left, right);
                    break;
                case 0:
                    break;
                default:
                    Debug.LogError("Something wrong in Sphere_S/Calculate_Direction");
                    break;
            }
        }
        else if (id == 1)
        {
            Return();
        }
        //random number for sphere to move into a direction

    }

    private void Return()
    {
        x = -x;
        z = -z;
        return;
    }

    private void ComingFromDown(int number, bool up, bool down, bool left, bool right)
    {
        if (up && right && left)
        {
            Return();
            return;
        }
        if (!up && right && left)
        {
            //MoveUp();
            return;
        }
        if (up && !right && left)
        {
            MoveRight();
            return;
        }
        if (up && right && !left)
        {
            MoveLeft();
            return;
        }
        if (!up && !right && left)
        {
            number = Random.Range(0, 2);
            switch (number)
            {
                case 0:
                    MoveRight();
                    break;
                case 1:
                    MoveUp();
                    break;
                default:
                    Debug.LogError("Sphere_S/ComingFromDown");
                    ResetPos();
                    break;
            }
            return;
        }
        if (!up && right && !left)
        {
            number = Random.Range(0, 2);
            switch (number)
            {
                case 0:
                    MoveLeft();
                    break;
                case 1:
                    MoveUp();
                    break;
                default:
                    Debug.LogError("Sphere_S/ComingFromDown");
                    ResetPos();
                    break;
            }
            return;
        }
        if (up && !right && !left)
        {
            number = Random.Range(0, 2);
            switch (number)
            {
                case 0:
                    MoveRight();
                    break;
                case 1:
                    MoveLeft();
                    break;
                default:
                    Debug.LogError("Sphere_S/ComingFromDown");
                    ResetPos();
                    break;
            }
            return;
        }
        if (!up && !right && !left)
        {
            number = Random.Range(0, 3);
            switch (number)
            {
                case 0:
                    MoveRight();
                    break;
                case 1:
                    MoveLeft();
                    break;
                case 2:
                    MoveUp();
                    break;
                default:
                    Debug.LogError("Sphere_S/ComingFromDown");
                    ResetPos();
                    break;
            }
            return;
        }
    }
    private void ComingFromUp(int number, bool up, bool down, bool left, bool right)
    {
        if (down && right && left)
        {
            Return();
            return;
        }
        else if (!down && right && left)
        {
            //MoveDown();
            return;
        }
        else if (down && !right && left)
        {
            MoveRight();
            return;
        }
        else if (down && right && !left)
        {
            MoveLeft();
            return;
        }
        else if (!down && !right && left)
        {
            number = Random.Range(0, 2);
            switch (number)
            {
                case 0:
                    MoveRight();
                    break;
                case 1:
                    MoveDown();
                    break;
                default:
                    Debug.LogError("Sphere_S/ComingFromUp");
                    ResetPos();
                    break;
            }
            return;
        }
        else if (!down && right && !left)
        {
            number = Random.Range(0, 2);
            switch (number)
            {
                case 0:
                    MoveLeft();
                    break;
                case 1:
                    MoveDown();
                    break;
                default:
                    Debug.LogError("Sphere_S/ComingFromUp");
                    ResetPos();
                    break;
            }
            return;
        }
        else if (down && !right && !left)
        {
            number = Random.Range(0, 2);
            switch (number)
            {
                case 0:
                    MoveRight();
                    break;
                case 1:
                    MoveLeft();
                    break;
                default:
                    Debug.LogError("Sphere_S/ComingFromUp");
                    ResetPos();
                    break;
            }
            return;
        }
        else if (!down && !right && !left)
        {
            number = Random.Range(0, 3);
            switch (number)
            {
                case 0:
                    MoveRight();
                    break;
                case 1:
                    MoveLeft();
                    break;
                case 2:
                    MoveDown();
                    break;
                default:
                    Debug.LogError("Sphere_S/ComingFromUp");
                    ResetPos();
                    break;
            }
            return;
        }
    }
    private void ComingFromLeft(int number, bool up, bool down, bool left, bool right)
    {
        if (up && right && down)
        {
            Return();
            return;
        }
        else if (!up && right && down)
        {
            MoveUp();
            return;
        }
        else if (up && !right && down)
        {
            //&MoveRight();
            return;
        }
        else if (up && right && !down)
        {
            MoveDown();
            return;
        }
        else if (!up && !right && down)
        {
            number = Random.Range(0, 2);
            switch (number)
            {
                case 0:
                    MoveRight();
                    break;
                case 1:
                    MoveUp();
                    break;
                default:
                    Debug.LogError("Sphere_S/ComingFromLeft");
                    ResetPos();
                    break;
            }
            return;
        }
        else if (!up && right && !down)
        {
            number = Random.Range(0, 2);
            switch (number)
            {
                case 0:
                    MoveDown();
                    break;

                case 1:
                    MoveUp();
                    break;
                default:
                    Debug.LogError("Sphere_S/ComingFromLeft");
                    ResetPos();
                    break;
            }
            return;
        }
        else if (up && !right && !down)
        {
            number = Random.Range(0, 2);
            switch (number)
            {
                case 0:
                    MoveRight();
                    break;
                case 1:
                    MoveDown();
                    break;
                default:
                    Debug.LogError("Sphere_S/ComingFromLeft");
                    ResetPos();
                    break;
            }
            return;
        }
        else if (!up && !right && !down)
        {
            number = Random.Range(0, 3);
            switch (number)
            {
                case 0:
                    MoveRight();
                    break;
                case 1:
                    MoveDown();
                    break;
                case 2:
                    MoveUp();
                    break;
                default:
                    Debug.LogError("Sphere_S/ComingFromLeft");
                    ResetPos();
                    break;
            }
            return;
        }
    }
    private void ComingFromRight(int number, bool up, bool down, bool left, bool right)
    {
        if (up && left && down)
        {
            Return();
            return;
        }
        else if (!up && left && down)
        {
            MoveUp();
            return;
        }
        else if (up && !left && down)
        {
            //MoveLeft();
            return;
        }
        else if (up && left && !down)
        {
            MoveDown();
            return;
        }
        else if (!up && !left && down)
        {
            number = Random.Range(0, 2);
            switch (number)
            {
                case 0:
                    MoveLeft();
                    break;
                case 1:
                    MoveUp();
                    break;
                default:
                    Debug.LogError("Sphere_S/ComingFromRight");
                    ResetPos();
                    break;

            }
            return;
        }
        else if (!up && left && !down)
        {
            number = Random.Range(0, 2);
            switch (number)
            {
                case 0:
                    MoveDown();
                    break;
                case 1:
                    MoveUp();
                    break;
                default:
                    Debug.LogError("Sphere_S/ComingFromRight");
                    ResetPos();
                    break;
            }
            return;
        }
        else if (up && !left && !down)
        {
            number = Random.Range(0, 2);
            switch (number)
            {
                case 0:
                    MoveLeft();
                    break;
                case 1:
                    MoveDown();
                    break;
                default:
                    Debug.LogError("Sphere_S/ComingFromRight");
                    ResetPos();
                    break;
            }
            return;
        }
        else if (!up && !left && !down)
        {
            number = Random.Range(0, 3);
            switch (number)
            {
                case 0:
                    MoveLeft();
                    break;
                case 1:
                    MoveDown();
                    break;
                case 2:
                    MoveUp();
                    break;
                default:
                    Debug.LogError("Sphere_S/ComingFromRight");
                    ResetPos();
                    break;
            }
            return;
        }
    }
    //no solution for floating precision
    private float RoundNumber(float num)
    {
        return Mathf.Round(num * 100.0f) / 100.0f;
    }
    private void MoveRight()
    {
        x = 1f;
        z = 0;
    }
    private void MoveLeft()
    {
        x = -1f;
        z = 0;
    }
    private void MoveUp()
    {
        x = 0;
        z = 1f;
    }
    private void MoveDown()
    {
        x = 0;
        z = -1f;
    }
    private void ResetPos()
    {
        x = 0;
        z = 0;
        transform.position = new Vector3(0, 1.5f, 0);
    }

    private int Coming_From_Direction()
    {
        //sphere coming from down
        if (transform.position.z > temp.z)
        {
            return 1;
        }
        //sphere coming from right
        if (transform.position.x < temp.x)
        {
            return 2;
        }
        //sphere coming from left
        if (transform.position.x > temp.x)
        {
            return 3;
        }
        //sphere coming from up
        if (transform.position.z < temp.z)
        {
            return 4;
        }
        else
        {
            Debug.LogError("Sphere_S/Coming_From_Direction");
            return 0;
        }
    }


}
