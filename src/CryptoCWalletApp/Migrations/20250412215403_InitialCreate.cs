using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoCWalletApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CryptoCurrency",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Symbol = table.Column<string>(type: "text", nullable: true),
                    Usd = table.Column<decimal>(type: "numeric", nullable: true),
                    UsdMarketCap = table.Column<decimal>(type: "numeric", nullable: true),
                    Usd24HVol = table.Column<decimal>(type: "numeric", nullable: true),
                    Usd24HChange = table.Column<decimal>(type: "numeric", nullable: true),
                    LastUpdatedAt = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CryptoCurrency", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CryptoCurrency");
        }
    }
}
