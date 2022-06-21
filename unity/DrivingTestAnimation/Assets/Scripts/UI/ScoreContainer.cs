using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;


public class ScoreContainer : MonoBehaviour
{

    [SerializeField]
    private TMP_Text score;

    // Start is called before the first frame update
    void Start()
    {
        this.score.text = "0";
    }
    
    void OnEnable()
    {
        QusetionManager.CorrectAnswerEvent += UpdateScore;
    }

    void UpdateScore(bool isCorrect, string rule)
    {
        if (isCorrect)
        {
            int privScore = 0;
            Int32.TryParse(this.score.text, out privScore);
            this.score.text = (privScore + 10).ToString();
        }
        
    }
}
