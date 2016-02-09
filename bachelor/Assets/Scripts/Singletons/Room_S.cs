using UnityEngine;
using System.Collections;

public class Room_S : Singleton<Room_S>
{

    // guarantee this will be always a singleton only - can't use the constructor!
    protected Room_S() { }

    //variables
    public int temperature = 0;
    public int lighting = 0;
    public float pee = 0;
    public bool wind = false;
    public int killfire = 0;
    private int temp;


    private bool drinked = false;

    //methods
    void Update()
    {
        Temperature_Change();
    }
    public void Drink()
    {
        killfire = +3;
        if (!drinked)
        {
            InvokeRepeating("Drinked", 0, 1f);
            drinked = true;
        }
    }
    private void Drinked()
    {
        if (pee > 0.95 && pee < 1.05)
            temperature--;
        if (pee < 1.1f)
            pee = pee + 0.1f;

    }

    private void Temperature_Change()
    {
        if (temp != temperature)
        {
            Obstacle_S.Instance.SpawnObstacles();
            temp = temperature;
        }
    }

    public void Use_Toilet()
    {
        if (pee != 0)
        {
            pee = 0;
            temperature++;
            CancelInvoke("Drinked");
            drinked = false;
        }


    }

}
