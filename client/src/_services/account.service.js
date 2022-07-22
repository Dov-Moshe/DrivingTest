import { BehaviorSubject } from 'rxjs';
import config from 'config';
import { apiCalls } from '../apiCalls';
import { Redirect } from 'react-router-dom';

const userSubject = new BehaviorSubject(null);
const scoreSubject = new BehaviorSubject(null);
const baseUrl = `${config.apiUrl}/accounts`;

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
    updateRules,
    getScores,
    getDetails,
    updateScores,
    user: userSubject.asObservable(),
    get userValue() { return userSubject.value },
    scores: scoreSubject.asObservable(),
    get scoreValue() { return scoreSubject.value },
};

function getDetails(email) {
    return apiCalls.post(`${baseUrl}/get-rules`, { email })
        .then(rules => {
            return rules;
        });
}

function login(email, password) {
    return apiCalls.post(`${baseUrl}/authenticate`, { email, password })
        .then(user => {
            userSubject.next(user);
            startRefreshTokenTimer();
            return user;
        });
}

function updateRules(email, rules) {
    return apiCalls.post(`${baseUrl}/update-rules`, { email, rules });
}

function updateScores(email, score, scoreDescription) {
    return apiCalls.post(`${baseUrl}/update-highscore`, { email, score, scoreDescription }).then(() => getScores()).then(() => getAccountByEmail());
}

function logout() {
    userSubject.next(null);
    location.replace('/account/login');
}

function refreshToken() {
    return apiCalls.post(`${baseUrl}/refresh-token`, {})
        .then(user => {
            userSubject.next(user);
            startRefreshTokenTimer();
            return user;
        });
}

function register(params) {
    return apiCalls.post(`${baseUrl}/register`, params);
}

function verifyEmail(token) {
    return apiCalls.post(`${baseUrl}/verify-email`, { token });
}

function forgotPassword(email) {
    return apiCalls.post(`${baseUrl}/forgot-password`, { email });
}

function validateResetToken(token) {
    return apiCalls.post(`${baseUrl}/validate-reset-token`, { token });
}

function resetPassword({ token, password, confirmPassword }) {
    return apiCalls.post(`${baseUrl}/reset-password`, { token, password, confirmPassword });
}

function getAll() {
    return apiCalls.get(baseUrl);
}

function getById(id) {
    return apiCalls.get(`${baseUrl}/${id}`);
}

function create(params) {
    return apiCalls.post(baseUrl, params);
}

function getAccountByEmail() {
    return apiCalls.post(`${baseUrl}/get-user-by-email`, { email: `${accountService.userValue.email}` }).then(user => {

        userSubject.next(user);
        return user;
    });
}

function getScores() {
    return apiCalls.get(`${baseUrl}/get-all-scores`).then(scores => {
        console.log(scores);

        // publish user to subscribers and start timer to refresh token
        scoreSubject.next(scores);
        return scores;
    });
}
function getScoresDesc() {
    return apiCalls.get(`${baseUrl}/get-all-scores`).then(scores => {
        console.log(scores);

        // publish user to subscribers and start timer to refresh token
        scoreSubject.next(scores);
        return scores;
    });
}

function update(id, params) {
    return apiCalls.put(`${baseUrl}/${id}`, params)
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
