using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonOnClick : MonoBehaviour
{
    [SerializeField]
    string nameButton;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(onClick);
    }

    void onClick()
    {
        QusetionManager.Instance.OnClickAnswer(nameButton);
    }
}
