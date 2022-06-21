using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoEnterDetect : MonoBehaviour
{
    [SerializeField]
    private float beginAngle;
    [SerializeField]
    private float endAngle;

    AdvanceCarController car;

    // Start is called before the first frame update
    void Start()
    {
        this.car = AdvanceCarController.Instance;
    }

    void OnTriggerEnter(Collider other)
    {
        if(GameObject.ReferenceEquals(other.gameObject, car.gameObject))
        {
            float angle = CalculationAngle.calculateAngle(car.GetComponent<Collider>(), this.gameObject.transform);
            Debug.Log(angle);
            
            if(CalculationAngle.isActiveAngle(angle, beginAngle, endAngle))
            {
                Debug.Log("Violation of sign 'No Enter'");
            }
        }
        
    } 
}
