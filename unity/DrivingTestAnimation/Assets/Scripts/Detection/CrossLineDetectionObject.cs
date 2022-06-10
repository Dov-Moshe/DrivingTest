using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossLineDetectionObject : MonoBehaviour
{
    //LineRenderer lineRenderer;
    //EdgeCollider2D edgeCollider;

    private AdvanceCarController car;
    // Start is called before the first frame update
    void Start()
    {
        this.car = AdvanceCarController.Instance;
        /*lineRenderer = GetComponent<LineRenderer>();
        edgeCollider = GetComponent<EdgeCollider2D>();*/
    }

    /*void Update()
    {
        AddPointsToCollider();
    }*/


    
    
    void OnTriggerEnter(Collider other)
    {
        if(GameObject.ReferenceEquals(other.gameObject, car.gameObject))
        {
            Logger.Instance.UpdateRuleMistake("continuous_line", car.transform.position);
            Debug.Log("Violation of cross-solid-line");        
        }
    }

    /*void AddPointsToCollider()
    {
        List<Vector2> edges = new List<Vector2>();
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            Vector3 pos = lineRenderer.GetPosition(i);
            edges.Add(new Vector2(pos.x, pos.y));
        }
        edgeCollider.SetPoints(edges);
    }*/
}
