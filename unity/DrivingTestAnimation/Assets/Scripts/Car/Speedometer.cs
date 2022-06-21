using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Speedometer : MonoBehaviour
{

    public static event Action<float, float> LimitPassed;

    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private TMP_Text text;
    [SerializeField]
    private Image arrow;

    [SerializeField]
    private float maxSpeed = 240f;
    [SerializeField]
    private float minAngle = 0f;
    [SerializeField]
    private float maxAngle = -270f;
    [SerializeField]
    private float kmph = 11f;

    private float speed = 0f; 
    public float Speed { get { return speed; } }

    // Singleton pattern
    private static Speedometer _instance;
    public static Speedometer Instance { get { return _instance; } }
    void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        } else {
            _instance = this;
        }
    }


    float privSpeed = 0f;
    public float[] limitArray = new float[] { 0f, 50f, 60f, 90f };
    //public int len = limitArray.Length;

    // Update is called once per frame
    void FixedUpdate()
    {
        this.privSpeed = this.speed;
        this.speed = rb.velocity.magnitude * kmph;
        text.text = ((int)(speed)).ToString();
        arrow.transform.eulerAngles = new Vector3(0f, 0f, Mathf.Lerp(minAngle, maxAngle, speed / maxSpeed));
        
        for (int i = 0; i < limitArray.Length; i ++)
        {
            if (this.speed > limitArray[i] && limitArray[i] > this.privSpeed)
            {
                //Debug.Log(speed);
                LimitPassed?.Invoke(this.speed, limitArray[i]);
            }
        }
    }

}
