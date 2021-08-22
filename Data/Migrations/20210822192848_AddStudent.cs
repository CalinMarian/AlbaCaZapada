using Microsoft.EntityFrameworkCore.Migrations;

namespace AlbaCaZapada.Data.Migrations
{
    public partial class AddStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Paied",
                table: "Students");

            migrationBuilder.AlterColumn<float>(
                name: "CNP",
                table: "Students",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CNP",
                table: "Students",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<bool>(
                name: "Paied",
                table: "Students",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
