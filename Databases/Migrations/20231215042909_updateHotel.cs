using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelApp.API.Databases.Migrations
{
    /// <inheritdoc />
    public partial class updateHotel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_HotelRoom_HotelRoomId",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Hotel_HotelId",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Hotel_Province_ProvinceId",
                table: "Hotel");

            migrationBuilder.DropForeignKey(
                name: "FK_Hotel_Users_UserId",
                table: "Hotel");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelRoom_Hotel_HotelId",
                table: "HotelRoom");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelRoom_TypeRoom_TypeRoomId",
                table: "HotelRoom");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Province",
                table: "Province");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HotelRoom",
                table: "HotelRoom");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hotel",
                table: "Hotel");

            migrationBuilder.RenameTable(
                name: "Province",
                newName: "Provinces");

            migrationBuilder.RenameTable(
                name: "HotelRoom",
                newName: "HotelRooms");

            migrationBuilder.RenameTable(
                name: "Hotel",
                newName: "Hotels");

            migrationBuilder.RenameIndex(
                name: "IX_HotelRoom_TypeRoomId",
                table: "HotelRooms",
                newName: "IX_HotelRooms_TypeRoomId");

            migrationBuilder.RenameIndex(
                name: "IX_HotelRoom_HotelId",
                table: "HotelRooms",
                newName: "IX_HotelRooms_HotelId");

            migrationBuilder.RenameIndex(
                name: "IX_Hotel_UserId",
                table: "Hotels",
                newName: "IX_Hotels_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Hotel_ProvinceId",
                table: "Hotels",
                newName: "IX_Hotels_ProvinceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Provinces",
                table: "Provinces",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HotelRooms",
                table: "HotelRooms",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hotels",
                table: "Hotels",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_HotelRooms_HotelRoomId",
                table: "Booking",
                column: "HotelRoomId",
                principalTable: "HotelRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Hotels_HotelId",
                table: "Booking",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelRooms_Hotels_HotelId",
                table: "HotelRooms",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelRooms_TypeRoom_TypeRoomId",
                table: "HotelRooms",
                column: "TypeRoomId",
                principalTable: "TypeRoom",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_Provinces_ProvinceId",
                table: "Hotels",
                column: "ProvinceId",
                principalTable: "Provinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_Users_UserId",
                table: "Hotels",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_HotelRooms_HotelRoomId",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Hotels_HotelId",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelRooms_Hotels_HotelId",
                table: "HotelRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelRooms_TypeRoom_TypeRoomId",
                table: "HotelRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_Provinces_ProvinceId",
                table: "Hotels");

            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_Users_UserId",
                table: "Hotels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Provinces",
                table: "Provinces");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hotels",
                table: "Hotels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HotelRooms",
                table: "HotelRooms");

            migrationBuilder.RenameTable(
                name: "Provinces",
                newName: "Province");

            migrationBuilder.RenameTable(
                name: "Hotels",
                newName: "Hotel");

            migrationBuilder.RenameTable(
                name: "HotelRooms",
                newName: "HotelRoom");

            migrationBuilder.RenameIndex(
                name: "IX_Hotels_UserId",
                table: "Hotel",
                newName: "IX_Hotel_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Hotels_ProvinceId",
                table: "Hotel",
                newName: "IX_Hotel_ProvinceId");

            migrationBuilder.RenameIndex(
                name: "IX_HotelRooms_TypeRoomId",
                table: "HotelRoom",
                newName: "IX_HotelRoom_TypeRoomId");

            migrationBuilder.RenameIndex(
                name: "IX_HotelRooms_HotelId",
                table: "HotelRoom",
                newName: "IX_HotelRoom_HotelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Province",
                table: "Province",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hotel",
                table: "Hotel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HotelRoom",
                table: "HotelRoom",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_HotelRoom_HotelRoomId",
                table: "Booking",
                column: "HotelRoomId",
                principalTable: "HotelRoom",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Hotel_HotelId",
                table: "Booking",
                column: "HotelId",
                principalTable: "Hotel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Hotel_Province_ProvinceId",
                table: "Hotel",
                column: "ProvinceId",
                principalTable: "Province",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Hotel_Users_UserId",
                table: "Hotel",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelRoom_Hotel_HotelId",
                table: "HotelRoom",
                column: "HotelId",
                principalTable: "Hotel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelRoom_TypeRoom_TypeRoomId",
                table: "HotelRoom",
                column: "TypeRoomId",
                principalTable: "TypeRoom",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
