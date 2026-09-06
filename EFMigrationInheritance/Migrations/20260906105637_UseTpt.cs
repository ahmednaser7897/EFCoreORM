using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFMigrationInheritance.Migrations
{
    /// <inheritdoc />
    public partial class UseTpt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Company",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "IsIntern",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "JobTitle",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "University",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "YearOfGraduation",
                table: "Participants");

            migrationBuilder.CreateTable(
                name: "Coporates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coporates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Coporates_Participants_Id",
                        column: x => x.Id,
                        principalTable: "Participants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Individuals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    University = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YearOfGraduation = table.Column<int>(type: "int", nullable: false),
                    IsIntern = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Individuals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Individuals_Participants_Id",
                        column: x => x.Id,
                        principalTable: "Participants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coporates");

            migrationBuilder.DropTable(
                name: "Individuals");

            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "Participants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsIntern",
                table: "Participants",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobTitle",
                table: "Participants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Participants",
                type: "VARCHAR(4)",
                maxLength: 4,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "University",
                table: "Participants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "YearOfGraduation",
                table: "Participants",
                type: "int",
                nullable: true);
        }
    }
}
