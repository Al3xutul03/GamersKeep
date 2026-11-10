using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class AlignProductsWithImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "product_categories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { "accessory", 23ul });

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
                keyValue: 23ul);

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
                keyValue: 31ul);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 32ul);

            migrationBuilder.InsertData(
                table: "product_images",
                columns: new[] { "ImageUrl", "CreatedAt", "DeletedAt", "ProductId", "UpdatedAt" },
                values: new object[,]
                {
                    { "/img/Caiete cu fișe de personaj.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 26ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Cană cu temă de zaruri.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 33ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Carti Rosii.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 20ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Cutie de depozitare pentru cărți.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 22ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Miniatură nevopsită Dragon.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 30ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Paravan pentru Dungeon Master.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 25ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Poster cu o hartă fantasy.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 35ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Set de 7 zaruri poliedrice.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 24ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Set de pensule pentru miniaturi.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 29ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Tavă de zaruri din catifea.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 21ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Tricou Gamer’s Keep.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 34ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 20ul,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Set de 100 de huse mate de culoare roșie, care protejează cărțile de joc de uzură și zgârieturi.", "Huse pentru cărți – Roșu" });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "Id", "AgeRating", "CreatedAt", "DeletedAt", "Description", "Name", "Price", "Stock", "UpdatedAt" },
                values: new object[,]
                {
                    { 36ul, "three-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Set de 100 de huse mate de culoare albastră, care protejează cărțile de joc de uzură și zgârieturi.", "Huse pentru cărți – Albastru", 49.00m, 40u, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 37ul, "three-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Set de 100 de huse mate de culoare verde, care protejează cărțile de joc de uzură și zgârieturi.", "Huse pentru cărți – Verde", 49.00m, 40u, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "product_categories",
                columns: new[] { "CategoryId", "ProductId", "CreatedAt", "DeletedAt", "UpdatedAt" },
                values: new object[,]
                {
                    { "accessory", 36ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "accessory", 37ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "product_images",
                columns: new[] { "ImageUrl", "CreatedAt", "DeletedAt", "ProductId", "UpdatedAt" },
                values: new object[,]
                {
                    { "/img/Carti Albastre.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 36ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Carti Verzi.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 37ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "product_categories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { "accessory", 36ul });

            migrationBuilder.DeleteData(
                table: "product_categories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { "accessory", 37ul });

            migrationBuilder.DeleteData(
                table: "product_images",
                keyColumn: "ImageUrl",
                keyValue: "/img/Caiete cu fișe de personaj.jpg");

            migrationBuilder.DeleteData(
                table: "product_images",
                keyColumn: "ImageUrl",
                keyValue: "/img/Cană cu temă de zaruri.jpg");

            migrationBuilder.DeleteData(
                table: "product_images",
                keyColumn: "ImageUrl",
                keyValue: "/img/Carti Albastre.jpg");

            migrationBuilder.DeleteData(
                table: "product_images",
                keyColumn: "ImageUrl",
                keyValue: "/img/Carti Rosii.jpg");

            migrationBuilder.DeleteData(
                table: "product_images",
                keyColumn: "ImageUrl",
                keyValue: "/img/Carti Verzi.jpg");

            migrationBuilder.DeleteData(
                table: "product_images",
                keyColumn: "ImageUrl",
                keyValue: "/img/Cutie de depozitare pentru cărți.jpg");

            migrationBuilder.DeleteData(
                table: "product_images",
                keyColumn: "ImageUrl",
                keyValue: "/img/Miniatură nevopsită Dragon.jpg");

            migrationBuilder.DeleteData(
                table: "product_images",
                keyColumn: "ImageUrl",
                keyValue: "/img/Paravan pentru Dungeon Master.jpg");

            migrationBuilder.DeleteData(
                table: "product_images",
                keyColumn: "ImageUrl",
                keyValue: "/img/Poster cu o hartă fantasy.jpg");

            migrationBuilder.DeleteData(
                table: "product_images",
                keyColumn: "ImageUrl",
                keyValue: "/img/Set de 7 zaruri poliedrice.jpg");

            migrationBuilder.DeleteData(
                table: "product_images",
                keyColumn: "ImageUrl",
                keyValue: "/img/Set de pensule pentru miniaturi.jpg");

            migrationBuilder.DeleteData(
                table: "product_images",
                keyColumn: "ImageUrl",
                keyValue: "/img/Tavă de zaruri din catifea.jpg");

            migrationBuilder.DeleteData(
                table: "product_images",
                keyColumn: "ImageUrl",
                keyValue: "/img/Tricou Gamer’s Keep.jpg");

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 36ul);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 37ul);

            migrationBuilder.InsertData(
                table: "product_images",
                columns: new[] { "ImageUrl", "CreatedAt", "DeletedAt", "ProductId", "UpdatedAt" },
                values: new object[,]
                {
                    { "/img/Caiete%20fise%20de%20personaj.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 26ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Cana%20cu%20zaruri.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 33ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Cutie%20depozitare%20carti.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 22ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Huse%20pentru%20carti.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 20ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Miniatura%20dragon.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 30ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Paravan%20Dungeon%20Master.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 25ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Poster%20harta%20fantasy.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 35ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Set%20pensule.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 29ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Set%20zaruri%20poliedrice.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 24ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Tava%20de%20zaruri.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 21ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Tricou%20Gamers%20Keep.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 34ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 20ul,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Huse transparente cu spate mat, care protejează cărțile de joc de uzură și zgârieturi.", "Huse pentru cărți (100 buc.)" });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "Id", "AgeRating", "CreatedAt", "DeletedAt", "Description", "Name", "Price", "Stock", "UpdatedAt" },
                values: new object[,]
                {
                    { 23ul, "three-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Organizator cu compartimente reglabile pentru piese, jetoane și zaruri, ideal pentru jocurile cu multe componente.", "Organizator de piese și jetoane", 89.00m, 25u, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 27ul, "twelve-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Hartă cu grilă de 2,5 cm, pe care se scrie cu markere lavabile; potrivită pentru aventuri și bătălii cu miniaturi.", "Hartă-grilă reutilizabilă", 129.00m, 18u, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 28ul, "twelve-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Set de 12 culori acrilice cu pigmentare ridicată, pentru baza și detaliile miniaturilor.", "Set de vopsele acrilice", 185.00m, 25u, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 31ul, "twelve-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Pereți, uși și coloane care se îmbină pentru a construi încăperi de dungeon pe masa de joc.", "Teren modular de dungeon", 320.00m, 10u, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 32ul, "three-plus", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Insignă emailată în formă de zar cu 20 de fețe, pentru rucsac, geacă sau curea.", "Pin emailat D20", 35.00m, 60u, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "product_categories",
                columns: new[] { "CategoryId", "ProductId", "CreatedAt", "DeletedAt", "UpdatedAt" },
                values: new object[,]
                {
                    { "accessory", 23ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "t-t-r-p-g", 27ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "terrain", 27ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "miniatures", 28ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "painting-supplies", 28ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "miniatures", 31ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "terrain", 31ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "other", 32ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "product_images",
                columns: new[] { "ImageUrl", "CreatedAt", "DeletedAt", "ProductId", "UpdatedAt" },
                values: new object[,]
                {
                    { "/img/Harta%20grila%20reutilizabila.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 27ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Organizator%20piese%20si%20jetoane.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 23ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Pin%20D20.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 32ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Set%20vopsele%20acrilice.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 28ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "/img/Teren%20modular%20dungeon.jpg", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 31ul, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });
        }
    }
}
