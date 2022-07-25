using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Events;
using System;

public class Route : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> points;
    GameObject currentPoint;
    int indexCurentPoint = 0;

    public static event Action<int, bool> OnPointPassed;
    public static event Action<GameObject> CurrentPoint;
    public int numOfPoints = 0;

    private static Route _instance;
    public static Route Instance { get { return _instance; } }
    // Singleton pattern
    void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        } else {
            _instance = this;
        }
        numOfPoints = points.Count;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentPoint = points[indexCurentPoint];
        currentPoint.SetActive(true);
        CurrentPoint?.Invoke(currentPoint);
    }

    public void OnPointTrigger()
    {
        if(points.Count - 1 == indexCurentPoint)
        {
            OnPointPassed?.Invoke(indexCurentPoint + 1, true);
            currentPoint.SetActive(false);
            CurrentPoint?.Invoke(null);
        }
        else
        {
            OnPointPassed?.Invoke(indexCurentPoint + 1, false);
            indexCurentPoint++;
            currentPoint = points[indexCurentPoint];
            currentPoint.SetActive(true);
            CurrentPoint?.Invoke(currentPoint);
        }
        Debug.Log(String.Format("Count: {0}, Index: {1}", points.Count.ToString(), indexCurentPoint.ToString()));
    }
}
