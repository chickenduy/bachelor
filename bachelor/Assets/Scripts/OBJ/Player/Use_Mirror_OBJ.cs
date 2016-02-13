using UnityEngine;
using System.Collections;

public class Use_Hidden_Wall_OBJ : MonoBehaviour
{

    public void OnMouseOver()
    {
        if (Player_S.Instance.Get_Key() && Player_S.Instance.pictures[0] && Player_S.Instance.pictures[1] && Player_S.Instance.pictures[2] && Player_S.Instance.pictures[3])
        {
            RaycastHit hit;
            Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
            bool is_hit = Physics.Raycast(ray, out hit, 3);
            User_Interface_S.Instance.Change_E_Button(is_hit);
            User_Interface_S.Instance.Change_Action(gameObject, is_hit);
        }
    }

    public void OnMouseExit()
    {
        User_Interface_S.Instance.Change_Action();
        User_Interface_S.Instance.Change_E_Button(false);
    }
}
