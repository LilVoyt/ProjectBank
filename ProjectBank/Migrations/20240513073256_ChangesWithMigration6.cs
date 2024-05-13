﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectBank.Migrations
{
    /// <inheritdoc />
    public partial class ChangesWithMigration6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Cards_CardId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_CardId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "CardId",
                table: "Transaction");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_CardReceiverID",
                table: "Transaction",
                column: "CardReceiverID");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_CardSenderID",
                table: "Transaction",
                column: "CardSenderID");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Cards_CardReceiverID",
                table: "Transaction",
                column: "CardReceiverID",
                principalTable: "Cards",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Cards_CardSenderID",
                table: "Transaction",
                column: "CardSenderID",
                principalTable: "Cards",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Cards_CardReceiverID",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Cards_CardSenderID",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_CardReceiverID",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_CardSenderID",
                table: "Transaction");

            migrationBuilder.AddColumn<Guid>(
                name: "CardId",
                table: "Transaction",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_CardId",
                table: "Transaction",
                column: "CardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Cards_CardId",
                table: "Transaction",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "Id");
        }
    }
}
