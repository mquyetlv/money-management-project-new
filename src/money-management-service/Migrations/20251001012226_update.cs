using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace money_management_service.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TRANSACTIONS_INVESTMENT_InvestmentId",
                table: "TRANSACTIONS");

            migrationBuilder.AlterColumn<Guid>(
                name: "InvestmentId",
                table: "TRANSACTIONS",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_TRANSACTIONS_INVESTMENT_InvestmentId",
                table: "TRANSACTIONS",
                column: "InvestmentId",
                principalTable: "INVESTMENT",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TRANSACTIONS_INVESTMENT_InvestmentId",
                table: "TRANSACTIONS");

            migrationBuilder.AlterColumn<Guid>(
                name: "InvestmentId",
                table: "TRANSACTIONS",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TRANSACTIONS_INVESTMENT_InvestmentId",
                table: "TRANSACTIONS",
                column: "InvestmentId",
                principalTable: "INVESTMENT",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
