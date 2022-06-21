using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContrallerManager : MonoBehaviour
{
    private bool TestIsPause = false;

    public static ContrallerManager Instance;

    // Singleton pattern
    void Awake() {
        Instance = this;
    }

    public void OnResume()
    {
        if(TestIsPause)
        {
            TestIsPause = false;
            Time.timeScale = 1f;
        }
    }

    public void OnPause()
    {
        if(!TestIsPause)
        {
            TestIsPause = true;
            Time.timeScale = 0f;
        }
    }
}
