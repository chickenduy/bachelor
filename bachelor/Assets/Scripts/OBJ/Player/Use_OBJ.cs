using UnityEngine;
using System.Collections;

public class Use_OBJ : MonoBehaviour {

    public void OnMouseEnter()
    {
        RaycastHit hit;
        Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
        User_Interface_S.Instance.Change_E_Button(Physics.Raycast(ray, out hit, 3));
        User_Interface_S.Instance.Show_Info_Panel(gameObject);
    }

    public void OnMouseExit()
    {
        User_Interface_S.Instance.Change_Info();
        User_Interface_S.Instance.Change_E_Button(false);
    }
}
