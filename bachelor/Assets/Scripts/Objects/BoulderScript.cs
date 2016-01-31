using UnityEngine;
using System.Collections;

public class BoulderScript : MonoBehaviour
{

    public float x = 0;
    public float z = 1f;

    private Vector3 temp;
    public float speed = 0.125f;
    private int rayLength = 2;

    private bool up;
    private bool down;
    private bool left;
    private bool right;

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
        temp = transform.position;
        transform.position = new Vector3(RoundNumber(transform.position.x + x * speed), RoundNumber(transform.position.y), RoundNumber(transform.position.z + z * speed));
        //print(transform.position.x);
        //TestPosition();
    }

    public void OnTriggerEnter(Collider col)
    {
        int number = 0;
        RaycastHit hitUP;
        RaycastHit hitDOWN;
        RaycastHit hitLEFT;
        RaycastHit hitRIGHT;
        Ray rayUP = new Ray(col.transform.position, Vector3.forward);
        Ray rayDOWN = new Ray(col.transform.position, Vector3.back);
        Ray rayLEFT = new Ray(col.transform.position, Vector3.left);
        Ray rayRIGHT = new Ray(col.transform.position, Vector3.right);
        up = Physics.Raycast(rayUP, out hitUP, rayLength);
        down = Physics.Raycast(rayDOWN, out hitDOWN, rayLength);
        left = Physics.Raycast(rayLEFT, out hitLEFT, rayLength);
        right = Physics.Raycast(rayRIGHT, out hitRIGHT, rayLength);




        //if (col.tag == "Player")
        //{
        //    Debug.Log("Wake Up");
        //    col.transform.position = new Vector3(150f, 1, 150f);
        //}
        //if (col.tag == "ice")
        //{
        //    Destroy(col.gameObject);
        //    Debug.Log("Destroy ICE");
        //}

        //Debug.Log("Collision: " + col.name + " - Position: " + transform.position + " - Temp: " + temp);
        if (col.tag == "return")
        {
            Return();
            return;
        }
        else
        {
            //coming from down
            if (transform.position.z > temp.z)
            {
                //Debug.Log("coming from bottom");
                ComingFromDown(number, up, down, left, right);
                return;
            }
            //coming from right
            if (transform.position.x < temp.x)
            {
                //Debug.Log("coming from right");
                ComingFromRight(number, up, down, left, right);
                return;
            }
            //coming from left
            if (transform.position.x > temp.x)
            {
                //Debug.Log("coming from left");
                ComingFromLeft(number, up, down, left, right);
                return;
            }
            //coming from up
            if (transform.position.z < temp.z)
            {
                //Debug.Log("coming from left");
                ComingFromUp(number, up, down, left, right);
                return;
            }
        }
    }

    private void Return()
    {
        //coming from right
        if (transform.position.x < temp.x)
        {
            MoveRight();
            return;
        }
        //coming from bottom
        else if (transform.position.z > temp.z)
        {
            MoveDown();
            return;
        }
        //coming from left
        else if (transform.position.x > temp.x)
        {
            MoveLeft();
            return;
        }
        //coming from top
        else if (transform.position.z < temp.z)
        {
            MoveUp();
            return;
        }
        else
        {
            Debug.LogError("Can't return");
            return;
        }
    }
    private void ComingFromDown(int number, bool up, bool down, bool left, bool right)
    {
        if (up && right && left)
        {
            MoveDown();
            return;
        }
        else if (!up && right && left)
        {
            MoveUp();
            return;
        }
        else if (up && !right && left)
        {
            MoveRight();
            return;
        }
        else if (up && right && !left)
        {
            MoveLeft();
            return;
        }
        else if (!up && !right && left)
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
                    Debug.LogError("Couldn't Move");
                    ResetPos();
                    break;
            }
            return;
        }
        else if (!up && right && !left)
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
                    Debug.LogError("Couldn't Move");
                    ResetPos();
                    break;
            }
            return;
        }
        else if (up && !right && !left)
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
                    Debug.LogError("Couldn't Move");
                    ResetPos();
                    break;
            }
            return;
        }
        else if (!up && !right && !left)
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
                    Debug.LogError("Couldn't Move");
                    ResetPos();
                    break;
            }
            return;
        }
    }
    private void ComingFromUp(int number, bool up, bool down, bool left, bool right)
    {
        Debug.Log("coming from top");
        if (down && right && left)
        {
            MoveUp();
            return;
        }
        else if (!down && right && left)
        {
            MoveDown();
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
                    Debug.LogError("Couldn't Move");
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
                    Debug.LogError("Couldn't Move");
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
                    Debug.LogError("Couldn't Move");
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
                    Debug.LogError("Couldn't Move");
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
            MoveLeft();
            return;
        }
        else if (!up && right && down)
        {
            MoveUp();
            return;
        }
        else if (up && !right && down)
        {
            MoveRight();
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
                    Debug.LogError("Couldn't Move");
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
                    Debug.LogError("Couldn't Move");
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
                    Debug.LogError("Couldn't Move");
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
                    Debug.LogError("Couldn't Move");
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
            MoveRight();
            return;
        }
        else if (!up && left && down)
        {
            MoveUp();
            return;
        }
        else if (up && !left && down)
        {
            MoveLeft();
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
                    Debug.LogError("Couldn't Move");
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
                    Debug.LogError("Couldn't Move");
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
                    Debug.LogError("Couldn't Move");
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
                    Debug.LogError("Couldn't Move");
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
        rock.SetTrigger("Right");
    }
    private void MoveLeft()
    {
        x = -1f;
        z = 0;
        rock.SetTrigger("Left");
    }
    private void MoveUp()
    {
        x = 0;
        z = 1f;
        rock.SetTrigger("Up");
    }
    private void MoveDown()
    {
        x = 0;
        z = -1f;
        rock.SetTrigger("Down");
    }
    private void ResetPos()
    {
        x = 0;
        z = 0;
        transform.position = new Vector3(0, 1.5f, 0);
    }



}
