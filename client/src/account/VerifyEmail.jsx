import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import queryString from 'query-string';

import { accountService, alertService } from '@/_services';

function VerifyEmail({ history }) {
    // during processing / finished.
    const EmailStatus = {
        Verifying: 'Verifying',
        Failed: 'Failed'
    }

    const [emailStatus, setEmailStatus] = useState(EmailStatus.Verifying);

    useEffect(() => {
        //get token from url
        const { token } = queryString.parse(location.search);

        history.replace(location.pathname);

        accountService.verifyEmail(token)
            .then(() => {
                alertService.success('הנתונים אומתו בהצלחה. כעת תוכל להתחבר', { keepAfterRouteChange: true });
                history.push('login');
            })
            .catch(() => {
                setEmailStatus(EmailStatus.Failed);
            });
    }, []);

    function getBody() {
        switch (emailStatus) {
            case EmailStatus.Verifying:
                return <div>Verifying...</div>;
            case EmailStatus.Failed:
                return <div>שגיאה באימות המידע, אנא בדוק את הפרטים</div>;
        }
    }

    return (
        <div>
            <h3 className="card-header">אימות משתמש</h3>
            <div className="card-body">{getBody()}</div>
        </div>
    )
}

export { VerifyEmail }; 