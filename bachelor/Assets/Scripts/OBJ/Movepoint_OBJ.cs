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
            Vector3 pos = new Vector3(0, 1, 0);
            Vector3 position = gameObject.transform.position + pos;

            RaycastHit hitUP;
            RaycastHit hitDOWN;
            RaycastHit hitLEFT;
            RaycastHit hitRIGHT;
            Ray rayUP = new Ray(position, Vector3.forward);
            Ray rayDOWN = new Ray(position, Vector3.back);
            Ray rayLEFT = new Ray(position, Vector3.left);
            Ray rayRIGHT = new Ray(position, Vector3.right);

            //tests in which direction is a wall/gameObject
            bool up = Physics.Raycast(rayUP, out hitUP, rayLength);
            bool down = Physics.Raycast(rayDOWN, out hitDOWN, rayLength);
            bool left = Physics.Raycast(rayLEFT, out hitLEFT, rayLength);
            bool right = Physics.Raycast(rayRIGHT, out hitRIGHT, rayLength);

            Sphere_S.Instance.Calculate_Direction(id, up, down, left, right);
        }
    }

}
