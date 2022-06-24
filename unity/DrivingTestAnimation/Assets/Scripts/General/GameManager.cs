using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

public class GameManager : MonoBehaviour
{

    private string[] rulesList;
    public string[] RulesList
    {
        get { return rulesList; }
        set { rulesList = value; }
    }

    private QusetionObj[] questions;
    public QusetionObj[] Questions
    {
        get { return questions; }
        set { questions = value; }
    }

    private string routeSelected;
    public string RouteSelected
    {
        get { return routeSelected; }
    }

    // how many question will be per test
    private int quesionsPerRule;
    public int QuesionsPerRule
    {
        get { return quesionsPerRule; }
    }


    public Dictionary<string, Queue<QusetionObj>> quesionsMap = new Dictionary<string, Queue<QusetionObj>>();
    public Dictionary<string, ExpObj> explanationMap = new Dictionary<string, ExpObj>();

    public static event Action<bool> LoadingDone;

    ReactCommunicate reactCommunicate;
    
    // Singleton pattern
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        } else {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        quesionsPerRule = 1;
        
        #if UNITY_EDITOR
            yield return StartCoroutine(EditorCommunicate.GetUserInfo());
            QuestionHandler handler = gameObject.AddComponent<QuestionHandler>();
            handler.GetAllQuestions();
        #elif UNITY_WEBGL
            reactCommunicate = gameObject.AddComponent<ReactCommunicate>();
            reactCommunicate.StartReact();
        #endif
        SceneManager.activeSceneChanged += ChangedActiveScene;
        yield return null;
        
    }

    void ChangedActiveScene(Scene current, Scene next)
    {
        Debug.Log("Scene Loaded");
        LoadingDone?.Invoke(true);
        SceneManager.activeSceneChanged -= ChangedActiveScene;
    }

    // get from react
    public void GetFromReact(string dataAsJson)
    {
        reactCommunicate.GetUserInfoReact(dataAsJson);
        QuestionHandler handler = gameObject.AddComponent<QuestionHandler>();
        handler.GetAllQuestions();
    }

    public void RouteSelectedAction(string route)
    {
        routeSelected = route;
        SceneManager.LoadScene("MainTest");
        //LoadingDone?.Invoke(true);
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

    public IEnumerator Finish()
    {
        OutputGenerator outputGenerator = gameObject.AddComponent<OutputGenerator>();
        outputGenerator.MakeOutput();
        (int score, string summary) = outputGenerator.GetOutputReact();

        #if UNITY_EDITOR
           yield return StartCoroutine(CreateRes(score, summary));
        #elif UNITY_WEBGL
            reactCommunicate.ResultToReact(score, summary);
        #endif
        SceneManager.LoadScene("FinishTest");
        yield return null;
    }

    private IEnumerator CreateRes(int score, string summary)
    {
        Debug.Log("got result");
        yield return null;
    }

    public void QuitTest()
    {
        // foreach (GameObject o in UnityEngine.Object.FindObjectsOfType<GameObject>()) {
        //         Destroy(o.gameObject);
        // }
        #if UNITY_WEBGL
            Application.Quit();
        #endif
    }
}
