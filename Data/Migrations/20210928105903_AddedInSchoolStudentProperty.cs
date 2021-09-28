using Microsoft.EntityFrameworkCore.Migrations;

namespace AlbaCaZapada.Data.Migrations
{
    public partial class AddedInSchoolStudentProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "InSchool",
                table: "Students",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InSchool",
                table: "Students");
        }
    }
}
