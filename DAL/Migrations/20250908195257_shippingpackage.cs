using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class shippingpackage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbShippments_TbUserSebders",
                table: "TbShippments");

            migrationBuilder.DropTable(
                name: "TbUserSebders");

            migrationBuilder.RenameColumn(
                name: "ShippingDate",
                table: "TbShippments",
                newName: "ShipingDate");

            migrationBuilder.AddColumn<string>(
                name: "Contact",
                table: "TbUserReceivers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OtherAddress",
                table: "TbUserReceivers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "TbUserReceivers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DelivryDate",
                table: "TbShippments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "ShipingPackgingId",
                table: "TbShippments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TbShipingPackging",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ShipingPackgingAname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShipingPackgingEname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbShipingPackging", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TbUserSenders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SenderName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OtherAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbUserSenders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbUserSebders_TbCities",
                        column: x => x.CityId,
                        principalTable: "TbCities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TbShippments_ShipingPackgingId",
                table: "TbShippments",
                column: "ShipingPackgingId");

            migrationBuilder.CreateIndex(
                name: "IX_TbUserSenders_CityId",
                table: "TbUserSenders",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_TbShippments_TbShipingPackging_ShipingPackgingId",
                table: "TbShippments",
                column: "ShipingPackgingId",
                principalTable: "TbShipingPackging",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TbShippments_TbUserSebders",
                table: "TbShippments",
                column: "SenderId",
                principalTable: "TbUserSenders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbShippments_TbShipingPackging_ShipingPackgingId",
                table: "TbShippments");

            migrationBuilder.DropForeignKey(
                name: "FK_TbShippments_TbUserSebders",
                table: "TbShippments");

            migrationBuilder.DropTable(
                name: "TbShipingPackging");

            migrationBuilder.DropTable(
                name: "TbUserSenders");

            migrationBuilder.DropIndex(
                name: "IX_TbShippments_ShipingPackgingId",
                table: "TbShippments");

            migrationBuilder.DropColumn(
                name: "Contact",
                table: "TbUserReceivers");

            migrationBuilder.DropColumn(
                name: "OtherAddress",
                table: "TbUserReceivers");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "TbUserReceivers");

            migrationBuilder.DropColumn(
                name: "DelivryDate",
                table: "TbShippments");

            migrationBuilder.DropColumn(
                name: "ShipingPackgingId",
                table: "TbShippments");

            migrationBuilder.RenameColumn(
                name: "ShipingDate",
                table: "TbShippments",
                newName: "ShippingDate");

            migrationBuilder.CreateTable(
                name: "TbUserSebders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SenderName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbUserSebders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbUserSebders_TbCities",
                        column: x => x.CityId,
                        principalTable: "TbCities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TbUserSebders_CityId",
                table: "TbUserSebders",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_TbShippments_TbUserSebders",
                table: "TbShippments",
                column: "SenderId",
                principalTable: "TbUserSebders",
                principalColumn: "Id");
        }
    }
}
