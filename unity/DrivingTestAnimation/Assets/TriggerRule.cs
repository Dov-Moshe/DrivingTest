using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRule : MonoBehaviour
{
    protected string ruleType;
    protected bool ignoreTrigger = false; 

    // the range of angles that sholud be active to the trigger
    [SerializeField]
    private float activeAngleBegin;
    [SerializeField]
    private float activeAngleEnd;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hello");
    }*/

    void OnTriggerEnter(Collider other)
    {
        if(GameManager.Instance.state == GameManager.GameState.Test)
        {
            float angle = calculateAngle(other);
            Debug.Log("angle: " + angle);
            if(!ignoreTrigger && isActiveAngle(angle))
            {
                OnTest(other);
            }
        }

        //Vector3 closestPoint = other.ClosestPoint(AdvanceCarController.Instance.transform.position);
                //Vector3 hitObjectPosition3D = AdvanceCarController.Instance.transform.position;
                //Vector2 hitObjectPosition2D = new Vector2(hitObjectPosition3D.x, hitObjectPosition3D.z);
                //Vector2 closestPoint2D = new Vector2(closestPoint.x, closestPoint.z);
                //Vector2 point = closestPoint2D - hitObjectPosition2D;

                //float theta = Mathf.Atan2(point.x, point.y);
                //float angle = (360 - ((theta * 180) / Mathf.PI)) % 360;

                //Vector3 targetDir = closestPoint - transform.position;
                //float angle = Vector3.Angle(targetDir, transform.forward);

                //float angle = Vector3.Angle(closestPoint, this.transform.position);

                //Debug.Log("car: " + closestPoint);
                //Debug.Log("point: " + this.transform.position);
                
                //if(closestPoint.x > transform.position.x)
                //    angle = 360 - angle;
    }

    public virtual void OnTest(Collider other){}

    public virtual void OnLearn(Collider other){}

    // calculating the angle between this object and the other collider
    private float calculateAngle(Collider other)
    {
        Vector3 closestPoint = other.ClosestPoint(AdvanceCarController.Instance.transform.position);
        Vector3 targetDir = closestPoint - transform.position;
        float angle = Vector3.Angle(targetDir, transform.forward);

        if(closestPoint.x > transform.position.x)
                    angle = 360 - angle;

        return angle;
    }

    // checking if this is angle that sholud be active to the trigger
    private bool isActiveAngle(float angle)
    {
        if((activeAngleBegin <= activeAngleEnd) && (activeAngleBegin <= angle && angle <= activeAngleEnd))
        {
            return true;
        } else if ((activeAngleBegin > activeAngleEnd) && ((activeAngleBegin <= angle && angle <= 0) || (0 <= angle && angle <= activeAngleEnd)))
        {
            return true;
        }
        return false;
    }

}
