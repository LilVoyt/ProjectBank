using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectBank.Migrations
{
    /// <inheritdoc />
    public partial class ChangesWithMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Cards_CardID",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_CardID",
                table: "Transactions");

            migrationBuilder.RenameTable(
                name: "Transactions",
                newName: "Transaction");

            migrationBuilder.RenameColumn(
                name: "CardID",
                table: "Transaction",
                newName: "CardSenderID");

            migrationBuilder.AddColumn<Guid>(
                name: "CardReceiverID",
                table: "Transaction",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transaction",
                table: "Transaction",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_CardReceiverID",
                table: "Transaction",
                column: "CardReceiverID");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Cards_CardReceiverID",
                table: "Transaction",
                column: "CardReceiverID",
                principalTable: "Cards",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Cards_CardReceiverID",
                table: "Transaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transaction",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_CardReceiverID",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "CardReceiverID",
                table: "Transaction");

            migrationBuilder.RenameTable(
                name: "Transaction",
                newName: "Transactions");

            migrationBuilder.RenameColumn(
                name: "CardSenderID",
                table: "Transactions",
                newName: "CardID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CardID",
                table: "Transactions",
                column: "CardID");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Cards_CardID",
                table: "Transactions",
                column: "CardID",
                principalTable: "Cards",
                principalColumn: "Id");
        }
    }
}
