const config = require('config.json');
const jwt = require('jsonwebtoken');
const bcrypt = require('bcryptjs');
const crypto = require("crypto");
const sendEmail = require('_helpers/send-email');
const db = require('_helpers/db');
const Role = require('_helpers/role');
//const Scores = require('_helpers/scores');

module.exports = {
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
   // getAllcores,
    //scoreDetails

};

async function updateRules( params ) {
    const account = await db.Account.findOne({ email: params.email });
    const newRules = params.rules;
    console.log(newRules);
    account.rules = newRules;
    await account.save();
}

async function updateHighscore( params ) {
    const account = await db.Account.findOne({ email: params.email });
    const newScore = params.score;
    const prevScore = account.score;
    if(newScore>prevScore){
        account.score = newScore;
    }
    await account.save();
}

async function getRules( params ) {
    const account = await db.Account.findOne({ email: params.email });
    const rules = account.rules;
    return rules;
}

async function authenticate({ email, password, ipAddress }) {
    // db record
    const account = await db.Account.findOne({ email });
console.log(account);
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
    // validate
    if (await db.Account.findOne({ email: params.email })) {
        // send already registered error in email to prevent account enumeration
        return await sendAlreadyRegisteredEmail(params.email, origin);
    }

    // create account object
    const account = new db.Account(params);

    // first registered account is an admin
    const isAdmin = params.title == 'Teacher';// (await db.Account.countDocuments({})) === 0;
    account.role = isAdmin ? Role.Admin : Role.User;
    account.verificationToken = randomTokenString();

    // hash password
    account.passwordHash = hash(params.password);
    account.score = 0;
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
        expires: new Date(Date.now() + 24*60*60*1000)
    };
    await account.save();

    // send email
    await sendPasswordResetEmail(account, origin);
}

async function getAllcores() {
    const accounts = await db.Account.find();
    return accounts.map(x => scoreDetails(x));
}

async function getAll() {
    const accounts = await db.Account.find();
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
   // if (!db.isValidId(id)) throw 'Account not found';
    const account = await db.Account.findOne({email:email1});
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
        expires: new Date(Date.now() + 7*24*60*60*1000),
        createdByIp: ipAddress
    });
}

function randomTokenString() {
    return crypto.randomBytes(40).toString('hex');
}

function basicDetails(account) {
    const { id, title, firstName, lastName, email, role, created, updated, isVerified, score, rules} = account;
    return { id, title, firstName, lastName, email, role, created, updated, isVerified, score, rules};
}

function scoreDetails(account) {
    const scoreRet = account.score;
    return scoreRet;
}

async function sendVerificationEmail(account, origin) {
    let message;
    if (origin) {
        const verifyUrl = `${origin}/account/verify-email?token=${account.verificationToken}`;
        message = `<p>Please click the below link to verify your email address:</p>
                   <p><a href="${verifyUrl}">${verifyUrl}</a></p>`;
    } else {
        message = `<p>Please use the below token to verify your email address with the <code>/account/verify-email</code> api route:</p>
                   <p><code>${account.verificationToken}</code></p>`;
    }

    await sendEmail({
        to: account.email,
        subject: 'Sign-up Verification API - Verify Email',
        html: `<h4>Verify Email</h4>
               <p>Thanks for registering!</p>
               ${message}`
    });
}

async function sendAlreadyRegisteredEmail(email, origin) {
    let message;
    if (origin) {
        message = `<p>If you don't know your password please visit the <a href="${origin}/account/forgot-password">forgot password</a> page.</p>`;
    } else {
        message = `<p>If you don't know your password you can reset it via the <code>/account/forgot-password</code> api route.</p>`;
    }

    await sendEmail({
        to: email,
        subject: 'Sign-up Verification API - Email Already Registered',
        html: `<h4>Email Already Registered</h4>
               <p>Your email <strong>${email}</strong> is already registered.</p>
               ${message}`
    });
}

async function sendPasswordResetEmail(account, origin) {
    let message;
    if (origin) {
        const resetUrl = `${origin}/account/reset-password?token=${account.resetToken.token}`;
        message = `<p>Please click the below link to reset your password, the link will be valid for 1 day:</p>
                   <p><a href="${resetUrl}">${resetUrl}</a></p>`;
    } else {
        message = `<p>Please use the below token to reset your password with the <code>/account/reset-password</code> api route:</p>
                   <p><code>${account.resetToken.token}</code></p>`;
    }

    await sendEmail({
        to: account.email,
        subject: 'Sign-up Verification API - Reset Password',
        html: `<h4>Reset Password Email</h4>
               ${message}`
    });
}