using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UTurnDetectObject : MonoBehaviour
{
    [SerializeField]
    private float enterBeginAngle;
    [SerializeField]
    private float enterEndAngle;
    [SerializeField]
    private float exitBeginAngle;
    [SerializeField]
    private float exitEndAngle;

    private bool isEnter = false;

    private AdvanceCarController car;

    /// Start is called before the first frame update
    void Start()
    {
        this.car = AdvanceCarController.Instance;
    }


    /// OnTriggerEnter is called when the Collider other enters the trigger.
    void OnTriggerEnter(Collider other)
    {
        if(GameObject.ReferenceEquals(other.gameObject, car.gameObject))
        {
            float angle = CalculationAngle.calculateAngle(car.GetComponent<Collider>(), this.gameObject.transform);
        
            if(CalculationAngle.isActiveAngle(angle, enterBeginAngle, enterEndAngle))
            {
                this.isEnter = true;
            }
        }
    }


    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    void OnTriggerExit(Collider other)
    {
        if(GameObject.ReferenceEquals(other.gameObject, car.gameObject))
        {
            float angle = CalculationAngle.calculateAngle(car.GetComponent<Collider>(), this.gameObject.transform);

            if(!this.isEnter)
            {
                return;
            }
            else if(CalculationAngle.isActiveAngle(angle, exitBeginAngle, exitEndAngle))
            {
                Logger.Instance.UpdateRuleMistake("no_turn_back_sign", new Vector3(car.transform.position.x, car.transform.position.y, car.transform.position.z));
            }

            this.isEnter = false;
        }
    }
}
