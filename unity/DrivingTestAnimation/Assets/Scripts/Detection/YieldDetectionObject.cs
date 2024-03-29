using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YieldDetectionObject : MonoBehaviour
{
    private AdvanceCarController car;

    [SerializeField]
    private GameObject otherCar = null;
    private bool otherIsInside = false;

    [SerializeField]
    private float beginAngle;
    [SerializeField]
    private float endAngle;


    // Start is called before the first frame update
    void Start()
    {
        this.car = AdvanceCarController.Instance;
    }


    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if(GameObject.ReferenceEquals(other.gameObject, car.gameObject) && this.otherIsInside)
        {
            float angle = CalculationAngle.calculateAngle(car.GetComponent<Collider>(), otherCar.transform);

            if(CalculationAngle.isActiveAngle(angle, beginAngle, endAngle) && this.otherIsInside)
            {
                Logger.Instance.UpdateRuleMistake("yield_sign", new Vector3(car.transform.position.x, car.transform.position.y, car.transform.position.z));
            }
        } else {
            otherIsInside = true;
            this.otherCar = other.gameObject;
        }
    }


    /// <summary>
    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerExit(Collider other)
    {
        if(!GameObject.ReferenceEquals(other.gameObject, car.gameObject))
        {
            this.otherCar = null;
            otherIsInside = false;
        }
    }
}
