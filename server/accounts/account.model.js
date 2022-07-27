const mongoose = require('mongoose');
const Schema = mongoose.Schema;

const schema = new Schema({
    email: { type: String, unique: true, required: true },
    passwordHash: { type: String, required: true },
    firstName: { type: String, required: true },
    lastName: { type: String, required: true },
    score: { type: Number, unique: false },
    scoreDescription: { type: Array, unique: false },
    rules: { type: Array, unique: false },
    verificationToken: String,
    verified: Date,
    created: { type: Date, default: Date.now },
    updated: Date,
});

schema.virtual('isVerified').get(function () {
    return !!(this.verified);
});

schema.set('toJSON', {
    virtuals: true,
    versionKey: false,
    transform: function (doc, ret) {
        // remove these props when object is serialized
        delete ret._id;
        delete ret.passwordHash;
    }
});

module.exports = mongoose.model('Account', schema);