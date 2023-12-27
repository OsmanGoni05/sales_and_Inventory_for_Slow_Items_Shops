using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace salesandInventoryforSlowItemsShops.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedUserModeTransactionDetailsupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdvancePayment",
                table: "TransactionDetails");

            migrationBuilder.DropColumn(
                name: "Bank",
                table: "TransactionDetails");

            migrationBuilder.DropColumn(
                name: "BrandtypeId",
                table: "TransactionDetails");

            migrationBuilder.DropColumn(
                name: "Cash",
                table: "TransactionDetails");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "TransactionDetails");

            migrationBuilder.DropColumn(
                name: "Due",
                table: "TransactionDetails");

            migrationBuilder.DropColumn(
                name: "GiverId",
                table: "TransactionDetails");

            migrationBuilder.DropColumn(
                name: "MFS",
                table: "TransactionDetails");

            migrationBuilder.DropColumn(
                name: "MoneyPaid",
                table: "TransactionDetails");

            migrationBuilder.DropColumn(
                name: "MoneyReceived",
                table: "TransactionDetails");

            migrationBuilder.DropColumn(
                name: "ParentTransactionId",
                table: "TransactionDetails");

            migrationBuilder.DropColumn(
                name: "QualitytypeId",
                table: "TransactionDetails");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "TransactionDetails");

            migrationBuilder.DropColumn(
                name: "State",
                table: "TransactionDetails");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "TransactionDetails");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "TransactionDetails");

            migrationBuilder.RenameColumn(
                name: "ReceiverId",
                table: "TransactionDetails",
                newName: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionDetails_TransactionId",
                table: "TransactionDetails",
                column: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionDetails_Transactions_TransactionId",
                table: "TransactionDetails",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionDetails_Transactions_TransactionId",
                table: "TransactionDetails");

            migrationBuilder.DropIndex(
                name: "IX_TransactionDetails_TransactionId",
                table: "TransactionDetails");

            migrationBuilder.RenameColumn(
                name: "TransactionId",
                table: "TransactionDetails",
                newName: "ReceiverId");

            migrationBuilder.AddColumn<double>(
                name: "AdvancePayment",
                table: "TransactionDetails",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Bank",
                table: "TransactionDetails",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "BrandtypeId",
                table: "TransactionDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Cash",
                table: "TransactionDetails",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "TransactionDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "Due",
                table: "TransactionDetails",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "GiverId",
                table: "TransactionDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "MFS",
                table: "TransactionDetails",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MoneyPaid",
                table: "TransactionDetails",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MoneyReceived",
                table: "TransactionDetails",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "ParentTransactionId",
                table: "TransactionDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QualitytypeId",
                table: "TransactionDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "TransactionDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "TransactionDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "TotalPrice",
                table: "TransactionDetails",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "TransactionDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
