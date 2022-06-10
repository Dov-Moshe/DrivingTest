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

    private float distance;

    private AdvanceCarController car;

    // Start is called before the first frame update
    void Start()
    {
        this.car = AdvanceCarController.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(car.transform.position, transform.position);
        //print("Distance to other: " + dist);
        if(dist > 5f)
        {
            distance += speed * Time.deltaTime;
            transform.position = path.path.GetPointAtDistance(distance);
            Quaternion rot = path.path.GetRotationAtDistance(distance);
            transform.rotation = rot;
        }
    }
}
