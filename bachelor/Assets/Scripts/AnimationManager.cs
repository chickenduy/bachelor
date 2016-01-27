using UnityEngine;
using System.Collections;

public class AnimationManager : MonoBehaviour {

	public class A_Manager
    {
        private Animator switch_light;
        private Animator switch_light_bathroom;
        private Animator switch_fan;
        private Animator fan;
        private Animator door_bathroom;
        private Animator window;
        private Animator night_stand;
        public Animator fireplace;
        private Animator toilet;
        private Animator wall;

        public A_Manager()
        {

            switch_light = GameObject.Find("Switch Light").GetComponent<Animator>();
            switch_light_bathroom = GameObject.Find("Switch Light Bathroom").GetComponent<Animator>();
            switch_fan = GameObject.Find("Switch Fan").GetComponent<Animator>();
            fan = GameObject.Find("Fan").GetComponent<Animator>();
            door_bathroom = GameObject.Find("Door Bathroom").GetComponent<Animator>();
            window = GameObject.Find("Window").GetComponent<Animator>();
            night_stand = GameObject.Find("Night Stand").GetComponent<Animator>();
            fireplace = GameObject.Find("Fireplace").GetComponent<Animator>();
            toilet = GameObject.Find("Toilet").GetComponent<Animator>();
            wall = GameObject.Find("Switch").GetComponent<Animator>();
        }

        public void Switch_Light_Main(bool state)
        {
            if (!state)
            {
                switch_light.SetTrigger("Activate");
            }
            else
            {
                switch_light.SetTrigger("Deactivate");
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

        public void Switch_Fan(bool state)
        {
            if (!state)
            {
                switch_fan.SetTrigger("Activate");
                fan.SetTrigger("Activate");
            }
            else
            {
                switch_fan.SetTrigger("Deactivate");
                fan.SetTrigger("Deactivate");
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

        public void Window(bool state)
        {
            if (!state)
            {
                window.SetTrigger("Open");
            }
            else
            {
                window.SetTrigger("Close");
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

        public void Fireplace(bool state, bool lighter)
        {
            if (lighter)
            {
                if (!state)
                {
                    fireplace.SetTrigger("Activate");
                }
                else
                {
                    fireplace.SetTrigger("Deactivate");
                }
            }
            else
            {
                //You need a lighter
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

        public void Wall()
        {
            if (Input.GetKeyDown("q"))
            {
                wall.SetTrigger("State1");
            }
            else if (Input.GetKeyDown("1"))
            {
                wall.SetTrigger("State2");
            }
        }

    }
}
