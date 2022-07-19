import React, { useState } from 'react';
import * as ToggleButton from 'react-toggle-button';
import { Router, Route, Link, browserHistory, IndexRoute } from 'react-router'
import { accountService } from '@/_services';
import { Settings } from '../Settings/Index';
import { HighScores } from '../HighScores/Index'
import { UnityComponent } from '@/unity';


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
    console.log('arrayOfToogles');
    console.log(arrayOfToogles);
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
        backgroundColor: "#F8F8F8",
        borderTop: "1px solid #E7E7E7",
        textAlign: "center",
        padding: "20px",
        position: "fixed",
        left: "0",
        bottom: "0",
        height: "60px",
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
        <div className="p-7 text-right" >
            {page === 'homePage' && (
                <div className="container">
                    {!isSettings && !isHighScoress &&  <div class="card"  style={{height: "18erm;"}}><div class="card-body">
                  <h3 className="card-title"> שלום {user.firstName}!</h3>
                      
                      <h4 className="card-text">ברוך הבא למשחק לימוד נהיגה</h4>
                     
                  <p class="card-text">המשחק נועד ללימוד ותרגול כללי נהיגה, בו תוכלו לעקוב אחר ההתקדמות ולמרגל נושאים לפי רצונכם</p>
              
                  <div className="form-row">
                        <div className="form-group col">
                            <button onClick={() => { currentPage('gamePage'); }} class="btn btn-primary">התחל משחק</button>
                            <button style={{margin : "20px"}} onClick={() => setSettings(!isSettings)} class="btn btn-primary">הגדרות</button>
                            <button onClick={() => setIsHighScores(!isHighScoress)} class="btn btn-primary">היסטוריית ניקוד</button>
                            <button style={{margin : "20px"}} class="btn btn-primary" onClick={accountService.logout} >התנתק </button>
                        </div>
                    </div>
               
                </div>
              </div>
                }
                    {isSettings && <Settings />}
                    {isHighScoress && <HighScores />}
                    {!isHighScoress && !isSettings && scoresV && scoresV.length > 0 && <div> <h5 class="card-header">טבלת הציונים המובילים</h5><table class="table table-striped" id="customers"> <thead> <tr>
      <th scope="col">#</th>
      <th scope="col">משתמש</th>
      <th scope="col">ניקוד</th>
    </tr>
  </thead> {scoresV.map((item, i) => <tr> <th scope="row">{i+1}</th><td>{item.email}</td><td>{item && item.score}</td></tr>)}</table></div>}

                </div>
            )}
            {page === 'gamePage' &&
                <div>
                    <UnityComponent />
                </div>
            }

                 {(isSettings || isHighScoress || page ==='gamePage') &&   
                  <footer style={style}>
                 <button className="btn btn-primary"  style={{margin : "20px"}} onClick={accountService.logout} >התנתק </button>
                 <button className="btn btn-primary" onClick={() => { setSettings(false); setIsHighScores(false); currentPage('homePage') }}>
               חזרה לדף הראשי
            </button>
           
</footer>}
        
        </div>
    );
}

export { Home };