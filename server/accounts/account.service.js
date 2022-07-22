const config = require('config.json');
const jwt = require('jsonwebtoken');
const bcrypt = require('bcryptjs');
const crypto = require("crypto");
const sendEmail = require('_helpers/send-email');
const db = require('_helpers/db');

module.exports = {
    isAuthorized,
    authenticate,
    register,
    verifyEmail,
    getAll,
    getById,
    create,
    updateHighscore,
    getRules,
    updateRules,
    getAccountByEmail,
    getFiveHighScores,
    getAllcores,
};

async function isAuthorized(req) {
    const account = await db.Account.findById(req.user.id);
    const refreshTokens = await db.RefreshToken.find({ account: account.id });
    return !!refreshTokens.find(x => x.token === token);
}
async function updateRules(params) {
    const account = await db.Account.findOne({ email: params.email });
    const newRules = params.rules;
    account.rules = newRules;
    await account.save();
}

async function updateHighscore(params) {
    const account = await db.Account.findOne({ email: params.email });
    const newScore = params.score;
    const scoreDescription = params.scoreDescription;
    const prevScore = account.score;
    account.scoreDescription = [...account.scoreDescription, { 'score': params.score, 'scoreDescription': params.scoreDescription }];// && account.scoreHistory.push({newScore, scoreDescription});
    if (newScore > prevScore) {
        account.score = newScore;
    }
    await account.save();
}

// todo check if the function return json format
async function getRules(params) {
    const account = await db.Account.findOne({ email: params.email });
    const rules = account.rules;
    return rules;
}

async function authenticate({ email, password, ipAddress }) {
    // db record
    const account = await db.Account.findOne({ email });
    if (!account || !account.isVerified || !bcrypt.compareSync(password, account.passwordHash)) {
        throw 'Email or password is incorrect';
    }

    // authentication successful so generate jwt and refresh tokens
    const jwtToken = generateJwtToken(account);
    const refreshToken = generateRefreshToken(account, ipAddress);

    // save refresh token
    await refreshToken.save();

    // return basic details and tokens
    return {
        ...basicDetails(account),
        jwtToken,
        refreshToken: refreshToken.token
    };
}

async function register(params, origin) {
    // create account object
    const account = new db.Account(params);
    account.verificationToken = randomTokenString();

    // hash password
    account.passwordHash = hash(params.password);
    account.score = 0;
    //account.scoreDescription=;
    account.rules = [];
    // save account
    await account.save();

    // send email
    await sendVerificationEmail(account, origin);
}

async function verifyEmail({ token }) {
    const account = await db.Account.findOne({ verificationToken: token });

    if (!account) throw 'Verification failed';

    account.verified = Date.now();
    account.verificationToken = undefined;
    await account.save();
}

async function forgotPassword({ email }, origin) {
    const account = await db.Account.findOne({ email });

    // always return ok response to prevent email enumeration
    if (!account) return;

    // create reset token that expires after 24 hours
    account.resetToken = {
        token: randomTokenString(),
        expires: new Date(Date.now() + 24 * 60 * 60 * 1000)
    };
    await account.save();

    // send email
    await sendPasswordResetEmail(account, origin);
}


//here my new function
async function getFiveHighScores() {
    const scores = db.Account.find().sort({ score: -1 }).limit(5);
    //const email = getEmailByScore(score);
    return { scores, item };
}

async function getAll() {
    const accounts = await db.Account.find()
    return accounts.map(x => basicDetails(x));
}
async function getAllcores() {
    const accounts = await db.Account.find({ "score": { $exists: true, $ne: 0 } }).sort({ score: -1 }).limit(5);
    return accounts.map(x => basicDetails(x));
}
async function getById(id) {
    const account = await getAccount(id);
    return basicDetails(account);
}

async function getByEmail(email) {
    const account = await getAccount(email);
    return basicDetails(account);
}
//new
async function getEmailByScore(score) {
    const account = await getAccount(score);
    return basicDetails(account).email;
}

async function create(params) {
    // validate
    if (await db.Account.findOne({ email: params.email })) {
        throw 'Email "' + params.email + '" is already registered';
    }

    const account = new db.Account(params);
    account.verified = Date.now();

    // hash password
    account.passwordHash = hash(params.password);
    //score
    account.score = params.score;
    // save account
    await account.save();

    return basicDetails(account);
}

// helper functions
async function getAccount(id) {
    if (!db.isValidId(id)) throw 'Account not found';
    const account = await db.Account.findById(id);
    if (!account) throw 'Account not found';
    return account;
}
async function getAccountByEmail(email1) {
    const account = await db.Account.findOne({ email: email1 });
    if (!account) throw 'Account not found';
    return account;
}
function hash(password) {
    return bcrypt.hashSync(password, 10);
}

function generateJwtToken(account) {
    // create a jwt token containing the account id that expires in 15 minutes
    return jwt.sign({ sub: account.id, id: account.id }, config.secret, { expiresIn: '15m' });
}

function generateRefreshToken(account, ipAddress) {
    // create a refresh token that expires in 7 days
    return new db.RefreshToken({
        account: account.id,
        token: randomTokenString(),
        expires: new Date(Date.now() + 7 * 24 * 60 * 60 * 1000),
        createdByIp: ipAddress
    });
}

function randomTokenString() {
    return crypto.randomBytes(40).toString('hex');
}

function basicDetails(account) {
    const { id, firstName, lastName, email, created, updated, isVerified, score, rules, scoreDescription } = account;
    return { id, firstName, lastName, email, created, updated, isVerified, score, rules, scoreDescription };
}

function scoreDetails(account) {
    const scoreRet = account.score;
    return scoreRet;
}

async function sendVerificationEmail(account, origin) {
    let message;
    if (origin) {
        const verifyUrl = `${origin}/account/verify-email?token=${account.verificationToken}`;
        message = `<p><a href="${verifyUrl}">${verifyUrl}</a></p>`;
    } else {
        message = `<p><code>${account.verificationToken}</code></p>`;
    }

    await sendEmail({
        to: account.email,
        subject: 'Email Verification',
        html: `<h4>Verify Email</h4>
               ${message}`
    });
}
