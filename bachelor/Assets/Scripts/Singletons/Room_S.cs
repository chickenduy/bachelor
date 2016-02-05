using UnityEngine;
using System.Collections;

public class Room_S : Singleton<Room_S> {

    // guarantee this will be always a singleton only - can't use the constructor!
    protected Room_S() { }

    //variables
    public int temperature = 0;
    public int lighting = 0;
    public float pee = 0;
    public bool wind = false;
    public int killfire = 0;

    private bool drinked = false;

    //methods
    public void Drink()
    {
        killfire =+ 5;
        if (!drinked)
        {
            InvokeRepeating("Drinked", 0, 30f);
            drinked = true;
        }
    }
    private void Drinked()
    {
        if(pee < 1f)
        pee = pee + 0.1f;
    }
}
