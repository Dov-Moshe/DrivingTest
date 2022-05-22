using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRule : MonoBehaviour
{
    [SerializeField]
    private Rule rule;

    // the range of angles that sholud be active to the trigger
    [SerializeField]
    private float activeAngleBegin;
    [SerializeField]
    private float activeAngleEnd;

    void OnTriggerEnter(Collider other)
    {
        float angle = calculateAngle(other);
        Debug.Log("angle: " + angle);

        string strRule = rule.ToString();
        Debug.Log("rule: " + strRule);

        if(GameManager.Instance.quesionsMap.ContainsKey(strRule) && isActiveAngle(angle))
        {
            TriggerTest.Instance.Trigger(strRule);
        }
    }

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

    public enum Rule {
        traffic_light,
        stop_sign,
        yield_sign,
        no_enter_sign,
        one_way_sign,
        crosswalk_sign,
        bump_sign,
        square_sign,
        red_white_sidewalk,
        red_yellow_sidewalk,
        speed_limit_sign,
        no_turn_back_sign,
        two_ways_sign,
        no_parking_sign,
    }
}
