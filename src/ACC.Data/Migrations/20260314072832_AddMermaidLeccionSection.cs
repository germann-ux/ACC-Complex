using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ACC.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMermaidLeccionSection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MermaidCodigo",
                schema: "acc_academic",
                table: "Lecciones",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MermaidDescripcion",
                schema: "acc_academic",
                table: "Lecciones",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MermaidTitulo",
                schema: "acc_academic",
                table: "Lecciones",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MermaidCodigo",
                schema: "acc_academic",
                table: "Lecciones");

            migrationBuilder.DropColumn(
                name: "MermaidDescripcion",
                schema: "acc_academic",
                table: "Lecciones");

            migrationBuilder.DropColumn(
                name: "MermaidTitulo",
                schema: "acc_academic",
                table: "Lecciones");
        }
    }
}
