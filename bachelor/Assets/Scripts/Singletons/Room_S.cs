using UnityEngine;
using System.Collections;
using UnityStandardAssets.Effects;

public class Room_S : Singleton<Room_S>
{

    // guarantee this will be always a singleton only - can't use the constructor!
    protected Room_S() { }

    //variables
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

    private int _lighting = 0;
    public int lighting
    {
        get
        {
            return lighting;
        }
        set
        {
            _lighting = value;
        }
    }

    private float _pee = 0;
    public float pee
    {
        get
        {
            return _pee;
        }
        set
        {
            if (value > 1.1f)
                value = 1.1f;
            _pee = value;
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
    private int temp;

    private bool drinked = false;
    public FireLight firelight;

    //methods
    void Start()
    {
        firelight = Player_S.Instance.gameObject.GetComponentInChildren<FireLight>();
    }
    void Update()
    {
        Temperature_Change();
    }
    public void Drink()
    {
        _killfire = +3;
        if (!drinked)
        {
            InvokeRepeating("Drinked", 0, 1f);
            drinked = true;
        }
    }
    private void Drinked()
    {
        if (_pee > 0.95 && _pee < 1.05)
            temperature--;
        if (_pee < 1.1f)
            _pee = _pee + 0.1f;

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
        if (_pee != 0)
        {
            _pee = 0;
            temperature++;
            CancelInvoke("Drinked");
            drinked = false;
        }
    }

    public void Use_Fire()
    {
        _killfire--;
    }

    public void Set_Fire_Kills(int amount)
    {
        _killfire = amount;
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


