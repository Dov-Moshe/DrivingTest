import React, { useCallback, useEffect, useState } from 'react';
import { accountService } from '@/_services';

import config from 'config';
const baseUrl = `${config.apiUrl}/accounts`;

import { Unity, useUnityContext } from "react-unity-webgl";

var unloadObject = ()=>{};

function UnityComponent() {

    const { unityProvider, addEventListener, removeEventListener, sendMessage, unload } = useUnityContext({
        loaderUrl: "/Build/Build/Build.loader.js",
        dataUrl: "/Build/Build/Build.data",
        frameworkUrl: "/Build/Build/Build.framework.js",
        codeUrl: "/Build/Build/Build.wasm",
        streamingAssetsUrl: "/Build/streamingassets",
    });

    unloadObject = unload;

    const [isTestDone, setIsTestDone] = useState(false);
    const [score, setScore] = useState(0);
    const [summary, setSummary] = useState("");

    function handleStartRules(dataSend) {
        sendMessage("GameManager", "GetFromReact", dataSend);
    }

    const handleStartTest = useCallback(()=>{
        var _email = accountService.userValue.email;
        // get json of rules' test
        var data = accountService.getDetails(_email);
        // sending the json to unity
        data.then(function(result) {
            var dataSend = JSON.stringify(result);
            handleStartRules(dataSend);
        });
    }, [sendMessage]);

    useEffect(() => {
        addEventListener("TestStart", handleStartTest);
        return () => {
          removeEventListener("TestStart", handleStartTest);
        };
    }, [addEventListener, removeEventListener, handleStartTest]);


    const handleTestDone = useCallback((score, summary)=>{
        setScore(score);
        setSummary(summary);
        setIsTestDone(true);
    }, []);

    useEffect(() => {
        if (isTestDone) {
            var _email = accountService.userValue.email; 
            accountService.updateScores(_email, score, summary);
        }
    }, [isTestDone]);


    useEffect(() => {
        addEventListener("TestDone", handleTestDone);
        return () => {
          removeEventListener("TestDone", handleTestDone);
        };
    }, [unityProvider, addEventListener, removeEventListener, handleTestDone]);

    return (
        <div align="center">
            <Unity unityProvider={unityProvider}
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
    );
}

function QuitUnity(){
    unloadObject();
};

export { UnityComponent, QuitUnity };