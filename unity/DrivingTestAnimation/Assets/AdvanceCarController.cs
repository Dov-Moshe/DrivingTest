using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Wheel
{
    public GameObject transform;
    public WheelCollider collider;
    public bool isFront;
}

public class AdvanceCarController : MonoBehaviour
{

    public static AdvanceCarController Instance;

    [SerializeField]
    private float verticalInput;
    [SerializeField]
    private float horizontalInput;
    [SerializeField]
    private bool breakWheels;

    [SerializeField]
    private List<Wheel> wheels;

    [SerializeField]
    private float straight = 20000f;
    [SerializeField]
    private float maxTuen = 30f;

    // Singleton pattern
    void Awake() {
        Instance = this;
    }

    void Update()
    {
        GetInput();
    }

    void FixedUpdate()
    {
        UpdateMotor();
        UpdateWheels();
        UpdateSteering();
    }

    void GetInput()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        breakWheels = Input.GetKey("space");
    }

    void UpdateMotor()
    {
        foreach (Wheel wheel in wheels)
        {
            wheel.collider.motorTorque = straight * Time.deltaTime * verticalInput;
        }
    }

    void UpdateSteering()
    {
        foreach (Wheel wheel in wheels)
        {
            if(wheel.isFront)
            {
                float angle = maxTuen * horizontalInput;
                wheel.collider.steerAngle = Mathf.Lerp(wheel.collider.steerAngle, angle, 0.5f);
            }

        }
    }

    void UpdateWheels()
    {
        foreach(Wheel wheel in wheels)
            {
                Vector3 position;
                Quaternion rotation;
                wheel.collider.GetWorldPose(out position, out rotation);
                wheel.transform.transform.position = position;
                wheel.transform.transform.rotation = rotation;
            }
    }

    public void ResumeCar()
    {

    }

    public void PauseCar()
    {
        
    }

    public Vector3 GetCarPosition()
    {
        return this.transform.transform.position;
    }




}
