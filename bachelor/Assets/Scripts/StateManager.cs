using UnityEngine;
using System.Collections;

public class StateManager : MonoBehaviour
{

    public class S_Manager
    {
        public bool switch_light;
        public bool switch_light_bathroom;
        public bool switch_fan;
        public bool door;
        public bool drawer;
        public bool window;
        public bool lighter;
        public bool fireplace;
        public bool toilet;

        public int temperature;
        public int light;
        public float pee;
        public bool wind;

        public S_Manager()
        {
            switch_light = false;
            switch_light_bathroom = false;
            switch_fan = false;
            door = false;
            drawer = false;
            window = false;
            lighter = false;
            fireplace = false;
            toilet = false;
            temperature = 0;
            light = 0;
            pee = 0;
            wind = false;
        }

        public bool Switch_Light(bool state)
        {
            return !state;
        }
        public bool Switch_Light_Bathroom(bool state)
        {
            return !state;
        }
        public bool Switch_Fan(bool state)
        {
            return !state;
        }
        public bool Door(bool state)
        {
            return !state;
        }
        public bool Drawer(bool state)
        {
            return !state;
        }
        public bool Window(bool state)
        {
            return !state;
        }
        public bool Fireplace(bool state)
        {
            return !state;
        }
        public bool Toilet(bool state)
        {
            return !state;
        }



    }
}




