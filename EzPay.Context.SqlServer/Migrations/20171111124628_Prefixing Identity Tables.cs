using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EzPay.Context.SqlServer.Migrations
{
    public partial class PrefixingIdentityTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleClaim",
                schema: "dbo",
                table: "RoleClaim");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                schema: "dbo",
                table: "Role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CitizenToken",
                schema: "dbo",
                table: "CitizenToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CitizenRole",
                schema: "dbo",
                table: "CitizenRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CitizenLogin",
                schema: "dbo",
                table: "CitizenLogin");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CitizenClaim",
                schema: "dbo",
                table: "CitizenClaim");

            migrationBuilder.RenameTable(
                name: "RoleClaim",
                schema: "dbo",
                newName: "_RoleClaim");

            migrationBuilder.RenameTable(
                name: "Role",
                schema: "dbo",
                newName: "_Role");

            migrationBuilder.RenameTable(
                name: "CitizenToken",
                schema: "dbo",
                newName: "_CitizenToken");

            migrationBuilder.RenameTable(
                name: "CitizenRole",
                schema: "dbo",
                newName: "_CitizenRole");

            migrationBuilder.RenameTable(
                name: "CitizenLogin",
                schema: "dbo",
                newName: "_CitizenLogin");

            migrationBuilder.RenameTable(
                name: "CitizenClaim",
                schema: "dbo",
                newName: "_CitizenClaim");

            migrationBuilder.AddPrimaryKey(
                name: "PK__RoleClaim",
                schema: "dbo",
                table: "_RoleClaim",
                column: "RoleClaimId");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Role",
                schema: "dbo",
                table: "_Role",
                column: "RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK__CitizenToken",
                schema: "dbo",
                table: "_CitizenToken",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__CitizenRole",
                schema: "dbo",
                table: "_CitizenRole",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__CitizenLogin",
                schema: "dbo",
                table: "_CitizenLogin",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__CitizenClaim",
                schema: "dbo",
                table: "_CitizenClaim",
                column: "CitizenClaimId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK__RoleClaim",
                schema: "dbo",
                table: "_RoleClaim");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Role",
                schema: "dbo",
                table: "_Role");

            migrationBuilder.DropPrimaryKey(
                name: "PK__CitizenToken",
                schema: "dbo",
                table: "_CitizenToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK__CitizenRole",
                schema: "dbo",
                table: "_CitizenRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK__CitizenLogin",
                schema: "dbo",
                table: "_CitizenLogin");

            migrationBuilder.DropPrimaryKey(
                name: "PK__CitizenClaim",
                schema: "dbo",
                table: "_CitizenClaim");

            migrationBuilder.RenameTable(
                name: "_RoleClaim",
                schema: "dbo",
                newName: "RoleClaim");

            migrationBuilder.RenameTable(
                name: "_Role",
                schema: "dbo",
                newName: "Role");

            migrationBuilder.RenameTable(
                name: "_CitizenToken",
                schema: "dbo",
                newName: "CitizenToken");

            migrationBuilder.RenameTable(
                name: "_CitizenRole",
                schema: "dbo",
                newName: "CitizenRole");

            migrationBuilder.RenameTable(
                name: "_CitizenLogin",
                schema: "dbo",
                newName: "CitizenLogin");

            migrationBuilder.RenameTable(
                name: "_CitizenClaim",
                schema: "dbo",
                newName: "CitizenClaim");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleClaim",
                schema: "dbo",
                table: "RoleClaim",
                column: "RoleClaimId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                schema: "dbo",
                table: "Role",
                column: "RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CitizenToken",
                schema: "dbo",
                table: "CitizenToken",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CitizenRole",
                schema: "dbo",
                table: "CitizenRole",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CitizenLogin",
                schema: "dbo",
                table: "CitizenLogin",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CitizenClaim",
                schema: "dbo",
                table: "CitizenClaim",
                column: "CitizenClaimId");
        }
    }
}
