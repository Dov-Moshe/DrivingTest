using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MessageLocation : MonoBehaviour
{
    [SerializeField]
    private TMP_Text pointStr;
    [SerializeField]
    private TMP_Text feedback;

    [SerializeField]
    private GameObject message;
    [SerializeField]
    private GameObject messageFinish;

    private string feedbackTemplate;


    // Start is called before the first frame update
    void Start()
    {
        // Add the gameObject as listener to class "Location Point", to get notification when the user arrive to the current location
        Route.OnPointPassed += DisplayMessage;
        // Makes the message invisible
        LeanTween.scale(gameObject, Vector3.zero, 0f).setIgnoreTimeScale(true);
        feedbackTemplate = feedback.text;
        messageFinish.SetActive(false);
    }

    void DisplayMessage(int point, bool isFinish)
    {
        if(isFinish)
        {
            message.SetActive(false);
            messageFinish.SetActive(true);
            pointStr.text = point.ToString();
            // Display the message
            LeanTween.scale(gameObject, new Vector3(1,1,1), 1f).setIgnoreTimeScale(true);
        }
        else
        {
            pointStr.text = point.ToString();
            feedback.text = String.Format(feedbackTemplate, point.ToString());
            // Display the message
            LeanTween.scale(gameObject, new Vector3(1,1,1), 1f).setIgnoreTimeScale(true);
        }
    }

    public void CloseMessage()
    {
        // Makes the message invisible
        LeanTween.scale(gameObject,  Vector3.zero, 1f).setIgnoreTimeScale(true);
    }

    public void EventFinishTest()
    {
        // Makes the message invisible
        LeanTween.scale(gameObject,  Vector3.zero, 1f).setIgnoreTimeScale(true);
        StartCoroutine(GameManager.Instance.Finish());
    }
}
