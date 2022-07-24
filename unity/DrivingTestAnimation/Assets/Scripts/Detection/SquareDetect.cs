using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareDetect : MonoBehaviour
{
    // if other car inside object (by index)
    private bool[] indexObjectStatus;

    private AdvanceCarController car;
    private bool carIsInside;

    // Start is called before the first frame update
    void Start()
    {
        this.indexObjectStatus = new bool[] { false, false, false, false };
        this.car = AdvanceCarController.Instance;
        this.carIsInside = false;
    }

    public void OnCarEnter(Collider other, int index)
    {
        if(!GameObject.ReferenceEquals(other.gameObject, car.gameObject)) // if it's not the main car, then mark the object
        {
            indexObjectStatus[index] = true;
            return;
        }
        
        if(!carIsInside && (indexObjectStatus[index] || indexObjectStatus[(index + 7) % 4])) // if it's the main car, check if other car inside relevant objects
        {
            Logger.Instance.UpdateRuleMistake("square_sign", new Vector3(car.transform.position.x, car.transform.position.y, car.transform.position.z));
            Debug.Log("Violation of taking other car right inside square");
        }

        if(!carIsInside) // if it's the main car then mark as enter
        {
            carIsInside = true;
        }
    }

    public void OnCarExit(Collider other, int index)
    {
        if(!GameObject.ReferenceEquals(other.gameObject, car.gameObject)) // if it's not the main car, then mark the object thye car inside
        {
            indexObjectStatus[index] = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(GameObject.ReferenceEquals(other.gameObject, car.gameObject))
        {
            carIsInside = false;
        }
    }
}
