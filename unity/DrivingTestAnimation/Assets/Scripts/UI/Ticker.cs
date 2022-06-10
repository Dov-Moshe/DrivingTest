using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ticker : MonoBehaviour
{
    
    private float widthTicker;

    [SerializeField]
    [Range(1f, 20f)]
    private float speedTicker = 20f;

    public TickerMessage tickerMessage;
    [SerializeField]
    private TickerMessage currentMessage;
    private int indexNextRule;

    [SerializeField]
    private TMP_Text[] messages;

    [SerializeField]
    private List<string> rules;

    private Dictionary<string, RuleResults> resultsMap;

    private Dictionary<string, int> leftQuestions;

    [SerializeField]
    private bool isDoneLoading;

    // Start is called before the first frame update
    void Start()
    {
        this.leftQuestions = new Dictionary<string, int>();
        this.rules = new List<string>();
        this.isDoneLoading = false;

        GameManager.LoadingDone += LoadData;

        widthTicker = GetComponent<RectTransform>().rect.width;

    }

    void OnEnable()
    {
        QusetionManager.CorrectAnswerEvent += UpdateRuleStatus;
    }

    void LoadData(bool isDone)
    {
        if (isDone)
        {
            this.resultsMap = GameManager.Instance.resultsMap;
            foreach(KeyValuePair<string, RuleResults> entry in this.resultsMap)
            {
                this.leftQuestions.Add(entry.Key, entry.Value.GetNumQuestionsLeft());
                this.rules.Add(entry.Key);
            }

            /*foreach(KeyValuePair<string, int> entry in this.leftQuestions)
            {
                Debug.Log("For " + entry.Key + ": " + entry.Value.ToString());
            }*/
            this.isDoneLoading = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (currentMessage != null && currentMessage.PosX > currentMessage.Width)
        {
            /*Debug.Log("PosX: " + currentMessage.PosX.ToString());
            Debug.Log("widthTicker: " + this.widthTicker.ToString());
            Debug.Log("currentMessageWidth: " + currentMessage.Width.ToString());
            Debug.Log("------------------");*/
            AddMessage(this.rules[indexNextRule]);
            this.indexNextRule = (this.indexNextRule + 1) % this.rules.Count;
        } else if (currentMessage == null && isDoneLoading)
        {
            AddMessage(this.rules[indexNextRule]);
            this.indexNextRule = (this.indexNextRule + 1) % this.rules.Count;
        }

        
    }

    void AddMessage(string rule)
    {
        int left;
        this.leftQuestions.TryGetValue(rule, out left);
        ExpObj exp;
        GameManager.Instance.explanationMap.TryGetValue(rule, out exp);
        string message = string.Format("עבור '{0}' נשארו לך עוד {1} שאלות", exp.title, left.ToString());

        // create instance of 'Ticker Message' and initialize it
        currentMessage = Instantiate(tickerMessage, transform);
        currentMessage.Initialize(widthTicker, message, speedTicker);
    }

    void UpdateRuleStatus(bool isCorrect, string rule)
    {
        int numQuestions;
        if (!this.leftQuestions.TryGetValue(rule, out numQuestions))
        {
            Debug.Log(string.Format("The rule {0} does not exist", rule));
            return;
        }

        if (numQuestions > 0)
        {
            this.leftQuestions[rule] -= 1;
        }
    }

}
