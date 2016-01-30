using UnityEngine;
using System.Collections;

public class AnimationManager : MonoBehaviour
{

    public class A_Manager
    {
        private Animator switch_light_main;
        private Animator switch_light_bathroom;
        private Animator switch_fan;
        private Animator fan;
        private Animator door_bathroom;
        private Animator window;
        private Animator night_stand;
        public Animator fireplace;
        private Animator toilet;

        private GameObject[] walls_obj;
        private Animator[] walls;

        public A_Manager()
        {
            switch_light_main = GameObject.Find("Switch Light").GetComponent<Animator>();
            switch_light_bathroom = GameObject.Find("Switch Light Bathroom").GetComponent<Animator>();
            switch_fan = GameObject.Find("Switch Fan").GetComponent<Animator>();
            fan = GameObject.Find("Fan").GetComponent<Animator>();
            door_bathroom = GameObject.Find("Door Bathroom").GetComponent<Animator>();
            window = GameObject.Find("Window").GetComponent<Animator>();
            night_stand = GameObject.Find("Night Stand").GetComponent<Animator>();
            fireplace = GameObject.Find("Fireplace").GetComponent<Animator>();
            toilet = GameObject.Find("Toilet").GetComponent<Animator>();
            walls_obj = GameObject.FindGameObjectsWithTag("moving wall");
            walls = new Animator[walls_obj.Length];
            for (int i = 0; i < walls_obj.Length; i++)
            {
                walls[i] = walls_obj[i].GetComponent<Animator>();
            }
        }

        public int Switch_Light_Main(bool state, int light)
        {
            if (!state)
            {
                switch_light_main.SetTrigger("Activate");
                return light++;
            }
            else
            {
                switch_light_main.SetTrigger("Deactivate");
                return light--;
            }
        }

        public void Switch_Light_Bathroom(bool state)
        {
            if (!state)
            {
                switch_light_bathroom.SetTrigger("Activate");
            }
            else
            {
                switch_light_bathroom.SetTrigger("Deactivate");
            }
        }

        public bool Switch_Fan(bool state)
        {
            if (!state)
            {
                switch_fan.SetTrigger("Activate");
                fan.SetTrigger("Activate");
                return true;
            }
            else
            {
                switch_fan.SetTrigger("Deactivate");
                fan.SetTrigger("Deactivate");
                return false;
            }
        }

        public void Door(bool state)
        {
            if (!state)
            {
                door_bathroom.SetTrigger("Open");
            }
            else
            {
                door_bathroom.SetTrigger("Close");
            }
        }

        public int Window(bool state, int temperature)
        {
            if (!state)
            {
                window.SetTrigger("Open");
                return temperature--;
            }
            else
            {
                window.SetTrigger("Close");
                return temperature++;
            }
        }

        public void Drawer(bool state, bool lighter)
        {
            if (!state)
            {
                night_stand.SetTrigger("Open");
                night_stand.SetBool("Lighter", lighter);
            }
            else
            {
                night_stand.SetTrigger("Close");
                night_stand.SetBool("Lighter", lighter);
            }
        }

        public int Fireplace(bool state, int temperature)
        {
            if (!state)
            {
                fireplace.SetTrigger("Activate");
                return temperature++;
            }
            else
            {
                fireplace.SetTrigger("Deactivate");
                return temperature--;
            }
        }

        public void Toilet(bool state)
        {
            if (state)
            {
                toilet.SetTrigger("Close");
            }
            else
            {
                toilet.SetTrigger("Open");
            }
        }


        public void Walls()
        {
            Debug.Log("walls");
            int number;
            for (int i = 0; i < walls.Length; i++)
            {
                print(walls[i].GetBool("move"));
                number = Random.Range(0, 10);
                if (number > 3)
                {
                    walls[i].SetBool("move", !walls[i].GetBool("move"));
                }
            }
        }

    }
}
