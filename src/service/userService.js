const bcrypt = require('bcryptjs');
const db = require('../models/index');

module.exports = {
    register: (data) => {
        return new Promise(async (resolve, reject) => {
            try {
                console.log(data)
                const user = await db.User.findOne({
                    where: {
                        email: data.email
                    }
                })
                if (user) {
                    resolve({
                        errCode: 1,
                        errMessage: "tai khoan da ton tai",
                    })
                }
                else {
                    const hashpassword = await bcrypt.hash(data.password, 12);
                    await db.User.create({
                        username: data.username ? data.username : "",
                        password: hashpassword,
                        firstName: data.firstName,
                        lastName: data.lastName,
                        gender: data.gender ? data.gender : true,
                        phoneNumber: data.phoneNumber ? data.phoneNumber : "",
                        email: data.email,
                        birthday: data.birthday,
                        roleId: 1,
                        image: data.image ? data.image : "",
                    })
                    resolve({
                        errCode: 0,
                        errMessage: "Register successfully",
                    });
                }
            } catch (error) {
                reject(error);
            }
        })
    },
    updateProfile: (data) => {
        return new Promise(async (resolve, reject) => {
            try {
                const user = await db.User.findByPk(data.id);
                if (user) {
                    user.username = data.username,
                        user.firstName = data.firstName,
                        user.lastName = data.lastName,
                        user.birthday = data.birthday,
                        user.image = data.image,
                        user.gender = data.gender,
                        user.phoneNumber = data.phoneNumber
                    const userupdate = await user.save();
                    resolve({
                        errCode: 0,
                        errMessage: "Update profile successful",
                        user: userupdate
                    })
                }
                else {
                    resolve({
                        errCode: 1,
                        errMessage: "Update profile failed",
                    })
                }
            } catch (error) {
                reject(error);
            }
        })
    },
    changePass: (data) => {
        return new Promise(async (resolve, reject) => {
            try {
                const user = await db.User.findByPk(data.id);
                if (user) {

                    const checkPass = await bcrypt.compareSync(data.old_password, user.password);
                    if (checkPass) {

                        user.password = await bcrypt.hash(data.new_password, 12)
                        await user.save();
                        resolve({
                            errCode: 0,
                            errMessage: "Change password successfully",
                        })
                    }
                    else {
                        resolve({
                            errCode: 1,
                            errMessage: "Change password failed",
                        })
                    }
                }
                else {
                    resolve({
                        errCode: 1,
                        errMessage: "User not found",
                    })
                }
            } catch (error) {
                reject(error);
            }
        })
    },
    registerManager: (data) => {
        return new Promise(async (resolve, reject) => {
            try {
                const user = await db.User.findByPk(data.user_id);
                if (!user) {
                    resolve({
                        errCode: 1,
                        errMessage: "User not found",
                    })
                } else {
                    console.log(data.image + "  Check");
                 const hotel =await db.Hotel.create({
                        user_id: data.user_id,
                        province_id: data.province_id,
                        hotel_name: data.hotelName,
                        hotel_desc: data.hotelDescription,
                        hotel_address: data.hotelAddress,
                        image: data.image,
                        hotel_email: data.hotelEmail,
                        hotel_phone: data.hotelPhone,
                        room_quantity : 0  })
                user.roleId = 2 ;
                await user.save();
            
                    resolve({
                        errCode: 0,
                        errMessage: "Register successfully",
                        hotel :hotel,
                    });
                
                }
            } catch (error) {
                reject(error);
            }
        })
    }
}