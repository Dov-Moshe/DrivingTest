import React, { useState } from 'react';
import * as ToggleButton from 'react-toggle-button';
import { Router, Route, Link, browserHistory, IndexRoute } from 'react-router'
import { accountService } from '@/_services';
import { Settings } from '../Settings/Index';
//import Unity, { UnityContext } from "react-unity-webgl";
/*
const unityContext = new UnityContext({
    loaderUrl: "/Build/Build/Build.loader.js",
    dataUrl: "/Build/Build/Build.data",
    frameworkUrl: "/Build/Build/Build.framework.js",
    codeUrl: "/Build/Build/Build.wasm",
    streamingAssetsUrl: "/Build/streamingassets"
  });
*/
const TRAFFIC_LIGHT = 'traffic_light';
const STOP_SIGN = 'stop_sign';
const YIELD_SIGN = 'yield_sign';
const NO_ENTER = 'no_enter_sign';
const ONE_WAY = 'one_way_sign';
const CROSS_WALK ='crosswalk_sign';
const BUMP = 'bump_sign';
const SQUARE ='square_sign';
const RED_WHITE = 'red_white_sidewalk';
//one_way_sign
const rootElement = document.getElementById("root");

function clickstartGame() {
    window.open("http://localhost:8111");
}

function clicksettings() {
    // add redirection
}

function clickSaveRules(arrayOfToogles, email) {
    debugger;
    console.log('arrayOfToogles');
    console.log(arrayOfToogles);
    accountService.updateRules(email, arrayOfToogles)
    // on finish
        .then(() => {
            alertService.success('saved successfully');
        })
    // error handler
        .catch(error => {
            alertService.error(error);
        });

    // add redirection
}
const addOrRemove = (array, item) => {
    const exists = array.includes(item)
  
    if (exists) {
      return array.filter((c) => { return c !== item })
    } else {
      const result = array
      result.push(item)
      return result
    }
}
let arrayOfToogles = [];
  function Home() {
    //const ruleArr = [TRAFFIC_LIGHT, STOP_SIGN];
    const user = accountService.userValue;
    const userRules = user.rules;
    arrayOfToogles = user.rules;
    const [isSettings, setSettings] = useState(false);
    const [isHighScoress, setIsHighScores] = useState(false);

    return (
        <div className="p-4">
            <div className="container">

                {!isSettings && !isHighScoress && <div>
                    <h1>Hi {user.firstName}!</h1>
                <p>Welcome To driving test!!</p>
                <button className="btn btn-primary" onClick={clickstartGame}>
                    Start Game
                </button>
                <button className="btn btn-primary" onClick={()=>setSettings(!isSettings)}>
                    Settings      
                </button>
                <button className="btn btn-primary" onClick={()=>setIsHighScores(!isHighScoress)}>
                    Show High Scores      
                </button></div>}
                {isSettings && <Settings/>}
                {isHighScoress && <Settings/>}
                <div></div>

            </div>
        </div>
    );
}

export { Home };