using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logger : MonoBehaviour
{
    string[] listCurrentRules;
    bool loggerIsOn = false;

    // Dictionary<rule, mistakes number>
    public Dictionary<string, RuleObj> rulesMistakes;

    

    // Singleton pattern
    private static Logger _instance;
    public static Logger Instance { get { return _instance; } }
    void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        } else {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        GameManager.LoadingDone += InitLogger;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void InitLogger(bool loadIsDone)
    {
        if(GameManager.Instance.RulesList != null) {
            this.loggerIsOn = true;
            this.listCurrentRules = GameManager.Instance.RulesList;
            this.rulesMistakes = new Dictionary<string, RuleObj>();
            foreach(string rule in listCurrentRules)
            {
                this.rulesMistakes.Add(rule, new RuleObj());
            }
        }
    }

    public void UpdateRuleMistake(string rule, Vector3 location)
    {
        if(this.rulesMistakes.ContainsKey(rule))
        {
            this.rulesMistakes[rule].mistakes++;
            this.rulesMistakes[rule].locations.Add(location);
            Debug.Log(this.rulesMistakes[rule].mistakes);
            Debug.Log(this.rulesMistakes[rule].locations[0]);
        }
        
    }

    public class RuleObj
    {
        public int mistakes = 0;
        public List<Vector3> locations = new List<Vector3>();

    }
}
