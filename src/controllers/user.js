const userService = require('../service/userService');


module.exports = {
    register :async(req,res,next) =>{
        const data = req.body;
        const mess = await userService.register(data);
        return res.status(200).json(mess);
    },
    updateProfile : async(req, res, next) =>{
         const data = req.body;
         const mess = await userService.updateProfile(data);
         return res.status(200).json(mess.user);
    },
    changePass :async (req, res, next) =>{
        const mess = await userService.changePass(req.body);
        console.log(mess);
        return res.status(200).json(mess);
    },
    registerManager :async (req, res, next) =>{
        const mess = await userService.registerManager(req.body);
        console.log(mess);
        return res.status(200).json(mess.hotel);
        // console.log(req.body);
    }
}