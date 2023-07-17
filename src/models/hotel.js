'use strict';
const {
  Model
} = require('sequelize');
module.exports = (sequelize, DataTypes) => {
  class Hotel extends Model {
    /**
     * Helper method for defining associations.
     * This method is not a part of Sequelize lifecycle.
     * The `models/index` file will call this method automatically.
     */
    static associate(models) {
      Hotel.belongsTo(models.User,{
        foreignKey: "user_id",
        targetKey: "id",
        as: "userData",
      })
      Hotel.belongsTo(models.Province,{
        foreignKey: "province_id",
        targetKey: "id",
        as: "provinceData",
      })
      Hotel.hasMany(models.HotelRoom,{
        foreignKey: "hotel_id",
        as: "hotelromData",
      })
      Hotel.hasMany(models.Booking ,{
        foreignKey: "hotel_id",
        as: "hotelbookingData",
      })
    }
  }
  Hotel.init({
    user_id : DataTypes.INTEGER, 
    province_id  : DataTypes.INTEGER,
    hotel_name : DataTypes.STRING,
    hotel_desc : DataTypes.TEXT,
    hotel_address : DataTypes.STRING,
    hotel_phone : DataTypes.STRING,
    hotel_email : DataTypes.STRING,
    room_quantity : DataTypes.INTEGER,
    image : DataTypes.STRING,
  }, {
    sequelize,
    modelName: 'Hotel',
  });
  return Hotel;
};