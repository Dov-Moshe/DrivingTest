using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteInit : MonoBehaviour
{
    [SerializeField]
    private GameObject[] routes;

    // Start is called before the first frame update
    void Start()
    {
        string route = GameManager.Instance.RouteSelected;
        if (route == "A"){ routes[0].SetActive(true); }
        else if (route == "B"){ routes[1].SetActive(true); }
        else if (route == "C"){ routes[2].SetActive(true); }
    }
}
