const express = require('express');
const router = express.Router();
const bookingController = require('../controllers/booking');

router.post('/',bookingController.createBooking);
router.get('/user', bookingController.getListBooking);
router.get('/hotel', bookingController.getListBookingByIdHotel);
router.put('/hotel', bookingController.updateStatus);
router.get('/hotel/revenue', bookingController.getRevenueByMonth);
module.exports = router;