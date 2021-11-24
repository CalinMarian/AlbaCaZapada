using Microsoft.EntityFrameworkCore.Migrations;

namespace AlbaCaZapada.Data.Migrations
{
    public partial class AddedStudentIndebtedProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "YearAndMonth",
                table: "Payments",
                newName: "Month");

            migrationBuilder.RenameColumn(
                name: "PartialPayment",
                table: "Payments",
                newName: "Indebted");

            migrationBuilder.AddColumn<bool>(
                name: "Indebted",
                table: "Students",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Indebted",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "Month",
                table: "Payments",
                newName: "YearAndMonth");

            migrationBuilder.RenameColumn(
                name: "Indebted",
                table: "Payments",
                newName: "PartialPayment");
        }
    }
}
