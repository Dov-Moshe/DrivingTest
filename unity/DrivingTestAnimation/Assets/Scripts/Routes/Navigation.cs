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
            float angle = Vector3.SignedAngle(targetDir, car.transform.forward.normalized, Vector3.up);
            navigationRect.localEulerAngles = new Vector3(0,0,angle);
        }
    }

    void UpdateCurrentNavigation(GameObject currentNavigation)
    {
        this.currentNavigation = currentNavigation;
    }


}
