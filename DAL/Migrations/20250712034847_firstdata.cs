using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class firstdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Log",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MessageTemplate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeStamp = table.Column<DateTime>(type: "datetime", nullable: true),
                    Exception = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Properties = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TbCarriers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    CarrierName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbCarriers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TbCountries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    CountryAName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CountryEName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbCountries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TbPaymentMethods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    MethdAName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    MethodEName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Commission = table.Column<double>(type: "float", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbPaymentMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TbSetting",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    KiloMeterRate = table.Column<double>(type: "float", nullable: true),
                    KilooGramRate = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbSetting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TbShippingTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ShippingTypeAName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ShippingTypeEName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ShippingFactor = table.Column<double>(type: "float", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbShippingTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TbSubscriptionPackages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    PackageName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ShippimentCount = table.Column<int>(type: "int", nullable: false),
                    NumberOfKiloMeters = table.Column<double>(type: "float", nullable: false),
                    TotalWeight = table.Column<double>(type: "float", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbSubscriptionPackages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TbCities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    CityAName = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    CityEName = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbCities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbCities_TbCountries",
                        column: x => x.CountryId,
                        principalTable: "TbCountries",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TbUserSubscriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PackageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubscriptionDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbUserSubscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbUserSubscriptions_TbSubscriptionPackages",
                        column: x => x.PackageId,
                        principalTable: "TbSubscriptionPackages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TbUserReceivers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceiverName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbUserReceivers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbUserReceivers_TbCities",
                        column: x => x.CityId,
                        principalTable: "TbCities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TbUserSebders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SenderName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "TbShippments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ShippingDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    SenderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceiverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShippingTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Width = table.Column<double>(type: "float", nullable: false),
                    Height = table.Column<double>(type: "float", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    Length = table.Column<double>(type: "float", nullable: false),
                    PackageValue = table.Column<decimal>(type: "decimal(8,4)", nullable: false),
                    ShippingRate = table.Column<decimal>(type: "decimal(8,4)", nullable: false),
                    PaymentMethodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserSubscriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TrackingNumber = table.Column<double>(type: "float", nullable: true),
                    ReferenceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbShippments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbShippments_TbPaymentMethods",
                        column: x => x.PaymentMethodId,
                        principalTable: "TbPaymentMethods",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TbShippments_TbShippingTypes",
                        column: x => x.ShippingTypeId,
                        principalTable: "TbShippingTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TbShippments_TbUserReceivers",
                        column: x => x.ReceiverId,
                        principalTable: "TbUserReceivers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TbShippments_TbUserSebders",
                        column: x => x.SenderId,
                        principalTable: "TbUserSebders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TbShippmentStatus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ShippmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarrierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbShippmentStatus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbShippmentStatus_TbCarriers",
                        column: x => x.CarrierId,
                        principalTable: "TbCarriers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TbShippmentStatus_TbShippments",
                        column: x => x.ShippmentId,
                        principalTable: "TbShippments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TbCities_CountryId",
                table: "TbCities",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_TbShippments_PaymentMethodId",
                table: "TbShippments",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_TbShippments_ReceiverId",
                table: "TbShippments",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_TbShippments_SenderId",
                table: "TbShippments",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_TbShippments_ShippingTypeId",
                table: "TbShippments",
                column: "ShippingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TbShippmentStatus_CarrierId",
                table: "TbShippmentStatus",
                column: "CarrierId");

            migrationBuilder.CreateIndex(
                name: "IX_TbShippmentStatus_ShippmentId",
                table: "TbShippmentStatus",
                column: "ShippmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TbUserReceivers_CityId",
                table: "TbUserReceivers",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_TbUserSebders_CityId",
                table: "TbUserSebders",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_TbUserSubscriptions_PackageId",
                table: "TbUserSubscriptions",
                column: "PackageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Log");

            migrationBuilder.DropTable(
                name: "TbSetting");

            migrationBuilder.DropTable(
                name: "TbShippmentStatus");

            migrationBuilder.DropTable(
                name: "TbUserSubscriptions");

            migrationBuilder.DropTable(
                name: "TbCarriers");

            migrationBuilder.DropTable(
                name: "TbShippments");

            migrationBuilder.DropTable(
                name: "TbSubscriptionPackages");

            migrationBuilder.DropTable(
                name: "TbPaymentMethods");

            migrationBuilder.DropTable(
                name: "TbShippingTypes");

            migrationBuilder.DropTable(
                name: "TbUserReceivers");

            migrationBuilder.DropTable(
                name: "TbUserSebders");

            migrationBuilder.DropTable(
                name: "TbCities");

            migrationBuilder.DropTable(
                name: "TbCountries");
        }
    }
}
