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
    private GameObject quest_panel;
    private Text info_panel_text;
    private Text quest_panel_text;

    private List<string> quests = new List<string>();

    // Use this for initialization
    void Start()
    {

        Change_E_Button(false);
        info_panel_text.text = "no info";
        quest_panel_text.text = "Find the Pictures.";
    }

    public void Register(GameObject obj, string name)
    {
        if (name == "E-Button")
            e_button = obj;
        else if (name == "Quest Panel")
        {
            quest_panel = obj;
            quest_panel_text = obj.GetComponentInChildren<Text>();
        }
        else if (name == "Info Panel")
        {
            info_panel = obj;
            info_panel_text = obj.GetComponentInChildren<Text>();
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


    public void Change_Info(string text)
    {
        info_panel.SetActive(true);
        info_panel_text.text = text;
    }

    public void Change_Quest(string text)
    {
        quest_panel_text.text = text;
    }

    public void Change_E_Button(bool state)
    {
        e_button.SetActive(state);
    }

    public void Show_Info_Panel(GameObject obj)
    {
        info_panel.SetActive(true);
        info_panel_text.text = ("Do you want to use the" + obj.tag + "?");
    }

    public void Change_Info()
    {
        info_panel.SetActive(false);
    }

    public void Print_OBJ()
    {
        Debug.Log(e_button + " " + quest_panel + " " + info_panel);
    }
}
