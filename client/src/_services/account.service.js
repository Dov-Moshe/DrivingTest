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
    register,
    verifyEmail,
    getAll,
    getById,
    create,
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

function register(params) {
    return apiCalls.post(`${baseUrl}/register`, params);
}

function verifyEmail(token) {
    return apiCalls.post(`${baseUrl}/verify-email`, { token });
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
        // publish user to subscribers and start timer to refresh token
        scoreSubject.next(scores);
        return scores;
    });
}
