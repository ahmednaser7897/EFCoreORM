using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFMigrationInheritance.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
