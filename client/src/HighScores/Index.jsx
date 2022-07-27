import React, { useState, useEffect } from 'react';
import * as ToggleButton from 'react-toggle-button';
import { Router, Route, Link, browserHistory, IndexRoute } from 'react-router'
import { accountService, alertService } from '@/_services';
const rootElement = document.getElementById("root");

function HighScores() {

  const scoresV = accountService.userValue && accountService.userValue.scoreDescription;
  return (
    <div className="container w-auto mb-4" >
      <h2 className='text-center font-sans-regular title-1 text-secondary pb-3'>טבלת היסטוריית הניקוד שלך:</h2>
      <div className='card shadow-lg p-3'>
        {scoresV && scoresV.length > 0 ? <div className='row'><div><div>
          <table className="table table-hover table-fixed">
            <thead>
              <tr>
                <th scope="col" className='col-xs-2'>#</th>
                <th scope="col" className='col-xs-8'>תאריך</th>
                <th scope="col" className='col-xs-2'>ציון</th>
                <th scope="col" className='col-xs-8'>תיאור ציון</th>
              </tr>
            </thead > 
            <tbody>{scoresV.map((item, i) => 
              <tr> 
                <th scope='row' className='col-xs-2'>{i + 1}</th>
                <td className='col-xs-8'>{item.testDate}</td>
                <td className='col-xs-2'>{item.score}</td>
                <td className='col-xs-8'>{item && item.scoreDescription}</td>
              </tr>)}
            </tbody>
          </table >
        </div></div></div> : <h2 className="card-header" style={{ margin: "50px" }}>אין ציונים להצגה</h2>
        }
      </div>


    </div >);
}

export { HighScores };