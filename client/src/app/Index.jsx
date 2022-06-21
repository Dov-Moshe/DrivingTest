import React, { useState, useEffect } from 'react';
import { Route, Switch, Redirect, useLocation } from 'react-router-dom';

import { Role } from '@/_helpers';
import { accountService } from '@/_services';
import { Nav, PrivateRoute, Alert } from '@/_components';
import { Home } from '@/home';
import { Account } from '@/account';

function App() {
    const { pathname } = useLocation();  
    const [user, setUser] = useState({});
    const [scores, setScores] = useState([]);
    const [scoreDesc, setScoreDesc] = useState([]);

    
    useEffect(() => {
        const subscription = accountService.user.subscribe(x => setUser(x));
        return subscription.unsubscribe;
        
    }, []);
    useEffect(() => {
        const subscription2 = accountService.scores.subscribe(y => setScores(y));
        return subscription2.unsubscribe;
    }, []);
   /* useEffect(() => {
        const subscription3 = accountService.scoreDescription.subscribe(z => setScoreDesc(z));
        return subscription3.unsubscribe;
    }, []);*/

    return (
        <div className={'app-container' + (user && ' bg-light')}>
            <Nav />
            <Alert />
            <Switch>
                <Redirect from="/:url*(/+)" to={pathname.slice(0, -1)} />
                <PrivateRoute exact path="/" component={Home} />
                <Route path="/account" component={Account} />
            </Switch>
          
        </div>
    );
}

export { App }; 