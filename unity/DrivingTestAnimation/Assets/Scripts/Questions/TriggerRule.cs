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

    private AdvanceCarController car;

    void Start()
    {
        this.car = AdvanceCarController.Instance;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(rule);
        if(GameObject.ReferenceEquals(other.gameObject, car.gameObject))
        {
            float angle = CalculationAngle.calculateAngle(car.GetComponent<Collider>(), this.gameObject.transform);
            string strRule = rule.ToString();
            if(GameManager.Instance.quesionsMap.ContainsKey(strRule) && CalculationAngle.isActiveAngle(angle, activeAngleBegin, activeAngleEnd))
            {
                Debug.Log("before triger test");
                TriggerTest.Instance.Trigger(strRule);
            }
        }
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
