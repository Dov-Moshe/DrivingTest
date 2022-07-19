require('rootpath')();
const express = require('express');
const app = express();
const bodyParser = require('body-parser');
const cookieParser = require('cookie-parser');
const cors = require('cors');
const errorHandler = require('_middleware/error-handler');

app.use(bodyParser.urlencoded({ extended: false }));
app.use(bodyParser.json());
app.use(cookieParser());
app.use(cors({ origin: (origin, callback) => callback(null, true), credentials: true }));
app.use('/accounts', require('./accounts/accounts.controller'));
app.use(errorHandler);

// start server non port 4000
const port = 4000;
app.listen(port, () => {
    console.log('Server listening on port ' + port);
});
