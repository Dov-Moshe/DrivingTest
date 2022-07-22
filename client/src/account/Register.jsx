import React from 'react';
import { Link } from 'react-router-dom';
import { Formik, Field, Form, ErrorMessage } from 'formik';
import * as Yup from 'yup';

import { accountService, alertService } from '@/_services';

function Register({ history }) {
    const initialValues = {
        firstName: '',
        lastName: '',
        email: '',
        password: '',
        confirmPassword: '',
        acceptTerms: true,
        score: 0,
        rules: []
    }

    const validationSchema = Yup.object().shape({
        firstName: Yup.string()
            .required('First Name is required'),
        lastName: Yup.string()
            .required('Last Name is required'),
        email: Yup.string()
            .email('Email is invalid')
            .required('Email is required'),
        password: Yup.string()
            .min(6, 'Password must be at least 6 characters')
            .required('Password is required'),
        confirmPassword: Yup.string()
            .oneOf([Yup.ref('password'), null], 'Passwords must match')
            .required('Confirm Password is required'),
    });

    // ui-> service (client)-> fetch-wrapper -> fetch ->server side-> response -> 
    function onSubmit(fields, { setStatus, setSubmitting }) {
        setStatus();
        // call async function syncronously
        accountService.register(fields)
            // on finish
            .then(() => {
                alertService.success('Registration successful, please check your email for verification instructions', { keepAfterRouteChange: true });
                history.push('login');
            })
            // error handler
            .catch(error => {
                setSubmitting(false);
                alertService.error(error);
            });
    }
    return (
        <Formik initialValues={initialValues} validationSchema={validationSchema} onSubmit={onSubmit}>
            {({ errors, touched, isSubmitting }) => (
                <Form>
                    <h3 className="card-header text-right">הרשמה</h3>
                    <div className="card-body">
                        <div className="form-row">
                            <div className="form-group col-7 text-right">
                                <label>שם פרטי</label>
                                <Field name="firstName" type="text"
                                    className={'form-control' + (errors.firstName && touched.firstName ? ' is-invalid' : '')} />
                                <ErrorMessage name="firstName" component="div" className="invalid-feedback" />
                            </div>
                            <div className="form-group col-7 text-right">
                                <label>שם משפחה</label>
                                <Field name="lastName" type="text" className={'form-control' +
                                    (errors.lastName && touched.lastName ? ' is-invalid' : '')} />
                                <ErrorMessage name="lastName" component="div" className="invalid-feedback" />
                            </div>
                        </div>
                        <div className="form-group text-right">
                            <label>מייל</label>
                            <Field name="email" type="text" className={'form-control' + (errors.email && touched.email ?
                                ' is-invalid' : '')} />
                            <ErrorMessage name="email" component="div" className="invalid-feedback" />
                        </div>
                        <div className="form-row">
                            <div className="form-group col text-right">
                                <label>סיסמה</label>
                                <Field name="password" type="password" className={'form-control' +
                                    (errors.password && touched.password ? ' is-invalid' : '')} />
                                <ErrorMessage name="password" component="div" className="invalid-feedback" />
                            </div>
                            <div className="form-group col text-right">
                                <label>אישור סיסמה</label>
                                <Field name="confirmPassword" type="password" className={'form-control' +
                                    (errors.confirmPassword && touched.confirmPassword ? ' is-invalid' : '')} />
                                <ErrorMessage name="confirmPassword" component="div" className="invalid-feedback" />
                            </div>
                        </div>

                        <div className="form-group text-right">
                            <button type="submit" disabled={isSubmitting} className="btn btn-primary">
                                {isSubmitting && <span className="spinner-border spinner-border-sm mr-1"></span>}
                                הרשמה
                            </button>
                            <Link to="login" className="btn btn-link text-right">ביטול</Link>
                        </div>
                    </div>
                </Form>
            )}
        </Formik>
    )
}

export { Register }; 