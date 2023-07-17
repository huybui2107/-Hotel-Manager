const db = require('../models/index');
const { Sequelize ,Op } = require('sequelize');
const moment = require('moment');

module.exports = {
    createBooking: (data) => {
        return new Promise(async (resolve, reject) => {
            try {
                const hotel = await db.Hotel.findOne({
                    include: [
                        {
                            model: db.HotelRoom,
                            as: "hotelromData",
                            where: {
                                id: data.room_id,
                            }
                        }
                    ],
                    raw: true,
                    nest: true
                })
                if (hotel) {

                    const checkin = new Date(data.checkinDate);
                    const checkout = new Date(data.checkoutDate);
                    const timeDiff = Math.abs(checkout.getTime() - checkin.getTime());
                    const numberOfNights = Math.ceil(timeDiff / (1000 * 3600 * 24));
                    const booking = await db.Booking.create({
                        user_id: data.user_id,
                        room_id: data.room_id,
                        hotel_id: hotel.id,
                        fullName: data.fullName,
                        phonenumber: data.phonenumber,
                        cccd: data.cccd,
                        email: data.email,
                        birthday: data.birthday,
                        status: 0,
                        totalprice: numberOfNights * hotel.hotelromData.price,
                        checkin_date: data.checkinDate,
                        checkout_date: data.checkoutDate,
                        deadline_date: new Date(checkout.getTime() + (1000 * 60 * 60 * 24)),
                    })
                    resolve({
                        errCode: 0,
                        errMessage: "Create successful",
                        booking: booking
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
    getListBooking: (user_id, page) => {
        return new Promise(async (resolve, reject) => {
            try {
                let startPage = page || 1;
                const maxItemInPage = 10;
                const { count: totalItem, rows: bookings } = await db.Booking.findAndCountAll({
                    attributes: {
                        exclude: ["room_id", "hotel_id", "createdAt", "updatedAt", "cccd"],
                    },
                    include: [
                        {
                            model: db.Hotel,
                            as: "hotelbookingData",
                            attributes: {
                                exclude: ["createdAt", "updatedAt"],
                            },
                        },
                        {
                            model: db.HotelRoom,
                            as: "hotelrombookingData",
                            attributes: {
                                exclude: ["createdAt", "updatedAt"],
                            },
                        }

                    ],
                    where: {
                        user_id: user_id
                    },
                    offset: (startPage - 1) * maxItemInPage,
                    limit: maxItemInPage,
                    raw: true,
                    nest: true,
                })
                if (bookings.length > 0) {
                    resolve({
                        maxPageItem: maxItemInPage,
                        books: bookings.map(book => {
                            return {
                                id: book.id,
                                user_id: book.user_id,
                                fullName: book.fullName,
                                phonenumber: book.phonenumber,
                                email: book.email,
                                birthday: book.birthday,
                                status: book.status,
                                totalprice: book.totalprice,
                                checkin_date: book.checkin_date,
                                checkout_date: book.checkout_date,
                                deadline_date: book.deadline_date,
                                room: {
                                    ...book.hotelrombookingData
                                },
                                hotel: {
                                    ...book.hotelbookingData
                                }
                            }
                        }),
                        totalPage: Math.ceil(totalItem / maxItemInPage),
                        page: startPage
                    })
                }

            } catch (error) {
                reject(error);
            }
        })
    },
    getListBookingByIdHotel: (data) => {
        return new Promise(async (resolve, reject) => {

            try {
                let startPage = data.page || 1;
                const maxItemInPage = 10;
                const { count: totalItem, rows: bookings } = await db.Booking.findAndCountAll({
                    attributes: {
                        exclude: ["createdAt", "updatedAt", "cccd"],
                    },

                    where: {
                        hotel_id: data.hotel_id,
                        status: data.status,
                    },
                    order: [
                        [data.sort, data.direction], // Sort by lastName column in ascending order

                    ],
                    offset: (startPage - 1) * maxItemInPage,
                    limit: maxItemInPage,
                    raw: true,
                    nest: true,
                })
                if (bookings.length > 0) {
                    resolve({
                        maxPageItem: maxItemInPage,
                        books: bookings,
                        totalPage: Math.ceil(totalItem / maxItemInPage),
                        page: startPage
                    })
                } else {
                    resolve({
                        errCode: 1,
                        errMessage: "Booking not found",
                    })
                }
            } catch (error) {
                reject(error);
            }
        })
    },
    updateStatus: (data) => {
        return new Promise(async (resolve, reject) => {
            try {
                const booking = await db.Booking.findByPk(data.id);
                if (booking) {
                    booking.status = data.status;
                    await booking.save();
                    resolve({
                        errCode: 0,
                        errMessage: "Update Booking success",
                    })
                } else {
                    resolve({
                        errCode: 1,
                        errMessage: "Booking not found",
                    })
                }
            } catch (error) {
                reject(error);
            }
        })
    },
    countMaxItemByHotel: (id, status,month) => {
        return new Promise(async (resolve, reject) => {
            try {
                const count = await db.Booking.count({
                    where: {
                        hotel_id: id,
                        status: status,
                        [Op.and]: [
                            Sequelize.where(Sequelize.fn('MONTH', Sequelize.col('checkin_date')), month),
                           
                        ],
                    }
                });
                resolve({
                    count: count
                })
            } catch (error) {
                reject(error);
            }

        })
    },
    countItemByMonth: (id, month) => {
        return new Promise(async (resolve, reject) => {
            try {
                const count = await db.Booking.count({
                    where: {
                        hotel_id: id,
                        [Op.and]: [
                            Sequelize.where(Sequelize.fn('MONTH', Sequelize.col('checkin_date')), month),
                           
                        ],
                    }
                });
                resolve({
                    count: count
                })
            } catch (error) {
                reject(error)
            }
        })
    },
    getTotalPriceByDate: (id, day) => {
        return new Promise(async (resolve, reject) => {
            try {
                 const formattedDay = moment(day, 'YYYY-MM-DD').format('YYYY-MM-DD 00:00:00');
                console.log(day);
                const totalprice = await db.Booking.findAll({
                    attributes: [
                        [Sequelize.fn('SUM', Sequelize.col('totalprice')), 'totalSum']
                      ],
                      where: {
                        hotel_id: id,
                        checkin_date: { [Op.gte]: Sequelize.literal(`DATE('${day}')`), [Op.lt]: Sequelize.literal(`DATE_ADD('${day}', INTERVAL 1 DAY)`) },
                        status: 3
                      },
                      raw: true,
                      nest :true
                });
                console.log(totalprice);
                resolve(totalprice);
            } catch (error) {
                reject(error);
            }
        })
    },
    countday: (month, year) => {
        return new Promise((resolve, reject) => {
          try {
            let date;
            switch (parseInt(month)) {
              case 1:
              case 3:
              case 5:
              case 7:
              case 8:
              case 10:
              case 12:
                date = 31;
                break;
              case 4:
              case 6:
              case 9:
              case 11:
                date = 30;
                break;
              case 2:
                if ( parseInt(year) % 400 == 0 || (parseInt(year) % 4 == 0 && parseInt(year) % 100 != 0)) {
                    date = 29;
                } else {
                    date = 28;
                }
                break;
              default:
                date = 0;
                break;
            }
            resolve(date);
          } catch (error) {
            reject(error);
          }
        });
      }
}