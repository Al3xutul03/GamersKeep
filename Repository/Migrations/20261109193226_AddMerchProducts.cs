using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddMerchProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "Id", "AgeRating", "CreatedAt", "DeletedAt", "Description", "Name", "Price", "Stock", "UpdatedAt" },
                values: new object[,]
                {
                    { 20ul, "three-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Huse transparente cu spate mat, care protejează cărțile de joc de uzură și zgârieturi.", "Huse pentru cărți (100 buc.)", 49.00m, 50u, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 21ul, "three-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Tavă pliabilă căptușită cu catifea, care ține zarurile pe masă și reduce zgomotul aruncărilor.", "Tavă de zaruri din catifea", 69.00m, 40u, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 22ul, "three-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Cutie rigidă cu capac magnetic, în care încap până la 400 de cărți protejate cu huse.", "Cutie de depozitare pentru cărți", 79.00m, 30u, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 23ul, "three-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Organizator cu compartimente reglabile pentru piese, jetoane și zaruri, ideal pentru jocurile cu multe componente.", "Organizator de piese și jetoane", 89.00m, 25u, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 24ul, "twelve-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Set complet de zaruri pentru jocuri de rol (d4, d6, d8, d10, d%, d12 și d20), cu pungă de transport.", "Set de 7 zaruri poliedrice", 59.00m, 60u, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 25ul, "twelve-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Paravan pliabil cu trei panouri, care ascunde notițele Maestrului de Joc și are tabele de reguli pe partea dinspre Maestru.", "Paravan pentru Dungeon Master", 119.00m, 20u, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 26ul, "twelve-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Bloc cu 30 de fișe de personaj pentru jocuri de rol fantasy, tipărite pe hârtie groasă.", "Caiete cu fișe de personaj", 45.00m, 40u, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 27ul, "twelve-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Hartă cu grilă de 2,5 cm, pe care se scrie cu markere lavabile; potrivită pentru aventuri și bătălii cu miniaturi.", "Hartă-grilă reutilizabilă", 129.00m, 18u, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 28ul, "twelve-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Set de 12 culori acrilice cu pigmentare ridicată, pentru baza și detaliile miniaturilor.", "Set de vopsele acrilice", 185.00m, 25u, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 29ul, "twelve-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Set de 5 pensule cu fire sintetice, pentru acoperire, uscat și detalii fine.", "Set de pensule pentru miniaturi", 95.00m, 30u, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 30ul, "twelve-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Miniatură detaliată din plastic, pregătită pentru pictat, ideală pentru aventuri fantasy.", "Miniatură nevopsită: Dragon", 145.00m, 12u, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 31ul, "twelve-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Pereți, uși și coloane care se îmbină pentru a construi încăperi de dungeon pe masa de joc.", "Teren modular de dungeon", 320.00m, 10u, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 32ul, "three-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Insignă emailată în formă de zar cu 20 de fețe, pentru rucsac, geacă sau curea.", "Pin emailat D20", 35.00m, 60u, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 33ul, "three-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Cană ceramică de 330 ml, decorată cu zaruri poliedrice.", "Cană cu temă de zaruri", 55.00m, 40u, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 34ul, "three-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Tricou din bumbac, cu emblema magazinului.", "Tricou Gamer's Keep", 99.00m, 35u, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 35ul, "three-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Poster A2 cu o hartă fantasy desenată în detaliu, potrivit pentru camera de joc.", "Poster cu o hartă fantasy", 69.00m, 30u, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "product_categories",
                columns: new[] { "CategoryId", "ProductId", "CreatedAt", "DeletedAt", "UpdatedAt" },
                values: new object[,]
                {
                    { "accessory", 20ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "accessory", 21ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "accessory", 22ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "accessory", 23ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "accessory", 24ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "t-t-r-p-g", 24ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "accessory", 25ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "t-t-r-p-g", 25ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "t-t-r-p-g", 26ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "t-t-r-p-g", 27ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "terrain", 27ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "miniatures", 28ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "painting-supplies", 28ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "painting-supplies", 29ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "fantasy", 30ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "miniatures", 30ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "miniatures", 31ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "terrain", 31ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "other", 32ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "other", 33ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "other", 34ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "other", 35ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "product_images",
                columns: new[] { "ImageUrl", "CreatedAt", "DeletedAt", "ProductId", "UpdatedAt" },
                values: new object[,]
                {
                    { "/img/Caiete%20fise%20de%20personaj.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 26ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Cana%20cu%20zaruri.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 33ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Cutie%20depozitare%20carti.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 22ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Harta%20grila%20reutilizabila.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 27ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Huse%20pentru%20carti.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 20ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Miniatura%20dragon.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 30ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Organizator%20piese%20si%20jetoane.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 23ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Paravan%20Dungeon%20Master.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 25ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Pin%20D20.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 32ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Poster%20harta%20fantasy.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 35ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Set%20pensule.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 29ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Set%20vopsele%20acrilice.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 28ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Set%20zaruri%20poliedrice.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 24ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Tava%20de%20zaruri.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 21ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Teren%20modular%20dungeon.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 31ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Tricou%20Gamers%20Keep.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 34ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "product_categories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { "accessory", 20ul });

            migrationBuilder.DeleteData(
                table: "product_categories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { "accessory", 21ul });

            migrationBuilder.DeleteData(
                table: "product_categories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { "accessory", 22ul });

            migrationBuilder.DeleteData(
                table: "product_categories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { "accessory", 23ul });

            migrationBuilder.DeleteData(
                table: "product_categories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { "accessory", 24ul });

            migrationBuilder.DeleteData(
                table: "product_categories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { "t-t-r-p-g", 24ul });

            migrationBuilder.DeleteData(
                table: "product_categories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { "accessory", 25ul });

            migrationBuilder.DeleteData(
                table: "product_categories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { "t-t-r-p-g", 25ul });

            migrationBuilder.DeleteData(
                table: "product_categories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { "t-t-r-p-g", 26ul });

            migrationBuilder.DeleteData(
                table: "product_categories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { "t-t-r-p-g", 27ul });

            migrationBuilder.DeleteData(
                table: "product_categories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { "terrain", 27ul });

            migrationBuilder.DeleteData(
                table: "product_categories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { "miniatures", 28ul });

            migrationBuilder.DeleteData(
                table: "product_categories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { "painting-supplies", 28ul });

            migrationBuilder.DeleteData(
                table: "product_categories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { "painting-supplies", 29ul });

            migrationBuilder.DeleteData(
                table: "product_categories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { "fantasy", 30ul });

            migrationBuilder.DeleteData(
                table: "product_categories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { "miniatures", 30ul });

            migrationBuilder.DeleteData(
                table: "product_categories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { "miniatures", 31ul });

            migrationBuilder.DeleteData(
                table: "product_categories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { "terrain", 31ul });

            migrationBuilder.DeleteData(
                table: "product_categories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { "other", 32ul });

            migrationBuilder.DeleteData(
                table: "product_categories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { "other", 33ul });

            migrationBuilder.DeleteData(
                table: "product_categories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { "other", 34ul });

            migrationBuilder.DeleteData(
                table: "product_categories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { "other", 35ul });

            migrationBuilder.DeleteData(
                table: "product_images",
                keyColumn: "ImageUrl",
                keyValue: "/img/Caiete%20fise%20de%20personaj.jpg");

            migrationBuilder.DeleteData(
                table: "product_images",
                keyColumn: "ImageUrl",
                keyValue: "/img/Cana%20cu%20zaruri.jpg");

            migrationBuilder.DeleteData(
                table: "product_images",
                keyColumn: "ImageUrl",
                keyValue: "/img/Cutie%20depozitare%20carti.jpg");

            migrationBuilder.DeleteData(
                table: "product_images",
                keyColumn: "ImageUrl",
                keyValue: "/img/Harta%20grila%20reutilizabila.jpg");

            migrationBuilder.DeleteData(
                table: "product_images",
                keyColumn: "ImageUrl",
                keyValue: "/img/Huse%20pentru%20carti.jpg");

            migrationBuilder.DeleteData(
                table: "product_images",
                keyColumn: "ImageUrl",
                keyValue: "/img/Miniatura%20dragon.jpg");

            migrationBuilder.DeleteData(
                table: "product_images",
                keyColumn: "ImageUrl",
                keyValue: "/img/Organizator%20piese%20si%20jetoane.jpg");

            migrationBuilder.DeleteData(
                table: "product_images",
                keyColumn: "ImageUrl",
                keyValue: "/img/Paravan%20Dungeon%20Master.jpg");

            migrationBuilder.DeleteData(
                table: "product_images",
                keyColumn: "ImageUrl",
                keyValue: "/img/Pin%20D20.jpg");

            migrationBuilder.DeleteData(
                table: "product_images",
                keyColumn: "ImageUrl",
                keyValue: "/img/Poster%20harta%20fantasy.jpg");

            migrationBuilder.DeleteData(
                table: "product_images",
                keyColumn: "ImageUrl",
                keyValue: "/img/Set%20pensule.jpg");

            migrationBuilder.DeleteData(
                table: "product_images",
                keyColumn: "ImageUrl",
                keyValue: "/img/Set%20vopsele%20acrilice.jpg");

            migrationBuilder.DeleteData(
                table: "product_images",
                keyColumn: "ImageUrl",
                keyValue: "/img/Set%20zaruri%20poliedrice.jpg");

            migrationBuilder.DeleteData(
                table: "product_images",
                keyColumn: "ImageUrl",
                keyValue: "/img/Tava%20de%20zaruri.jpg");

            migrationBuilder.DeleteData(
                table: "product_images",
                keyColumn: "ImageUrl",
                keyValue: "/img/Teren%20modular%20dungeon.jpg");

            migrationBuilder.DeleteData(
                table: "product_images",
                keyColumn: "ImageUrl",
                keyValue: "/img/Tricou%20Gamers%20Keep.jpg");

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
        }
    }
}
