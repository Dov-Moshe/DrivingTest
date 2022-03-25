using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerLearnRule : MonoBehaviour
{
    [SerializeField]
    private string ruleType;
    [SerializeField]
    private Sprite newSprite;
    [SerializeField]
    private bool ignoreTrigger = false;
    [SerializeField]
    private GameObject windowLearn;
    
    // the range of angles that sholud be active to the trigger
    [SerializeField]
    private float activeAngleBegin;
    [SerializeField]
    private float activeAngleEnd;
    
    /*[SerializeField]
    private Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();*/

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(GameManager.Instance.state == GameManager.GameState.Learn)
        {
            float angle = calculateAngle(other);
            Debug.Log("angle: " + angle);
            if(!ignoreTrigger && isActiveAngle(angle))
            {
                Debug.Log("Active");
                //spriteRenderer.sprite = newSprite;
                ContrallerManager.Instance.OnClickPlayPause();
                windowLearn.GetComponent<Image>().sprite = newSprite;
                windowLearn.SetActive(true);
            }
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


}
