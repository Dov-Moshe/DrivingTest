using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationPoint : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (GameObject.ReferenceEquals(other.gameObject, AdvanceCarController.Instance.gameObject))
        {
            transform.parent.GetComponent<Route>().OnPointTrigger();
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        if (GameObject.ReferenceEquals(other.gameObject, AdvanceCarController.Instance.gameObject))
        {
            this.gameObject.SetActive(false);
        }
    }
}
