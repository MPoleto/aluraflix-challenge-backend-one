using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aluraflix_backend.Migrations
{
    public partial class DeleteSetNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Categorias_CategoriaID",
                table: "Videos");

            migrationBuilder.AlterColumn<int>(
                name: "CategoriaID",
                table: "Videos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Categorias_CategoriaID",
                table: "Videos",
                column: "CategoriaID",
                principalTable: "Categorias",
                principalColumn: "CategoriaID",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Categorias_CategoriaID",
                table: "Videos");

            migrationBuilder.AlterColumn<int>(
                name: "CategoriaID",
                table: "Videos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Categorias_CategoriaID",
                table: "Videos",
                column: "CategoriaID",
                principalTable: "Categorias",
                principalColumn: "CategoriaID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
