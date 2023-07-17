'use strict';
/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up(queryInterface, Sequelize) {
    await queryInterface.createTable('Bookings', {
      id: {
        allowNull: false,
        autoIncrement: true,
        primaryKey: true,
        type: Sequelize.INTEGER
      },
      room_id :{ type :Sequelize.INTEGER },
      user_id :{ type :Sequelize.INTEGER} ,
      hotel_id: { type: Sequelize.INTEGER },
      fullName : { type: Sequelize.STRING },
      phonenumber  :{ type: Sequelize.STRING},
      cccd : { type: Sequelize.STRING },
      email  : { type: Sequelize.STRING },
      birthday   : { type: Sequelize.DATE },
      status : { type: Sequelize.INTEGER },
      totalprice  :{ type :Sequelize.INTEGER },
      checkin_date    : { type: Sequelize.DATE },
      checkout_date    : { type: Sequelize.DATE },
      deadline_date    : { type: Sequelize.DATE },

  

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
    await queryInterface.dropTable('Bookings');
  }
};