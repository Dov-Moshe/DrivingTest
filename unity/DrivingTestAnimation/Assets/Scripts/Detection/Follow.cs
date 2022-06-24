using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Follow : MonoBehaviour
{
    [SerializeField]
    private PathCreator path;
    [SerializeField]
    private float speed = 2;

    private bool isPause = true;

    private float distance;

    [SerializeField]
    private float offset = 0.0f;


    private AdvanceCarController car;


    // Start is called before the first frame update
    void Start()
    {
        this.car = AdvanceCarController.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isPause)
        {
            float dist = Vector3.Distance(car.transform.position, transform.position);
            
            if(dist < 2f)
            {
                float angle = CalculationAngle.calculateAngle(car.GetComponent<Collider>(), transform);
                if(CalculationAngle.isActiveAngle(angle, 340f, 30f))
                {
                    return;
                }
            }

            distance += speed * Time.deltaTime;
            transform.position = path.path.GetPointAtDistance(distance + offset);
            Quaternion rot = path.path.GetRotationAtDistance(distance + offset);
            transform.rotation = rot;


        }
        
    }

    public void Pause()
    {
        isPause = true;
    }

    public void Resume()
    {
        isPause = false;
    }


}
