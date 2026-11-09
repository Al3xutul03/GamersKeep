using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class SeedProductImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "product_images",
                columns: new[] { "ImageUrl", "CreatedAt", "DeletedAt", "ProductId", "UpdatedAt" },
                values: new object[,]
                {
                    { "/img/7%20Wonders%20Duel.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 7ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Carcassonne.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 2ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Catan.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 1ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Codenames.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 8ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Dixit.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 9ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Gloomhaven.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 5ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Pandemic.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 6ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Terraforming%20Mars.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 4ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Ticket%20to%20Ride%20Europe.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 3ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "product_images",
                keyColumn: "ImageUrl",
                keyValue: "/img/7%20Wonders%20Duel.jpg");

            migrationBuilder.DeleteData(
                table: "product_images",
                keyColumn: "ImageUrl",
                keyValue: "/img/Carcassonne.jpg");

            migrationBuilder.DeleteData(
                table: "product_images",
                keyColumn: "ImageUrl",
                keyValue: "/img/Catan.jpg");

            migrationBuilder.DeleteData(
                table: "product_images",
                keyColumn: "ImageUrl",
                keyValue: "/img/Codenames.jpg");

            migrationBuilder.DeleteData(
                table: "product_images",
                keyColumn: "ImageUrl",
                keyValue: "/img/Dixit.jpg");

            migrationBuilder.DeleteData(
                table: "product_images",
                keyColumn: "ImageUrl",
                keyValue: "/img/Gloomhaven.jpg");

            migrationBuilder.DeleteData(
                table: "product_images",
                keyColumn: "ImageUrl",
                keyValue: "/img/Pandemic.jpg");

            migrationBuilder.DeleteData(
                table: "product_images",
                keyColumn: "ImageUrl",
                keyValue: "/img/Terraforming%20Mars.jpg");

            migrationBuilder.DeleteData(
                table: "product_images",
                keyColumn: "ImageUrl",
                keyValue: "/img/Ticket%20to%20Ride%20Europe.jpg");
        }
    }
}
