using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;


public class SummaryMessage : MonoBehaviour
{
    [SerializeField]
    private TMP_Text scoreField;

    [SerializeField]
    private TMP_Text summaryField;

    // Start is called before the first frame update
    void Start()
    {
        OutputGenerator output = GameManager.Instance.gameObject.GetComponent<OutputGenerator>();
        (int score, string summary) = output.GetOutputDisplay();

        // reverse string of score for display
        char[] arr = score.ToString().ToCharArray();
        Array.Reverse(arr);
        string score_str = new string(arr);

        scoreField.text = scoreField.text + score_str.ToString();
        summaryField.text = summary;
    }
}
