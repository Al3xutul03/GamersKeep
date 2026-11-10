using Microsoft.EntityFrameworkCore;
using Repository.Entity;
using Repository.Enums.Types;

namespace Repository.Context;

public static class AppDbContextSeed
{
    public static void SeedData(ModelBuilder modelBuilder)
    {
        var seedDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        const string defaultPasswordHash = "$2a$11$K7Tq1Z9z8mJ9V6Q7X5R8uO1Y3W2E4R6T8Y0U2I4O6P8A0S2D4F6G8";

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
            new Product { Id = 20, Name = "Huse pentru cărți – Roșu", AgeRating = AgeRatingType.ThreePlus, Price = 49.00m, Stock = 50, Description = "Set de 100 de huse mate de culoare roșie, care protejează cărțile de joc de uzură și zgârieturi.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Product { Id = 21, Name = "Tavă de zaruri din catifea", AgeRating = AgeRatingType.ThreePlus, Price = 69.00m, Stock = 40, Description = "Tavă pliabilă căptușită cu catifea, care ține zarurile pe masă și reduce zgomotul aruncărilor.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Product { Id = 22, Name = "Cutie de depozitare pentru cărți", AgeRating = AgeRatingType.ThreePlus, Price = 79.00m, Stock = 30, Description = "Cutie rigidă cu capac magnetic, în care încap până la 400 de cărți protejate cu huse.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Product { Id = 24, Name = "Set de 7 zaruri poliedrice", AgeRating = AgeRatingType.TwelvePlus, Price = 59.00m, Stock = 60, Description = "Set complet de zaruri pentru jocuri de rol (d4, d6, d8, d10, d%, d12 și d20), cu pungă de transport.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Product { Id = 25, Name = "Paravan pentru Dungeon Master", AgeRating = AgeRatingType.TwelvePlus, Price = 119.00m, Stock = 20, Description = "Paravan pliabil cu trei panouri, care ascunde notițele Maestrului de Joc și are tabele de reguli pe partea dinspre Maestru.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Product { Id = 26, Name = "Caiete cu fișe de personaj", AgeRating = AgeRatingType.TwelvePlus, Price = 45.00m, Stock = 40, Description = "Bloc cu 30 de fișe de personaj pentru jocuri de rol fantasy, tipărite pe hârtie groasă.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Product { Id = 29, Name = "Set de pensule pentru miniaturi", AgeRating = AgeRatingType.TwelvePlus, Price = 95.00m, Stock = 30, Description = "Set de 5 pensule cu fire sintetice, pentru acoperire, uscat și detalii fine.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Product { Id = 30, Name = "Miniatură nevopsită: Dragon", AgeRating = AgeRatingType.TwelvePlus, Price = 145.00m, Stock = 12, Description = "Miniatură detaliată din plastic, pregătită pentru pictat, ideală pentru aventuri fantasy.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Product { Id = 33, Name = "Cană cu temă de zaruri", AgeRating = AgeRatingType.ThreePlus, Price = 55.00m, Stock = 40, Description = "Cană ceramică de 330 ml, decorată cu zaruri poliedrice.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Product { Id = 34, Name = "Tricou Gamer's Keep", AgeRating = AgeRatingType.ThreePlus, Price = 99.00m, Stock = 35, Description = "Tricou din bumbac, cu emblema magazinului.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Product { Id = 35, Name = "Poster cu o hartă fantasy", AgeRating = AgeRatingType.ThreePlus, Price = 69.00m, Stock = 30, Description = "Poster A2 cu o hartă fantasy desenată în detaliu, potrivit pentru camera de joc.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Product { Id = 36, Name = "Huse pentru cărți – Albastru", AgeRating = AgeRatingType.ThreePlus, Price = 49.00m, Stock = 40, Description = "Set de 100 de huse mate de culoare albastră, care protejează cărțile de joc de uzură și zgârieturi.", CreatedAt = seedDate, UpdatedAt = seedDate },
            new Product { Id = 37, Name = "Huse pentru cărți – Verde", AgeRating = AgeRatingType.ThreePlus, Price = 49.00m, Stock = 40, Description = "Set de 100 de huse mate de culoare verde, care protejează cărțile de joc de uzură și zgârieturi.", CreatedAt = seedDate, UpdatedAt = seedDate }
        );

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

            new ProductCategory { ProductId = 20, CategoryId = CategoryType.Accessory, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 21, CategoryId = CategoryType.Accessory, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 22, CategoryId = CategoryType.Accessory, CreatedAt = seedDate, UpdatedAt = seedDate },

            new ProductCategory { ProductId = 24, CategoryId = CategoryType.TTRPG, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 24, CategoryId = CategoryType.Accessory, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 25, CategoryId = CategoryType.TTRPG, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 25, CategoryId = CategoryType.Accessory, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 26, CategoryId = CategoryType.TTRPG, CreatedAt = seedDate, UpdatedAt = seedDate },

            new ProductCategory { ProductId = 29, CategoryId = CategoryType.PaintingSupplies, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 30, CategoryId = CategoryType.Miniatures, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 30, CategoryId = CategoryType.Fantasy, CreatedAt = seedDate, UpdatedAt = seedDate },

            new ProductCategory { ProductId = 33, CategoryId = CategoryType.Other, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 34, CategoryId = CategoryType.Other, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 35, CategoryId = CategoryType.Other, CreatedAt = seedDate, UpdatedAt = seedDate },

            new ProductCategory { ProductId = 36, CategoryId = CategoryType.Accessory, CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductCategory { ProductId = 37, CategoryId = CategoryType.Accessory, CreatedAt = seedDate, UpdatedAt = seedDate }
        );

        modelBuilder.Entity<DeliveryMethod>().HasData(
            new DeliveryMethod
            {
                Id = 1,
                Name = "Curier standard",
                Description = "Livrare la adresă prin curier.",
                EstimatedDelivery = "2-4 zile lucrătoare",
                Price = 15.00m,
                IsActive = true,
                SortOrder = 1,
                CreatedAt = seedDate,
                UpdatedAt = seedDate
            },
            new DeliveryMethod
            {
                Id = 2,
                Name = "Curier express",
                Description = "Livrare prioritară la adresă prin curier.",
                EstimatedDelivery = "1 zi lucrătoare",
                Price = 25.00m,
                IsActive = true,
                SortOrder = 2,
                CreatedAt = seedDate,
                UpdatedAt = seedDate
            }
        );

        modelBuilder.Entity<ProductImage>().HasData(
            new ProductImage { ProductId = 1, ImageUrl = "/img/Catan.jpg", CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductImage { ProductId = 2, ImageUrl = "/img/Carcassonne.jpg", CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductImage { ProductId = 3, ImageUrl = "/img/Ticket%20to%20Ride%20Europe.jpg", CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductImage { ProductId = 4, ImageUrl = "/img/Terraforming%20Mars.jpg", CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductImage { ProductId = 5, ImageUrl = "/img/Gloomhaven.jpg", CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductImage { ProductId = 6, ImageUrl = "/img/Pandemic.jpg", CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductImage { ProductId = 7, ImageUrl = "/img/7%20Wonders%20Duel.jpg", CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductImage { ProductId = 8, ImageUrl = "/img/Codenames.jpg", CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductImage { ProductId = 9, ImageUrl = "/img/Dixit.jpg", CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductImage { ProductId = 10, ImageUrl = "/img/Azul.jpg", CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductImage { ProductId = 11, ImageUrl = "/img/Wingspan.jpg", CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductImage { ProductId = 12, ImageUrl = "/img/Scythe.jpg", CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductImage { ProductId = 13, ImageUrl = "/img/Root.jpg", CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductImage { ProductId = 14, ImageUrl = "/img/Arkham%20Horror%20The%20Card%20Game.jpg", CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductImage { ProductId = 15, ImageUrl = "/img/Sherlock%20Holmes%20Consulting%20Detective.jpg", CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductImage { ProductId = 16, ImageUrl = "/img/Dungeons%20%26%20Dragons%20Player%27s%20Handbook.jpg", CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductImage { ProductId = 17, ImageUrl = "/img/Dungeons%20%26%20Dragons%20Monster%20Manual.jpg", CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductImage { ProductId = 18, ImageUrl = "/img/Call%20of%20Cthulhu%20Keeper%20Rulebook.jpg", CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductImage { ProductId = 19, ImageUrl = "/img/Magic%20The%20Gathering%20-%20Foundations%20Starter%20Kit.jpg", CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductImage { ProductId = 20, ImageUrl = "/img/Carti Rosii.jpg", CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductImage { ProductId = 21, ImageUrl = "/img/Tavă de zaruri din catifea.jpg", CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductImage { ProductId = 22, ImageUrl = "/img/Cutie de depozitare pentru cărți.jpg", CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductImage { ProductId = 24, ImageUrl = "/img/Set de 7 zaruri poliedrice.jpg", CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductImage { ProductId = 25, ImageUrl = "/img/Paravan pentru Dungeon Master.jpg", CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductImage { ProductId = 26, ImageUrl = "/img/Caiete cu fișe de personaj.jpg", CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductImage { ProductId = 29, ImageUrl = "/img/Set de pensule pentru miniaturi.jpg", CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductImage { ProductId = 30, ImageUrl = "/img/Miniatură nevopsită Dragon.jpg", CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductImage { ProductId = 33, ImageUrl = "/img/Cană cu temă de zaruri.jpg", CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductImage { ProductId = 34, ImageUrl = "/img/Tricou Gamer’s Keep.jpg", CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductImage { ProductId = 35, ImageUrl = "/img/Poster cu o hartă fantasy.jpg", CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductImage { ProductId = 36, ImageUrl = "/img/Carti Albastre.jpg", CreatedAt = seedDate, UpdatedAt = seedDate },
            new ProductImage { ProductId = 37, ImageUrl = "/img/Carti Verzi.jpg", CreatedAt = seedDate, UpdatedAt = seedDate }
        );
    }
}