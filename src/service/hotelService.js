
const bcrypt = require('bcryptjs');
const db = require('../models/index');
const { Op,Sequelize } = require("sequelize");

module.exports = {
    updateHotel: (data) => {
        return new Promise(async (resolve, reject) => {
            try {
                const hotel = await db.Hotel.findByPk(data.id);
                if (!hotel) {
                    resolve({
                        errCode: 1,
                        errMessage: "Hotel not found",
                    })
                }
                else {
                    hotel.hotel_name = data.hotelName,
                        hotel.hotel_email = data.hotelEmail,
                        hotel.hotel_phone = data.hotelPhone,
                        hotel.hotel_address = data.hotelAddress,
                        hotel.hotel_desc = data.hotelDescription,
                        hotel.image = data.image
                    const hotelUpdate = await hotel.save();
                    resolve({
                        errCode: 0,
                        errMessage: "Update profile successful",
                        hotel: hotelUpdate
                    })
                }

            } catch (error) {
                reject(error);
            }

        })
    },
    searchHotel: (data) => {
        return new Promise(async(resolve, reject) => {
            try {
                let startPage = data.page || 1;
                const maxItemInPage = 10;
                const { count: totalItem, rows: hotels } =await db.Hotel.findAndCountAll({
                    
                    include: [
                        {
                            model: db.HotelRoom,
                            as: "hotelromData",
                            where: {
                                type_id: data.type_room_id,
                                bed_quantity: data.bed_quantity
                            }
                        },
                        {
                            model: db.Booking,
                            as: "hotelbookingData",
                            where: {
                                [Op.or]: [
                                    {
                                        checkin_date: {
                                            [Op.gt]: data.checkin_date,
                                            [Op.gt]: data.checkout_date
                                        }
                                    },
                                    {
                                        checkout_date: {
                                            [Op.lt]: data.checkout_date,
                                            [Op.lt]: data.checkin_date
                                        }
                                    }
                                ]
                            }
                        }

                    ],
                    where: {
                        province_id: data.province_id
                    },
                    offset: (startPage - 1) * maxItemInPage,
                    limit: maxItemInPage,
                    raw: true,
                    nest: true,
                    distinct: true,
                    
                })

                if (hotels && totalItem > 0) {
                    resolve({
                        maxPageItem: maxItemInPage,
                        hotels: hotels,
                        totalPage: Math.ceil(totalItem / maxItemInPage),
                        page: startPage
                    })
                } else {
                    resolve({
                        errCode: 1,
                        errMessage: "Hotel not found",
                    })
                }
            } catch (error) {
                reject(error);
            }
        })
    },
    // /////  ROOM
    createRoom: (data) => {
        return new Promise(async (resolve, reject) => {
            try {

                const room = await db.HotelRoom.create({
                    hotel_id: data.hotel_id,
                    type_id: data.type_id,
                    name: data.roomName,
                    bed_quantity: data.bed_quantity,
                    price: data.price,
                    description: data.description,
                    image: data.image,
                })
                const hotel = await db.Hotel.findByPk(data.hotel_id);
                hotel.room_quantity = hotel.room_quantity + 1;
                const newHotel = await hotel.save();
                resolve({
                    errCode: 0,
                    errMessage: "Create room successfully",
                    hotel: newHotel,
                    room: room
                })
            } catch (error) {
                reject(error);
            }
        })
    },
    searchRoomAll: (hotel_id, page) => {
        return new Promise(async (resolve, reject) => {
            try {
                let startPage = page || 1;
                const maxItemInPage = 10;
                const { count: totalItem, rows: hotelRooms } = await db.HotelRoom.findAndCountAll({
                    distinct: true,
                    where: {
                        hotel_id: hotel_id
                    },
                    offset: (startPage - 1) * maxItemInPage,
                    limit: maxItemInPage,
                    raw: true,
                    nest: true,
                })

                if (hotelRooms && totalItem > 0) {
                    resolve({
                        maxPageItem: maxItemInPage,
                        rooms: hotelRooms,
                        totalPage: Math.ceil(totalItem / maxItemInPage),
                        page: startPage
                    })
                } else {
                    resolve({
                        errCode: 1,
                        errMessage: "HotelRoom not found",
                    })
                }


            } catch (error) {
                reject(error);
            }
        })
    },
    searchRoomById : (data) =>{
            return new Promise(async(resolve, reject) =>{
                 try {
                    let startPage = data.page || 1;
                    const maxItemInPage = 10;
                    const { count: totalItem, rows: hotelRooms } =await db.HotelRoom.findAndCountAll({
                        
                        include: [
                            
                            {
                                model: db.Booking,
                                as: "hotelrombookingData",
                                where: {
                                    [Op.or]: [
                                        {
                                            checkin_date: {
                                                [Op.gt]: data.checkin_date,
                                                [Op.gt]: data.checkout_date
                                            }
                                        },
                                        {
                                            checkout_date: {
                                                [Op.lt]: data.checkout_date,
                                                [Op.lt]: data.checkin_date
                                            }
                                        }
                                    ]
                                }
                            }
    
                        ],
                        where: {
                            hotel_id: data.hotel_id,
                            type_id :data.type_room_id,
                            bed_quantity : data.bed_quantity,
                        },
                        offset: (startPage - 1) * maxItemInPage,
                        limit: maxItemInPage,
                        raw: true,
                        nest: true,
                        distinct: true,
                        
                    })
    
                    if (hotelRooms && totalItem > 0) {
                        resolve({
                            maxPageItem: maxItemInPage,
                            hotelRooms: hotelRooms,
                            totalPage: Math.ceil(totalItem / maxItemInPage),
                            page: startPage
                        })
                    } else {
                        resolve({
                            errCode: 1,
                            errMessage: "HotelRoom not found",
                        })
                    }
                 } catch (error) {
                    reject(error);
                 }
            })
    },
    updateRoom: (data) => {
        return new Promise(async (resolve, reject) => {
            try {
                const room = await db.HotelRoom.findByPk(data.id);
                if (!room) {
                    resolve({
                        errCode: 1,
                        errMessage: "Room not found",
                    })
                } else {
                    room.name = data.name,
                        room.description = data.description,
                        room.type_id = data.type_id,
                        room.bed_quantity = data.bed_quantity,
                        room.price = data.price
                    const roomUpdate = await room.save();
                    resolve({
                        errCode: 0,
                        errMessage: "Update room successful",
                        room: roomUpdate
                    })
                }
            } catch (error) {
                reject(error);
            }
        })
    },
    deleteRoom: (id) => {
        return new Promise(async (resolve, reject) => {
            try {
                const room = await db.HotelRoom.findByPk(id);
                const hotel = await db.Hotel.findByPk(room.hotel_id);
                if (room && hotel) {
                    await room.destroy();
                    hotel.room_quantity = hotel.room_quantity - 1;
                    await hotel.save();
                    resolve({
                        errCode: 0,
                        errMessage: "Delete successful",
                    })
                }
                else {
                    resolve({
                        errCode: 1,
                        errMessage: "Delete failed",
                    })
                }
            } catch (error) {
                reject(error);
            }
        })
    }
}