using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class TrimSeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "product_categories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { "trading-card-game", 20ul });

            migrationBuilder.DeleteData(
                table: "product_categories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { "trading-card-game", 21ul });

            migrationBuilder.DeleteData(
                table: "product_categories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { "miniatures", 22ul });

            migrationBuilder.DeleteData(
                table: "product_categories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { "sci-fi", 22ul });

            migrationBuilder.DeleteData(
                table: "product_categories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { "wargame", 22ul });

            migrationBuilder.DeleteData(
                table: "product_categories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { "painting-supplies", 23ul });

            migrationBuilder.DeleteData(
                table: "product_categories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { "painting-supplies", 24ul });

            migrationBuilder.DeleteData(
                table: "product_categories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { "fantasy", 25ul });

            migrationBuilder.DeleteData(
                table: "product_categories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { "miniatures", 25ul });

            migrationBuilder.DeleteData(
                table: "product_categories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { "t-t-r-p-g", 26ul });

            migrationBuilder.DeleteData(
                table: "product_categories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { "terrain", 26ul });

            migrationBuilder.DeleteData(
                table: "product_categories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { "accessory", 27ul });

            migrationBuilder.DeleteData(
                table: "product_categories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { "accessory", 28ul });

            migrationBuilder.DeleteData(
                table: "product_categories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { "fantasy", 29ul });

            migrationBuilder.DeleteData(
                table: "product_categories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { "puzzle", 29ul });

            migrationBuilder.DeleteData(
                table: "product_categories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { "children", 30ul });

            migrationBuilder.DeleteData(
                table: "product_categories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { "party", 30ul });

            migrationBuilder.DeleteData(
                table: "product_categories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { "travel-sized", 30ul });

            migrationBuilder.DeleteData(
                table: "product_categories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { "party", 31ul });

            migrationBuilder.DeleteData(
                table: "product_categories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { "board-game", 32ul });

            migrationBuilder.DeleteData(
                table: "product_categories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { "romance", 32ul });

            migrationBuilder.DeleteData(
                table: "product_categories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { "two-players", 32ul });

            migrationBuilder.DeleteData(
                table: "product_categories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { "children", 33ul });

            migrationBuilder.DeleteData(
                table: "product_categories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { "cooperative", 33ul });

            migrationBuilder.DeleteData(
                table: "product_categories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { "strategy", 34ul });

            migrationBuilder.DeleteData(
                table: "product_categories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { "travel-sized", 34ul });

            migrationBuilder.DeleteData(
                table: "product_categories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { "two-players", 34ul });

            migrationBuilder.DeleteData(
                table: "product_categories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { "other", 35ul });

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 20ul);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 21ul);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 22ul);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 23ul);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 24ul);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 25ul);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 26ul);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 27ul);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 28ul);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 29ul);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 30ul);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 31ul);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 32ul);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 33ul);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 34ul);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 35ul);

            migrationBuilder.InsertData(
                table: "product_images",
                columns: new[] { "ImageUrl", "CreatedAt", "DeletedAt", "ProductId", "UpdatedAt" },
                values: new object[,]
                {
                    { "/img/Arkham%20Horror%20The%20Card%20Game.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 14ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Azul.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 10ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Call%20of%20Cthulhu%20Keeper%20Rulebook.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 18ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Dungeons%20%26%20Dragons%20Monster%20Manual.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 17ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Dungeons%20%26%20Dragons%20Player%27s%20Handbook.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 16ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Magic%20The%20Gathering%20-%20Foundations%20Starter%20Kit.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 19ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Root.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 13ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Scythe.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 12ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Sherlock%20Holmes%20Consulting%20Detective.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 15ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Wingspan.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 11ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "product_images",
                keyColumn: "ImageUrl",
                keyValue: "/img/Arkham%20Horror%20The%20Card%20Game.jpg");

            migrationBuilder.DeleteData(
                table: "product_images",
                keyColumn: "ImageUrl",
                keyValue: "/img/Azul.jpg");

            migrationBuilder.DeleteData(
                table: "product_images",
                keyColumn: "ImageUrl",
                keyValue: "/img/Call%20of%20Cthulhu%20Keeper%20Rulebook.jpg");

            migrationBuilder.DeleteData(
                table: "product_images",
                keyColumn: "ImageUrl",
                keyValue: "/img/Dungeons%20%26%20Dragons%20Monster%20Manual.jpg");

            migrationBuilder.DeleteData(
                table: "product_images",
                keyColumn: "ImageUrl",
                keyValue: "/img/Dungeons%20%26%20Dragons%20Player%27s%20Handbook.jpg");

            migrationBuilder.DeleteData(
                table: "product_images",
                keyColumn: "ImageUrl",
                keyValue: "/img/Magic%20The%20Gathering%20-%20Foundations%20Starter%20Kit.jpg");

            migrationBuilder.DeleteData(
                table: "product_images",
                keyColumn: "ImageUrl",
                keyValue: "/img/Root.jpg");

            migrationBuilder.DeleteData(
                table: "product_images",
                keyColumn: "ImageUrl",
                keyValue: "/img/Scythe.jpg");

            migrationBuilder.DeleteData(
                table: "product_images",
                keyColumn: "ImageUrl",
                keyValue: "/img/Sherlock%20Holmes%20Consulting%20Detective.jpg");

            migrationBuilder.DeleteData(
                table: "product_images",
                keyColumn: "ImageUrl",
                keyValue: "/img/Wingspan.jpg");

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "Id", "AgeRating", "CreatedAt", "DeletedAt", "Description", "Name", "Price", "Stock", "UpdatedAt" },
                values: new object[,]
                {
                    { 20ul, "seven-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Collector's box with booster packs, card sleeves, energy cards, and condition markers.", "Pokemon TCG: Elite Trainer Box", 259.00m, 18u, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 21ul, "seven-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Three complete collectible dueling decks featuring iconic cards.", "Yu-Gi-Oh! Legendary Decks", 165.00m, 15u, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 22ul, "twelve-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Complete tabletop wargaming starter box with Space Marines, Tyranids, and terrain.", "Warhammer 40,000: Ultimate Starter Set", 749.00m, 4u, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 23ul, "twelve-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "High-pigment acrylic paints and starter brush for miniature hobbyists.", "Citadel Base Paint Set", 185.00m, 25u, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 24ul, "twelve-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Precision synthetic and sable brushes for basecoating, drybrushing, and fine details.", "Army Painter Wargamer Brush Set", 115.00m, 30u, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 25ul, "twelve-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Highly detailed pre-primed plastic dragon miniature for TTRPGs.", "Nolzur's Marvelous Unpainted Miniatures: Dragon", 145.00m, 10u, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 26ul, "twelve-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Snap-together 3D dungeon walls, doors, and pillars for tabletop encounters.", "Modular Dungeon Terrain Core Set", 320.00m, 8u, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 27ul, "three-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Durable polypropylene trading card sleeves with textured matte backing.", "Dragon Shield Matte Sleeves (100ct)", 55.00m, 50u, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 28ul, "three-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Portable PU leather and velvet tray to keep dice rolls contained.", "Foldable Velvet Dice Tray", 65.00m, 40u, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 29ul, "seven-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Premium interlocking jigsaw puzzle featuring a scenic fantasy kingdom.", "Ravensburger 1000pc Fantasy Castle Puzzle", 89.00m, 22u, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 30ul, "three-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Fast-paced pocket-sized pattern recognition game in a travel tin.", "Dobble (Spot It!)", 69.00m, 35u, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 31ul, "eighteen-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Adult party card game of kitty-powered Russian Roulette.", "Exploding Kittens: NSFW Edition", 109.00m, 19u, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 32ul, "sixteen-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Romantic comedy board game for two players where you create and play out a relationship.", "Fog of Love", 245.00m, 7u, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 33ul, "three-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Cooperative color-matching game with chunky wooden fruit pieces for young children.", "My First Orchard", 125.00m, 14u, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 34ul, "seven-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Boardless two-player abstract strategy game with bakelite tiles in a travel pouch.", "Hive Pocket", 119.00m, 17u, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 35ul, "three-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Official D20 shield enamel pin badge for backpacks and lanyards.", "GamersKeep Collector's Enamel Pin", 35.00m, 60u, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "product_categories",
                columns: new[] { "CategoryId", "ProductId", "CreatedAt", "DeletedAt", "UpdatedAt" },
                values: new object[,]
                {
                    { "trading-card-game", 20ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "trading-card-game", 21ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "miniatures", 22ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "sci-fi", 22ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "wargame", 22ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "painting-supplies", 23ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "painting-supplies", 24ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "fantasy", 25ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "miniatures", 25ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "t-t-r-p-g", 26ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "terrain", 26ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "accessory", 27ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "accessory", 28ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "fantasy", 29ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "puzzle", 29ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "children", 30ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "party", 30ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "travel-sized", 30ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "party", 31ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "board-game", 32ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "romance", 32ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "two-players", 32ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "children", 33ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "cooperative", 33ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "strategy", 34ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "travel-sized", 34ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "two-players", 34ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "other", 35ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });
        }
    }
}
