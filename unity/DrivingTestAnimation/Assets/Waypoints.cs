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

    // Singleton pattern
    void Awake() {
        Instance = this;
    }

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
