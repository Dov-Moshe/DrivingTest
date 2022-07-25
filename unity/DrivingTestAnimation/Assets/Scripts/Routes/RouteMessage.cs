using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class RouteMessage : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textField;

    private string template;

    int numOfLocations;
    // Start is called before the first frame update
    void Start()
    {
        Route.OnPointPassed += UpdateNumLocations;
        this.numOfLocations = Route.Instance.numOfPoints;
        this.template = textField.text;
        textField.text = String.Format(this.template, this.numOfLocations.ToString());
    }

    void UpdateNumLocations(int point, bool isFinish)
    {
        textField.text = String.Format(this.template, (this.numOfLocations - point).ToString());
    }
}
