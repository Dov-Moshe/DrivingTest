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
        Debug.Log("inside triger test");
        
        currentRuleTest = rule;

        RuleResults ruleResults;
        if (!GameManager.Instance.resultsMap.TryGetValue(rule, out ruleResults))
        {
            Debug.Log("Error: this rule is not existing in this current test!");
            return;
        }

        if (ruleResults.GetNumQuestionsLeft() == 0)
        {
            Debug.Log("The questions for this test are done!");
            return;
        }

        Queue<QusetionObj> queue;
        if (!GameManager.Instance.quesionsMap.TryGetValue(rule, out queue))
        {
            Debug.Log("Error: this rule is not existing in this current test!");
            return;
        }

        if (queue.Count == 0)
        {
            Debug.Log("Error: the question queue is empty!");
            return;
        }

        QusetionObj q = queue.Dequeue();
        Debug.Log("inside triger test before pause");
        ContrallerManager.Instance.OnPause();
        Debug.Log("inside triger test after pause");
        QusetionManager.Instance.PopUpQuestion(rule, q);
    }
}
