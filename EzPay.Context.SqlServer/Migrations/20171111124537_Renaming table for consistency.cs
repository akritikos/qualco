using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EzPay.Context.SqlServer.Migrations
{
    public partial class Renamingtableforconsistency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Settlements_SettlementType_Type",
                schema: "dbo",
                table: "Settlements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SettlementType",
                schema: "dbo",
                table: "SettlementType");

            migrationBuilder.RenameTable(
                name: "SettlementType",
                schema: "dbo",
                newName: "SettlementTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SettlementTypes",
                schema: "dbo",
                table: "SettlementTypes",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Settlements_SettlementTypes_Type",
                schema: "dbo",
                table: "Settlements",
                column: "Type",
                principalSchema: "dbo",
                principalTable: "SettlementTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Settlements_SettlementTypes_Type",
                schema: "dbo",
                table: "Settlements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SettlementTypes",
                schema: "dbo",
                table: "SettlementTypes");

            migrationBuilder.RenameTable(
                name: "SettlementTypes",
                schema: "dbo",
                newName: "SettlementType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SettlementType",
                schema: "dbo",
                table: "SettlementType",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Settlements_SettlementType_Type",
                schema: "dbo",
                table: "Settlements",
                column: "Type",
                principalSchema: "dbo",
                principalTable: "SettlementType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
