'use strict';
/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up(queryInterface, Sequelize) {
    await queryInterface.createTable('Users', {
      id: {
        allowNull: false,
        autoIncrement: true,
        primaryKey: true,
        type: Sequelize.INTEGER
      },
      username :{ type :Sequelize.STRING },
      password :{ type :Sequelize.STRING} ,
      firstName: { type: Sequelize.STRING },
      lastName: { type: Sequelize.STRING },
      gender: { type: Sequelize.BOOLEAN },
      phoneNumber :{ type: Sequelize.STRING},
      email: { type: Sequelize.STRING },
      roleId  : {type :Sequelize.INTEGER},
      birthday :{ type: Sequelize.DATE},
      image: { type: Sequelize.STRING},
  

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
    await queryInterface.dropTable('Users');
  }
};