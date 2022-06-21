import { BehaviorSubject } from 'rxjs';

import config from 'config';
import { fetchWrapper, history } from '@/_helpers';

const userSubject = new BehaviorSubject(null);
const scoreSubject = new BehaviorSubject(null);
const baseUrl = `${config.apiUrl}/accounts`;
//const baseUrl = `localhost:4000/accounts`;

export const accountService = {
    login,
    logout,
    refreshToken,
    register,
    verifyEmail,
    forgotPassword,
    validateResetToken,
    resetPassword,
    getAll,
    getById,
    create,
    update,
    delete: _delete,
    updateRules,
    getScores,
    getDetails,
    updateScores,
    user: userSubject.asObservable(),
    get userValue() { return userSubject.value },
    scores: scoreSubject.asObservable(),
    get scoreValue() { return scoreSubject.value },
   // scoreDesc: scoreDescSubject.asObservable(),
   // get scoreDescValue() { return scoreDescSubject.value }
};

// todo check if the function return json format
function getDetails(email) {
    return fetchWrapper.post(`${baseUrl}/get-rules`, { email })
        .then(rules => {
            return rules;
        });
}

function login(email, password) {
    return fetchWrapper.post(`${baseUrl}/authenticate`, { email, password })
        .then(user => {
            // publish user to subscribers and start timer to refresh token
            userSubject.next(user);
            startRefreshTokenTimer();
            return user;
        });
}

function updateRules(email,rules) {
    return fetchWrapper.post(`${baseUrl}/update-rules`,{email , rules});
}

function updateScores(email, score, summary) {
    return fetchWrapper.post(`${baseUrl}/update-highscore`,{email, score ,summary });
}

function logout() {
    // revoke token, stop refresh timer, publish null to user subscribers and redirect to login page
    fetchWrapper.post(`${baseUrl}/revoke-token`, {});
    stopRefreshTokenTimer();
    userSubject.next(null);
    history.push('/account/login');
}

function refreshToken() {
    return fetchWrapper.post(`${baseUrl}/refresh-token`, {})
        .then(user => {
            // publish user to subscribers and start timer to refresh token
            userSubject.next(user);
            startRefreshTokenTimer();
            return user;
        });
}

function register(params) {
    return fetchWrapper.post(`${baseUrl}/register`,params);
}

function verifyEmail(token) {
    return fetchWrapper.post(`${baseUrl}/verify-email`, { token });
}

function forgotPassword(email) {
    return fetchWrapper.post(`${baseUrl}/forgot-password`, { email });
}

function validateResetToken(token) {
    return fetchWrapper.post(`${baseUrl}/validate-reset-token`, { token });
}

function resetPassword({ token, password, confirmPassword }) {
    return fetchWrapper.post(`${baseUrl}/reset-password`, { token, password, confirmPassword });
}

function getAll() {
    return fetchWrapper.get(baseUrl);
}

function getById(id) {
    return fetchWrapper.get(`${baseUrl}/${id}`);
}

function create(params) {
    return fetchWrapper.post(baseUrl, params);
}

 function getScores() {
    return fetchWrapper.get(`${baseUrl}/get-all-scores`).then(scores => {
        console.log(scores);
       
        // publish user to subscribers and start timer to refresh token
        scoreSubject.next(scores);
        return scores;
    });
}
function getScoresDesc() {
    return fetchWrapper.get(`${baseUrl}/get-all-scores`).then(scores => {
        console.log(scores);
       
        // publish user to subscribers and start timer to refresh token
        scoreSubject.next(scores);
        return scores;
    });
}

function update(id, params) {
    return fetchWrapper.put(`${baseUrl}/${id}`, params)
        .then(user => {
            // update stored user if the logged in user updated their own record
            if (user.id === userSubject.value.id) {
                // publish updated user to subscribers
                user = { ...userSubject.value, ...user };
                userSubject.next(user);
            }
            return user;
        });
}

// prefixed with underscore because 'delete' is a reserved word in javascript
function _delete(id) {
    return fetchWrapper.delete(`${baseUrl}/${id}`)
        .then(x => {
            // auto logout if the logged in user deleted their own record
            if (id === userSubject.value.id) {
                logout();
            }
            return x;
        });
}

// helper functions

let refreshTokenTimeout;

function startRefreshTokenTimer() {
    // parse json object from base64 encoded jwt token
    const jwtToken = JSON.parse(atob(userSubject.value.jwtToken.split('.')[1]));

    // set a timeout to refresh the token a minute before it expires
    const expires = new Date(jwtToken.exp * 1000);
    const timeout = expires.getTime() - Date.now() - (60 * 1000);
    refreshTokenTimeout = setTimeout(refreshToken, timeout);
}

function stopRefreshTokenTimer() {
    clearTimeout(refreshTokenTimeout);
}
