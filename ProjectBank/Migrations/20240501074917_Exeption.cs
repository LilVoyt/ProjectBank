using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectBank.Migrations
{
    /// <inheritdoc />
    public partial class Exeption : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountEmployee_Accounts_AccountId",
                table: "AccountEmployee");

            migrationBuilder.DropColumn(
                name: "AccID",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "AccountEmployee",
                newName: "AccountsId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountEmployee_Accounts_AccountsId",
                table: "AccountEmployee",
                column: "AccountsId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountEmployee_Accounts_AccountsId",
                table: "AccountEmployee");

            migrationBuilder.RenameColumn(
                name: "AccountsId",
                table: "AccountEmployee",
                newName: "AccountId");

            migrationBuilder.AddColumn<Guid>(
                name: "AccID",
                table: "Employees",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_AccountEmployee_Accounts_AccountId",
                table: "AccountEmployee",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
