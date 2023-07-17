'use strict';
/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up(queryInterface, Sequelize) {
    await queryInterface.createTable('Hotels', {
      id: {
        allowNull: false,
        autoIncrement: true,
        primaryKey: true,
        type: Sequelize.INTEGER
      },
      user_id :{ type :Sequelize.INTEGER },
      province_id :{ type :Sequelize.INTEGER} ,
      hotel_name: { type: Sequelize.STRING },
      hotel_desc: { type: Sequelize.TEXT },
      hotel_address: { type: Sequelize.STRING },
      hotel_phone :{ type: Sequelize.STRING},
      hotel_email: { type: Sequelize.STRING },
      room_quantity  : {type :Sequelize.INTEGER},
      image :{ type: Sequelize.STRING},

  

      createdAt: {
        allowNull: false,
        type: Sequelize.DATE
      },
      updatedAt: {
        allowNull: false,
        type: Sequelize.DATE
      }
    });
  },
  async down(queryInterface, Sequelize) {
    await queryInterface.dropTable('Hotels');
  }
};