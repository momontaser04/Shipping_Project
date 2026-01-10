using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class carriersss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbShippmentStatus_TbCarriers",
                table: "TbShippmentStatus");

            migrationBuilder.DropIndex(
                name: "IX_TbShippmentStatus_CarrierId",
                table: "TbShippmentStatus");

            migrationBuilder.DropColumn(
                name: "CarrierId",
                table: "TbShippmentStatus");

            migrationBuilder.AddColumn<Guid>(
                name: "CarrierId",
                table: "TbShippments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TbShippments_CarrierId",
                table: "TbShippments",
                column: "CarrierId");

            migrationBuilder.AddForeignKey(
                name: "FK_TbShippmentStatus_TbCarriers",
                table: "TbShippments",
                column: "CarrierId",
                principalTable: "TbCarriers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbShippmentStatus_TbCarriers",
                table: "TbShippments");

            migrationBuilder.DropIndex(
                name: "IX_TbShippments_CarrierId",
                table: "TbShippments");

            migrationBuilder.DropColumn(
                name: "CarrierId",
                table: "TbShippments");

            migrationBuilder.AddColumn<Guid>(
                name: "CarrierId",
                table: "TbShippmentStatus",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_TbShippmentStatus_CarrierId",
                table: "TbShippmentStatus",
                column: "CarrierId");

            migrationBuilder.AddForeignKey(
                name: "FK_TbShippmentStatus_TbCarriers",
                table: "TbShippmentStatus",
                column: "CarrierId",
                principalTable: "TbCarriers",
                principalColumn: "Id");
        }
    }
}
