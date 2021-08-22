using Microsoft.EntityFrameworkCore.Migrations;

namespace AlbaCaZapada.Data.Migrations
{
    public partial class StudentList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CNP",
                table: "Students",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "CNP",
                table: "Students",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
