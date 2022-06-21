using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.IO;

[System.Serializable]
public class QuestionHandler : MonoBehaviour
{
    public QuestionHandler(){}

    public void GetAllQuestions() {
        foreach (var rule in GameManager.Instance.RulesList)
        {
            StartCoroutine(GetQuestionsOfRule(rule));
        }
    }

    private IEnumerator GetQuestionsOfRule(string rule)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, rule + ".json");
        Debug.Log(filePath);
        UnityWebRequest www = UnityWebRequest.Get(filePath);
        yield return www.SendWebRequest();
        string dataAsJson = www.downloadHandler.text;
        
        (QusetionObj[] qusetionsArray, string exp, string title) = JsonHelperRules.FromJson<QusetionObj, string>(dataAsJson);
        Queue<QusetionObj> queue = new Queue<QusetionObj>(qusetionsArray);
        ExpObj expObj = new ExpObj(title, exp);
        RuleResults resultsObj = new RuleResults(GameManager.Instance.QuesionsPerRule);

        // adding to the map
        GameManager.Instance.quesionsMap.Add(rule, queue);
        GameManager.Instance.explanationMap.Add(rule, expObj);
        GameManager.Instance.resultsMap.Add(rule, resultsObj);
    }
}