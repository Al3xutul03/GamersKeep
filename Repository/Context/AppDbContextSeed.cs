using Microsoft.EntityFrameworkCore;
using Repository.Entity;
using Repository.Enums.Types;

namespace Repository.Context;

/// <summary>
/// Provides initial seed data for testing and development.
/// </summary>
public static class AppDbContextSeed
{
    public static void SeedData(ModelBuilder modelBuilder)
    {
        var seedDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        // Pre-computed BCrypt hash for "Password123!"
        const string defaultPasswordHash = "$2a$11$K7Tq1Z9z8mJ9V6Q7X5R8uO1Y3W2E4R6T8Y0U2I4O6P8A0S2D4F6G8";

        // 1. Seed 5 Users (3 simple users, 2 administrators)
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Name = "AlexGamer",
                Email = "alex@gamerskeep.com",
                PasswordHash = defaultPasswordHash,
                IsAdmin = false,
                Settings = "{}",
                CreatedAt = seedDate,
                UpdatedAt = seedDate
            },
            new User
            {
                Id = 2,
                Name = "MariaBoard",
                Email = "maria@gamerskeep.com",
                PasswordHash = defaultPasswordHash,
                IsAdmin = false,
                Settings = "{}",
                CreatedAt = seedDate,
                UpdatedAt = seedDate
            },
            new User
            {
                Id = 3,
                Name = "DanDice",
                Email = "dan@gamerskeep.com",
                PasswordHash = defaultPasswordHash,
                IsAdmin = false,
                Settings = "{}",
                CreatedAt = seedDate,
                UpdatedAt = seedDate
            },
            new User
            {
                Id = 4,
                Name = "AdminMaster",
                Email = "admin1@gamerskeep.com",
                PasswordHash = defaultPasswordHash,
                IsAdmin = true,
                Settings = "{}",
                CreatedAt = seedDate,
                UpdatedAt = seedDate
            },
            new User
            {
                Id = 5,
                Name = "KeeperAdmin",
                Email = "admin2@gamerskeep.com",
                PasswordHash = defaultPasswordHash,
                IsAdmin = true,
                Settings = "{}",
                CreatedAt = seedDate,
                UpdatedAt = seedDate
            }
        );

        // 2. Seed all CategoryType entries (all 25 enum values)
        modelBuilder.Entity<Category>().HasData(
            new Category { Type = CategoryType.Strategy, Description = "Games focused on tactical planning, resource management, and strategic decision-making.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Type = CategoryType.CrimeAndMistery, Description = "Investigative games involving deduction, solving crimes, and uncovering secrets.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Type = CategoryType.Family, Description = "Accessible and engaging games suitable for players of all ages to enjoy together.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Type = CategoryType.Children, Description = "Simple, fun, and educational games designed specifically for younger players.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Type = CategoryType.TwoPlayers, Description = "Games designed or balanced specifically for head-to-head or cooperative two-player experiences.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Type = CategoryType.TravelSized, Description = "Compact and portable games ideal for playing on the go.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Type = CategoryType.Party, Description = "Social, fast-paced games built for larger groups and lively gatherings.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Type = CategoryType.Cooperative, Description = "Games where players work together as a team against the game itself.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Type = CategoryType.TTRPG, Description = "Tabletop Role-Playing Games including rulebooks, adventures, and dice.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Type = CategoryType.BoardGame, Description = "Classic and modern tabletop board games.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Type = CategoryType.TradingCardGame, Description = "Collectible and trading card games, booster packs, and starter decks.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Type = CategoryType.Wargame, Description = "Tabletop miniature wargaming sets, rules, and army expansions.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Type = CategoryType.Puzzle, Description = "Jigsaw puzzles, 3D puzzles, and logic brainteasers.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Type = CategoryType.Accessory, Description = "Gaming accessories including card sleeves, playmats, dice towers, and organizers.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Type = CategoryType.PaintingSupplies, Description = "Acrylic paints, brushes, primers, and hobby tools for miniature painting.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Type = CategoryType.Miniatures, Description = "Unpainted and pre-painted plastic, resin, and metal miniatures.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Type = CategoryType.Terrain, Description = "3D tabletop terrain, battle mats, and scenery for RPGs and wargames.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Type = CategoryType.Fantasy, Description = "Games set in magical worlds of dragons, dungeons, and mythical creatures.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Type = CategoryType.SciFi, Description = "Futuristic games featuring space exploration, cybernetics, and alien civilizations.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Type = CategoryType.Horror, Description = "Suspenseful and dark-themed games of survival, monsters, and the occult.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Type = CategoryType.Mystery, Description = "Enigmatic games centered around riddles, hidden identities, and suspense.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Type = CategoryType.Romance, Description = "Games focusing on relationships, dating sim mechanics, and storytelling.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Type = CategoryType.Adventure, Description = "Exploration-driven games with quests, storytelling, and character progression.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Type = CategoryType.Historical, Description = "Games inspired by real-world historical eras, battles, and civilizations.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Category { Type = CategoryType.Other, Description = "Miscellaneous tabletop items, novelty gifts, and merchandise.", CreatedAt = seedDate, UpdatedAt = seedDate }
        );

        // 3. Seed 35 Products (no ProductImages)
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Catan", AgeRating = AgeRatingType.SevenPlus, Price = 189.99m, Stock = 15, Description = "Trade, build, and settle the island of Catan in this modern classic strategy board game.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Product { Id = 2, Name = "Carcassonne", AgeRating = AgeRatingType.SevenPlus, Price = 149.50m, Stock = 20, Description = "Tile-placement game where players build medieval landscapes and deploy followers.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Product { Id = 3, Name = "Ticket to Ride: Europe", AgeRating = AgeRatingType.SevenPlus, Price = 219.00m, Stock = 12, Description = "Cross-country train adventure across historic European cities.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Product { Id = 4, Name = "Terraforming Mars", AgeRating = AgeRatingType.TwelvePlus, Price = 299.99m, Stock = 10, Description = "Compete as corporations to transform Mars into a habitable planet.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Product { Id = 5, Name = "Gloomhaven", AgeRating = AgeRatingType.SixteenPlus, Price = 699.00m, Stock = 5, Description = "Epic cooperative campaign of tactical dungeon-crawling in a persistent dark fantasy world.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Product { Id = 6, Name = "Pandemic", AgeRating = AgeRatingType.SevenPlus, Price = 179.90m, Stock = 18, Description = "Work together as a disease control team to save humanity from four deadly outbreaks.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Product { Id = 7, Name = "7 Wonders Duel", AgeRating = AgeRatingType.TwelvePlus, Price = 139.00m, Stock = 22, Description = "Head-to-head civilization building card game for two players.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Product { Id = 8, Name = "Codenames", AgeRating = AgeRatingType.TwelvePlus, Price = 95.00m, Stock = 30, Description = "Social word deduction party game of rival spymasters and secret agents.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Product { Id = 9, Name = "Dixit", AgeRating = AgeRatingType.SevenPlus, Price = 159.00m, Stock = 16, Description = "Creative storytelling game using beautifully illustrated surrealist cards.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Product { Id = 10, Name = "Azul", AgeRating = AgeRatingType.SevenPlus, Price = 195.00m, Stock = 14, Description = "Draft colorful ceramic tiles to decorate the Royal Palace of Evora.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Product { Id = 11, Name = "Wingspan", AgeRating = AgeRatingType.TwelvePlus, Price = 275.00m, Stock = 9, Description = "Engine-building board game about attracting diverse bird species to wildlife preserves.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Product { Id = 12, Name = "Scythe", AgeRating = AgeRatingType.SixteenPlus, Price = 399.00m, Stock = 7, Description = "Alternate-history 1920s strategy game of farming, mechs, and territory control.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Product { Id = 13, Name = "Root", AgeRating = AgeRatingType.TwelvePlus, Price = 289.00m, Stock = 11, Description = "Asymmetric woodland wargame of adventure and might.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Product { Id = 14, Name = "Arkham Horror: The Card Game", AgeRating = AgeRatingType.SixteenPlus, Price = 295.00m, Stock = 8, Description = "Cooperative living card game of cosmic horror and eldritch mystery.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Product { Id = 15, Name = "Sherlock Holmes Consulting Detective", AgeRating = AgeRatingType.TwelvePlus, Price = 235.00m, Stock = 6, Description = "Cooperative crime investigation game set in Victorian London.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Product { Id = 16, Name = "Dungeons & Dragons: Player's Handbook", AgeRating = AgeRatingType.TwelvePlus, Price = 229.00m, Stock = 25, Description = "Essential core rulebook for creating characters and playing the world's greatest TTRPG.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Product { Id = 17, Name = "Dungeons & Dragons: Monster Manual", AgeRating = AgeRatingType.TwelvePlus, Price = 229.00m, Stock = 20, Description = "Bestiary filled with iconic fantasy creatures, dragons, and villains.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Product { Id = 18, Name = "Call of Cthulhu: Keeper Rulebook", AgeRating = AgeRatingType.SixteenPlus, Price = 249.00m, Stock = 12, Description = "Core rulebook for Lovecraftian horror investigative roleplaying.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Product { Id = 19, Name = "Magic: The Gathering - Foundations Starter Kit", AgeRating = AgeRatingType.TwelvePlus, Price = 99.00m, Stock = 35, Description = "Two ready-to-play 60-card decks designed to learn Magic: The Gathering.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Product { Id = 20, Name = "Pokemon TCG: Elite Trainer Box", AgeRating = AgeRatingType.SevenPlus, Price = 259.00m, Stock = 18, Description = "Collector's box with booster packs, card sleeves, energy cards, and condition markers.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Product { Id = 21, Name = "Yu-Gi-Oh! Legendary Decks", AgeRating = AgeRatingType.SevenPlus, Price = 165.00m, Stock = 15, Description = "Three complete collectible dueling decks featuring iconic cards.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Product { Id = 22, Name = "Warhammer 40,000: Ultimate Starter Set", AgeRating = AgeRatingType.TwelvePlus, Price = 749.00m, Stock = 4, Description = "Complete tabletop wargaming starter box with Space Marines, Tyranids, and terrain.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Product { Id = 23, Name = "Citadel Base Paint Set", AgeRating = AgeRatingType.TwelvePlus, Price = 185.00m, Stock = 25, Description = "High-pigment acrylic paints and starter brush for miniature hobbyists.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Product { Id = 24, Name = "Army Painter Wargamer Brush Set", AgeRating = AgeRatingType.TwelvePlus, Price = 115.00m, Stock = 30, Description = "Precision synthetic and sable brushes for basecoating, drybrushing, and fine details.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Product { Id = 25, Name = "Nolzur's Marvelous Unpainted Miniatures: Dragon", AgeRating = AgeRatingType.TwelvePlus, Price = 145.00m, Stock = 10, Description = "Highly detailed pre-primed plastic dragon miniature for TTRPGs.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Product { Id = 26, Name = "Modular Dungeon Terrain Core Set", AgeRating = AgeRatingType.TwelvePlus, Price = 320.00m, Stock = 8, Description = "Snap-together 3D dungeon walls, doors, and pillars for tabletop encounters.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Product { Id = 27, Name = "Dragon Shield Matte Sleeves (100ct)", AgeRating = AgeRatingType.ThreePlus, Price = 55.00m, Stock = 50, Description = "Durable polypropylene trading card sleeves with textured matte backing.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Product { Id = 28, Name = "Foldable Velvet Dice Tray", AgeRating = AgeRatingType.ThreePlus, Price = 65.00m, Stock = 40, Description = "Portable PU leather and velvet tray to keep dice rolls contained.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Product { Id = 29, Name = "Ravensburger 1000pc Fantasy Castle Puzzle", AgeRating = AgeRatingType.SevenPlus, Price = 89.00m, Stock = 22, Description = "Premium interlocking jigsaw puzzle featuring a scenic fantasy kingdom.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Product { Id = 30, Name = "Dobble (Spot It!)", AgeRating = AgeRatingType.ThreePlus, Price = 69.00m, Stock = 35, Description = "Fast-paced pocket-sized pattern recognition game in a travel tin.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Product { Id = 31, Name = "Exploding Kittens: NSFW Edition", AgeRating = AgeRatingType.EighteenPlus, Price = 109.00m, Stock = 19, Description = "Adult party card game of kitty-powered Russian Roulette.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Product { Id = 32, Name = "Fog of Love", AgeRating = AgeRatingType.SixteenPlus, Price = 245.00m, Stock = 7, Description = "Romantic comedy board game for two players where you create and play out a relationship.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Product { Id = 33, Name = "My First Orchard", AgeRating = AgeRatingType.ThreePlus, Price = 125.00m, Stock = 14, Description = "Cooperative color-matching game with chunky wooden fruit pieces for young children.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Product { Id = 34, Name = "Hive Pocket", AgeRating = AgeRatingType.SevenPlus, Price = 119.00m, Stock = 17, Description = "Boardless two-player abstract strategy game with bakelite tiles in a travel pouch.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Product { Id = 35, Name = "GamersKeep Collector's Enamel Pin", AgeRating = AgeRatingType.ThreePlus, Price = 35.00m, Stock = 60, Description = "Official D20 shield enamel pin badge for backpacks and lanyards.", CreatedAt = seedDate, UpdatedAt = seedDate }
        );

        // 4. Seed ProductCategories (ensuring all 25 CategoryType values are linked to products)
        modelBuilder.Entity<ProductCategory>().HasData(
            new ProductCategory { ProductId = 1, CategoryId = CategoryType.BoardGame, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 1, CategoryId = CategoryType.Strategy, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 1, CategoryId = CategoryType.Family, CreatedAt = seedDate, UpdatedAt = seedDate },

            new ProductCategory { ProductId = 2, CategoryId = CategoryType.BoardGame, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 2, CategoryId = CategoryType.Family, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 2, CategoryId = CategoryType.Historical, CreatedAt = seedDate, UpdatedAt = seedDate },

            new ProductCategory { ProductId = 3, CategoryId = CategoryType.BoardGame, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 3, CategoryId = CategoryType.Family, CreatedAt = seedDate, UpdatedAt = seedDate },

            new ProductCategory { ProductId = 4, CategoryId = CategoryType.BoardGame, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 4, CategoryId = CategoryType.Strategy, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 4, CategoryId = CategoryType.SciFi, CreatedAt = seedDate, UpdatedAt = seedDate },

            new ProductCategory { ProductId = 5, CategoryId = CategoryType.BoardGame, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 5, CategoryId = CategoryType.Cooperative, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 5, CategoryId = CategoryType.Fantasy, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 5, CategoryId = CategoryType.Adventure, CreatedAt = seedDate, UpdatedAt = seedDate },

            new ProductCategory { ProductId = 6, CategoryId = CategoryType.BoardGame, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 6, CategoryId = CategoryType.Cooperative, CreatedAt = seedDate, UpdatedAt = seedDate },

            new ProductCategory { ProductId = 7, CategoryId = CategoryType.BoardGame, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 7, CategoryId = CategoryType.TwoPlayers, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 7, CategoryId = CategoryType.Strategy, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 7, CategoryId = CategoryType.Historical, CreatedAt = seedDate, UpdatedAt = seedDate },

            new ProductCategory { ProductId = 8, CategoryId = CategoryType.BoardGame, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 8, CategoryId = CategoryType.Party, CreatedAt = seedDate, UpdatedAt = seedDate },

            new ProductCategory { ProductId = 9, CategoryId = CategoryType.BoardGame, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 9, CategoryId = CategoryType.Party, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 9, CategoryId = CategoryType.Family, CreatedAt = seedDate, UpdatedAt = seedDate },

            new ProductCategory { ProductId = 10, CategoryId = CategoryType.BoardGame, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 10, CategoryId = CategoryType.Family, CreatedAt = seedDate, UpdatedAt = seedDate },

            new ProductCategory { ProductId = 11, CategoryId = CategoryType.BoardGame, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 11, CategoryId = CategoryType.Strategy, CreatedAt = seedDate, UpdatedAt = seedDate },

            new ProductCategory { ProductId = 12, CategoryId = CategoryType.BoardGame, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 12, CategoryId = CategoryType.Strategy, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 12, CategoryId = CategoryType.SciFi, CreatedAt = seedDate, UpdatedAt = seedDate },

            new ProductCategory { ProductId = 13, CategoryId = CategoryType.BoardGame, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 13, CategoryId = CategoryType.Strategy, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 13, CategoryId = CategoryType.Adventure, CreatedAt = seedDate, UpdatedAt = seedDate },

            new ProductCategory { ProductId = 14, CategoryId = CategoryType.BoardGame, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 14, CategoryId = CategoryType.Horror, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 14, CategoryId = CategoryType.Mystery, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 14, CategoryId = CategoryType.Cooperative, CreatedAt = seedDate, UpdatedAt = seedDate },

            new ProductCategory { ProductId = 15, CategoryId = CategoryType.BoardGame, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 15, CategoryId = CategoryType.CrimeAndMistery, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 15, CategoryId = CategoryType.Mystery, CreatedAt = seedDate, UpdatedAt = seedDate },

            new ProductCategory { ProductId = 16, CategoryId = CategoryType.TTRPG, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 16, CategoryId = CategoryType.Fantasy, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 16, CategoryId = CategoryType.Adventure, CreatedAt = seedDate, UpdatedAt = seedDate },

            new ProductCategory { ProductId = 17, CategoryId = CategoryType.TTRPG, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 17, CategoryId = CategoryType.Fantasy, CreatedAt = seedDate, UpdatedAt = seedDate },

            new ProductCategory { ProductId = 18, CategoryId = CategoryType.TTRPG, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 18, CategoryId = CategoryType.Horror, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 18, CategoryId = CategoryType.Mystery, CreatedAt = seedDate, UpdatedAt = seedDate },

            new ProductCategory { ProductId = 19, CategoryId = CategoryType.TradingCardGame, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 19, CategoryId = CategoryType.Fantasy, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 19, CategoryId = CategoryType.TwoPlayers, CreatedAt = seedDate, UpdatedAt = seedDate },

            new ProductCategory { ProductId = 20, CategoryId = CategoryType.TradingCardGame, CreatedAt = seedDate, UpdatedAt = seedDate },

            new ProductCategory { ProductId = 21, CategoryId = CategoryType.TradingCardGame, CreatedAt = seedDate, UpdatedAt = seedDate },

            new ProductCategory { ProductId = 22, CategoryId = CategoryType.Wargame, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 22, CategoryId = CategoryType.Miniatures, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 22, CategoryId = CategoryType.SciFi, CreatedAt = seedDate, UpdatedAt = seedDate },

            new ProductCategory { ProductId = 23, CategoryId = CategoryType.PaintingSupplies, CreatedAt = seedDate, UpdatedAt = seedDate },

            new ProductCategory { ProductId = 24, CategoryId = CategoryType.PaintingSupplies, CreatedAt = seedDate, UpdatedAt = seedDate },

            new ProductCategory { ProductId = 25, CategoryId = CategoryType.Miniatures, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 25, CategoryId = CategoryType.Fantasy, CreatedAt = seedDate, UpdatedAt = seedDate },

            new ProductCategory { ProductId = 26, CategoryId = CategoryType.Terrain, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 26, CategoryId = CategoryType.TTRPG, CreatedAt = seedDate, UpdatedAt = seedDate },

            new ProductCategory { ProductId = 27, CategoryId = CategoryType.Accessory, CreatedAt = seedDate, UpdatedAt = seedDate },

            new ProductCategory { ProductId = 28, CategoryId = CategoryType.Accessory, CreatedAt = seedDate, UpdatedAt = seedDate },

            new ProductCategory { ProductId = 29, CategoryId = CategoryType.Puzzle, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 29, CategoryId = CategoryType.Fantasy, CreatedAt = seedDate, UpdatedAt = seedDate },

            new ProductCategory { ProductId = 30, CategoryId = CategoryType.Party, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 30, CategoryId = CategoryType.TravelSized, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 30, CategoryId = CategoryType.Children, CreatedAt = seedDate, UpdatedAt = seedDate },

            new ProductCategory { ProductId = 31, CategoryId = CategoryType.Party, CreatedAt = seedDate, UpdatedAt = seedDate },

            new ProductCategory { ProductId = 32, CategoryId = CategoryType.BoardGame, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 32, CategoryId = CategoryType.Romance, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 32, CategoryId = CategoryType.TwoPlayers, CreatedAt = seedDate, UpdatedAt = seedDate },

            new ProductCategory { ProductId = 33, CategoryId = CategoryType.Children, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 33, CategoryId = CategoryType.Cooperative, CreatedAt = seedDate, UpdatedAt = seedDate },

            new ProductCategory { ProductId = 34, CategoryId = CategoryType.TwoPlayers, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 34, CategoryId = CategoryType.TravelSized, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 34, CategoryId = CategoryType.Strategy, CreatedAt = seedDate, UpdatedAt = seedDate },

            new ProductCategory { ProductId = 35, CategoryId = CategoryType.Other, CreatedAt = seedDate, UpdatedAt = seedDate }
        );
    }
}
