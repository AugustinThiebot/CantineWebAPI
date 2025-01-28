using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Cantine.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CreateNSeedDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientCategories",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    DiscountType = table.Column<string>(type: "TEXT", nullable: false),
                    DiscountValue = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientCategories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProductName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    CategoryId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Budget = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_ClientCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ClientCategories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ClientCategories",
                columns: new[] { "CategoryId", "DiscountType", "DiscountValue", "Name" },
                values: new object[,]
                {
                    { new Guid("1efe7a31-8dcc-4ff0-9b2d-5f148e2989cc"), "Fixed", 10m, "Stagiaire" },
                    { new Guid("3c2d2d86-441c-4a6b-abc4-08edc0149ab1"), "Fixed", 7.5m, "Interne" },
                    { new Guid("ad100c63-43fc-45d9-944f-31d70d6befa8"), "Percentage", 100m, "VIP" },
                    { new Guid("b941481f-d14c-4a94-bb5e-2b29489ec86a"), "None", 0m, "Visiteur" },
                    { new Guid("ee613ca8-47ae-49dc-931e-acecf4c1930e"), "Fixed", 6m, "Prestataire" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Price", "ProductName" },
                values: new object[,]
                {
                    { new Guid("61c5959d-6089-4291-8b5a-467340dfd859"), 1m, "Fromage" },
                    { new Guid("62f8e186-3853-4251-813f-a5c5cede5af8"), 6m, "Plat" },
                    { new Guid("6d024167-3abd-45d0-8326-abcbe6750700"), 0.40m, "Pain" },
                    { new Guid("8e144565-6da3-4ffe-93ba-6a8f72f77fe8"), 4m, "Petit Salade Bar" },
                    { new Guid("a24f0638-dd84-4cea-87d2-42c63dfc767a"), 1m, "Portion de fruit" },
                    { new Guid("a56a4686-5ab2-46c4-b8dd-b24afbd0f123"), 6m, "Grand Salade Bar" },
                    { new Guid("c5cc549e-30c2-452d-a9d5-133fc7641c16"), 1m, "Boisson" },
                    { new Guid("ca8a3f05-7322-4b3a-844c-6dbcd2e7af3c"), 3m, "Dessert" },
                    { new Guid("fa025408-f032-4013-b855-5653cedab972"), 3m, "Entrée" }
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Budget", "CategoryId", "Name" },
                values: new object[] { new Guid("3720da0a-9109-48c5-89aa-acc7b7684294"), 100m, new Guid("1efe7a31-8dcc-4ff0-9b2d-5f148e2989cc"), "Michel Blanc" });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_CategoryId",
                table: "Clients",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ClientCategories");
        }
    }
}
