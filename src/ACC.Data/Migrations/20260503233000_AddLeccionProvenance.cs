using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Infrastructure;

#nullable disable

namespace ACC.Data.Migrations
{
    /// <inheritdoc />
    [DbContext(typeof(ACCDbContext))]
    [Migration("20260503233000_AddLeccionProvenance")]
    public partial class AddLeccionProvenance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AulaId",
                schema: "acc_academic",
                table: "Lecciones",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EstadoLeccion",
                schema: "acc_academic",
                table: "Lecciones",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "Publicado");

            migrationBuilder.AddColumn<string>(
                name: "OrigenLeccion",
                schema: "acc_academic",
                table: "Lecciones",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "Oficial");

            migrationBuilder.CreateIndex(
                name: "IX_Lecciones_AulaId",
                schema: "acc_academic",
                table: "Lecciones",
                column: "AulaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lecciones_Aulas_AulaId",
                schema: "acc_academic",
                table: "Lecciones",
                column: "AulaId",
                principalSchema: "acc_academic",
                principalTable: "Aulas",
                principalColumn: "AulaId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lecciones_Aulas_AulaId",
                schema: "acc_academic",
                table: "Lecciones");

            migrationBuilder.DropIndex(
                name: "IX_Lecciones_AulaId",
                schema: "acc_academic",
                table: "Lecciones");

            migrationBuilder.DropColumn(
                name: "AulaId",
                schema: "acc_academic",
                table: "Lecciones");

            migrationBuilder.DropColumn(
                name: "EstadoLeccion",
                schema: "acc_academic",
                table: "Lecciones");

            migrationBuilder.DropColumn(
                name: "OrigenLeccion",
                schema: "acc_academic",
                table: "Lecciones");
        }
    }
}
