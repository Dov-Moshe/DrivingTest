import React, { useState } from 'react';
import * as ToggleButton from 'react-toggle-button';
import { Router, Route, Link, browserHistory, IndexRoute } from 'react-router'
import { accountService } from '@/_services';

const rootElement = document.getElementById("root");

function clickstartGame() {
    window.open("http://localhost:8111");
}

function clicksettings() {
    // add redirection
}

function Home() {

    const user = accountService.userValue;
    const [traffic_light, setToggleValueTraffic_light] = useState(true);
    const onToggleHandlerTraffic_light = () => {
        setToggleValueTraffic_light(!traffic_light);
    }
    const [stop_sign, setToggleValueStop_sign] = useState(true);
    const onToggleHandlerStop_sign = () => {
        setToggleValueStop_sign(!stop_sign);
    }
    const [yield_sign, setToggleValueYield_sign] = useState(true);
    const onToggleHandlerYield_sign = () => {
        setToggleValueYield_sign(!yield_sign);
    }
    const [no_enter_sign, setToggleValueNo_enter_sign] = useState(true);
    const onToggleHandlerNo_enter_sign = () => {
        setToggleValueNo_enter_sign(!no_enter_sign);
    }
    const [one_way_sign, setToggleValueOne_way_sign] = useState(true);
    const onToggleHandlerOne_way_sign = () => {
        setToggleValueOne_way_sign(!one_way_sign);
    }
    const [crosswalk_sign, setToggleValueCrosswalk_sign] = useState(true);
    const onToggleHandlerCrosswalk_sign = () => {
        setToggleValueCrosswalk_sign(!crosswalk_sign);
    }
    const [bump_sign, setToggleValueBump_sign] = useState(true);
    const onToggleHandlerBump_sign = () => {
        setToggleValueBump_sign(!bump_sign);
    }
    const [square_sign, setToggleValueSquare_sign] = useState(true);
    const onToggleHandlerSquare_sign = () => {
        setToggleValueSquare_sign(!square_sign);
    }
    const [red_white_sidewalk, setToggleValueRed_white_sidewalk] = useState(true);
    const onToggleHandlerRed_white_sidewalk = () => {
        setToggleValueRed_white_sidewalk(!red_white_sidewalk);
    }
    return (
        <div className="p-4">
            <div className="container">
                <h1>Hi {user.firstName}!</h1>
                <p>Welcome To driving test!!</p>
                <button className="btn btn-primary" onClick={clickstartGame}>
                    Start Game
                </button>

                <div></div>
                <button className="btn btn-primary" onClick={clicksettings}>
                    Settings1
                </button>
                <button className="btn btn-primary" onClick={clicksettings}>
                    Settings
                </button>
                <ToggleButton
                    value={traffic_light}
                    onToggle={onToggleHandlerTraffic_light}
                />
                <div></div>
                <ToggleButton
                    value={stop_sign}
                    onToggle={onToggleHandlerStop_sign}
                />
                <div></div>
                <ToggleButton
                    value={yield_sign}
                    onToggle={onToggleHandlerYield_sign}
                />
                <div></div>
                <ToggleButton
                    value={no_enter_sign}
                    onToggle={onToggleHandlerNo_enter_sign}
                />

                <div></div>
                <ToggleButton
                    value={one_way_sign}
                    onToggle={onToggleHandlerOne_way_sign}
                />
                <div></div>
                <ToggleButton
                    value={crosswalk_sign}
                    onToggle={onToggleHandlerCrosswalk_sign}
                />
                <div></div>
                <ToggleButton
                    value={bump_sign}
                    onToggle={onToggleHandlerBump_sign}
                />
                <div></div>
                <ToggleButton
                    value={square_sign}
                    onToggle={onToggleHandlerSquare_sign}
                />
                <div></div>
                <ToggleButton
                    value={red_white_sidewalk}
                    onToggle={onToggleHandlerRed_white_sidewalk}
                />
            </div>
        </div>
    );
}

export { Home };