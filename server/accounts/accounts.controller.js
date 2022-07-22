const express = require('express');
const router = express.Router();
const Joi = require('joi');
const validateRequest = require('_middleware/validate-request');
const authorize = require('_middleware/authorize')
const accountService = require('./account.service');

// routes
router.post('/authenticate', authenticateSchema, authenticate);
router.post('/register', registerSchema, register);
router.post('/verify-email', verifyEmailSchema, verifyEmail);
router.post('/update-highscore', updateHighscoreSchema, updateHighscore);
router.post('/get-rules', getRulesSchema, getRules);
router.post('/update-rules', updateRulesSchema, updateRules);
router.get('/get-all-scores', getHighscoresSchema, getHighscores);
router.post('/get-user-by-email', getAccountByEmail);



module.exports = router;

function updateRules(req, res, next) {
    console.log(req.body);
    accountService.updateRules(req.body)
        .then(() => res.json({ message: 'update rules successful' }))
        .catch(next);
}

function updateRulesSchema(req, res, next) {
    const schema = Joi.object({
        email: Joi.string().email().required(),
        rules: Joi.array().items(Joi.string())
    });
    validateRequest(req, next, schema);
}

function updateHighscore(req, res, next) {
    accountService.updateHighscore(req.body)
        .then(() => res.json({ message: 'Highscore updated successfully' }))
        .catch(next);
}

function updateHighscoreSchema(req, res, next) {
    const schema = Joi.object({
        email: Joi.string().email().required(),
        score: Joi.number().required(),
        scoreDescription: Joi.string().required()
    });
    validateRequest(req, next, schema);
}

function getRules(req, res, next) {
    const { email } = req.body;
    accountService.getAccountByEmail(email)
        .then(account => res.json({ rules: account.rules, email: account.email }))
        .catch(next);
}
function getAccountByEmail(req, res, next) {
    const { email } = req.body;
    accountService.getAccountByEmail(email)
        .then(account => res.json(account))
        .catch(next);
}

function getRulesSchema(req, res, next) {
    const schema = Joi.object({
        email: Joi.string().email().required(),
    });
    validateRequest(req, next, schema);
}


function authenticateSchema(req, res, next) {
    const schema = Joi.object({
        email: Joi.string().required(),
        password: Joi.string().required()
    });
    validateRequest(req, next, schema);
}

function authenticate(req, res, next) {
    const { email, password } = req.body;
    const ipAddress = req.ip;
    accountService.authenticate({ email, password, ipAddress })
        .then(({ refreshToken, ...account }) => {
            setTokenCookie(res, refreshToken);
            res.json(account);
        })
        .catch(next);
}

function registerSchema(req, res, next) {
    const schema = Joi.object({
        firstName: Joi.string().required(),
        lastName: Joi.string().required(),
        email: Joi.string().email().required(),
        password: Joi.string().min(6).required(),
        confirmPassword: Joi.string().valid(Joi.ref('password')).required(),
        acceptTerms: Joi.boolean().valid(true).required()
    });
    validateRequest(req, next, schema);
}

function register(req, res, next) {
    accountService.register(req.body, req.get('origin'))
        .then(() => res.json({ message: 'Registration successful, please check your email for verification instructions' }))
        .catch(next);
}

function verifyEmailSchema(req, res, next) {
    const schema = Joi.object({
        token: Joi.string().required()
    });
    validateRequest(req, next, schema);
}

function verifyEmail(req, res, next) {
    accountService.verifyEmail(req.body)
        .then(() => res.json({ message: 'Verification successful, you can now login' }))
        .catch(next);
}

function getAll(req, res, next) {
    accountService.getAll()
        .then(accounts => res.json(accounts))
        .catch(next);
}

function getHighscores(req, res, next) {
    accountService.getAllcores()
        .then(accounts => { res.json(accounts.map(a => { return { 'email': a.email, 'score': a.score } })) })
        .catch(next);
}

function getHighscoresSchema(req, res, next) {
    const schema = Joi.object({
    });
    validateRequest(req, next, schema);
}

function getById(req, res, next) {
    accountService.getById(req.params.id)
        .then(account => account ? res.json(account) : res.sendStatus(404))
        .catch(next);
}

function setTokenCookie(res, token) {
    const cookieOptions = {
        httpOnly: true,
        expires: new Date(Date.now() + 7 * 24 * 60 * 60 * 1000)
    };
    res.cookie('refreshToken', token, cookieOptions);
}