using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TickerMessage : MonoBehaviour
{
    RectTransform rect;
    private float width;
    public float Width {
        get { return this.rect.rect.width; }
    }

    public float PosX {
        get { return this.rect.anchoredPosition.x; }
    }

    public void Initialize(float widthTicker, string message, float speedTicker)
    {
        this.rect = GetComponent<RectTransform>();
        GetComponent<TMP_Text>().text = message;
        LeanTween.moveX(gameObject, 700, speedTicker).setDelay(1f).setIgnoreTimeScale(true).setOnComplete(()=>{Destroy(gameObject);});
    }
}