using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public static Waypoints Instance;

    [SerializeField]
    private int directionsCount;

    [SerializeField]
    private float spanGizmos = 1f;

    [SerializeField]
    private Vector3 initPosition = new Vector3(0f, 0f, 0f);

    [SerializeField]
    private Vector3 initRotation = new Vector3(0, 0, 0);

    // Singleton pattern
    void Awake() {
        Instance = this;
    }

    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    void OnDrawGizmos()
    {
        foreach(Transform t in transform)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(t.position, spanGizmos);
        }

        Gizmos.color = Color.red;
        for(int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1).position);
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        AdvanceCarController car = AdvanceCarController.Instance;
        car.transform.position = this.initPosition;
        car.transform.eulerAngles = this.initRotation;
        //this.directionsCount = CountDirectionPoints();
    }
    
    // count points of direction
    /*private int CountDirectionPoints()
    {
        int count = 0;
        for(int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).GetComponent<DrivingInstructionPoint>().GetDirection != DrivingInstructionPoint.Direction.None)
                count++;
        }
        return count;
    }*/

    


    // curent point (num)


}
