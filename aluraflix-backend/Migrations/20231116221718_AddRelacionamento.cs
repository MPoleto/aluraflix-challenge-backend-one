using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aluraflix_backend.Migrations
{
    public partial class AddRelacionamento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Categorias",
                newName: "CategoriaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CategoriaID",
                table: "Categorias",
                newName: "ID");
        }
    }
}
