using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EzPay.Model.Migrations
{
    public partial class StartUp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Citizens",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", maxLength: 10, nullable: false),
                    Address = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    County = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    Email = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Telephone = table.Column<string>(type: "varchar(13)", unicode: false, maxLength: 13, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citizens", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SettlementTypes",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
                    Downpayment = table.Column<double>(type: "decimal(4, 2)", nullable: false),
                    Interest = table.Column<double>(type: "decimal(4, 2)", nullable: false),
                    MaxInstallments = table.Column<int>(type: "int", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettlementTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<double>(type: "decimal(7, 2)", nullable: false),
                    Citizen = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    DueDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Bills_Citizens_Citizen",
                        column: x => x.Citizen,
                        principalSchema: "dbo",
                        principalTable: "Citizens",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Settlements",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
                    Installments = table.Column<int>(type: "int", maxLength: 3, nullable: false),
                    Type = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settlements", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Settlements_SettlementTypes_Type",
                        column: x => x.Type,
                        principalSchema: "dbo",
                        principalTable: "SettlementTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                schema: "dbo",
                columns: table => new
                {
                    Bill = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    Method = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Bill);
                    table.ForeignKey(
                        name: "FK_Payments_Bills_Bill",
                        column: x => x.Bill,
                        principalSchema: "dbo",
                        principalTable: "Bills",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Settled",
                schema: "dbo",
                columns: table => new
                {
                    Bill = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Settlement = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settled", x => new { x.Bill, x.Settlement });
                    table.ForeignKey(
                        name: "FK_Settled_Bills_Bill",
                        column: x => x.Bill,
                        principalSchema: "dbo",
                        principalTable: "Bills",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Settled_Settlements_Settlement",
                        column: x => x.Settlement,
                        principalSchema: "dbo",
                        principalTable: "Settlements",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bills_Citizen",
                schema: "dbo",
                table: "Bills",
                column: "Citizen");

            migrationBuilder.CreateIndex(
                name: "IX_Settled_Settlement",
                schema: "dbo",
                table: "Settled",
                column: "Settlement");

            migrationBuilder.CreateIndex(
                name: "IX_Settlements_Type",
                schema: "dbo",
                table: "Settlements",
                column: "Type");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Settled",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Bills",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Settlements",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Citizens",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "SettlementTypes",
                schema: "dbo");
        }
    }
}
