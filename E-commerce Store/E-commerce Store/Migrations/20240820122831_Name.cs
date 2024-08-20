using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce_Store.Migrations
{
    /// <inheritdoc />
    public partial class Name : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Names",
                columns: table => new
                {
                    NameId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Names", x => x.NameId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_NameId",
                table: "Users",
                column: "NameId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Names_NameId",
                table: "Users",
                column: "NameId",
                principalTable: "Names",
                principalColumn: "NameId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Names_NameId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Names");

            migrationBuilder.DropIndex(
                name: "IX_Users_NameId",
                table: "Users");
        }
    }
}
