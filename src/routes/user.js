const express = require('express');
const router = express.Router();
const authController = require('../controllers/user');



router.post('/register',authController.register);
router.put('/updateProfile',authController.updateProfile);
router.put('/changePassword',authController.changePass);
router.post('/registerManage',authController.registerManager);
module.exports = router ;