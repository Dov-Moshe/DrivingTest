using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTest : MonoBehaviour
{
    public static TriggerTest Instance;

    void Awake() {
        Instance = this;
    }

    private string currentRuleTest;

    public void Trigger(string rule)
    {
        
        currentRuleTest = rule;

        Queue<QusetionObj> queue;
        if (!GameManager.Instance.quesionsMap.TryGetValue(rule, out queue))
        {
            Debug.Log("Error: this rule is not existing in this current test!");
            return;
        }

        RuleQuestionResults result;
        if (!Logger.Instance.questionResultsMap.TryGetValue(rule, out result))
        {
            Debug.Log("Error: this rule is not existing in this current test!");
            return;
        }
        if(result.GetNumQuestionsLeft() == 0)
        {
            return;
        }


        if (queue.Count == 0)
        {
            Debug.Log("Error: the question queue is empty!");
            return;
        }

        QusetionObj q = queue.Dequeue();
        ContrallerManager.Instance.OnPause();
        QusetionManager.Instance.PopUpQuestion(rule, q);
    }
}
