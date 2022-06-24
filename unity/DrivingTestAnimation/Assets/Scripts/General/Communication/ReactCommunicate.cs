using System.Runtime.InteropServices;
using UnityEngine;

public class ReactCommunicate : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void TestStart();
    
    public void StartReact () {
        #if UNITY_WEBGL == true && UNITY_EDITOR == false
            TestStart();
        #endif
    }

    [DllImport("__Internal")]
    private static extern void TestDone(int score, string summary);
    
    public void ResultToReact (int score, string summary) {
        Debug.Log("Result to react: ");
        Debug.Log(score);
        Debug.Log(summary);
        #if UNITY_WEBGL == true && UNITY_EDITOR == false
            TestDone(score, summary);
        #endif
    }
    
    public void GetUserInfoReact (string dataAsJson) {
        // convert json string to array
        string[] rulesFromJson = GetJsonHandler.getJsonTuple<string>(dataAsJson);
        // Set rules
        GameManager.Instance.RulesList = rulesFromJson;


        Debug.Log(dataAsJson);
    }
}