'use strict';
const {
  Model
} = require('sequelize');
module.exports = (sequelize, DataTypes) => {
  class TypeRoom extends Model {
    /**
     * Helper method for defining associations.
     * This method is not a part of Sequelize lifecycle.
     * The `models/index` file will call this method automatically.
     */
    static associate(models) {
      TypeRoom.hasMany(models.HotelRoom,{
        foreignKey: "type_id",
        as: "typeroomData",
      })
    }
  }
  TypeRoom.init({
    
	  name :DataTypes.STRING
  }, {
    sequelize,
    modelName: 'TypeRoom',
  });
  return TypeRoom;
};