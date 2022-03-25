using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContrallerManager : MonoBehaviour
{
    public static bool TestIsPause = false;

    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Sprite spritePause;
    [SerializeField]
    private Sprite spritePlay;


    public static ContrallerManager Instance;

    // Singleton pattern
    void Awake() {
        Instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickPlayPause()
    {
        if (TestIsPause)
        {
            Resume();
        } else {
            Pause();
        }
    }

    void Resume()
    {
        TestIsPause = false;
        Time.timeScale = 1f;
    }

    void Pause()
    {
        TestIsPause = true;
        Time.timeScale = 0f;
    }
}
