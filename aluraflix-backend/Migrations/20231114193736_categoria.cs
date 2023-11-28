using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aluraflix_backend.Migrations
{
    public partial class categoria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoriaID",
                table: "Videos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.ID);
                });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "ID", "Cor", "Titulo" },
                values: new object[] { 1, "#8c8c8c", "Livre" });

            migrationBuilder.CreateIndex(
                name: "IX_Videos_CategoriaID",
                table: "Videos",
                column: "CategoriaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Categorias_CategoriaID",
                table: "Videos",
                column: "CategoriaID",
                principalTable: "Categorias",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Categorias_CategoriaID",
                table: "Videos");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropIndex(
                name: "IX_Videos_CategoriaID",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "CategoriaID",
                table: "Videos");
        }
    }
}
