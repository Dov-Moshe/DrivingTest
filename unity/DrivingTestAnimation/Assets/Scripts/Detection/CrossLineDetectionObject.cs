using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossLineDetectionObject : MonoBehaviour
{
    private AdvanceCarController car;
    // Start is called before the first frame update
    void Start()
    {
        this.car = AdvanceCarController.Instance;
    }

    void OnTriggerEnter(Collider other)
    {
        if(GameObject.ReferenceEquals(other.gameObject, car.gameObject))
        {
            Logger.Instance.UpdateRuleMistake("continuous_line", car.transform.position);
            //Debug.Log("Violation of cross-solid-line");        
        }
    }
}
