using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrakesMessage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AdvanceCarController.NotifyBrakesStatus += ActiveMessage;
    }

    void ActiveMessage(bool brakesStauts)
    {
        if (brakesStauts)
        {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        } else
        {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
