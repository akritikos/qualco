using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EzPay.Model.Migrations
{
    public partial class Minorreformation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Settled",
                schema: "dbo");

            migrationBuilder.AddColumn<long>(
                name: "CitizenId",
                schema: "dbo",
                table: "Settlements",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<string>(
                name: "County",
                schema: "dbo",
                table: "Citizens",
                type: "varchar(30)",
                unicode: false,
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 15,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                schema: "dbo",
                table: "Citizens",
                type: "varchar(30)",
                unicode: false,
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                schema: "dbo",
                table: "Bills",
                type: "decimal(8, 2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "decimal(7, 2)");

            migrationBuilder.AddColumn<Guid>(
                name: "SettlementId",
                schema: "dbo",
                table: "Bills",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Settlements_CitizenId",
                schema: "dbo",
                table: "Settlements",
                column: "CitizenId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_SettlementId",
                schema: "dbo",
                table: "Bills",
                column: "SettlementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Settlements_SettlementId",
                schema: "dbo",
                table: "Bills",
                column: "SettlementId",
                principalSchema: "dbo",
                principalTable: "Settlements",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Settlements_Citizens_CitizenId",
                schema: "dbo",
                table: "Settlements",
                column: "CitizenId",
                principalSchema: "dbo",
                principalTable: "Citizens",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Settlements_SettlementId",
                schema: "dbo",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Settlements_Citizens_CitizenId",
                schema: "dbo",
                table: "Settlements");

            migrationBuilder.DropIndex(
                name: "IX_Settlements_CitizenId",
                schema: "dbo",
                table: "Settlements");

            migrationBuilder.DropIndex(
                name: "IX_Bills_SettlementId",
                schema: "dbo",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "CitizenId",
                schema: "dbo",
                table: "Settlements");

            migrationBuilder.DropColumn(
                name: "SettlementId",
                schema: "dbo",
                table: "Bills");

            migrationBuilder.AlterColumn<string>(
                name: "County",
                schema: "dbo",
                table: "Citizens",
                unicode: false,
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldUnicode: false,
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                schema: "dbo",
                table: "Citizens",
                unicode: false,
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldUnicode: false,
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                schema: "dbo",
                table: "Bills",
                type: "decimal(7, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8, 2)");

            migrationBuilder.CreateTable(
                name: "Settled",
                schema: "dbo",
                columns: table => new
                {
                    Bill = table.Column<Guid>(nullable: false),
                    Settlement = table.Column<Guid>(nullable: false)
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
                name: "IX_Settled_Settlement",
                schema: "dbo",
                table: "Settled",
                column: "Settlement");
        }
    }
}
