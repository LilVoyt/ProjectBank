using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectBank.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountEmployee_Accounts_AccountsId",
                table: "AccountEmployee");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountEmployee_Employees_EmployeesId",
                table: "AccountEmployee");

            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Customers_CustomerID",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Accounts_AccountID",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Cards_CardReceiverID",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Cards_CardSenderID",
                table: "Transaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cards",
                table: "Cards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "Employee");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "Customer");

            migrationBuilder.RenameTable(
                name: "Cards",
                newName: "Card");

            migrationBuilder.RenameTable(
                name: "Accounts",
                newName: "Account");

            migrationBuilder.RenameIndex(
                name: "IX_Cards_AccountID",
                table: "Card",
                newName: "IX_Card_AccountID");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_CustomerID",
                table: "Account",
                newName: "IX_Account_CustomerID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employee",
                table: "Employee",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customer",
                table: "Customer",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Card",
                table: "Card",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Account",
                table: "Account",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Customer_CustomerID",
                table: "Account",
                column: "CustomerID",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountEmployee_Account_AccountsId",
                table: "AccountEmployee",
                column: "AccountsId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountEmployee_Employee_EmployeesId",
                table: "AccountEmployee",
                column: "EmployeesId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Card_Account_AccountID",
                table: "Card",
                column: "AccountID",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Card_CardReceiverID",
                table: "Transaction",
                column: "CardReceiverID",
                principalTable: "Card",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Card_CardSenderID",
                table: "Transaction",
                column: "CardSenderID",
                principalTable: "Card",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Customer_CustomerID",
                table: "Account");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountEmployee_Account_AccountsId",
                table: "AccountEmployee");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountEmployee_Employee_EmployeesId",
                table: "AccountEmployee");

            migrationBuilder.DropForeignKey(
                name: "FK_Card_Account_AccountID",
                table: "Card");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Card_CardReceiverID",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Card_CardSenderID",
                table: "Transaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employee",
                table: "Employee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customer",
                table: "Customer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Card",
                table: "Card");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Account",
                table: "Account");

            migrationBuilder.RenameTable(
                name: "Employee",
                newName: "Employees");

            migrationBuilder.RenameTable(
                name: "Customer",
                newName: "Customers");

            migrationBuilder.RenameTable(
                name: "Card",
                newName: "Cards");

            migrationBuilder.RenameTable(
                name: "Account",
                newName: "Accounts");

            migrationBuilder.RenameIndex(
                name: "IX_Card_AccountID",
                table: "Cards",
                newName: "IX_Cards_AccountID");

            migrationBuilder.RenameIndex(
                name: "IX_Account_CustomerID",
                table: "Accounts",
                newName: "IX_Accounts_CustomerID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cards",
                table: "Cards",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountEmployee_Accounts_AccountsId",
                table: "AccountEmployee",
                column: "AccountsId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountEmployee_Employees_EmployeesId",
                table: "AccountEmployee",
                column: "EmployeesId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Customers_CustomerID",
                table: "Accounts",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Accounts_AccountID",
                table: "Cards",
                column: "AccountID",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
    }
}
