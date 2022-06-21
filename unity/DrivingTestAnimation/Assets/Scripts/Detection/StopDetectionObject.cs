using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StopDetectionObject : MonoBehaviour
{
    [SerializeField]
    private float beginAngle;
    [SerializeField]
    private float endAngle;

    private AdvanceCarController car;
    private Speedometer speedometer;
    private float speed;
    private bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        this.car = AdvanceCarController.Instance;
        this.speedometer = Speedometer.Instance;
    }

    void Update()
    {
        if(isActive)
        {
            CheckForStop();
        }
    }

    void CheckForStop() {
        this.speed = this.speedometer.Speed;
        //Debug.Log(this.speed);

        if (speed < 1f)
        {
            isActive = false;
        }
    }

    

    void OnTriggerEnter(Collider other)
    {
        if(GameObject.ReferenceEquals(other.gameObject, car.gameObject))
        {
            float angle = CalculationAngle.calculateAngle(car.GetComponent<Collider>(), this.gameObject.transform);
            //Debug.Log("stop: " + angle.ToString());
            
            if(CalculationAngle.isActiveAngle(angle, beginAngle, endAngle))
            {
                this.isActive = true;
            }
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        if(GameObject.ReferenceEquals(other.gameObject, car.gameObject))
        {
            if(this.isActive)
            {
                this.isActive = false;
                if(speed > 1f)
                {
                    Logger.Instance.UpdateRuleMistake("stop_sign", car.transform.position);
                    Debug.Log("Violation of sign 'Stop'");
                } 
            }
        }
    }
}
