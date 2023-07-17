'use strict';
const {
  Model
} = require('sequelize');
module.exports = (sequelize, DataTypes) => {
  class User extends Model {
    /**
     * Helper method for defining associations.
     * This method is not a part of Sequelize lifecycle.
     * The `models/index` file will call this method automatically.
     */
    static associate(models) {
      User.belongsTo(models.Role, { foreignKey: "roleId", targetKey : "id", as: "roleData" });
      User.hasMany(models.Hotel,{ foreignKey: "user_id", as: "userData", })
      User.hasMany(models.Booking ,{ foreignKey: "user_id", as: "userbookingData", })
    }
  }
  User.init({
     username : DataTypes.STRING,
	  password : DataTypes.STRING,
	  firstName :DataTypes.STRING,
	  lastName :DataTypes.STRING,
	  gender :DataTypes.BOOLEAN,
	  phoneNumber : DataTypes.STRING,
	  email : DataTypes.STRING,
	  roleId  : DataTypes.INTEGER,
	  birthday : DataTypes.DATE,
	  image :DataTypes.STRING
  }, {
    sequelize,
    modelName: 'User',
  });
  return User;
};