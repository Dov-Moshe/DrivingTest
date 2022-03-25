using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState state;

    // Array or of rules
    //private string[] rules = ['square', 'crosswalk']

    // Singleton pattern
    void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GetUserInfo();
        SetRulesAndState();      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Get user info before beginning
    void GetUserInfo() {
        // get user
        // get rules
        // api
        // { username: user@name, name: name, list_rules: [] }

    }

    // Set rules
    void SetRules()
    {
        // input rules into game
    }

    // Set state
    void SetRulesAndState()
    {
        state = GameState.Learn;
        
    }

    public enum GameState {
        Test,
        Learn
    }
}
