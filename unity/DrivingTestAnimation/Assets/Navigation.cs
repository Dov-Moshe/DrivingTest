using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigation : MonoBehaviour
{
    AdvanceCarController car;
    GameObject currentNavigation = null;
    [SerializeField]
    private RectTransform navigationRect;
    [SerializeField]
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        this.car = AdvanceCarController.Instance;
        Route.CurrentPoint += UpdateCurrentNavigation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(this.currentNavigation != null)
        {
            Vector3 targetDir = (currentNavigation.transform.position - car.transform.position).normalized;
            targetDir.y = 0.0f;
            float angle = Vector3.Angle(targetDir, new Vector3(car.transform.forward.x, 0f, car.transform.forward.z).normalized);
            navigationRect.localEulerAngles = new Vector3(0,0,angle);
            //navigationRect.localEulerAngles = Quaternion.LookRotation(difference.normalized);
            
            // Vector3 targetDir = new Vector3(currentNavigation.transform.position.x, 0f, cam.transform.position.y) - new Vector3(cam.transform.position.x, 0f, cam.transform.position.z);
            // float angle = Vector3.Angle(targetDir, cam.transform.position);
            // navigationRect.localEulerAngles = new Vector3(0,0,angle);
        }
    }

    void UpdateCurrentNavigation(GameObject currentNavigation)
    {
        this.currentNavigation = currentNavigation;
    }


}
