import React, { useState, useEffect } from 'react';
import { NavLink, Route } from 'react-router-dom';

import { accountService } from '@/_services';

function Nav() {
    const [user, setUser] = useState({});

    useEffect(() => {
        const subscription = accountService.user.subscribe(x => setUser(x));
        return subscription.unsubscribe;
    }, []);

    // only show nav when logged in
    if (!user) return null;

    return (
        <div>
            <nav className="navbar navbar-expand navbar-dark bg-dark">
                <div className="navbar-nav">
                    <NavLink exact to="/" className="nav-item nav-link">Home</NavLink>
                    <a onClick={accountService.logout} className="nav-item nav-link">Logout</a>
                </div>
            </nav>
       
        </div>
    );
}


export { Nav }; 