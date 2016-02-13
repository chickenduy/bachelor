using UnityEngine;
using System.Collections;

public class Use_OBJ : MonoBehaviour {

    public void OnMouseOver()
    {
        RaycastHit hit;
        Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
        bool is_hit = Physics.Raycast(ray, out hit, 3);
        print(hit.collider.gameObject.tag);
        User_Interface_S.Instance.Change_E_Button(is_hit);
        User_Interface_S.Instance.Change_Action(gameObject, is_hit);
    }

    public void OnMouseExit()
    {
        User_Interface_S.Instance.Change_Action();
        User_Interface_S.Instance.Change_E_Button(false);
    }
}
