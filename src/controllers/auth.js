const authService = require('../service/authService');


module.exports = {

    login :async( req,res, next ) =>{
         const data = req.body;
         try {
             const result = await authService.login(data) ;
            // const response = {
            //     hotel : result.hotel,
            //     user : result.user,
            // }
            // console.log(response);
            // res.status(200).json(response)
            res.status(200).json(result)
            // console.log(authService.login(data));
         } catch (error) {
            res.status(500).json({
                        errCode: -1,
                        errMessage: error,
            })
         }
    }
}