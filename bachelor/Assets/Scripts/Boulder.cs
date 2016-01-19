using UnityEngine;
using System.Collections;

public class Boulder : MonoBehaviour {


    public float x;
    public float y;
    public float z;

    // Use this for initialization
    void Start () {
        x = -0.1f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z + z);

    }

    public void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "rightTurn")
        {
            transform.position = new Vector3(roundNumber(transform.position.x ), roundNumber(transform.position.y ), roundNumber(transform.position.z ));
            x = 0;
            z = 0.1f;
        }
        if (collider.tag == "leftTurn")
        {
            x = 0.1f;
            z = 0;
            transform.position = new Vector3(roundNumber(transform.position.x), roundNumber(transform.position.y), roundNumber(transform.position.z));
        }
        else
        {
            x = 0;
            z = 0;
            print(collider.tag);
        }
    }

    private float roundNumber(float floatNumber)
    {
        if (floatNumber < 0)
            return Mathf.Ceil(floatNumber / 0.5f) * 0.5f;
        else if (floatNumber > 0)
            return Mathf.Floor(floatNumber / 0.5f) * 0.5f;
        else
            return 0.5f;
    }

}
