'use strict';
const {
  Model
} = require('sequelize');
module.exports = (sequelize, DataTypes) => {
  class Booking extends Model {
    /**
     * Helper method for defining associations.
     * This method is not a part of Sequelize lifecycle.
     * The `models/index` file will call this method automatically.
     */
    static associate(models) {
      Booking.belongsTo(models.User ,{
        foreignKey: "user_id",
        targetKey: "id",
        as: "userbookingData",
      })
      Booking.belongsTo(models.Hotel ,{
        foreignKey: "hotel_id",
        targetKey: "id",
        as: "hotelbookingData",
      })
      Booking.belongsTo(models.HotelRoom ,{
        foreignKey: "room_id",
        targetKey: "id",
        as: "hotelrombookingData",
      })
    }
  }
  Booking.init({
    user_id: DataTypes.INTEGER,
    room_id: DataTypes.INTEGER,
    hotel_id: DataTypes.INTEGER,
    fullName: DataTypes.STRING,
    phonenumber: DataTypes.STRING,
    cccd: DataTypes.STRING,
    email: DataTypes.STRING,
    birthday: DataTypes.DATE,
    status: DataTypes.INTEGER,
    totalprice: DataTypes.INTEGER,
    checkin_date: DataTypes.DATE,
    checkout_date: DataTypes.DATE,
    deadline_date: DataTypes.DATE,
  }, {
    sequelize,
    modelName: 'Booking',
  });
  return Booking;
};