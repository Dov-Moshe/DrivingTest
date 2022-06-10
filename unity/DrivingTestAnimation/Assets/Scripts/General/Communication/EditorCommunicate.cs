using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System.Collections;

public static class EditorCommunicate
{
    // Get user info before beginning
    public static IEnumerator GetUserInfo() {

        string uri = "test_details";

        UnityWebRequest webRequest = UnityWebRequest.Get(Application.absoluteURL +  uri);

        // Request and wait for the desired page.
        yield return webRequest.SendWebRequest();

        string[] pages = uri.Split('/');
        int page = pages.Length - 1;

        // request status
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
        
        // get data (json string) from response
        string dataAsJson = webRequest.downloadHandler.text;
        // convert json string to array
        (string[] rulesFromJson, string emailFromJson) = GetJsonHandler.getJsonTuple<string>(dataAsJson);

        // Set rules
        GameManager.Instance.RulesList = rulesFromJson;
        
        // Set Email
        GameManager.Instance.Email = emailFromJson;

        Debug.Log(GameManager.Instance.RulesList[0]);
        //Debug.Log(RulesList[1]);
        Debug.Log(GameManager.Instance.Email);
    }
}