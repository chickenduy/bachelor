using UnityEngine;
using System.Collections;

public class BoulderScript : MonoBehaviour
{

    public float x = 0;
    public float z = 1f;

    private Vector3 temp;
    public float speed = 0.5f;
    private int rayLength = 2;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("Move", 0, 0.01F);
    }

    // Update is called once per frame
    void Move()
    {
        print(RoundNumber(8.241f));
        temp = transform.position;
        transform.position = new Vector3(RoundNumber(transform.position.x + x * speed), RoundNumber(transform.position.y), RoundNumber(transform.position.z + z * speed));
        //print(transform.position.x);
        //TestPosition();

    }

    public void OnTriggerEnter(Collider col)
    {
        Debug.Log("Calc Direction");
        int number;
        RaycastHit hitUP;
        RaycastHit hitDOWN;
        RaycastHit hitLEFT;
        RaycastHit hitRIGHT;
        Ray rayUP = new Ray(col.transform.position, Vector3.forward);
        Ray rayDOWN = new Ray(col.transform.position, Vector3.back);
        Ray rayLEFT = new Ray(col.transform.position, Vector3.left);
        Ray rayRIGHT = new Ray(col.transform.position, Vector3.right);

        if (col.tag == "Player")
        {
            Debug.Log("Wake Up");
            col.transform.position = new Vector3(150f, 1, 150f);
        }
        if (col.tag == "ice")
        {
            Destroy(col.gameObject);
            Debug.Log("Destroy ICE");
        }

        //if (col.tag == "special")
        //{
        //coming from right
        if (transform.position.x < temp.x)
        {
            if (Physics.Raycast(rayUP, out hitUP, rayLength) && Physics.Raycast(rayLEFT, out hitLEFT, rayLength) && Physics.Raycast(rayDOWN, out hitDOWN, rayLength))
            {
                MoveRight();
            }
            else if (!Physics.Raycast(rayUP, out hitUP, rayLength) && Physics.Raycast(rayLEFT, out hitLEFT, rayLength) && Physics.Raycast(rayDOWN, out hitDOWN, rayLength))
            {
                MoveUp();
            }
            else if (Physics.Raycast(rayUP, out hitUP, rayLength) && !Physics.Raycast(rayLEFT, out hitLEFT, rayLength) && Physics.Raycast(rayDOWN, out hitDOWN, rayLength))
            {
                MoveLeft();
            }
            else if (Physics.Raycast(rayUP, out hitUP, rayLength) && Physics.Raycast(rayLEFT, out hitLEFT, rayLength) && !Physics.Raycast(rayDOWN, out hitDOWN, rayLength))
            {
                MoveDown();
            }
            else if (!Physics.Raycast(rayUP, out hitUP, rayLength) && !Physics.Raycast(rayLEFT, out hitLEFT, rayLength) && Physics.Raycast(rayDOWN, out hitDOWN, rayLength))
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
            }
            else if (!Physics.Raycast(rayUP, out hitUP, rayLength) && Physics.Raycast(rayLEFT, out hitLEFT, rayLength) && !Physics.Raycast(rayDOWN, out hitDOWN, rayLength))
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
            }
            else if (Physics.Raycast(rayUP, out hitUP, rayLength) && !Physics.Raycast(rayLEFT, out hitLEFT, rayLength) && !Physics.Raycast(rayDOWN, out hitDOWN, rayLength))
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
            }
            else if (!Physics.Raycast(rayUP, out hitUP, rayLength) && !Physics.Raycast(rayLEFT, out hitLEFT, rayLength) && !Physics.Raycast(rayDOWN, out hitDOWN, rayLength))
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
            }
        }

        //coming from left
        else if (transform.position.x > temp.x)
        {
            if (Physics.Raycast(rayUP, out hitUP, rayLength) && Physics.Raycast(rayRIGHT, out hitRIGHT, rayLength) && Physics.Raycast(rayDOWN, out hitDOWN, rayLength))
            {
                MoveLeft();
            }
            else if (!Physics.Raycast(rayUP, out hitUP, rayLength) && Physics.Raycast(rayRIGHT, out hitRIGHT, rayLength) && Physics.Raycast(rayDOWN, out hitDOWN, rayLength))
            {
                MoveUp();
            }
            else if (Physics.Raycast(rayUP, out hitUP, rayLength) && !Physics.Raycast(rayRIGHT, out hitRIGHT, rayLength) && Physics.Raycast(rayDOWN, out hitDOWN, rayLength))
            {
                MoveRight();
            }
            else if (Physics.Raycast(rayUP, out hitUP, rayLength) && Physics.Raycast(rayRIGHT, out hitRIGHT, rayLength) && !Physics.Raycast(rayDOWN, out hitDOWN, rayLength))
            {
                MoveDown();
            }
            else if (!Physics.Raycast(rayUP, out hitUP, rayLength) && !Physics.Raycast(rayRIGHT, out hitRIGHT, rayLength) && Physics.Raycast(rayDOWN, out hitDOWN, rayLength))
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
            }
            else if (!Physics.Raycast(rayUP, out hitUP, rayLength) && Physics.Raycast(rayRIGHT, out hitRIGHT, rayLength) && !Physics.Raycast(rayDOWN, out hitDOWN, rayLength))
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
            }
            else if (Physics.Raycast(rayUP, out hitUP, rayLength) && !Physics.Raycast(rayRIGHT, out hitRIGHT, rayLength) && !Physics.Raycast(rayDOWN, out hitDOWN, rayLength))
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
            }
            else if (!Physics.Raycast(rayUP, out hitUP, rayLength) && !Physics.Raycast(rayRIGHT, out hitRIGHT, rayLength) && !Physics.Raycast(rayDOWN, out hitDOWN, rayLength))
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
            }


            //coming from bottom
            else if (transform.position.z > temp.z)
            {
                if (Physics.Raycast(rayUP, out hitUP, rayLength) && Physics.Raycast(rayRIGHT, out hitRIGHT, rayLength) && Physics.Raycast(rayLEFT, out hitLEFT, rayLength))
                {
                    MoveDown();
                }
                else if (!Physics.Raycast(rayUP, out hitUP, rayLength) && Physics.Raycast(rayRIGHT, out hitRIGHT, rayLength) && Physics.Raycast(rayLEFT, out hitLEFT, rayLength))
                {
                    MoveUp();
                }
                else if (Physics.Raycast(rayUP, out hitUP, rayLength) && !Physics.Raycast(rayRIGHT, out hitRIGHT, rayLength) && Physics.Raycast(rayLEFT, out hitLEFT, rayLength))
                {
                    MoveRight();
                }
                else if (Physics.Raycast(rayUP, out hitUP, rayLength) && Physics.Raycast(rayRIGHT, out hitRIGHT, rayLength) && !Physics.Raycast(rayLEFT, out hitLEFT, rayLength))
                {
                    MoveLeft();
                }
                else if (!Physics.Raycast(rayUP, out hitUP, rayLength) && !Physics.Raycast(rayRIGHT, out hitRIGHT, rayLength) && Physics.Raycast(rayLEFT, out hitLEFT, rayLength))
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
                }
                else if (!Physics.Raycast(rayUP, out hitUP, rayLength) && Physics.Raycast(rayRIGHT, out hitRIGHT, rayLength) && !Physics.Raycast(rayLEFT, out hitLEFT, rayLength))
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
                }
                else if (Physics.Raycast(rayUP, out hitUP, rayLength) && !Physics.Raycast(rayRIGHT, out hitRIGHT, rayLength) && !Physics.Raycast(rayLEFT, out hitLEFT, rayLength))
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
                }
                else if (!Physics.Raycast(rayUP, out hitUP, rayLength) && !Physics.Raycast(rayRIGHT, out hitRIGHT, rayLength) && !Physics.Raycast(rayLEFT, out hitLEFT, rayLength))
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
                }
            }

            //coming from top
            else if (transform.position.z < temp.z)
            {
                if (Physics.Raycast(rayDOWN, out hitDOWN, rayLength) && Physics.Raycast(rayRIGHT, out hitRIGHT, rayLength) && Physics.Raycast(rayLEFT, out hitLEFT, rayLength))
                {
                    MoveUp();
                }
                else if (!Physics.Raycast(rayDOWN, out hitDOWN, rayLength) && Physics.Raycast(rayRIGHT, out hitRIGHT, rayLength) && Physics.Raycast(rayLEFT, out hitLEFT, rayLength))
                {
                    MoveDown();
                }
                else if (Physics.Raycast(rayDOWN, out hitDOWN, rayLength) && !Physics.Raycast(rayRIGHT, out hitRIGHT, rayLength) && Physics.Raycast(rayLEFT, out hitLEFT, rayLength))
                {
                    MoveRight();
                }
                else if (Physics.Raycast(rayDOWN, out hitDOWN, rayLength) && Physics.Raycast(rayRIGHT, out hitRIGHT, rayLength) && !Physics.Raycast(rayLEFT, out hitLEFT, rayLength))
                {
                    MoveLeft();
                }
                else if (!Physics.Raycast(rayDOWN, out hitDOWN, rayLength) && !Physics.Raycast(rayRIGHT, out hitRIGHT, rayLength) && Physics.Raycast(rayLEFT, out hitLEFT, rayLength))
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
                }
                else if (!Physics.Raycast(rayDOWN, out hitDOWN, rayLength) && Physics.Raycast(rayRIGHT, out hitRIGHT, rayLength) && !Physics.Raycast(rayLEFT, out hitLEFT, rayLength))
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
                }
                else if (Physics.Raycast(rayDOWN, out hitDOWN, rayLength) && !Physics.Raycast(rayRIGHT, out hitRIGHT, rayLength) && !Physics.Raycast(rayLEFT, out hitLEFT, rayLength))
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
                }
                else if (!Physics.Raycast(rayDOWN, out hitDOWN, rayLength) && !Physics.Raycast(rayRIGHT, out hitRIGHT, rayLength) && !Physics.Raycast(rayLEFT, out hitLEFT, rayLength))
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
                }
            }
        }


        ////Debug.Log(col.tag);
        //else if (col.tag == "corner")
        //{
        //    //print("COORDINATES: " + transform.position + " - TEMP" + temp);
        //    //coming from right
        //    if (transform.position.x < temp.x)
        //    {
        //        //move up
        //        if (!Physics.Raycast(rayUP, out hitUP, rayLength) && Physics.Raycast(rayLEFT, out hitLEFT, rayLength) && Physics.Raycast(rayDOWN, out hitDOWN, rayLength))
        //        {
        //            MoveUp();
        //        }
        //        //move down
        //        else if (Physics.Raycast(rayUP, out hitUP, rayLength) && Physics.Raycast(rayLEFT, out hitLEFT, rayLength) && !Physics.Raycast(rayDOWN, out hitDOWN, rayLength))
        //        {
        //            MoveDown();
        //        }
        //        else
        //        {
        //            Debug.LogError("Can't turn");
        //        }
        //    }
        //    //coming from bottom
        //    else if (transform.position.z > temp.z)
        //    {
        //        //move right
        //        if (Physics.Raycast(rayUP, out hitUP, rayLength) && Physics.Raycast(rayLEFT, out hitLEFT, rayLength) && !Physics.Raycast(rayRIGHT, out hitRIGHT, rayLength))
        //        {
        //            MoveRight();
        //        }
        //        //move left
        //        else if (Physics.Raycast(rayUP, out hitUP, rayLength) && !Physics.Raycast(rayLEFT, out hitLEFT, rayLength) && Physics.Raycast(rayRIGHT, out hitRIGHT, rayLength))
        //        {
        //            MoveLeft();
        //        }
        //        else
        //        {
        //            Debug.LogError("Can't turn");
        //        }
        //    }
        //    //coming from left
        //    else if (transform.position.x > temp.x)
        //    {
        //        //move down
        //        if (Physics.Raycast(rayUP, out hitUP, rayLength) && !Physics.Raycast(rayDOWN, out hitDOWN, rayLength) && Physics.Raycast(rayRIGHT, out hitRIGHT, rayLength))
        //        {
        //            MoveDown();
        //        }
        //        //move up
        //        else if (!Physics.Raycast(rayUP, out hitUP, rayLength) && Physics.Raycast(rayDOWN, out hitDOWN, rayLength) && Physics.Raycast(rayRIGHT, out hitRIGHT, rayLength))
        //        {
        //            MoveUp();
        //        }
        //        else
        //        {
        //            Debug.LogError("Can't turn");
        //        }
        //    }
        //    //coming from top
        //    else if (transform.position.z < temp.z)
        //    {
        //        //move left
        //        if (Physics.Raycast(rayDOWN, out hitDOWN, rayLength) && !Physics.Raycast(rayLEFT, out hitLEFT, rayLength) && Physics.Raycast(rayRIGHT, out hitRIGHT, rayLength))
        //        {
        //            MoveLeft();
        //        }
        //        //move right
        //        else if (Physics.Raycast(rayDOWN, out hitDOWN, rayLength) && Physics.Raycast(rayLEFT, out hitLEFT, rayLength) && !Physics.Raycast(rayRIGHT, out hitRIGHT, rayLength))
        //        {
        //            MoveRight();
        //        }
        //        else
        //        {
        //            Debug.LogError("Can't turn");
        //        }
        //    }
        //}

        //else if (col.tag == "dead end")
        //{
        //    //coming from right
        //    if (transform.position.x < temp.x)
        //    {
        //        //going back
        //        if (Physics.Raycast(rayLEFT, out hitLEFT, rayLength))
        //        {
        //            x = -x;
        //            z = 0;
        //        }
        //        //else keep going
        //    }
        //    //coming from bottom
        //    else if (transform.position.z > temp.z)
        //    {
        //        //going back
        //        if (Physics.Raycast(rayUP, out hitUP, rayLength))
        //        {
        //            x = 0;
        //            z = -z;
        //        }
        //        //else keep going
        //    }
        //    //coming from left
        //    else if (transform.position.x > temp.x)
        //    {
        //        //going back
        //        if (Physics.Raycast(rayRIGHT, out hitRIGHT, rayLength))
        //        {
        //            x = -x;
        //            z = 0;
        //        }
        //        //else keep going

        //    }
        //    //coming from top
        //    else if (transform.position.z < temp.z)
        //    {
        //        //going back
        //        if (Physics.Raycast(rayDOWN, out hitDOWN, rayLength))
        //        {
        //            x = 0;
        //            z = -z;
        //        }
        //        //else keep going
        //    }
        //    else
        //        SetNewPos();
        //}

        //else if (col.tag == "3_cross")
        //{
        //    number = Random.Range(0, 2);
        //    //coming from right
        //    if (transform.position.x < temp.x)
        //    {
        //        // |-
        //        if (Physics.Raycast(rayLEFT, out hitLEFT, rayLength))
        //        {
        //            //turn up or down
        //            if (number == 0)
        //            {
        //                MoveUp();
        //            }
        //            else
        //            {
        //                MoveDown();
        //            }
        //        }
        //        //  T
        //        else if (Physics.Raycast(rayUP, out hitUP, rayLength))
        //        {
        //            //left or down
        //            if (number == 0)
        //            {
        //                MoveLeft();
        //            }
        //            else
        //            {
        //                MoveDown();
        //            }
        //        }
        //        // _I_
        //        else if (Physics.Raycast(rayDOWN, out hitDOWN, rayLength))
        //        {
        //            //turn left or up
        //            if (number == 0)
        //            {
        //                MoveLeft();
        //            }
        //            else
        //            {
        //                MoveUp();
        //            }
        //        }
        //    }
        //    //coming from bottom
        //    else if (transform.position.z > temp.z)
        //    {
        //        // -|
        //        if (Physics.Raycast(rayRIGHT, out hitRIGHT, rayLength))
        //        {
        //            //turn up or left
        //            if (number == 0)
        //            {
        //                MoveUp();
        //            }
        //            else
        //            {
        //                MoveLeft();
        //            }
        //        }
        //        //  T
        //        else if (Physics.Raycast(rayUP, out hitUP, rayLength))
        //        {
        //            //left or right
        //            if (number == 0)
        //            {
        //                MoveLeft();
        //            }
        //            else
        //            {
        //                MoveRight();
        //            }
        //        }
        //        // |-
        //        else if (Physics.Raycast(rayLEFT, out hitLEFT, rayLength))
        //        {
        //            //turn right or up
        //            if (number == 0)
        //            {
        //                MoveRight();
        //            }
        //            else
        //            {
        //                MoveUp();
        //            }
        //        }
        //    }
        //    //coming from left
        //    else if (transform.position.x > temp.x)
        //    {
        //        // -|
        //        if (Physics.Raycast(rayRIGHT, out hitRIGHT, rayLength))
        //        {
        //            //move up or down
        //            if (number == 0)
        //            {
        //                MoveUp();
        //            }
        //            else
        //            {
        //                MoveDown();
        //            }
        //        }
        //        //  T
        //        else if (Physics.Raycast(rayUP, out hitUP, rayLength))
        //        {
        //            //move right or down
        //            if (number == 0)
        //            {
        //                MoveRight();
        //            }
        //            else
        //            {
        //                MoveDown();
        //            }
        //        }
        //        // _I_
        //        else if (Physics.Raycast(rayDOWN, out hitDOWN, rayLength))
        //        {
        //            //move right or up
        //            if (number == 0)
        //            {
        //                MoveRight();
        //            }
        //            else
        //            {
        //                MoveUp();
        //            }
        //        }

        //    }
        //    //coming from top
        //    else if (transform.position.z < temp.z)
        //    {
        //        // |-
        //        if (Physics.Raycast(rayLEFT, out hitLEFT, rayLength))
        //        {
        //            //move right or down
        //            if (number == 0)
        //            {
        //                MoveRight();
        //            }
        //            else
        //            {
        //                MoveDown();
        //            }
        //        }
        //        //  -|
        //        else if (Physics.Raycast(rayRIGHT, out hitRIGHT, rayLength))
        //        {
        //            //left or down
        //            if (number == 0)
        //            {
        //                MoveLeft();
        //            }
        //            else
        //            {
        //                MoveDown();
        //            }
        //        }
        //        // _I_
        //        else if (Physics.Raycast(rayDOWN, out hitDOWN, rayLength))
        //        {
        //            //move left or right
        //            if (number == 0)
        //            {
        //                MoveLeft();
        //            }
        //            else
        //            {
        //                MoveRight();
        //            }
        //        }
        //    }
        //    else
        //        Debug.LogError("Can't move");
        //}


        //else if (col.tag == "return")
        //{
        //    if (transform.position.x < temp.x)
        //    {
        //        x = -x;
        //        z = 0;
        //    }
        //    //coming from bottom
        //    else if (transform.position.z > temp.z)
        //    {
        //        x = 0;
        //        z = -z;
        //    }
        //    //coming from left
        //    else if (transform.position.x > temp.x)
        //    {
        //        x = -x;
        //        z = 0;
        //    }
        //    //coming from top
        //    else if (transform.position.z < temp.z)
        //    {
        //        x = 0;
        //        z = -z;
        //    }
        //    else
        //        Debug.LogError("Can't return");
        //}

        //else if (col.tag == "4_cross")
        //{
        //    number = Random.Range(0, 3);
        //    //coming from right
        //    if (transform.position.x < temp.x)
        //    {
        //        if (number == 0)
        //        {
        //            MoveUp();
        //        }
        //        else if (number == 1)
        //        {
        //            MoveLeft();
        //        }
        //        else if (number == 2)
        //        {
        //            MoveDown();
        //        }
        //    }
        //    //coming from bottom
        //    else if (transform.position.z > temp.z)
        //    {
        //        if (number == 0)
        //        {
        //            MoveRight();
        //        }
        //        else if (number == 1)
        //        {
        //            MoveUp();
        //        }
        //        else if (number == 2)
        //        {
        //            MoveLeft();
        //        }
        //    }
        //    //coming from left
        //    else if (transform.position.x > temp.x)
        //    {
        //        if (number == 0)
        //        {
        //            MoveUp();
        //        }
        //        else if (number == 1)
        //        {
        //            MoveRight();
        //        }
        //        else if (number == 2)
        //        {
        //            MoveDown();
        //        }
        //    }
        //    //coming from top
        //    else if (transform.position.z < temp.z)
        //    {
        //        if (number == 0)
        //        {
        //            MoveRight();
        //        }
        //        else if (number == 1)
        //        {
        //            MoveDown();
        //        }
        //        else if (number == 2)
        //        {
        //            MoveLeft();
        //        }
        //    }
        //}
        //}

        //private void TestPosition()
        //{
        //    //at crossroad
        //    if(transform.position.x == crossroads[0].transform.position.x && crossroads[0].transform.position.z == transform.position.z)
        //    {
        //        print("same coordinates");
        //        print("COORDINATES: " + transform.position.x + " - TEMP" + temp.x);
        //        if (transform.position.x < temp.x && !Physics.Raycast(rayFRONT, out hitFRONT, rayLength) && Physics.Raycast(rayLEFT, out hitLEFT, rayLength) && Physics.Raycast(rayBACK, out hitBACK, rayLength))
        //        {
        //            x = 0;
        //            z = 1f * speed;
        //        }

        //    }

    }

    //no solution for floating precision
    private float RoundNumber(float num)
    {
        return Mathf.Round(num * 1000.0f) / 1000.0f;

    }

    private void SetNewPos()
    {
        Debug.Log("ERROR in SOMETHING");
        transform.position = new Vector3(0, 1.25f, 0);
        x = 0;
        z = 0;
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

}
