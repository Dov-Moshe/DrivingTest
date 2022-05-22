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

    // brakes
    [SerializeField]
    private bool brakesWheels;
    private bool brakesWheelsInput;
    private Rigidbody rb;
    private float old_drag;

    [SerializeField]
    private List<Wheel> wheels;

    [SerializeField]
    private float straight = 20000f;
    [SerializeField]
    private float maxTuen = 30f;

    public static event Action<bool> NotifyBrakesStatus;

    // Singleton pattern
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        Instance = this;
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        brakesWheels = false;
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
        UpdateBrakes();
    }

    void GetInput()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        brakesWheelsInput = Input.GetKey("space");
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

    void UpdateBrakes()
    {
        if (brakesWheelsInput && rb.drag <= 30)
        {
            brakesWheels = brakesWheelsInput;
            float old_drag = rb.drag;
            NotifyBrakesStatus?.Invoke(true);
        }
        else if (rb.drag > 30 && brakesWheelsInput)
        {
            rb.drag = old_drag;
            brakesWheels = false;
            brakesWheelsInput = false;
            NotifyBrakesStatus?.Invoke(false);
            return;
        }
        else if (!brakesWheels || rb.drag > 30)
        {
            return;
        }  
        rb.drag += 0.5f;
    }

    public Vector3 GetCarPosition()
    {
        return this.transform.transform.position;
    }
}
