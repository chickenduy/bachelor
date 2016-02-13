using UnityEngine;
using System.Collections;

public class Use_Mirror_OBJ : MonoBehaviour
{

    public void OnMouseOver()
    {
        if (Maze_S.Instance.room_discovered[gameObject.GetComponent<Mirror_OBJ>().id])
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
