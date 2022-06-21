import React, { useEffect, useState } from 'react';
import { accountService, alertService } from '@/_services';

import config from 'config';
const baseUrl = `${config.apiUrl}/accounts`;

/*********/
import Unity, { UnityContext } from "react-unity-webgl";

const unityContext = new UnityContext({
  loaderUrl: "/Build/Build/Build.loader.js",
  dataUrl: "/Build/Build/Build.data",
  frameworkUrl: "/Build/Build/Build.framework.js",
  codeUrl: "/Build/Build/Build.wasm",
  streamingAssetsUrl: "/Build/streamingassets"
});
/*********/

function UnityComponent() {

    const [isTestDone, setIsTestDone] = useState(false);
    const [score, setScore] = useState(0);
    const [summary, setSummary] = useState("");

    useEffect(function () {
        unityContext.on("TestStart", function () {
            var _email = accountService.userValue.email;
            // grt json of rules' test
            var data = accountService.getDetails(_email);
            // sending the json to unity
            data.then(function(result) {
                var dataSend = JSON.stringify(result);
                console.log("dataSend");
                console.log(dataSend);
                unityContext.send("GameManager", "GetFromReact", dataSend);
            });
        });
    }, []);
    
    // get fields
    // remove usernames from all the page
    useEffect(function () {
        unityContext.on("TestDone", function (score, summary) {
            setScore(score);
            setSummary(summary)
            setIsTestDone(true);
        });
    }, []);
    // insert to DB
    useEffect(() => {
        if (isTestDone) {
            // . updatescores(score,summary)
       
        var _email = accountService.userValue.email; 
        accountService.updateRules(_email, score, summary)
          console.log(score);
          console.log(summary);
        }
      }, [isTestDone]);

    return (
        <div align="center">
            <Unity 
                unityContext={unityContext}
                style={{
                width: "50%",
                height:"500px",
                justifySelf: "center",
                alignSelf: "center",
                border: "2px solid black",
                margin: "50px",
                background: "grey"
                }}
            />
            {isTestDone === true}
        </div>
    )
}

export { UnityComponent };