using System.Runtime.InteropServices;
using UnityEngine;

public static class ReactCommunicate
{
    [DllImport("__Internal")]
    private static extern void TestStart();
    
    public static void StartReact () {
        #if UNITY_WEBGL == true && UNITY_EDITOR == false
            TestStart();
        #endif
    }

    [DllImport("__Internal")]
    private static extern void TestDone(int score, string summary);
    
    public static void ResultToReact (int score, string summary) {
        #if UNITY_WEBGL == true && UNITY_EDITOR == false
            TestDone(score, summary);
        #endif
    }
    
    public static void GetUserInfoReact (string dataAsJson) {
        // convert json string to array
        (string[] rulesFromJson, string emailFromJson) = GetJsonHandler.getJsonTuple<string>(dataAsJson);
        // Set rules
        GameManager.Instance.RulesList = rulesFromJson;
        // Set Email
        GameManager.Instance.Email = emailFromJson;
    }
}