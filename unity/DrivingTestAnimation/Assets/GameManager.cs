using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private string[] rulesList;
    public string[] RulesList
    {
        get { return rulesList; }
        set { rulesList = value; }
    }

    private string email;
    public string Email
    {
        get { return email; }
        set { email = value; }
    }

    private QusetionObj[] questions;
    public QusetionObj[] Questions
    {
        get { return questions; }
        set { questions = value; }
    }

    public Dictionary<string, Queue<QusetionObj>> quesionsMap = new Dictionary<string, Queue<QusetionObj>>();
    public Dictionary<string, ExpObj> explanationMap = new Dictionary<string, ExpObj>();

    // results
    public Dictionary<string, RuleResults> resultsMap = new Dictionary<string, RuleResults>();

    // how many question will be per test
    int NUM_OF_QUESTION_PER_RULE;

    public static event Action<bool> LoadingDone;

    // Singleton pattern
    void Awake() {
        Instance = this;
        DontDestroyOnLoad(this.transform.gameObject);
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        this.NUM_OF_QUESTION_PER_RULE = 2;
        yield return StartCoroutine(GetUserInfo());
        yield return StartCoroutine(GetAllQuestions());
    }

    // Get user info before beginning
    IEnumerator GetUserInfo() {

        /*string filePath = Path.Combine(Application.streamingAssetsPath, "string_api.json");
        UnityWebRequest www = UnityWebRequest.Get(filePath);
        yield return www.SendWebRequest();*/


        string uri = "/test_details";

        UnityWebRequest webRequest = UnityWebRequest.Get(Application.absoluteURL +  uri);

        // Request and wait for the desired page.
        yield return webRequest.SendWebRequest();

        string[] pages = uri.Split('/');
        int page = pages.Length - 1;

        switch (webRequest.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
                Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                break;
            case UnityWebRequest.Result.Success:
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                break;
        }

        string dataAsJson = webRequest.downloadHandler.text;

        (string[] rulesFromJson, string emailFromJson) = JsonHelperTuple.getJsonTuple<string>(dataAsJson);

        // Set rules
        RulesList = rulesFromJson;
        
        // Set Email
        Email = emailFromJson;

        Debug.Log(RulesList[0]);
        //Debug.Log(RulesList[1]);
        Debug.Log(Email);
    }

    private IEnumerator GetAllQuestions() {
        foreach (var rule in RulesList)
        {
            yield return StartCoroutine(GetQuestionsOfRule(rule));
        }
        LoadingDone?.Invoke(true);
    }

    private IEnumerator GetQuestionsOfRule(string rule)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, rule + ".json");
        UnityWebRequest www = UnityWebRequest.Get(filePath);
        yield return www.SendWebRequest();
        string dataAsJson = www.downloadHandler.text;
        (QusetionObj[] qusetionsArray, string exp, string title) = JsonHelperRules.FromJson<QusetionObj, string>(dataAsJson);
        Queue<QusetionObj> queue = new Queue<QusetionObj>(qusetionsArray);
        ExpObj expObj = new ExpObj(title, exp);
        RuleResults resultsObj = new RuleResults(NUM_OF_QUESTION_PER_RULE);

        // adding to the map
        quesionsMap.Add(rule, queue);
        explanationMap.Add(rule, expObj);
        resultsMap.Add(rule, resultsObj);
    }

    public static class JsonHelperTuple
    {
        public static (T[], T) getJsonTuple<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>> (json);
            return (wrapper.rules, wrapper.email);
        }
    
        [Serializable]
        private class Wrapper<T>
        {
            public T[] rules;
            public T email;
        }
    }


    IEnumerator SetTestResults(string results)
    {
        string uri = "/test_results";
        UnityWebRequest webRequest = UnityWebRequest.Post(Application.absoluteURL +  uri, results);

        // Request and wait.
        yield return webRequest.SendWebRequest();

        switch (webRequest.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
                Debug.LogError("Error: " + webRequest.error);
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.LogError("HTTP Error: " + webRequest.error);
                break;
            case UnityWebRequest.Result.Success:
                Debug.Log("Received: " + webRequest.downloadHandler.text);
                break;
        }
        
    }

    public IEnumerator CheckFinish()
    {
        foreach(KeyValuePair<string, RuleResults> entry in resultsMap)
        {
            if(entry.Value.GetNumQuestionsLeft() != 0)
                yield return null;
        }

        Finish();
    }

    private IEnumerator Finish()
    {
        yield return StartCoroutine(SetTestResults("10"));

        SceneManager.LoadScene("FinishTest");
    }

}
