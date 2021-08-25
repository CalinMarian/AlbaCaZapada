using Microsoft.EntityFrameworkCore.Migrations;

namespace AlbaCaZapada.Data.Migrations
{
    public partial class addedMoreStudentProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstParent",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondParent",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Details",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "FirstParent",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "SecondParent",
                table: "Students");
        }
    }
}
