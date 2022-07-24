using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LimitDetection : MonoBehaviour
{
    [System.Serializable]
    public struct DetectionObj
    {
        public float key;
        public GameObject value;
    }


    [SerializeField]
    private List<DetectionObj> detectionObjects;

    private ILookup<float, GameObject> lookup;

    private AdvanceCarController car;

    // Singleton pattern
    private static LimitDetection _instance;
    public static LimitDetection Instance { get { return _instance; } }
    void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        } else {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // convert the list to lookup data structure
        lookup = detectionObjects.ToLookup(x => x.key, x => x.value);

        this.car = AdvanceCarController.Instance;

        // adding the method 'FindObjectDetection' as listener to 'Speedometer' events
        Speedometer.LimitPassed += FindObjectDetection;
    }


    // Find the object the car touching
    void FindObjectDetection(float speed, float limit)
    {
        foreach (GameObject x in lookup[limit])
        {
            if (CollidersTouching.IsTouching(x.GetComponent<Collider>()) && limit < speed)
            {
                Logger.Instance.UpdateRuleMistake("speed_limit_sign", car.transform.position);
                Debug.Log("OverLimit " + speed.ToString());
            }

        }
    }
}