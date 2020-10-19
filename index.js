const express = require('express');
const app = express();
const mongoose = require('mongoose');
const dotenv = require('dotenv');

dotenv.config();

//import router
const authRoute = require('./routes/auth');
const postRoute = require('./routes/post');

//db connect
mongoose.connect(
  'mongodb+srv://admin:test1234@cluster0.l6nkj.mongodb.net/test?retryWrites=true',
  {
    useCreateIndex: true,
    useNewUrlParser: true,
    useUnifiedTopology: true,
  },

  () => {
    console.log('db connected  \u2601 ');
  }
);

// Middleware

app.use(express.json());

//route middleware
//!everything in auth route will have prefix '/api/user'

app.use('/api/user', authRoute);
app.use('/api/posts', postRoute);

app.listen(3000, console.log('Server Runing'));
