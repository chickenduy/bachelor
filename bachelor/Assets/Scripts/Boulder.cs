using UnityEngine;
using System.Collections;

public class Boulder : MonoBehaviour {

    public GameObject[] crossroads = new GameObject[1];

    public float x;
    public float y;
    public float z;

    private Vector3 temp;
    private float speed;
    // Use this for initialization
    void Start () {
        speed = 0.125f;
        x = -1f*speed;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        temp = transform.position;
        transform.position = new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z + z);
        //transform.position = new Vector3(roundNumber(transform.position.x), roundNumber(transform.position.y), roundNumber(transform.position.z));
        //print(transform.position.x);
        TestPosition();
    }

    private void TestPosition()
    {
        //at crossroad
        if(transform.position.x == crossroads[0].transform.position.x && crossroads[0].transform.position.z == transform.position.z)
        {
            if (transform.position.x < temp.x)
            {
                x = 0;
                z = 1f * speed;
            }

        }

    }

    private float RoundNumber(float floatNumber)
    {
        if (floatNumber < 0) { 
        return Mathf.Ceil(floatNumber / 0.5f) * 0.5f;
        }
        else if (floatNumber > 0)
            return Mathf.Floor(floatNumber / 0.5f) * 0.5f;
        else
            return 0.5f;
    }

}
