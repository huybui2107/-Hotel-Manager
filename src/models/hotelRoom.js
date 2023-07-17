'use strict';
const {
  Model
} = require('sequelize');
module.exports = (sequelize, DataTypes) => {
  class HotelRoom extends Model {
    /**
     * Helper method for defining associations.
     * This method is not a part of Sequelize lifecycle.
     * The `models/index` file will call this method automatically.
     */
    static associate(models) {
      HotelRoom.belongsTo(models.Hotel,{
        foreignKey: "hotel_id",
        targetKey: "id",
        as: "hotelromData",
      })
      HotelRoom.belongsTo(models.TypeRoom,{
        foreignKey: "type_id",
        targetKey: "id",
        as: "typeroomData",
      })
      HotelRoom.hasMany(models.Booking ,{
        foreignKey: "room_id",
        as: "hotelrombookingData",
      })
    }
  }
  HotelRoom.init({
    hotel_id : DataTypes.INTEGER, 
    type_id  : DataTypes.INTEGER,
    name : DataTypes.STRING,
    bed_quantity : DataTypes.INTEGER,
    price : DataTypes.INTEGER,
    description : DataTypes.TEXT,
    image : DataTypes.STRING,
  }, {
    sequelize,
    modelName: 'HotelRoom',
  });
  return HotelRoom;
};