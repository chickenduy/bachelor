using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class User_Interface_S : Singleton<User_Interface_S>
{
    // guarantee this will be always a singleton only - can't use the constructor!
    protected User_Interface_S() { }

    private GameObject e_button;
    private Text info_panel;
    private Text quest_panel;

    // Use this for initialization
    void Start()
    {
        e_button.SetActive(false);
        info_panel.text = "no info";
        quest_panel.text = "Find the exit.";

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Register(GameObject obj, string name)
    {
        if (name == "E-Button")
            e_button = obj;
        else if (name == "Quest Panel")
            quest_panel = obj.GetComponentInChildren<Text>();
        else if (name == "Info Panel")
            info_panel = obj.GetComponentInChildren<Text>();
    }

    public void Change_Info(string text)
    {
        info_panel.text = text;
    }

    public void Change_Quest(string text)
    {
        quest_panel.text = text;
    }

    public void Change_E_Button(bool state)
    {
        e_button.SetActive(state);
    }

    public void Print_OBJ()
    {
        Debug.Log(e_button + " " + quest_panel + " " + info_panel);
    }

}
