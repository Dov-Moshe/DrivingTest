using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LimitDetectionObject : MonoBehaviour
{
    Collider coliider;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        this.coliider = GetComponent<Collider>();
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if((CollidersTouching.ColliderExist(GetComponent<Collider>()) == null) &&
            GameObject.ReferenceEquals(other.gameObject, AdvanceCarController.Instance.gameObject))
        {
            CollidersTouching.collidersTouching.Add(coliider);
        }
    }

    /// <summary>
    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerExit(Collider other)
    {
        if(CollidersTouching.ColliderExist(coliider) != null && GameObject.ReferenceEquals(other.gameObject, AdvanceCarController.Instance.gameObject))
        {
            CollidersTouching.collidersTouching.RemoveAt(CollidersTouching.ColliderExist(coliider) ?? 0);
        }
    }
}
