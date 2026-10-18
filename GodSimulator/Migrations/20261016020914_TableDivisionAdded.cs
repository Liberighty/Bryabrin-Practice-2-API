using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GodSimulator.Migrations
{
    /// <inheritdoc />
    public partial class TableDivisionAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DivisionId",
                table: "Updates",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Divisions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Divisions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Updates_DivisionId",
                table: "Updates",
                column: "DivisionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Updates_Divisions_DivisionId",
                table: "Updates",
                column: "DivisionId",
                principalTable: "Divisions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Updates_Divisions_DivisionId",
                table: "Updates");

            migrationBuilder.DropTable(
                name: "Divisions");

            migrationBuilder.DropIndex(
                name: "IX_Updates_DivisionId",
                table: "Updates");

            migrationBuilder.DropColumn(
                name: "DivisionId",
                table: "Updates");
        }
    }
}
