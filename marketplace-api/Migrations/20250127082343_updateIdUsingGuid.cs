using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace marketplace_api.Migrations
{
    /// <inheritdoc />
    public partial class updateIdUsingGuid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Stocks_StockId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_StockId",
                table: "Comments");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Stocks",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Comments",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<Guid>(
                name: "StockId1",
                table: "Comments",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_StockId1",
                table: "Comments",
                column: "StockId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Stocks_StockId1",
                table: "Comments",
                column: "StockId1",
                principalTable: "Stocks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Stocks_StockId1",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_StockId1",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "StockId1",
                table: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Stocks",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Comments",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_StockId",
                table: "Comments",
                column: "StockId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Stocks_StockId",
                table: "Comments",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "Id");
        }
    }
}
