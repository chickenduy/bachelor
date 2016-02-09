using UnityEngine;
using System.Collections;

public class Room_S : Singleton<Room_S>
{

    // guarantee this will be always a singleton only - can't use the constructor!
    protected Room_S() { }

    //variables
    private int temperature = 0;
    private int lighting = 0;
    private float pee = 0;
    private bool wind = false;
    private int killfire = 0;

    private int temp;
    private bool drinked = false;

    //methods
    void Start()
    {
        temperature = Game_S.Instance.temperature;
        lighting = Game_S.Instance.lighting;
        pee = Game_S.Instance.pee;
        wind = Game_S.Instance.wind;
        killfire = Game_S.Instance.killfire;
    }
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

    public float Get_Pee()
    {
        return pee;
    }

    public int Get_Temperature()
    {
        return temperature;
    }

    public int Get_Lighting()
    {
        return lighting;
    }

    public bool Get_Wind()
    {
        return wind;
    }

    public int Get_Fire_Kills()
    {
        return killfire;
    }

    public void Use_Fire()
    {
        killfire--;
    }

    public void Set_Fire_Kills(int amount)
    {
        killfire = amount;
    }

    public void Temperature_Lower()
    {
        temperature = temperature - 1;
    }
    public void Temperature_Higher()
    {
        temperature = temperature + 1;
    }
}

    
