import React, { useState } from 'react';
import * as ToggleButton from 'react-toggle-button';
import { Router, Route, Link, browserHistory, IndexRoute } from 'react-router'
import { accountService } from '@/_services';
import { Settings } from '../Settings/Index';
import { HighScores } from '../HighScores/Index'
import { UnityComponent, QuitUnity } from '@/unity';


const TRAFFIC_LIGHT = 'traffic_light';
const STOP_SIGN = 'stop_sign';
const YIELD_SIGN = 'yield_sign';
const NO_ENTER = 'no_enter_sign';
const ONE_WAY = 'one_way_sign';
const CROSS_WALK = 'crosswalk_sign';
const BUMP = 'bump_sign';
const SQUARE = 'square_sign';
const RED_WHITE = 'red_white_sidewalk';
//one_way_sign
const rootElement = document.getElementById("root");

function clicksettings() {
    // add redirection
}

function clickSaveRules(arrayOfToogles, email) {
    accountService.updateRules(email, arrayOfToogles)
        // on finish
        .then(() => {
            alertService.success('השינויים נשמרו בהצלחה!');
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
    const style = {
        borderTop: "1px solid #E7E7E7",
        textAlign: "center",
        position: "fixed",
        paddingBottom: "30px",
        left: "0",
        bottom: "0",
        height: "100%",
        width: "100%"
    };
    const [page, currentPage] = useState('homePage');
    const user = accountService.userValue;
    const scoresV = accountService.scoreValue;
    const userRules = user.rules;
    arrayOfToogles = user.rules;
    const [isSettings, setSettings] = useState(false);
    const [isHighScoress, setIsHighScores] = useState(false);

    return (
        <div className='container min-vw-100' style={{"background-image": "url('../src/static_src/imgstreet2.png')", "background-size": "cover"}}>
            <div className='row'>
                {page === 'homePage' && (
                    <div className="container min-vh-100 justify-content-center pt-5 col-sm-7" >
                        {!isSettings && !isHighScoress && <div style={{ height: "18erm;" }}><div class="card-body">

                            <div className='text-center'>
                                <p className="text-body font-sans-regular text-dark" style={{ fontSize: "24px" }}> שלום {user.firstName}!</p>
                                <p className="text-body font-sans-bold title-1 text-dark">ברוך הבא למשחק לימוד נהיגה</p>
                                <p class="text-body font-sans-regular text-dark" style={{ fontSize: "18px" }}>המשחק נועד ללימוד ותרגול כללי נהיגה, בו תוכלו לעקוב אחר ההתקדמות ולמרגל נושאים לפי רצונכם</p>
                            </div>
                            
                            <div className="form-row text-center">
                                <div className="form-group col">
                                    <button onClick={() => { currentPage('gamePage'); }} class="btn btn-success">התחל משחק</button>
                                    <button style={{ margin: "20px" }} onClick={() => setSettings(!isSettings)} class="btn btn-primary">הגדרות</button>
                                    <button onClick={() => setIsHighScores(!isHighScoress)} class="btn btn-primary">היסטוריית ניקוד</button>
                                    <button style={{ margin: "20px" }} class="btn btn-secondary" onClick={accountService.logout} >התנתק </button>
                                </div>
                            </div>

                        </div>
                        </div>
                        }
                        {isSettings && <Settings />}
                        {isHighScoress && <HighScores />}
                        {!isHighScoress && !isSettings && scoresV && scoresV.length > 0 && <div className='card shadow-lg p-3 h-50 d'> <h5 class="card-header">טבלת הציונים המובילים</h5><table class="table table-striped"> <thead> <tr>
                            <th scope="col">#</th>
                            <th scope="col">משתמש</th>
                            <th scope="col">ניקוד</th>
                        </tr>
                        </thead> {scoresV.map((item, i) => <tr> <th scope="row">{i + 1}</th><td>{item.email}</td><td>{item && item.score}</td></tr>)}</table></div>}

                    </div>
                )}
                {page === 'gamePage' &&
                    <div className='container min-vh-100 justify-content-center align-items-center'>
                        <UnityComponent />
                    </div>
                }
            </div>

            <div className='container text-center footer navbar-fixed-bottom '>
                    {(isSettings || isHighScoress || page === 'gamePage') &&
                        <div className='text-center'>
                            <button className="btn btn-primary" style={{ margin: "20px" }} onClick={accountService.logout} >התנתק </button>
                            <button className="btn btn-primary" onClick={() => { setSettings(false); setIsHighScores(false);
                            if(page === 'gamePage'){
                                    QuitUnity();
                                    setTimeout(function() {
                                        currentPage('homePage')
                                    }, (2000));
                                } else{
                                    currentPage('homePage')
                                }}}>
                                חזרה לדף הראשי
                            </button>
                        </div>
                    }
            </div>
            

        </div>
    );
}

export { Home };