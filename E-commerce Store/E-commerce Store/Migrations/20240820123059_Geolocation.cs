using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce_Store.Migrations
{
    /// <inheritdoc />
    public partial class Geolocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Geolocations",
                columns: table => new
                {
                    geolocationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    lat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lon = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Geolocations", x => x.geolocationId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_GeolocationId",
                table: "Addresses",
                column: "GeolocationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Geolocations_GeolocationId",
                table: "Addresses",
                column: "GeolocationId",
                principalTable: "Geolocations",
                principalColumn: "geolocationId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Geolocations_GeolocationId",
                table: "Addresses");

            migrationBuilder.DropTable(
                name: "Geolocations");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_GeolocationId",
                table: "Addresses");
        }
    }
}
