using UnityEngine;
using System.Collections;
using UnityStandardAssets.Effects;

public class Room_S : Singleton<Room_S>
{

    // guarantee this will be always a singleton only - can't use the constructor!
    protected Room_S() { }

    //private    
    private int temp;

    //getter/setter
    private int _temperature = 0;
    public int temperature
    {
        get
        {
            return _temperature;
        }
        set
        {
            _temperature = value;
        }
    }
    private bool _wind = false;
    public bool wind
    {
        get
        {
            return _wind;
        }
        set
        {
            _wind = value;
        }
    }
    private int _killfire = 0;
    public int killfire
    {
        get
        {
            return _killfire;
        }
        set
        {
            if (value < 0)
                value = 0;
            _killfire = value;
        }
    }
    private FireLight _firelight;
    public FireLight firelight
    {
        get
        {
            return _firelight;
        }
    }
    private bool _electricity = false;
    public bool electricity
    {
        get
        {
            return _electricity;
        }
        set
        {
            _electricity = value;
        }
    }

    /*----------------------------------------------------------------------------------------------------*/

    //methods
    void Start()
    {
        _firelight = Player_S.Instance.gameObject.GetComponentInChildren<FireLight>();
    }
    void Update()
    {
        Temperature_Change();
    }
    public void Drink()
    {
        _killfire = +3;
    }


    private void Temperature_Change()
    {
        if (temp != temperature)
        {
            Obstacle_S.Instance.Spawn_Obstacles();
            temp = temperature;
        }
    }


    public void Use_Fire()
    {
        _killfire--;
    }

    public void Increase_Fire()
    {
        _killfire += 3;
    }

    public void Temperature_Lower()
    {
        temperature = temperature - 1;
    }
    public void Temperature_Higher()
    {
        temperature = temperature + 1;
    }

    public void Set_Wind(bool state)
    {
        wind = state;
    }
}


