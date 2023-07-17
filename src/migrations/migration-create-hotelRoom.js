'use strict';
/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up(queryInterface, Sequelize) {
    await queryInterface.createTable('HotelRooms', {
      id: {
        allowNull: false,
        autoIncrement: true,
        primaryKey: true,
        type: Sequelize.INTEGER
      },
      hotel_id :{ type :Sequelize.INTEGER },
      type_id :{ type :Sequelize.INTEGER} ,
      name: { type: Sequelize.STRING },
      bed_quantity: { type: Sequelize.INTEGER },
      price: { type: Sequelize.INTEGER },
      description :{ type: Sequelize.TEXT},
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
    await queryInterface.dropTable('HotelRooms');
  }
};