using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class SummaryMessage : MonoBehaviour
{
    [SerializeField]
    private TMP_Text summaryField;
    // Start is called before the first frame update
    void Start()
    {
        OutputGenerator output = GameManager.Instance.gameObject.GetComponent<OutputGenerator>();
        (int score, string summary) = output.GetOutput();
        summaryField.text = summary;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // void UpdateSummary(string summary)
    // {
    //     summaryField.text = summary;
    // }
}
