const express = require('express');
const dotenv = require('dotenv');
const app = express();
const cors = require("cors");
const path = require('path');
const connectDB = require('./config/connectDB');
const cookie_parser = require('cookie-parser');
dotenv.config({ path: path.join(__dirname, '.env') });
const authRouter = require('./routes/auth');
const userRouter = require('./routes/user');
const hotelRouter = require('./routes/hotel');
const bookingRouter = require('./routes/booking');

app.use(express.json());
app.use(cookie_parser());
app.use(cors());
app.options("*", cors());


app.use('/auth',authRouter);
app.use('/user',userRouter);
app.use('/hotel',hotelRouter);
app.use('/booking',bookingRouter);

app.use((err, req, res, next) => {
    console.log(err);
    const status = err.statusCode;
    const message = err.message;
    const data = err.data;
    res.status(status).json({
        message: message,
        data: data
    })
})

connectDB();
app.listen(process.env.PORT, () => {
    console.log("Connect to server successfully...");
})