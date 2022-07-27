import React from 'react';
import { Router } from 'react-router-dom';
import { render } from 'react-dom';
import { accountService } from './_services';
import { App } from './app';
import { createBrowserHistory } from 'history';

const history = createBrowserHistory();
import './styles.less';


(function startApp() {
    render(
        <Router history={history}>
            <App />
        </Router>,
        document.getElementById('app')
    );
})();