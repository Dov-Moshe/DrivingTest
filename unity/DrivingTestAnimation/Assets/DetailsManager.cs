using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailsManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        QusetionManager.CorrectAnswerEvent += UpdateResult;
    }


    void UpdateResult(bool isCorrect, string rule)
    {
        RuleResults value;
        GameManager.Instance.resultsMap.TryGetValue(rule, out value);

        value.IncCount(isCorrect);
        value.UpdateQuestionsLeft();

    }
}
