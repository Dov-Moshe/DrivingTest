using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareDetectionObject : MonoBehaviour
{
    [SerializeField]
    private int index;

    void OnTriggerEnter(Collider other)
    {
        transform.parent.GetComponent<SquareDetect>().OnCarEnter(other, index);
    }

    void OnTriggerExit(Collider other)
    {
        transform.parent.GetComponent<SquareDetect>().OnCarExit(other, index);
    }
}
