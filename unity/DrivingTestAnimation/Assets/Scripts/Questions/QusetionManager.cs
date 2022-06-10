using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.EventSystems;
using System;

public class QusetionManager : MonoBehaviour
{
    [SerializeField]
    private GameObject questionContainer;

    [SerializeField]
    private TMP_Text question;
    [SerializeField]
    private TMP_Text answerA;
    [SerializeField]
    private TMP_Text answerB;
    [SerializeField]
    private TMP_Text answerC;
    [SerializeField]
    private TMP_Text answerD;

    [SerializeField]
    private TMP_Text CorrectAnswerText;

    [SerializeField]
    private TMP_Text CorrectAnswerWhenWorngText;

    [SerializeField]
    private GameObject answersBox, correctFrame, worngFrame, rightAnswerView, closeWindow;

    private QusetionObj currentQuestion;
    public QusetionObj CurrentQuestion
    {
        get { return currentQuestion; }
        set { currentQuestion = value; }
    }

    private string currentRule;

    public static event Action<bool, string> CorrectAnswerEvent;

    public static QusetionManager Instance;

    void Awake() {
        Instance = this;
    }

    void Start()
    {
        LeanTween.scale(questionContainer, new Vector3(0,0,0), 0);
        
    }

    public void PopUpQuestion(string rule, QusetionObj q)
    {
        // load the question
        question.text = q.question;
        answerA.text = q.answer_a;
        answerB.text = q.answer_b;
        answerC.text = q.answer_c;
        answerD.text = q.answer_d;

        // saving the current question
        CurrentQuestion = q;
        // saving the current rule of the question
        this.currentRule = rule;


        LeanTween.scale(questionContainer, new Vector3(1,1,1), 0.5f).setIgnoreTimeScale(true);
    }

    public void OnClickAnswer(string answer) {

        closeWindow.SetActive(true);
        bool isCorrect = false;

        var correct_answer = CurrentQuestion.GetType().GetProperty(
            char.ToUpper(CurrentQuestion.correct_answer[0]) + CurrentQuestion.correct_answer.Substring(1)).GetValue(CurrentQuestion, null);
        if(CurrentQuestion.correct_answer == answer)
        {
            Debug.Log("you are correct!");
            CorrectAnswerText.text = (string)correct_answer;
            answersBox.SetActive(false);
            correctFrame.SetActive(true);
            isCorrect = true;
        } else
        {
            Debug.Log("you are worng! the correct answer is " + CurrentQuestion.correct_answer);
            var worng_answer = CurrentQuestion.GetType().GetProperty(char.ToUpper(answer[0]) + answer.Substring(1)).GetValue(CurrentQuestion, null);
            
            answersBox.SetActive(false);
            worngFrame.SetActive(true);
        }
        // first arg is boolean if the answer correct, second arg is the rule
        CorrectAnswerEvent?.Invoke(isCorrect, this.currentRule);
    }


    public void ClickFindCorrectAnswwr()
    {
        var correct_answer = CurrentQuestion.GetType().GetProperty(
            char.ToUpper(CurrentQuestion.correct_answer[0]) + CurrentQuestion.correct_answer.Substring(1)).GetValue(CurrentQuestion, null);
        CorrectAnswerWhenWorngText.text = (string)correct_answer;
        rightAnswerView.SetActive(true);
    }

    public void CloseWindow()
    {
        LeanTween.scale(questionContainer, new Vector3(0,0,0), 0.5f).setOnComplete(()=>{
            rightAnswerView.SetActive(false);
            worngFrame.SetActive(false);
            correctFrame.SetActive(false);
            closeWindow.SetActive(false);
            answersBox.SetActive(true);
        });
        
        //StartCoroutine(GameManager.Instance.CheckFinish());
    }
}
