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
        transform.position = new Vector3(Round_Number(transform.position.x + x * speed), Round_Number(transform.position.y), Round_Number(transform.position.z + z * speed));
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
                    Coming_From_Down(number, up, down, left, right);
                    break;
                case 2:
                    Coming_From_Right(number, up, down, left, right);
                    break;
                case 3:
                    Coming_From_Left(number, up, down, left, right);
                    break;
                case 4:
                    Coming_From_Up(number, up, down, left, right);
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
            x = -x;
            z = -z;
        }
        //random number for sphere to move into a direction

    }

    private void Coming_From_Down(int number, bool up, bool down, bool left, bool right)
    {
        if (up && right && left)
        {
            Move_Down();
        }
        if (!up && right && left)
        {
            //MoveUp();
        }
        if (up && !right && left)
        {
            Move_Right();
        }
        if (up && right && !left)
        {
            Move_Left();
        }
        if (!up && !right && left)
        {
            number = Random.Range(0, 2);
            switch (number)
            {
                case 0:
                    Move_Right();
                    break;
                case 1:
                    Move_Up();
                    break;
                default:
                    Debug.LogError("Sphere_S/ComingFromDown");
                    Reset_Position();
                    break;
            }
        }
        if (!up && right && !left)
        {
            number = Random.Range(0, 2);
            switch (number)
            {
                case 0:
                    Move_Left();
                    break;
                case 1:
                    Move_Up();
                    break;
                default:
                    Debug.LogError("Sphere_S/ComingFromDown");
                    Reset_Position();
                    break;
            }
        }
        if (up && !right && !left)
        {
            number = Random.Range(0, 2);
            switch (number)
            {
                case 0:
                    Move_Right();
                    break;
                case 1:
                    Move_Left();
                    break;
                default:
                    Debug.LogError("Sphere_S/ComingFromDown");
                    Reset_Position();
                    break;
            }
        }
        if (!up && !right && !left)
        {
            number = Random.Range(0, 3);
            switch (number)
            {
                case 0:
                    Move_Right();
                    break;
                case 1:
                    Move_Left();
                    break;
                case 2:
                    Move_Up();
                    break;
                default:
                    Debug.LogError("Sphere_S/ComingFromDown");
                    Reset_Position();
                    break;
            }
        }
    }

    private void Coming_From_Up(int number, bool up, bool down, bool left, bool right)
    {
        if (down && right && left)
        {
            Move_Up();
        }
        else if (!down && right && left)
        {
            //MoveDown();
        }
        else if (down && !right && left)
        {
            Move_Right();
        }
        else if (down && right && !left)
        {
            Move_Left();
        }
        else if (!down && !right && left)
        {
            number = Random.Range(0, 2);
            switch (number)
            {
                case 0:
                    Move_Right();
                    break;
                case 1:
                    Move_Down();
                    break;
                default:
                    Debug.LogError("Sphere_S/ComingFromUp");
                    Reset_Position();
                    break;
            }
        }
        else if (!down && right && !left)
        {
            number = Random.Range(0, 2);
            switch (number)
            {
                case 0:
                    Move_Left();
                    break;
                case 1:
                    Move_Down();
                    break;
                default:
                    Debug.LogError("Sphere_S/ComingFromUp");
                    Reset_Position();
                    break;
            }
        }
        else if (down && !right && !left)
        {
            number = Random.Range(0, 2);
            switch (number)
            {
                case 0:
                    Move_Right();
                    break;
                case 1:
                    Move_Left();
                    break;
                default:
                    Debug.LogError("Sphere_S/ComingFromUp");
                    Reset_Position();
                    break;
            }
        }
        else if (!down && !right && !left)
        {
            number = Random.Range(0, 3);
            switch (number)
            {
                case 0:
                    Move_Right();
                    break;
                case 1:
                    Move_Left();
                    break;
                case 2:
                    Move_Down();
                    break;
                default:
                    Debug.LogError("Sphere_S/ComingFromUp");
                    Reset_Position();
                    break;
            }
        }
    }
    private void Coming_From_Left(int number, bool up, bool down, bool left, bool right)
    {
        if (up && right && down)
        {
            Move_Left();
        }
        else if (!up && right && down)
        {
            Move_Up();
        }
        else if (up && !right && down)
        {
            //&MoveRight();
        }
        else if (up && right && !down)
        {
            Move_Down();
        }
        else if (!up && !right && down)
        {
            number = Random.Range(0, 2);
            switch (number)
            {
                case 0:
                    Move_Right();
                    break;
                case 1:
                    Move_Up();
                    break;
                default:
                    Debug.LogError("Sphere_S/ComingFromLeft");
                    Reset_Position();
                    break;
            }
        }
        else if (!up && right && !down)
        {
            number = Random.Range(0, 2);
            switch (number)
            {
                case 0:
                    Move_Down();
                    break;

                case 1:
                    Move_Up();
                    break;
                default:
                    Debug.LogError("Sphere_S/ComingFromLeft");
                    Reset_Position();
                    break;
            }
        }
        else if (up && !right && !down)
        {
            number = Random.Range(0, 2);
            switch (number)
            {
                case 0:
                    Move_Right();
                    break;
                case 1:
                    Move_Down();
                    break;
                default:
                    Debug.LogError("Sphere_S/ComingFromLeft");
                    Reset_Position();
                    break;
            }
        }
        else if (!up && !right && !down)
        {
            number = Random.Range(0, 3);
            switch (number)
            {
                case 0:
                    Move_Right();
                    break;
                case 1:
                    Move_Down();
                    break;
                case 2:
                    Move_Up();
                    break;
                default:
                    Debug.LogError("Sphere_S/ComingFromLeft");
                    Reset_Position();
                    break;
            }
        }
    }
    private void Coming_From_Right(int number, bool up, bool down, bool left, bool right)
    {
        if (up && left && down)
        {
            Move_Right();
        }
        else if (!up && left && down)
        {
            Move_Up();
        }
        else if (up && !left && down)
        {
            //MoveLeft();
        }
        else if (up && left && !down)
        {
            Move_Down();
        }
        else if (!up && !left && down)
        {
            number = Random.Range(0, 2);
            switch (number)
            {
                case 0:
                    Move_Left();
                    break;
                case 1:
                    Move_Up();
                    break;
                default:
                    Debug.LogError("Sphere_S/ComingFromRight");
                    Reset_Position();
                    break;

            }
        }
        else if (!up && left && !down)
        {
            number = Random.Range(0, 2);
            switch (number)
            {
                case 0:
                    Move_Down();
                    break;
                case 1:
                    Move_Up();
                    break;
                default:
                    Debug.LogError("Sphere_S/ComingFromRight");
                    Reset_Position();
                    break;
            }
        }
        else if (up && !left && !down)
        {
            number = Random.Range(0, 2);
            switch (number)
            {
                case 0:
                    Move_Left();
                    break;
                case 1:
                    Move_Down();
                    break;
                default:
                    Debug.LogError("Sphere_S/ComingFromRight");
                    Reset_Position();
                    break;
            }
        }
        else if (!up && !left && !down)
        {
            number = Random.Range(0, 3);
            switch (number)
            {
                case 0:
                    Move_Left();
                    break;
                case 1:
                    Move_Down();
                    break;
                case 2:
                    Move_Up();
                    break;
                default:
                    Debug.LogError("Sphere_S/ComingFromRight");
                    Reset_Position();
                    break;
            }
        }
    }
    //no solution for floating precision
    private float Round_Number(float num)
    {
        return Mathf.Round(num * 100.0f) / 100.0f;
    }
    private void Move_Right()
    {
        x = 1f;
        z = 0;
        rock.SetTrigger("Right");
    }
    private void Move_Left()
    {
        x = -1f;
        z = 0;
        rock.SetTrigger("Left");

    }
    private void Move_Up()
    {
        x = 0;
        z = 1f;
        rock.SetTrigger("Up");

    }
    private void Move_Down()
    {
        x = 0;
        z = -1f;
        rock.SetTrigger("Down");

    }
    private void Reset_Position()
    {
        Debug.Log("Reset Position");
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
