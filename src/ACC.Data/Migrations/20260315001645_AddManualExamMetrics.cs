using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ACC.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddManualExamMetrics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IntentosMaximos",
                schema: "acc_academic",
                table: "ExamenesSubModulo",
                type: "int",
                nullable: false,
                defaultValue: 3);

            migrationBuilder.AddColumn<int>(
                name: "TiempoLimiteSegundos",
                schema: "acc_academic",
                table: "ExamenesSubModulo",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IntentosMaximos",
                schema: "acc_academic",
                table: "ExamenesModulos",
                type: "int",
                nullable: false,
                defaultValue: 3);

            migrationBuilder.AddColumn<int>(
                name: "TiempoLimiteSegundos",
                schema: "acc_academic",
                table: "ExamenesModulos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumeroAciertos",
                schema: "acc_academic",
                table: "ExamenesIntentos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "PorcentajeObtenido",
                schema: "acc_academic",
                table: "ExamenesIntentos",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "TiempoSegundos",
                schema: "acc_academic",
                table: "ExamenesIntentos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalPreguntas",
                schema: "acc_academic",
                table: "ExamenesIntentos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IntentosMaximos",
                schema: "acc_academic",
                table: "ExamenesSubModulo");

            migrationBuilder.DropColumn(
                name: "TiempoLimiteSegundos",
                schema: "acc_academic",
                table: "ExamenesSubModulo");

            migrationBuilder.DropColumn(
                name: "IntentosMaximos",
                schema: "acc_academic",
                table: "ExamenesModulos");

            migrationBuilder.DropColumn(
                name: "TiempoLimiteSegundos",
                schema: "acc_academic",
                table: "ExamenesModulos");

            migrationBuilder.DropColumn(
                name: "NumeroAciertos",
                schema: "acc_academic",
                table: "ExamenesIntentos");

            migrationBuilder.DropColumn(
                name: "PorcentajeObtenido",
                schema: "acc_academic",
                table: "ExamenesIntentos");

            migrationBuilder.DropColumn(
                name: "TiempoSegundos",
                schema: "acc_academic",
                table: "ExamenesIntentos");

            migrationBuilder.DropColumn(
                name: "TotalPreguntas",
                schema: "acc_academic",
                table: "ExamenesIntentos");
        }
    }
}
