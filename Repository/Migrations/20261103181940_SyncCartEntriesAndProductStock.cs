using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class SyncCartEntriesAndProductStock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<uint>(
                name: "Stock",
                table: "products",
                type: "int unsigned",
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.CreateTable(
                name: "cart_entries",
                columns: table => new
                {
                    UserId = table.Column<ulong>(type: "bigint unsigned", nullable: false),
                    ProductId = table.Column<ulong>(type: "bigint unsigned", nullable: false),
                    Quantity = table.Column<uint>(type: "int unsigned", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cart_entries", x => new { x.UserId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_cart_entries_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cart_entries_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1ul,
                column: "Stock",
                value: 15u);

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2ul,
                column: "Stock",
                value: 20u);

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3ul,
                column: "Stock",
                value: 12u);

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4ul,
                column: "Stock",
                value: 10u);

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5ul,
                column: "Stock",
                value: 5u);

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6ul,
                column: "Stock",
                value: 18u);

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7ul,
                column: "Stock",
                value: 22u);

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 8ul,
                column: "Stock",
                value: 30u);

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 9ul,
                column: "Stock",
                value: 16u);

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 10ul,
                column: "Stock",
                value: 14u);

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 11ul,
                column: "Stock",
                value: 9u);

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 12ul,
                column: "Stock",
                value: 7u);

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 13ul,
                column: "Stock",
                value: 11u);

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 14ul,
                column: "Stock",
                value: 8u);

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 15ul,
                column: "Stock",
                value: 6u);

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 16ul,
                column: "Stock",
                value: 25u);

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 17ul,
                column: "Stock",
                value: 20u);

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 18ul,
                column: "Stock",
                value: 12u);

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 19ul,
                column: "Stock",
                value: 35u);

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 20ul,
                column: "Stock",
                value: 18u);

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 21ul,
                column: "Stock",
                value: 15u);

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 22ul,
                column: "Stock",
                value: 4u);

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 23ul,
                column: "Stock",
                value: 25u);

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 24ul,
                column: "Stock",
                value: 30u);

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 25ul,
                column: "Stock",
                value: 10u);

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 26ul,
                column: "Stock",
                value: 8u);

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 27ul,
                column: "Stock",
                value: 50u);

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 28ul,
                column: "Stock",
                value: 40u);

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 29ul,
                column: "Stock",
                value: 22u);

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 30ul,
                column: "Stock",
                value: 35u);

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 31ul,
                column: "Stock",
                value: 19u);

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 32ul,
                column: "Stock",
                value: 7u);

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 33ul,
                column: "Stock",
                value: 14u);

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 34ul,
                column: "Stock",
                value: 17u);

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 35ul,
                column: "Stock",
                value: 60u);

            migrationBuilder.CreateIndex(
                name: "IX_cart_entries_ProductId",
                table: "cart_entries",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cart_entries");

            migrationBuilder.DropColumn(
                name: "Stock",
                table: "products");
        }
    }
}
