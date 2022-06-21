import React, { useState, useEffect } from 'react';
import * as ToggleButton from 'react-toggle-button';
import { Router, Route, Link, browserHistory, IndexRoute } from 'react-router'
import { accountService, alertService } from '@/_services';

const rootElement = document.getElementById("root");

function HighScores() {
    const scoresV = accountService.userValue && accountService.userValue.scoreDescription;
    return (<div className="container" >
          {scoresV && scoresV.length>0 ? <div><h2 class="card-header" style={{margin : "50px"}}>טבלת היסטוריית הניקוד שלך:</h2> <table  class="table table-striped"><thead> <tr>
      <th scope="col">#</th>
      <th scope="col">ציון</th>
      <th scope="col">תיאור ציון</th>
    </tr>
  </thead>  {scoresV.map((item, i )=> <tr> <th scope="row">{i+1}</th><td>{item.score}</td><td>{item && item.scoreDescription}</td></tr>)}</table></div> :<h2 class="card-header" style={{margin : "50px"}}>אין ציונים להצגה</h2> }
               
    </div>);
}

export { HighScores };