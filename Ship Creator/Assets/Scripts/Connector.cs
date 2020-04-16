using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connector : MonoBehaviour
{
    GameObject First;
    GameObject Second;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Object")
                {
                    if (First == null && Second == null)
                    {
                        First = hit.transform.gameObject;
                    }
                    if (First != null && Second == null)
                    {
                        Second = hit.transform.gameObject;
                    }
                }
            }
        }

        if (Second != null && First != null)
        {
            First.transform.parent = Second.transform;
        }
    }
}
