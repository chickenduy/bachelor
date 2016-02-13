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

    private int current_quest = 0;

    private List<string> quests = new List<string>();

    // Use this for initialization
    void Start()
    {
        e_button.SetActive(false);
        info_panel.SetActive(false);
        action_panel.SetActive(false);
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
    }

    private void Quests()
    {
        quests.Add("Find the Pictures");
        quests.Add("Find the Key");
        quests.Add("Find the Key Hole");
        quests.Add("Find Elictricity");
        quests.Add("Find the Exit");
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

}
