using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightDetectionObject : MonoBehaviour
{
    [SerializeField]
    private GameObject trafficLight;
    [SerializeField]
    private float enterBeginAngle;
    [SerializeField]
    private float enterEndAngle;
    [SerializeField]
    private float exitBeginAngle;
    [SerializeField]
    private float exitEndAngle;

    private AdvanceCarController car;
    
    private bool isEnter = false;

    // Start is called before the first frame update
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
                bool isRed = CheckTrafficLightColor();
                if(isRed == true)
                {
                    Logger.Instance.UpdateRuleMistake("traffic_light", new Vector3(car.transform.position.x, car.transform.position.y, car.transform.position.z));
                }
                
            }

            this.isEnter = false;
        }
    }

    bool CheckTrafficLightColor()
    {
        for (int i = 0; i < trafficLight.transform.childCount; i++)
        {
            GameObject child  = trafficLight.transform.GetChild(i).gameObject;
            Debug.Log(child.name);
            if(child.activeSelf == true && child.name == "Red_light")
            {
                    return true;
            }
        }
        return false;
    }
}
