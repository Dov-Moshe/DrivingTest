import React from 'react';
import { Link } from 'react-router-dom';
import { Formik, Field, Form, ErrorMessage } from 'formik';
import * as Yup from 'yup';

import { accountService, alertService } from '@/_services';

function Login({ history, location }) {
    const initialValues = {
        email: '',
        password: ''
    };

    const validationSchema = Yup.object().shape({
        email: Yup.string()
            .email('Email is invalid')
            .required('Email is required'),
        password: Yup.string().required('Password is required')
    });
    function onSubmit({ email, password }, { setSubmitting }) {
        alertService.clear();
        // jsx-> service(client)-> wrapper-client -> fatch-> server side (port 4000)-> response 

        accountService.login(email, password)
            .then(() => {
                accountService.getScores();
                const { from } = location.state || { from: { pathname: "/" } };
                history.push(from);
            })
            .catch(error => {
                setSubmitting(false);
                alertService.error(error);
            });
    }

    return (
        <Formik initialValues={initialValues} validationSchema={validationSchema} onSubmit={onSubmit}>
            {({ errors, touched, isSubmitting }) => (
                <Form>
                    <h3 className="card-header text-center">כניסה</h3>
                    <div className="card-body">
                        <div className="form-group text-right m-3">
                            <label>מייל</label>
                            <Field name="email" type="text" className={'form-control' + (errors.email && touched.email ? ' is-invalid' : '')} />
                            <ErrorMessage name="email" component="div" className="invalid-feedback" />
                        </div>
                        <div className="form-group text-right m-3">
                            <label>סיסמה</label>
                            <Field name="password" type="password" className={'form-control' + (errors.password && touched.password ? ' is-invalid' : '')} />
                            <ErrorMessage name="password" component="div" className="invalid-feedback" />
                        </div>
                        <div className="form-row text-right m-3">
                            <div className="form-group col">
                                <button type="submit" disabled={isSubmitting} className="btn btn-primary">
                                    {isSubmitting && <span className="spinner-border spinner-border-sm mr-1"></span>}
                                    כניסה
                                </button>
                                <Link to="register" className="btn btn-link">הרשמה</Link>
                            </div>
                        </div>
                    </div>
                </Form>
            )}
        </Formik>
    )
}

export { Login };