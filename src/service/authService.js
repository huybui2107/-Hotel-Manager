const bcrypt = require('bcryptjs');
const db = require('../models/index');

module.exports = {
    login :(data) => {
        return new Promise(async(resolve, reject) => {
            try {
                console.log(data)
                const user = await db.User.findOne({
                    where :{
                        email : data.email
                    }
                })
                if(!user){
                    resolve({
                        errCode: 1,
                        errMessage: "User not found !",
                    })
                }else {
                    const checkPass = await bcrypt.compareSync(data.password,user.password);
                    if(!checkPass){
                        resolve({
                            errCode: 1,
                            errMessage: "Password incorrect !",
                        })
                    }
                    else {
                        const hotel = await db.Hotel.findOne({
                            where :{
                                user_id : user.id
                            }
                        })
                        
                        resolve({
                            errCode: 0,
                            errMessage: "Login successful",
                            user :user,
                            hotel : hotel ? hotel : ""
                        })
                    }
                }
            } catch (error) {
                reject(error);
            }
        })
    }
}