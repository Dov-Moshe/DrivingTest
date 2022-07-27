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
const RED_WHITE = 'red_white_sidewalk';
const SQUARE = 'square_sign';
const SPEED_LIMIT = 'speed_limit_sign';
const NO_TURN_BACK_SIGN = 'no_turn_back_sign';
const BLINKER = 'blinker';
const CONTINUOUS_LINE = 'continuous_line';
const rootElement = document.getElementById("root");

function clickstartGame() {
    window.open("http://localhost:8111");
}


function clickSaveRules(arrayOfToogles, email) {
    accountService.updateRules(email, arrayOfToogles)
        // on finish
        .then(() => {
            alertService.success('השינויים נשמרו בהצלחה', { keepAfterRouteChange: true });
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
    const [square_sign, setToggleValueSquare_sign] = useState(userRules.indexOf(SQUARE) > -1);
    const onToggleHandlerSquare_sign = () => {
        setToggleValueSquare_sign(!square_sign);
        !square_sign ? arrayOfToogles.push(SQUARE) : arrayOfToogles.splice(arrayOfToogles.indexOf(SQUARE, 1));
    }

    const [speed_limit_sign, setToggleValuespeed_limit_sign] = useState(userRules.indexOf(SPEED_LIMIT) > -1);
    const onToggleHandlerspeed_limit_sign = () => {
        setToggleValuespeed_limit_sign(!speed_limit_sign);
        !speed_limit_sign ? arrayOfToogles.push(SPEED_LIMIT) : arrayOfToogles.splice(arrayOfToogles.indexOf(SPEED_LIMIT, 1));
    }
    const [no_turn_back_sign, setToggleValueno_turn_back_sign] = useState(userRules.indexOf(NO_TURN_BACK_SIGN) > -1);
    const onToggleHandlerno_turn_back_sign = () => {
        setToggleValueno_turn_back_sign(!no_turn_back_sign);
        !no_turn_back_sign ? arrayOfToogles.push(NO_TURN_BACK_SIGN) : arrayOfToogles.splice(arrayOfToogles.indexOf(NO_TURN_BACK_SIGN, 1));
    }
    const [blinker, setToggleValueBlinker] = useState(userRules.indexOf(BLINKER) > -1);
    const onToggleHandlerBlinker = () => {
        setToggleValueBlinker(!blinker);
        !blinker ? arrayOfToogles.push(BLINKER) : arrayOfToogles.splice(arrayOfToogles.indexOf(BLINKER, 1));
    }
    const [continuous_line, setToggleValueContinuous_line] = useState(userRules.indexOf(CONTINUOUS_LINE) > -1);
    const onToggleHandlerContinuous_line = () => {
        setToggleValueContinuous_line(!continuous_line);
        !continuous_line ? arrayOfToogles.push(CONTINUOUS_LINE) : arrayOfToogles.splice(arrayOfToogles.indexOf(CONTINUOUS_LINE), 1);
    }

    return (
        <div className="container">
            <h2 className='text-center font-sans-regular title-2 text-secondary pb-3'>הגדרות</h2>
            <h2 className='text-center font-sans-regular title-3 text-secondary pb-3'>בחר את האלמנטים בהם תרצה להתאמן, בסיום שמור את השינויים.</h2>  
            <div>
                <table class="table" style={{borderCollapse: "collapse"}}>
                    <div></div>
                    <tr><th>רמזור</th><td> <ToggleButton
                        value={traffic_light}
                        onToggle={onToggleHandlerTraffic_light}
                    />
                    </td></tr>
                    <div></div>
                    <tr><th>סימן עצור</th><td> <ToggleButton
                        value={stop_sign}
                        onToggle={onToggleHandlerStop_sign}
                    />
                    </td></tr>
                    <div></div>
                    <tr><th>סימן השתלבות</th><td> <ToggleButton
                        value={yield_sign}
                        onToggle={onToggleHandlerYield_sign}
                    />
                    </td></tr>
                    <div></div>
                    <tr><th>סימן אין כניסה</th><td> <ToggleButton
                        value={no_enter_sign}
                        onToggle={onToggleHandlerNo_enter_sign}
                    />
                    </td></tr>
                    <div></div>
                    <tr><th>זכות קדימה בכיכר</th><td><ToggleButton
                        value={square_sign}
                        onToggle={onToggleHandlerSquare_sign}
                    />
                    </td></tr>
                    <div></div>
                    <tr><th>מהירות נסיעה</th><td><ToggleButton
                        value={speed_limit_sign}
                        onToggle={onToggleHandlerspeed_limit_sign}
                    />
                    </td></tr>
                    <div></div>
                    <tr><th>פניית פרסה</th><td><ToggleButton
                        value={no_turn_back_sign}
                        onToggle={onToggleHandlerno_turn_back_sign}
                    />
                    </td></tr>
                    <div></div>
                    <tr><th>איתות</th><td > <ToggleButton
                        value={blinker}
                        onToggle={onToggleHandlerBlinker}
                    />
                    </td></tr>
                    <div></div>
                    <tr><th>קו הפרדה רצוף</th><td ><ToggleButton
                        value={continuous_line}
                        onToggle={onToggleHandlerContinuous_line}
                    />
                    </td></tr>
                </table>
                
                <div className='text-center'>
                    <button className="btn btn-primary w-25" onClick={() => clickSaveRules(arrayOfToogles, user.email)}>
                    שמור שינויים
                    </button>
                </div>
                

            </div>

        </div>
    );
}

export { Settings };