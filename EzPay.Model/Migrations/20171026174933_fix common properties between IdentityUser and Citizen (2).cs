using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EzPay.Model.Migrations
{
    public partial class fixcommonpropertiesbetweenIdentityUserandCitizen2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                schema: "dbo",
                table: "Citizen");

            migrationBuilder.DropColumn(
                name: "Telephone",
                schema: "dbo",
                table: "Citizen");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                schema: "dbo",
                table: "Citizen",
                type: "varchar(13)",
                unicode: false,
                maxLength: 13,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                schema: "dbo",
                table: "Citizen",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                schema: "dbo",
                table: "Citizen",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(13)",
                oldUnicode: false,
                oldMaxLength: 13,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                schema: "dbo",
                table: "Citizen",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                schema: "dbo",
                table: "Citizen",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telephone",
                schema: "dbo",
                table: "Citizen",
                unicode: false,
                maxLength: 13,
                nullable: true);
        }
    }
}
