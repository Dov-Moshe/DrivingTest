using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkerDetectionObject : MonoBehaviour
{
    AdvanceCarController car;
    BlinkerDetect blinkerDetect;
    // the rotation (y) of the car when enetr to the object
    float enterRotCar;

    // Start is called before the first frame update
    void Start()
    {
        // save a reference to the car gameobject
        this.car = AdvanceCarController.Instance;
        this.enterRotCar = car.transform.eulerAngles.y;
        this.blinkerDetect = BlinkerDetect.Instance;
    }


    // OnTriggerEnter is called when the Collider other enters the trigger.
    void OnTriggerEnter(Collider other)
    {
        if(GameObject.ReferenceEquals(other.gameObject, car.gameObject))
        {
            this.enterRotCar = car.transform.eulerAngles.y;
            Debug.Log("blinker enter: " + enterRotCar.ToString());
        }
        
    }

    // OnTriggerExit is called when the Collider other has stopped touching the trigger.
    void OnTriggerExit(Collider other)
    {
        if(GameObject.ReferenceEquals(other.gameObject, car.gameObject))
        {
            float exitRotCar = car.transform.eulerAngles.y;
            Debug.Log("blinker exit: " + exitRotCar.ToString());
            if((Mathf.Abs(enterRotCar - exitRotCar) % 360) > 60 && !(blinkerDetect.isBlinkerRight || blinkerDetect.isBlinkerLeft))
            {
                Logger.Instance.UpdateRuleMistake("blinker", new Vector3(car.transform.position.x, car.transform.position.y, car.transform.position.z));
                Debug.Log("Violation of Blinker");
            }
        }
        

    }

}
