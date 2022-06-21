import React, { useState } from 'react';
import * as ToggleButton from 'react-toggle-button';
import { Router, Route, Link, browserHistory, IndexRoute } from 'react-router'
import { accountService, alertService } from '@/_services';
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
const CROSS_WALK = 'crosswalk_sign';
const BUMP = 'bump_sign';
const SQUARE = 'square_sign';
const RED_WHITE = 'red_white_sidewalk';
const rootElement = document.getElementById("root");

function clickstartGame() {
    window.open("http://localhost:8111");
}


function clickSaveRules(arrayOfToogles, email) {
    console.log('arrayOfToogles');
    console.log(arrayOfToogles);
    accountService.updateRules(email, arrayOfToogles)
        // on finish
        .then(() => {
            console.log('hii');
            alertService.success('saved successfully', { keepAfterRouteChange: true });
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
function Settings() {
    const user = accountService.userValue;
    const userRules = user.rules;
    arrayOfToogles = user.rules;
    const [traffic_light, setToggleValueTraffic_light] = useState(userRules.indexOf(TRAFFIC_LIGHT) > -1);
    const onToggleHandlerTraffic_light = () => {
        setToggleValueTraffic_light(!traffic_light);
        !traffic_light ? arrayOfToogles.push(TRAFFIC_LIGHT) : arrayOfToogles.splice(arrayOfToogles.indexOf(TRAFFIC_LIGHT), 1);
    }
    const [stop_sign, setToggleValueStop_sign] = useState(userRules.indexOf(STOP_SIGN) > -1);
    const onToggleHandlerStop_sign = () => {
        setToggleValueStop_sign(!stop_sign);
        !stop_sign ? arrayOfToogles.push(STOP_SIGN) : arrayOfToogles.splice(arrayOfToogles.indexOf(STOP_SIGN), 1);

    }
    const [yield_sign, setToggleValueYield_sign] = useState(userRules.indexOf(YIELD_SIGN) > -1);
    const onToggleHandlerYield_sign = () => {
        setToggleValueYield_sign(!yield_sign);
        !yield_sign ? arrayOfToogles.push(YIELD_SIGN) : arrayOfToogles.splice(arrayOfToogles.indexOf(YIELD_SIGN), 1);

    }
    const [no_enter_sign, setToggleValueNo_enter_sign] = useState(userRules.indexOf(NO_ENTER) > -1);
    const onToggleHandlerNo_enter_sign = () => {
        setToggleValueNo_enter_sign(!no_enter_sign);
        !no_enter_sign ? arrayOfToogles.push(NO_ENTER) : arrayOfToogles.splice(arrayOfToogles.indexOf(NO_ENTER), 1);

    }
    const [one_way_sign, setToggleValueOne_way_sign] = useState(userRules.indexOf(ONE_WAY) > -1);
    const onToggleHandlerOne_way_sign = () => {
        setToggleValueOne_way_sign(!one_way_sign);
        !one_way_sign ? arrayOfToogles.push(ONE_WAY) : arrayOfToogles.splice(arrayOfToogles.indexOf(ONE_WAY), 1);

    }
    const [crosswalk_sign, setToggleValueCrosswalk_sign] = useState(userRules.indexOf(CROSS_WALK) > -1);
    const onToggleHandlerCrosswalk_sign = () => {
        setToggleValueCrosswalk_sign(!crosswalk_sign);
        !crosswalk_sign ? arrayOfToogles.push(CROSS_WALK) : arrayOfToogles.splice(arrayOfToogles.indexOf(CROSS_WALK), 1);
    }
    const [bump_sign, setToggleValueBump_sign] = useState(userRules.indexOf(BUMP) > -1);
    const onToggleHandlerBump_sign = () => {
        setToggleValueBump_sign(!bump_sign);
        !bump_sign ? arrayOfToogles.push(BUMP) : arrayOfToogles.splice(arrayOfToogles.indexOf(BUMP), 1);

    }
    const [square_sign, setToggleValueSquare_sign] = useState(userRules.indexOf(SQUARE) > -1);
    const onToggleHandlerSquare_sign = () => {
        setToggleValueSquare_sign(!square_sign);
        !square_sign ? arrayOfToogles.push(SQUARE) : arrayOfToogles.splice(arrayOfToogles.indexOf(SQUARE, 1));

    }
    const [red_white_sidewalk, setToggleValueRed_white_sidewalk] = useState(userRules.indexOf(RED_WHITE) > -1);
    const onToggleHandlerRed_white_sidewalk = () => {
        setToggleValueRed_white_sidewalk(!red_white_sidewalk);
        !red_white_sidewalk ? arrayOfToogles.push(RED_WHITE) : arrayOfToogles.splice(arrayOfToogles.indexOf(RED_WHITE), 1);
    }
    return (
        <div className="container">
            <h3>הגדרות</h3>
            <h6 >בחר את האלמנטים בהם תרצה להתאמן, בסיום שמור את השינויים.</h6>  <button className="btn btn-primary" onClick={() => clickSaveRules(arrayOfToogles, user.email)}>
                שמור שינויים
            </button>
            <table class="table" >
                <div></div>
                <tr><th>רמזור</th><td> <ToggleButton
                    value={traffic_light}
                    onToggle={onToggleHandlerTraffic_light}
                /></td></tr>
                <div></div>
                <tr><th>סימן עצור</th><td> <ToggleButton
                    value={stop_sign}
                    onToggle={onToggleHandlerStop_sign}
                /></td></tr>
                <div></div>
                <tr><th>סימן השתלבות</th><td> <ToggleButton
                    value={yield_sign}
                    onToggle={onToggleHandlerYield_sign}
                /></td></tr>
                <div></div>
                <tr><th>סימן אין כניסה</th><td> <ToggleButton
                    value={no_enter_sign}
                    onToggle={onToggleHandlerNo_enter_sign}
                /></td></tr>

                <div></div>
                <tr><th>סימן כביש חד סטרי</th><td><ToggleButton
                    value={one_way_sign}
                    onToggle={onToggleHandlerOne_way_sign}
                /></td></tr>
                <div></div>
                <tr><th>מעבר חצייה</th><td><ToggleButton
                    value={crosswalk_sign}
                    onToggle={onToggleHandlerCrosswalk_sign}
                /></td></tr>
                <div></div>
                <tr><th>סימן בולם מהירות</th><td> <ToggleButton
                    value={bump_sign}
                    onToggle={onToggleHandlerBump_sign}
                /></td></tr>
                <div></div>
                <tr><th>סימן כיכר לפניך</th><td><ToggleButton
                    value={square_sign}
                    onToggle={onToggleHandlerSquare_sign}
                /></td></tr>
                <div></div>
                <tr><th>סימן אדום לבן</th><td><ToggleButton
                    value={red_white_sidewalk}
                    onToggle={onToggleHandlerRed_white_sidewalk}
                /></td></tr>
            </table>

        </div>
    );
}

export { Settings };