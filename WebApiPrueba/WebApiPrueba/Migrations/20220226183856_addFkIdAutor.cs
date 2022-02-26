using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiPrueba.Migrations
{
    public partial class addFkIdAutor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "cantPaginas",
                table: "Libros",
                newName: "CantPaginas");

            migrationBuilder.AddColumn<int>(
                name: "IdAutor",
                table: "Libros",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdAutor",
                table: "Libros");

            migrationBuilder.RenameColumn(
                name: "CantPaginas",
                table: "Libros",
                newName: "cantPaginas");
        }
    }
}
