using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BlinkerDetect : MonoBehaviour
{
    public bool isBlinkerRight = false;
    public bool isBlinkerLeft = false;

    [SerializeField]
    private GameObject rightBlinker;
    [SerializeField]
    private GameObject leftBlinker;

    public static event Action<string> BlinkerOn;

    Vector3 scaleBlinker;

    AdvanceCarController car;

    // Singleton pattern
    private static BlinkerDetect _instance;
    public static BlinkerDetect Instance { get { return _instance; } }
    void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        } else {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // save a reference to the car gameobject
        this.car = AdvanceCarController.Instance;
        // save the original scale
        this.scaleBlinker = rightBlinker.transform.localScale;
        // set the init scale of blinker
        LeanTween.scale(rightBlinker, Vector3.zero, 0);
        LeanTween.scale(leftBlinker, Vector3.zero, 0);
    }

    // Update is called once per frame
    void Update()
    {
        CheckBlinker();
    }


    void CheckBlinker()
    {

        if (Input.GetKeyDown(KeyCode.Period)) // right
        {
            if (isBlinkerLeft) // off left blinker
            {
                LeanTween.cancel(leftBlinker);
                LeanTween.scale(leftBlinker, Vector3.zero, 0.5f);
                isBlinkerLeft = false;
            }
            if (isBlinkerRight) // off right blinker
            {
                LeanTween.cancel(rightBlinker);
                LeanTween.scale(rightBlinker, Vector3.zero, 0.5f);
                isBlinkerRight = false;
                
            } else // on right blinker
            {
                isBlinkerRight = true;
                LeanTween.scale(rightBlinker, scaleBlinker, 0.5f).setEase(LeanTweenType.easeInOutSine).setLoopPingPong();
                BlinkerOn?.Invoke("right");
            }
        }
        else if (Input.GetKeyDown(KeyCode.Comma)) // left
        {
            if (isBlinkerRight) // off right blinker
            {
                LeanTween.cancel(rightBlinker);
                LeanTween.scale(rightBlinker, Vector3.zero, 0.5f);
                isBlinkerRight = false;
            }
            if (isBlinkerLeft) // off left blinker
            {
                LeanTween.cancel(leftBlinker);
                LeanTween.scale(leftBlinker, Vector3.zero, 0.5f);
                isBlinkerLeft = false;
                
            } else // on left blinker
            {
                isBlinkerLeft = true;
                LeanTween.scale(leftBlinker, scaleBlinker, 0.5f).setEase(LeanTweenType.easeInOutSine).setLoopPingPong();
                BlinkerOn?.Invoke("left");
            }
        }
    }
}
