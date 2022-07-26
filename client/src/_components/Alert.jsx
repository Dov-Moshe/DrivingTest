import React, { useState, useEffect } from 'react';
import PropTypes from 'prop-types';

import { alertService, AlertType } from '@/_services';

const propTypes = {
    id: PropTypes.string,
};

const defaultProps = {
    id: 'default-alert',
};

function Alert({ id }) {
    const [alerts, setAlerts] = useState([]);

    useEffect(() => {
        // subsribe a new alert.
        const subscription = alertService.onAlert(id)
            .subscribe(alert => {
                if (alert.message) {
                    setAlerts(alerts => ([...alerts, alert]));
                    setTimeout(() => setAlerts(alerts => alerts.filter(x => x !== alert)), 3000);
                }
            });
    }, []);



    function cssClasses(alert) {
        if (!alert) return;

        const classes = ['alert', 'alert-dismissable'];

        const alertTypeClass = {
            [AlertType.Success]: 'alert alert-success',
            [AlertType.Error]: 'alert alert-danger',
            [AlertType.Info]: 'alert alert-info',
            [AlertType.Warning]: 'alert alert-warning'
        }

        classes.push(alertTypeClass[alert.type]);

        return classes.join(' ');
    }

    if (!alerts.length) return null;

    return (
        <div className="container">
            <div className="m-3">
                {alerts.map((alert, index) =>
                    <div key={index} className={cssClasses(alert)}>
                        <span dangerouslySetInnerHTML={{ __html: alert.message }}></span>
                    </div>
                )}
            </div>
        </div>
    );
}

Alert.propTypes = propTypes;
Alert.defaultProps = defaultProps;
export { Alert };