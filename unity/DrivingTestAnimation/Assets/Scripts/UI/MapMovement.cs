using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class MapMovement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Camera cam;

    private Vector3 startMousePosition;

    [SerializeField]
    private float scale = 0.1f;

    private bool isEnter = false;

    private AdvanceCarController car;

    void Start()
    {
        this.car = AdvanceCarController.Instance;
    }


    // Update is called once per frame
    void Update()
    {
        if (isEnter)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startMousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
            } else if (Input.GetMouseButton(0))
            {
                Vector3 mouseMovement = startMousePosition - cam.ScreenToWorldPoint(Input.mousePosition);

                cam.transform.position += mouseMovement;
            }
            float size = cam.orthographicSize - Input.mouseScrollDelta.y * scale;
            if(3f < size && size < 50)
            {
                cam.orthographicSize -= Input.mouseScrollDelta.y * scale;
            }
        }
        
    }

    public void FocusCar()
    {
        cam.transform.position = new Vector3(car.transform.position.x, cam.transform.position.y, car.transform.position.z);
    }

    //Detect if the Cursor starts to pass over the GameObject
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        isEnter = true;
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        isEnter = false;
    }
}
