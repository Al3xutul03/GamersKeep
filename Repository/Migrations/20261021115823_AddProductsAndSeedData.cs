using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddProductsAndSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    Type = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "text", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.Type);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    Id = table.Column<ulong>(type: "bigint unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AgeRating = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "text", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "product_categories",
                columns: table => new
                {
                    ProductId = table.Column<ulong>(type: "bigint unsigned", nullable: false),
                    CategoryId = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_categories", x => new { x.ProductId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_product_categories_categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "categories",
                        principalColumn: "Type",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_categories_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "product_images",
                columns: table => new
                {
                    ImageUrl = table.Column<string>(type: "varchar(768)", maxLength: 768, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProductId = table.Column<ulong>(type: "bigint unsigned", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_images", x => x.ImageUrl);
                    table.ForeignKey(
                        name: "FK_product_images_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "Type", "CreatedAt", "DeletedAt", "Description", "UpdatedAt" },
                values: new object[,]
                {
                    { "accessory", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Gaming accessories including card sleeves, playmats, dice towers, and organizers.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "adventure", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Exploration-driven games with quests, storytelling, and character progression.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "board-game", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Classic and modern tabletop board games.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "children", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Simple, fun, and educational games designed specifically for younger players.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "cooperative", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Games where players work together as a team against the game itself.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "crime-and-mistery", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Investigative games involving deduction, solving crimes, and uncovering secrets.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "family", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Accessible and engaging games suitable for players of all ages to enjoy together.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "fantasy", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Games set in magical worlds of dragons, dungeons, and mythical creatures.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "historical", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Games inspired by real-world historical eras, battles, and civilizations.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "horror", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Suspenseful and dark-themed games of survival, monsters, and the occult.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "miniatures", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Unpainted and pre-painted plastic, resin, and metal miniatures.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "mystery", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Enigmatic games centered around riddles, hidden identities, and suspense.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "other", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Miscellaneous tabletop items, novelty gifts, and merchandise.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "painting-supplies", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Acrylic paints, brushes, primers, and hobby tools for miniature painting.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "party", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Social, fast-paced games built for larger groups and lively gatherings.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "puzzle", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Jigsaw puzzles, 3D puzzles, and logic brainteasers.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "romance", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Games focusing on relationships, dating sim mechanics, and storytelling.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "sci-fi", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Futuristic games featuring space exploration, cybernetics, and alien civilizations.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "strategy", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Games focused on tactical planning, resource management, and strategic decision-making.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "t-t-r-p-g", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Tabletop Role-Playing Games including rulebooks, adventures, and dice.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "terrain", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "3D tabletop terrain, battle mats, and scenery for RPGs and wargames.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "trading-card-game", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Collectible and trading card games, booster packs, and starter decks.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "travel-sized", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Compact and portable games ideal for playing on the go.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "two-players", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Games designed or balanced specifically for head-to-head or cooperative two-player experiences.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "wargame", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Tabletop miniature wargaming sets, rules, and army expansions.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "Id", "AgeRating", "CreatedAt", "DeletedAt", "Description", "Name", "Price", "UpdatedAt" },
                values: new object[,]
                {
                    { 1ul, "seven-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Trade, build, and settle the island of Catan in this modern classic strategy board game.", "Catan", 189.99m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2ul, "seven-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Tile-placement game where players build medieval landscapes and deploy followers.", "Carcassonne", 149.50m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3ul, "seven-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Cross-country train adventure across historic European cities.", "Ticket to Ride: Europe", 219.00m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 4ul, "twelve-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Compete as corporations to transform Mars into a habitable planet.", "Terraforming Mars", 299.99m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5ul, "sixteen-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Epic cooperative campaign of tactical dungeon-crawling in a persistent dark fantasy world.", "Gloomhaven", 699.00m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 6ul, "seven-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Work together as a disease control team to save humanity from four deadly outbreaks.", "Pandemic", 179.90m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 7ul, "twelve-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Head-to-head civilization building card game for two players.", "7 Wonders Duel", 139.00m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 8ul, "twelve-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Social word deduction party game of rival spymasters and secret agents.", "Codenames", 95.00m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 9ul, "seven-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Creative storytelling game using beautifully illustrated surrealist cards.", "Dixit", 159.00m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 10ul, "seven-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Draft colorful ceramic tiles to decorate the Royal Palace of Evora.", "Azul", 195.00m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 11ul, "twelve-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Engine-building board game about attracting diverse bird species to wildlife preserves.", "Wingspan", 275.00m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 12ul, "sixteen-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Alternate-history 1920s strategy game of farming, mechs, and territory control.", "Scythe", 399.00m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 13ul, "twelve-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Asymmetric woodland wargame of adventure and might.", "Root", 289.00m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 14ul, "sixteen-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Cooperative living card game of cosmic horror and eldritch mystery.", "Arkham Horror: The Card Game", 295.00m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 15ul, "twelve-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Cooperative crime investigation game set in Victorian London.", "Sherlock Holmes Consulting Detective", 235.00m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 16ul, "twelve-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Essential core rulebook for creating characters and playing the world's greatest TTRPG.", "Dungeons & Dragons: Player's Handbook", 229.00m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 17ul, "twelve-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Bestiary filled with iconic fantasy creatures, dragons, and villains.", "Dungeons & Dragons: Monster Manual", 229.00m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 18ul, "sixteen-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Core rulebook for Lovecraftian horror investigative roleplaying.", "Call of Cthulhu: Keeper Rulebook", 249.00m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 19ul, "twelve-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Two ready-to-play 60-card decks designed to learn Magic: The Gathering.", "Magic: The Gathering - Foundations Starter Kit", 99.00m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 20ul, "seven-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Collector's box with booster packs, card sleeves, energy cards, and condition markers.", "Pokemon TCG: Elite Trainer Box", 259.00m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 21ul, "seven-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Three complete collectible dueling decks featuring iconic cards.", "Yu-Gi-Oh! Legendary Decks", 165.00m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 22ul, "twelve-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Complete tabletop wargaming starter box with Space Marines, Tyranids, and terrain.", "Warhammer 40,000: Ultimate Starter Set", 749.00m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 23ul, "twelve-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "High-pigment acrylic paints and starter brush for miniature hobbyists.", "Citadel Base Paint Set", 185.00m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 24ul, "twelve-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Precision synthetic and sable brushes for basecoating, drybrushing, and fine details.", "Army Painter Wargamer Brush Set", 115.00m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 25ul, "twelve-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Highly detailed pre-primed plastic dragon miniature for TTRPGs.", "Nolzur's Marvelous Unpainted Miniatures: Dragon", 145.00m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 26ul, "twelve-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Snap-together 3D dungeon walls, doors, and pillars for tabletop encounters.", "Modular Dungeon Terrain Core Set", 320.00m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 27ul, "three-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Durable polypropylene trading card sleeves with textured matte backing.", "Dragon Shield Matte Sleeves (100ct)", 55.00m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 28ul, "three-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Portable PU leather and velvet tray to keep dice rolls contained.", "Foldable Velvet Dice Tray", 65.00m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 29ul, "seven-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Premium interlocking jigsaw puzzle featuring a scenic fantasy kingdom.", "Ravensburger 1000pc Fantasy Castle Puzzle", 89.00m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 30ul, "three-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Fast-paced pocket-sized pattern recognition game in a travel tin.", "Dobble (Spot It!)", 69.00m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 31ul, "eighteen-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Adult party card game of kitty-powered Russian Roulette.", "Exploding Kittens: NSFW Edition", 109.00m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 32ul, "sixteen-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Romantic comedy board game for two players where you create and play out a relationship.", "Fog of Love", 245.00m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 33ul, "three-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Cooperative color-matching game with chunky wooden fruit pieces for young children.", "My First Orchard", 125.00m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 34ul, "seven-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Boardless two-player abstract strategy game with bakelite tiles in a travel pouch.", "Hive Pocket", 119.00m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 35ul, "three-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Official D20 shield enamel pin badge for backpacks and lanyards.", "GamersKeep Collector's Enamel Pin", 35.00m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Email", "Name", "PasswordHash", "RefreshToken", "RefreshTokenExpiryTime", "Settings", "UpdatedAt" },
                values: new object[,]
                {
                    { 1ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "alex@gamerskeep.com", "AlexGamer", "$2a$11$K7Tq1Z9z8mJ9V6Q7X5R8uO1Y3W2E4R6T8Y0U2I4O6P8A0S2D4F6G8", null, null, "{}", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "maria@gamerskeep.com", "MariaBoard", "$2a$11$K7Tq1Z9z8mJ9V6Q7X5R8uO1Y3W2E4R6T8Y0U2I4O6P8A0S2D4F6G8", null, null, "{}", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "dan@gamerskeep.com", "DanDice", "$2a$11$K7Tq1Z9z8mJ9V6Q7X5R8uO1Y3W2E4R6T8Y0U2I4O6P8A0S2D4F6G8", null, null, "{}", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Email", "IsAdmin", "Name", "PasswordHash", "RefreshToken", "RefreshTokenExpiryTime", "Settings", "UpdatedAt" },
                values: new object[,]
                {
                    { 4ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "admin1@gamerskeep.com", true, "AdminMaster", "$2a$11$K7Tq1Z9z8mJ9V6Q7X5R8uO1Y3W2E4R6T8Y0U2I4O6P8A0S2D4F6G8", null, null, "{}", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "admin2@gamerskeep.com", true, "KeeperAdmin", "$2a$11$K7Tq1Z9z8mJ9V6Q7X5R8uO1Y3W2E4R6T8Y0U2I4O6P8A0S2D4F6G8", null, null, "{}", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "product_categories",
                columns: new[] { "CategoryId", "ProductId", "CreatedAt", "DeletedAt", "UpdatedAt" },
                values: new object[,]
                {
                    { "board-game", 1ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "family", 1ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "strategy", 1ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "board-game", 2ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "family", 2ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "historical", 2ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "board-game", 3ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "family", 3ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "board-game", 4ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "sci-fi", 4ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "strategy", 4ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "adventure", 5ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "board-game", 5ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "cooperative", 5ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "fantasy", 5ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "board-game", 6ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "cooperative", 6ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "board-game", 7ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "historical", 7ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "strategy", 7ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "two-players", 7ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "board-game", 8ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "party", 8ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "board-game", 9ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "family", 9ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "party", 9ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "board-game", 10ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "family", 10ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "board-game", 11ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "strategy", 11ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "board-game", 12ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "sci-fi", 12ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "strategy", 12ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "adventure", 13ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "board-game", 13ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "strategy", 13ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "board-game", 14ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "cooperative", 14ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "horror", 14ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "mystery", 14ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "board-game", 15ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "crime-and-mistery", 15ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "mystery", 15ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "adventure", 16ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "fantasy", 16ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "t-t-r-p-g", 16ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "fantasy", 17ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "t-t-r-p-g", 17ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "horror", 18ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "mystery", 18ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "t-t-r-p-g", 18ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "fantasy", 19ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "trading-card-game", 19ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "two-players", 19ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
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

            migrationBuilder.CreateIndex(
                name: "IX_product_categories_CategoryId",
                table: "product_categories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_product_images_ProductId",
                table: "product_images",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "product_categories");

            migrationBuilder.DropTable(
                name: "product_images");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1ul);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: 2ul);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: 3ul);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: 4ul);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: 5ul);
        }
    }
}
