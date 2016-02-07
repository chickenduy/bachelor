using UnityEngine;
using System.Collections;

public class Movepoint_OBJ : MonoBehaviour
{

    private int id = 0;
    private int rayLength = 3;

    public void OnTriggerEnter(Collider col)
    {
        if (col.tag == "sphere")
        {

            RaycastHit hitUP = new RaycastHit();
            RaycastHit hitDOWN = new RaycastHit();
            RaycastHit hitLEFT = new RaycastHit();
            RaycastHit hitRIGHT = new RaycastHit();

            Ray rayUP = new Ray(gameObject.transform.position, Vector3.forward);
            Ray rayDOWN = new Ray(gameObject.transform.position, Vector3.back);
            Ray rayLEFT = new Ray(gameObject.transform.position, Vector3.left);
            Ray rayRIGHT = new Ray(gameObject.transform.position, Vector3.right);

            //tests in which direction is a wall/gameObject
            bool up = Physics.Raycast(rayUP, out hitUP, rayLength, 9);
            bool down = Physics.Raycast(rayDOWN, out hitDOWN, rayLength, 9);
            bool left = Physics.Raycast(rayLEFT, out hitLEFT, rayLength, 9);
            bool right = Physics.Raycast(rayRIGHT, out hitRIGHT, rayLength, 9);

            Debug.Log(gameObject.name);
            Debug.Log("Up: " + up);
            Debug.Log("Down: " + down);
            Debug.Log("Left: " + left);
            Debug.Log("Right: " + right);

            Sphere_S.Instance.Calculate_Direction(id, up, down, left, right);
        }
    }

}
