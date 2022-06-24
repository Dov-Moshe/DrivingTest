using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneAactive : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> cars;
    private AdvanceCarController car;
    // Start is called before the first frame update
    void Start()
    {
        this.car = AdvanceCarController.Instance;
    }

    
    void OnTriggerEnter(Collider other)
    {
        if(GameObject.ReferenceEquals(other.gameObject, car.gameObject))
        {
            foreach (GameObject c in cars)
            {
                c.GetComponent<Follow>().Resume();
            }
        }
    }

    
    void OnTriggerExit(Collider other)
    {
        if(GameObject.ReferenceEquals(other.gameObject, car.gameObject))
        {
            foreach (GameObject c in cars)
            {
                c.GetComponent<Follow>().Pause();
            }
        }
        
    }
}
