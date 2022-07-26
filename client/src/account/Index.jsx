import React, { useEffect } from 'react';
import { Route, Switch } from 'react-router-dom';

import { accountService } from '@/_services';

import { Login } from './Login';
import { Register } from './Register';
import { VerifyEmail } from './VerifyEmail';

function Account({ history, match }) {
    const { path } = match;

    useEffect(() => {
        // redirect to home if already logged in
        if (accountService.userValue) {
            history.push('/');
        }
    }, []);

    return (
        <div className="container backg min-vw-100" style={{"background-image": "url('../src/static_src/imgstreet2.png')", "background-size": "cover"}}>
            <div className="row d-flex min-vh-100 justify-content-center align-items-center">
                <div className="col-sm-6 mt-8">
                    <div className="card shadow-lg">
                        <Switch>
                            <Route path={`${path}/login`} component={Login} />
                            <Route path={`${path}/register`} component={Register} />
                            <Route path={`${path}/verify-email`} component={VerifyEmail} />
                        </Switch>
                    </div>
                </div>
            </div>
        </div>
    );
}

export { Account };