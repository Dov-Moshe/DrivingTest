const express = require('express');
const router = express.Router();
const Joi = require('joi');
const accountService = require('./account.service');

function isValidate(req, next, schema) {
    const { error, value } = schema.validate(req.body, { allowUnknown: true, });
    if (error != undefined) {
        next(`${error.details[0].message}`);
    } else {
        req.body = value;
        // call the next hendler 
        next();
    }
}

function updateRules(req, res, next) {

    accountService.updateRules(req.body)
        .then(() => res.json({ message: 'update rules successful' }))
        .catch(next);
}

function updateRulesSchema(req, res, next) {
    const schema = Joi.object({
        email: Joi.string().email().required(),
        rules: Joi.array().items(Joi.string())
    });
    isValidate(req, next, schema);
}
function throwError(err, req, res, next) {
    return res.status(500).json({ message: err.message });
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
    isValidate(req, next, schema);
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
    isValidate(req, next, schema);
}

function authenticateSchema(req, res, next) {
    const schema = Joi.object({
        email: Joi.string().required(),
        password: Joi.string().required()
    });
    isValidate(req, next, schema);
}

function authenticate(req, res, next) {
    //  accountService.isAuthorized(req).then((isAuth) => {
    const { email, password } = req.body;
    const ipAddress = req.ip;
    accountService.authenticate({ email, password, ipAddress })
        .then(({ refreshToken, ...account }) => {
            setTokenCookie(res, refreshToken);
            res.json(account);
        })
        .catch(next);
    //} ).catch(next);

}

function registerSchema(req, res, next) {
    const schema = Joi.object({
        firstName: Joi.string().required(),
        lastName: Joi.string().required(),
        email: Joi.string().email().required(),
        password: Joi.string().min(6).required(),
        confirmPassword: Joi.string().valid(Joi.ref('password')).required(),
    });
    isValidate(req, next, schema);
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
    isValidate(req, next, schema);
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

router.post('/authenticate', authenticateSchema, authenticate, throwError);
router.post('/register', registerSchema, register, throwError);
router.post('/verify-email', verifyEmailSchema, verifyEmail, throwError);
router.post('/update-highscore', updateHighscoreSchema, updateHighscore, throwError);
router.post('/get-rules', getRulesSchema, getRules, throwError);
router.post('/update-rules', updateRulesSchema, updateRules, throwError);
router.get('/get-all-scores', getHighscores, throwError);
router.post('/get-user-by-email', getAccountByEmail, throwError);

module.exports = router;
