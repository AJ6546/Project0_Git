using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit))
        {
            if(Input.GetMouseButtonDown(0))
            {
                if (hit.transform.GetComponent<Pickup>())
                {
                    Pickup item = hit.transform.GetComponent<Pickup>();
                    transform.LookAt(new Vector3(item.transform.position.x, transform.position.y, item.transform.position.z));
                    if (PickupDistance(item) <= item.GetPickupRadius())
                        item.PickupItem();
                }
            }
        }
    }

    public float PickupDistance(Pickup item)
    {
        return Vector3.Distance(item.transform.position, transform.position);
    }

}
