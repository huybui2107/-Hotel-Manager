const express = require('express');
const router = express.Router();
const hotelController = require('../controllers/hotel');



router.get('/search', hotelController.searchHotel);
router.put('/update',hotelController.updateHotel);


// ROOM
router.get('/room/searchAll',hotelController.searchRoomAll);
router.get('/room/search',hotelController.searchRoomById);
router.post('/room',hotelController.createRoom);
router.put('/room',hotelController.updateRoom);
router.delete('/room/:id',hotelController.deleteRoom);
module.exports = router;