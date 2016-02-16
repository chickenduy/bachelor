using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class User_Interface_S : Singleton<User_Interface_S>
{
    // guarantee this will be always a singleton only - can't use the constructor!
    protected User_Interface_S() { }

    private GameObject e_button;
    private GameObject info_panel;
    private Text info_panel_text;
    private GameObject quest_panel;
    private Text quest_panel_text;
    private GameObject action_panel;
    private Text action_panel_text;
    private Image speed_ability;
    private Image shoot_ability;
    private Text shoot_ability_text;
    private Image shoot_ability_text_background;
    private Image see_ability;
    private Image go_ability;
    private GameObject power_panel;
    private Animator power_panel_animator;
    private Text power_panel_text;
    private bool[] power_panel_open = new bool[4];

    private int current_quest = 0;

    private List<string> quests = new List<string>();

    private float cooldown = 30f;

    // Use this for initialization
    void Start()
    {
        e_button.SetActive(false);
        info_panel.SetActive(false);
        action_panel.SetActive(false);
    }

    void Update()
    {
        shoot_ability_text.text = Room_S.Instance.killfire.ToString();
        if (Room_S.Instance.killfire <= 0)
        {
            shoot_ability.fillAmount = 1f;
            shoot_ability_text_background.color = new Color(101f / 255f, 101f / 255f, 101f / 255f);
        }
        if (Room_S.Instance.killfire > 0)
        {
            shoot_ability.fillAmount = 0f;
            shoot_ability_text_background.color = new Color(0f, 0f, 101f / 255f);
        }
        if (Player_S.Instance.abilities[1])
            see_ability.fillAmount = see_ability.fillAmount + Time.deltaTime / Power_S.Instance.Timer_See;
        if (Player_S.Instance.abilities[2])
            speed_ability.fillAmount = speed_ability.fillAmount + Time.deltaTime / Power_S.Instance.Timer_Speed;
        if (Player_S.Instance.abilities[3])
            go_ability.fillAmount = go_ability.fillAmount + Time.deltaTime / Power_S.Instance.Timer_Go;
        if (!power_panel_open[0] || !power_panel_open[1] || !power_panel_open[2] || !power_panel_open[3])
            if (Input.anyKeyDown)
            {
                Disable_Power_Panel();
            }
    }

    public void Register(GameObject obj, string name)
    {
        if (name == "E-Button")
            e_button = obj;
        else if (name == "Quest Panel")
        {
            quest_panel = obj;
            quest_panel_text = obj.GetComponentInChildren<Text>();
            quest_panel_text.text = "Find the Pictures.";
            Quests();
        }
        else if (name == "Info Panel")
        {
            info_panel = obj;
            info_panel_text = obj.GetComponentInChildren<Text>();
        }
        else if (name == "Action Panel")
        {
            action_panel = obj;
            action_panel_text = obj.GetComponentInChildren<Text>();
        }
        else if (name == "Speed Ability")
        {
            speed_ability = obj.GetComponent<Image>();
            speed_ability.fillAmount = 1f;
        }
        else if (name == "Shoot Ability Text")
        {
            shoot_ability_text = obj.GetComponent<Text>();
            shoot_ability_text.text = Room_S.Instance.killfire.ToString();
        }
        else if (name == "See Ability")
        {
            see_ability = obj.GetComponent<Image>();
            see_ability.fillAmount = 1f;
        }
        else if (name == "Go Ability")
        {
            go_ability = obj.GetComponent<Image>();
            go_ability.fillAmount = 1f;
        }
        else if (name == "Shoot Ability")
        {
            shoot_ability = obj.GetComponent<Image>();
            shoot_ability.fillAmount = 1f;
        }
        else if (name == "Shoot Ability Text Background")
        {
            shoot_ability_text_background = obj.GetComponent<Image>();
            shoot_ability_text_background.color = new Color(101, 101, 101);
        }
        else if (name == "Power Panel")
        {
            power_panel = obj;
            power_panel_animator = obj.GetComponent<Animator>();
            power_panel_text = obj.GetComponentInChildren<Text>();
        }

    }

    private void Quests()
    {
        quests.Add("Quest: Find the Pictures");
        quests.Add("Quest: Find the Key");
        quests.Add("Quest: Find the Key Hole");
        quests.Add("Quest: Find Elictricity");
        quests.Add("Quest: Find the Exit");
    }

    public void Next_Quest()
    {
        switch (current_quest)
        {
            case 0:
                if (Player_S.Instance.Get_Key())
                {
                    //if player has key skip next quest
                    current_quest = 2;
                }
                current_quest++;
                break;
            case 1:
                current_quest++;
                break;
            case 2:
                current_quest++;
                break;
            case 3:
                current_quest++;
                break;
            default:
                current_quest = 0;
                Debug.LogError("Something wrong in User_Interface_S/Next_Quest");
                break;
        }
        Change_Quest(quests[current_quest]);
    }

    public void Change_Info(string text)
    {
        info_panel.SetActive(true);
        info_panel_text.text = text;
    }

    public void Change_Quest(string text)
    {
        quest_panel_text.text = text;
    }

    public void Change_Action(GameObject obj, bool state)
    {
        action_panel.SetActive(state);
        action_panel_text.text = ("Do you want to use the " + obj.tag + "?");
    }

    public void Change_E_Button(bool state)
    {
        e_button.SetActive(state);
    }

    public void Show_Info_Panel(string text)
    {
        info_panel.SetActive(true);
        info_panel_text.text = text;
        StartCoroutine(Disable_Info(5f));
    }

    public void Change_Info()
    {
        info_panel.SetActive(false);
    }
    public void Change_Action()
    {
        action_panel.SetActive(false);
    }

    public void Print_OBJ()
    {
        Debug.Log(e_button + " " + quest_panel + " " + info_panel);
    }

    private IEnumerator Disable_Info(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        info_panel.SetActive(false);
    }

    public void Destroyed()
    {
        Change_E_Button(false);
        Change_Action();
    }

    public void Activate_Shoot_Ability()
    {
        power_panel_text.text = "HERO, YOU GAINED A NEW POWER \n The power to shoot more water";
        if (!power_panel_open[0])
        {
            power_panel_animator.SetBool("open", true);
            power_panel_open[0] = true;
        }
        shoot_ability_text.text = Room_S.Instance.killfire.ToString();
    }

    public void Activate_See_Ability()
    {
        power_panel_text.text = "HERO, YOU GAINED A NEW POWER \n The power to go see walls";
        if (!power_panel_open[1])
        {
            power_panel_animator.SetBool("open", true);
            power_panel_open[1] = true;
        }
        see_ability.fillAmount = 0f;
    }
    public void Activate_Speed_Ability()
    {
        power_panel_text.text = "HERO, YOU GAINED A NEW POWER \n The power to run like a gazelle";
        if (!power_panel_open[2])
        {
            power_panel_animator.SetBool("open", true);
            power_panel_open[2] = true;
        }
        speed_ability.fillAmount = 0f;
    }
    public void Activate_Go_Ability()
    {
        power_panel_text.text = "HERO, YOU GAINED A NEW POWER \n The power to go through walls";
        if (!power_panel_open[3])
        {
            power_panel_animator.SetBool("open", true);
            power_panel_open[3] = true;
        }
        go_ability.fillAmount = 0f;
    }

    private void Disable_Power_Panel()
    {
        power_panel_animator.SetBool("open", false);
    }
}
