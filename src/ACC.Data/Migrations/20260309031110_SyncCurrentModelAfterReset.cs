using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ACC.Data.Migrations
{
    /// <inheritdoc />
    public partial class SyncCurrentModelAfterReset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AulaEstudiantes_Usuarios_UsuarioId",
                schema: "acc_academic",
                table: "AulaEstudiantes");

            migrationBuilder.DropForeignKey(
                name: "FK_Aulas_Modulos_ModuloId",
                schema: "acc_academic",
                table: "Aulas");

            migrationBuilder.DropForeignKey(
                name: "FK_Aulas_Usuarios_DocenteId",
                schema: "acc_academic",
                table: "Aulas");

            migrationBuilder.DropForeignKey(
                name: "FK_CapituloTags_Capitulos_Id_Capitulo",
                schema: "acc_academic",
                table: "CapituloTags");

            migrationBuilder.DropForeignKey(
                name: "FK_CapituloTags_Tags_Id_Tag",
                schema: "acc_academic",
                table: "CapituloTags");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamenesHabilitados_SubModulos_Id_SubModulo",
                schema: "acc_academic",
                table: "ExamenesHabilitados");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamenesHabilitados_Usuarios_UsuarioId",
                schema: "acc_academic",
                table: "ExamenesHabilitados");

            migrationBuilder.DropForeignKey(
                name: "FK_Notificaciones_Aulas_AulaId",
                schema: "acc_academic",
                table: "Notificaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Notificaciones_Usuarios_UsuarioId",
                schema: "acc_academic",
                table: "Notificaciones");

            migrationBuilder.DropTable(
                name: "TareasAsignadas",
                schema: "acc_academic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TemaTags",
                schema: "acc_academic",
                table: "TemaTags");

            migrationBuilder.DropIndex(
                name: "IX_TemaTags_Id_Tag",
                schema: "acc_academic",
                table: "TemaTags");

            migrationBuilder.DropIndex(
                name: "IX_TemaTags_Id_Tema_Id_Tag",
                schema: "acc_academic",
                table: "TemaTags");

            migrationBuilder.DropIndex(
                name: "IX_Notificaciones_AulaId",
                schema: "acc_academic",
                table: "Notificaciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ModuloTags",
                schema: "acc_academic",
                table: "ModuloTags");

            migrationBuilder.DropIndex(
                name: "IX_ModuloTags_Id_Modulo_Id_Tag",
                schema: "acc_academic",
                table: "ModuloTags");

            migrationBuilder.DropIndex(
                name: "IX_ModuloTags_Id_Tag",
                schema: "acc_academic",
                table: "ModuloTags");

            migrationBuilder.DropIndex(
                name: "IX_ExamenesHabilitados_Id_SubModulo",
                schema: "acc_academic",
                table: "ExamenesHabilitados");

            migrationBuilder.DropIndex(
                name: "IX_ExamenesHabilitados_UsuarioId",
                schema: "acc_academic",
                table: "ExamenesHabilitados");

            migrationBuilder.DropIndex(
                name: "IX_Aulas_ModuloId",
                schema: "acc_academic",
                table: "Aulas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AulaEstudiantes",
                schema: "acc_academic",
                table: "AulaEstudiantes");

            migrationBuilder.DropColumn(
                name: "DescripcionTag",
                schema: "acc_academic",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "IdEvaluacion",
                schema: "acc_academic",
                table: "Lecciones");

            migrationBuilder.DropColumn(
                name: "EtiquetaNivel",
                schema: "acc_academic",
                table: "ContenidoCapitulos");

            migrationBuilder.DropColumn(
                name: "Tags",
                schema: "acc_academic",
                table: "ContenidoCapitulos");

            migrationBuilder.RenameColumn(
                name: "NombreTag",
                schema: "acc_academic",
                table: "Tags",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "Id_Tag",
                schema: "acc_academic",
                table: "Tags",
                newName: "IdTag");

            migrationBuilder.RenameColumn(
                name: "TieneEvaluacion",
                schema: "acc_academic",
                table: "Lecciones",
                newName: "TieneVideo");

            migrationBuilder.RenameColumn(
                name: "HtmlBody",
                schema: "acc_academic",
                table: "Lecciones",
                newName: "Teoria");

            migrationBuilder.RenameColumn(
                name: "Id_SubModulo",
                schema: "acc_academic",
                table: "ExamenesHabilitados",
                newName: "Tipo");

            migrationBuilder.RenameColumn(
                name: "Id_Tag",
                schema: "acc_academic",
                table: "CapituloTags",
                newName: "CapituloId");

            migrationBuilder.RenameColumn(
                name: "Id_Capitulo",
                schema: "acc_academic",
                table: "CapituloTags",
                newName: "TagId");

            migrationBuilder.RenameIndex(
                name: "IX_CapituloTags_Id_Tag",
                schema: "acc_academic",
                table: "CapituloTags",
                newName: "IX_CapituloTags_CapituloId");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "acc_academic",
                table: "Aulas",
                newName: "AulaId");

            migrationBuilder.AddColumn<int>(
                name: "Categoria",
                schema: "acc_academic",
                table: "Tags",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                schema: "acc_academic",
                table: "Tags",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Icono",
                schema: "acc_academic",
                table: "Tags",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                schema: "acc_academic",
                table: "ProgresoUsuarios",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Fecha",
                schema: "acc_academic",
                table: "ProgresoUsuarios",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                schema: "acc_academic",
                table: "Notificaciones",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaEnvio",
                schema: "acc_academic",
                table: "Notificaciones",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "CharpDialog",
                schema: "acc_academic",
                table: "Lecciones",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CharpTip",
                schema: "acc_academic",
                table: "Lecciones",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ejemplo",
                schema: "acc_academic",
                table: "Lecciones",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NivelBloom",
                schema: "acc_academic",
                table: "Lecciones",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Practica",
                schema: "acc_academic",
                table: "Lecciones",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VideoId",
                schema: "acc_academic",
                table: "Lecciones",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "FechaHabilitacion",
                schema: "acc_academic",
                table: "ExamenesHabilitados",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RefId",
                schema: "acc_academic",
                table: "ExamenesHabilitados",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ReglaFuente",
                schema: "acc_academic",
                table: "ExamenesHabilitados",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Tipo",
                schema: "acc_academic",
                table: "ContenidoCapitulos",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "Dificultad",
                schema: "acc_academic",
                table: "ContenidoCapitulos",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                schema: "acc_academic",
                table: "ContenidoCapitulos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Nivel",
                schema: "acc_academic",
                table: "ContenidoCapitulos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "TemaId",
                schema: "acc_academic",
                table: "Capitulos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SubmoduloId",
                schema: "acc_academic",
                table: "Capitulos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ModuloId",
                schema: "acc_academic",
                table: "Capitulos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ModuloId",
                schema: "acc_academic",
                table: "Aulas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "DocenteId",
                schema: "acc_academic",
                table: "Aulas",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                schema: "acc_academic",
                table: "Aulas",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<bool>(
                name: "ArchivarAula",
                schema: "acc_academic",
                table: "Aulas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CerrarAula",
                schema: "acc_academic",
                table: "Aulas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaActualizacion",
                schema: "acc_academic",
                table: "Aulas",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                schema: "acc_academic",
                table: "Aulas",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<int>(
                name: "SubModuloId",
                schema: "acc_academic",
                table: "Aulas",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                schema: "acc_academic",
                table: "AulaEstudiantes",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "AulaEstudianteId",
                schema: "acc_academic",
                table: "AulaEstudiantes",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaInscripcion",
                schema: "acc_academic",
                table: "AulaEstudiantes",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TemaTags",
                schema: "acc_academic",
                table: "TemaTags",
                columns: new[] { "Id_Tag", "Id_Tema" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ModuloTags",
                schema: "acc_academic",
                table: "ModuloTags",
                columns: new[] { "Id_Tag", "Id_Modulo" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AulaEstudiantes",
                schema: "acc_academic",
                table: "AulaEstudiantes",
                column: "AulaEstudianteId");

            migrationBuilder.CreateTable(
                name: "Anuncios",
                schema: "acc_academic",
                columns: table => new
                {
                    AnuncioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AulaId = table.Column<int>(type: "int", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Cuerpo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    DocenteId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anuncios", x => x.AnuncioId);
                    table.ForeignKey(
                        name: "FK_Anuncios_Aulas_AulaId",
                        column: x => x.AulaId,
                        principalSchema: "acc_academic",
                        principalTable: "Aulas",
                        principalColumn: "AulaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Evaluaciones",
                schema: "acc_academic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AulaId = table.Column<int>(type: "int", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evaluaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Evaluaciones_Aulas_AulaId",
                        column: x => x.AulaId,
                        principalSchema: "acc_academic",
                        principalTable: "Aulas",
                        principalColumn: "AulaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Examenes",
                schema: "acc_academic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroPreguntas = table.Column<int>(type: "int", nullable: false),
                    PuntajeAprobacion = table.Column<int>(type: "int", nullable: false),
                    ContenidoHtml = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examenes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExamenesModulos",
                schema: "acc_academic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroPreguntas = table.Column<int>(type: "int", nullable: false),
                    PuntajeAprobacion = table.Column<int>(type: "int", nullable: false),
                    ContenidoHtml = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModuloId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamenesModulos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamenesModulos_Modulos_ModuloId",
                        column: x => x.ModuloId,
                        principalSchema: "acc_academic",
                        principalTable: "Modulos",
                        principalColumn: "Id_Modulo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExamenesSubModulo",
                schema: "acc_academic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroPreguntas = table.Column<int>(type: "int", nullable: false),
                    PuntajeAprobacion = table.Column<int>(type: "int", nullable: false),
                    ContenidoHtml = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubModuloId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamenesSubModulo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamenesSubModulo_SubModulos_SubModuloId",
                        column: x => x.SubModuloId,
                        principalSchema: "acc_academic",
                        principalTable: "SubModulos",
                        principalColumn: "Id_SubModulo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvitacionesAula",
                schema: "acc_academic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AulaId = table.Column<int>(type: "int", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Activa = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    ExpiraEn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    NumUsos = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvitacionesAula", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvitacionesAula_Aulas_AulaId",
                        column: x => x.AulaId,
                        principalSchema: "acc_academic",
                        principalTable: "Aulas",
                        principalColumn: "AulaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tareas",
                schema: "acc_academic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AulaId = table.Column<int>(type: "int", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: false),
                    Enunciado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    FechaLimite = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Scope = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tareas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tareas_Aulas_AulaId",
                        column: x => x.AulaId,
                        principalSchema: "acc_academic",
                        principalTable: "Aulas",
                        principalColumn: "AulaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EvaluacionResultados",
                schema: "acc_academic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EvaluacionId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Calificacion = table.Column<double>(type: "float", nullable: false),
                    FechaCalificacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluacionResultados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvaluacionResultados_Evaluaciones_EvaluacionId",
                        column: x => x.EvaluacionId,
                        principalSchema: "acc_academic",
                        principalTable: "Evaluaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EvaluacionResultados_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalSchema: "acc_academic",
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExamenesIntentos",
                schema: "acc_academic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ExamenSubModuloId = table.Column<int>(type: "int", nullable: true),
                    ExamenModuloId = table.Column<int>(type: "int", nullable: true),
                    ExamenId = table.Column<int>(type: "int", nullable: true),
                    Aprobado = table.Column<bool>(type: "bit", nullable: false),
                    FechaIntento = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Calificacion = table.Column<double>(type: "float", nullable: false),
                    NumeroIntento = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamenesIntentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamenesIntentos_ExamenesModulos_ExamenModuloId",
                        column: x => x.ExamenModuloId,
                        principalSchema: "acc_academic",
                        principalTable: "ExamenesModulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExamenesIntentos_ExamenesSubModulo_ExamenSubModuloId",
                        column: x => x.ExamenSubModuloId,
                        principalSchema: "acc_academic",
                        principalTable: "ExamenesSubModulo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExamenesIntentos_Examenes_ExamenId",
                        column: x => x.ExamenId,
                        principalSchema: "acc_academic",
                        principalTable: "Examenes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExamenesIntentos_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalSchema: "acc_academic",
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TareasAsignaciones",
                schema: "acc_academic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TareaId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Calificacion = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Retroalimentacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaEntrega = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    EstadoEntrega = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TareasAsignaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TareasAsignaciones_Tareas_TareaId",
                        column: x => x.TareaId,
                        principalSchema: "acc_academic",
                        principalTable: "Tareas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TareasAsignaciones_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalSchema: "acc_academic",
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExamenesAprobatorios",
                schema: "acc_academic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    ExamenId = table.Column<int>(type: "int", nullable: false),
                    ExamenIntentoId = table.Column<int>(type: "int", nullable: false),
                    FechaAprobacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Calificacion = table.Column<double>(type: "float", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamenesAprobatorios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamenesAprobatorios_ExamenesIntentos_ExamenIntentoId",
                        column: x => x.ExamenIntentoId,
                        principalSchema: "acc_academic",
                        principalTable: "ExamenesIntentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamenesAprobatorios_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalSchema: "acc_academic",
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TemaTags_Id_Tema",
                schema: "acc_academic",
                table: "TemaTags",
                column: "Id_Tema");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_Nombre",
                schema: "acc_academic",
                table: "Tags",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProgresoUsuarios_UsuarioId_SubTemaId_Completado",
                schema: "acc_academic",
                table: "ProgresoUsuarios",
                columns: new[] { "UsuarioId", "SubTemaId", "Completado" });

            migrationBuilder.CreateIndex(
                name: "IX_Notificaciones_AulaId_FechaEnvio",
                schema: "acc_academic",
                table: "Notificaciones",
                columns: new[] { "AulaId", "FechaEnvio" });

            migrationBuilder.CreateIndex(
                name: "IX_ModuloTags_Id_Modulo",
                schema: "acc_academic",
                table: "ModuloTags",
                column: "Id_Modulo");

            migrationBuilder.CreateIndex(
                name: "IX_ExamenesHabilitados_UsuarioId_Tipo_RefId",
                schema: "acc_academic",
                table: "ExamenesHabilitados",
                columns: new[] { "UsuarioId", "Tipo", "RefId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Aulas_ModuloId_SubModuloId",
                schema: "acc_academic",
                table: "Aulas",
                columns: new[] { "ModuloId", "SubModuloId" });

            migrationBuilder.CreateIndex(
                name: "IX_Aulas_SubModuloId",
                schema: "acc_academic",
                table: "Aulas",
                column: "SubModuloId");

            migrationBuilder.CreateIndex(
                name: "IX_AulaEstudiantes_AulaId_UsuarioId",
                schema: "acc_academic",
                table: "AulaEstudiantes",
                columns: new[] { "AulaId", "UsuarioId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Anuncios_AulaId_Fecha",
                schema: "acc_academic",
                table: "Anuncios",
                columns: new[] { "AulaId", "Fecha" });

            migrationBuilder.CreateIndex(
                name: "IX_Evaluaciones_AulaId_Fecha",
                schema: "acc_academic",
                table: "Evaluaciones",
                columns: new[] { "AulaId", "Fecha" });

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionResultados_EvaluacionId",
                schema: "acc_academic",
                table: "EvaluacionResultados",
                column: "EvaluacionId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionResultados_EvaluacionId_UsuarioId",
                schema: "acc_academic",
                table: "EvaluacionResultados",
                columns: new[] { "EvaluacionId", "UsuarioId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionResultados_UsuarioId",
                schema: "acc_academic",
                table: "EvaluacionResultados",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamenesAprobatorios_ExamenIntentoId",
                schema: "acc_academic",
                table: "ExamenesAprobatorios",
                column: "ExamenIntentoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExamenesAprobatorios_UsuarioId_Tipo_ExamenId",
                schema: "acc_academic",
                table: "ExamenesAprobatorios",
                columns: new[] { "UsuarioId", "Tipo", "ExamenId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExamenesIntentos_ExamenId",
                schema: "acc_academic",
                table: "ExamenesIntentos",
                column: "ExamenId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamenesIntentos_ExamenModuloId",
                schema: "acc_academic",
                table: "ExamenesIntentos",
                column: "ExamenModuloId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamenesIntentos_ExamenSubModuloId",
                schema: "acc_academic",
                table: "ExamenesIntentos",
                column: "ExamenSubModuloId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamenesIntentos_IdUsuario_ExamenId_NumeroIntento",
                schema: "acc_academic",
                table: "ExamenesIntentos",
                columns: new[] { "IdUsuario", "ExamenId", "NumeroIntento" },
                unique: true,
                filter: "[ExamenId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ExamenesIntentos_IdUsuario_ExamenModuloId_NumeroIntento",
                schema: "acc_academic",
                table: "ExamenesIntentos",
                columns: new[] { "IdUsuario", "ExamenModuloId", "NumeroIntento" },
                unique: true,
                filter: "[ExamenModuloId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ExamenesIntentos_IdUsuario_ExamenSubModuloId_Aprobado",
                schema: "acc_academic",
                table: "ExamenesIntentos",
                columns: new[] { "IdUsuario", "ExamenSubModuloId", "Aprobado" });

            migrationBuilder.CreateIndex(
                name: "IX_ExamenesIntentos_IdUsuario_ExamenSubModuloId_NumeroIntento",
                schema: "acc_academic",
                table: "ExamenesIntentos",
                columns: new[] { "IdUsuario", "ExamenSubModuloId", "NumeroIntento" },
                unique: true,
                filter: "[ExamenSubModuloId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ExamenesModulos_ModuloId",
                schema: "acc_academic",
                table: "ExamenesModulos",
                column: "ModuloId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamenesSubModulo_SubModuloId",
                schema: "acc_academic",
                table: "ExamenesSubModulo",
                column: "SubModuloId");

            migrationBuilder.CreateIndex(
                name: "IX_InvitacionesAula_AulaId_Activa",
                schema: "acc_academic",
                table: "InvitacionesAula",
                columns: new[] { "AulaId", "Activa" });

            migrationBuilder.CreateIndex(
                name: "IX_InvitacionesAula_Token",
                schema: "acc_academic",
                table: "InvitacionesAula",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_AulaId_FechaLimite",
                schema: "acc_academic",
                table: "Tareas",
                columns: new[] { "AulaId", "FechaLimite" });

            migrationBuilder.CreateIndex(
                name: "IX_TareasAsignaciones_TareaId",
                schema: "acc_academic",
                table: "TareasAsignaciones",
                column: "TareaId");

            migrationBuilder.CreateIndex(
                name: "IX_TareasAsignaciones_TareaId_UsuarioId",
                schema: "acc_academic",
                table: "TareasAsignaciones",
                columns: new[] { "TareaId", "UsuarioId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TareasAsignaciones_UsuarioId",
                schema: "acc_academic",
                table: "TareasAsignaciones",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_AulaEstudiantes_Usuarios_UsuarioId",
                schema: "acc_academic",
                table: "AulaEstudiantes",
                column: "UsuarioId",
                principalSchema: "acc_academic",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Aulas_Modulos_ModuloId",
                schema: "acc_academic",
                table: "Aulas",
                column: "ModuloId",
                principalSchema: "acc_academic",
                principalTable: "Modulos",
                principalColumn: "Id_Modulo");

            migrationBuilder.AddForeignKey(
                name: "FK_Aulas_SubModulos_SubModuloId",
                schema: "acc_academic",
                table: "Aulas",
                column: "SubModuloId",
                principalSchema: "acc_academic",
                principalTable: "SubModulos",
                principalColumn: "Id_SubModulo");

            migrationBuilder.AddForeignKey(
                name: "FK_Aulas_Usuarios_DocenteId",
                schema: "acc_academic",
                table: "Aulas",
                column: "DocenteId",
                principalSchema: "acc_academic",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CapituloTags_Capitulos_CapituloId",
                schema: "acc_academic",
                table: "CapituloTags",
                column: "CapituloId",
                principalSchema: "acc_academic",
                principalTable: "Capitulos",
                principalColumn: "IdCapitulo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CapituloTags_Tags_TagId",
                schema: "acc_academic",
                table: "CapituloTags",
                column: "TagId",
                principalSchema: "acc_academic",
                principalTable: "Tags",
                principalColumn: "IdTag",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notificaciones_Aulas_AulaId",
                schema: "acc_academic",
                table: "Notificaciones",
                column: "AulaId",
                principalSchema: "acc_academic",
                principalTable: "Aulas",
                principalColumn: "AulaId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Notificaciones_Usuarios_UsuarioId",
                schema: "acc_academic",
                table: "Notificaciones",
                column: "UsuarioId",
                principalSchema: "acc_academic",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AulaEstudiantes_Usuarios_UsuarioId",
                schema: "acc_academic",
                table: "AulaEstudiantes");

            migrationBuilder.DropForeignKey(
                name: "FK_Aulas_Modulos_ModuloId",
                schema: "acc_academic",
                table: "Aulas");

            migrationBuilder.DropForeignKey(
                name: "FK_Aulas_SubModulos_SubModuloId",
                schema: "acc_academic",
                table: "Aulas");

            migrationBuilder.DropForeignKey(
                name: "FK_Aulas_Usuarios_DocenteId",
                schema: "acc_academic",
                table: "Aulas");

            migrationBuilder.DropForeignKey(
                name: "FK_CapituloTags_Capitulos_CapituloId",
                schema: "acc_academic",
                table: "CapituloTags");

            migrationBuilder.DropForeignKey(
                name: "FK_CapituloTags_Tags_TagId",
                schema: "acc_academic",
                table: "CapituloTags");

            migrationBuilder.DropForeignKey(
                name: "FK_Notificaciones_Aulas_AulaId",
                schema: "acc_academic",
                table: "Notificaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Notificaciones_Usuarios_UsuarioId",
                schema: "acc_academic",
                table: "Notificaciones");

            migrationBuilder.DropTable(
                name: "Anuncios",
                schema: "acc_academic");

            migrationBuilder.DropTable(
                name: "EvaluacionResultados",
                schema: "acc_academic");

            migrationBuilder.DropTable(
                name: "ExamenesAprobatorios",
                schema: "acc_academic");

            migrationBuilder.DropTable(
                name: "InvitacionesAula",
                schema: "acc_academic");

            migrationBuilder.DropTable(
                name: "TareasAsignaciones",
                schema: "acc_academic");

            migrationBuilder.DropTable(
                name: "Evaluaciones",
                schema: "acc_academic");

            migrationBuilder.DropTable(
                name: "ExamenesIntentos",
                schema: "acc_academic");

            migrationBuilder.DropTable(
                name: "Tareas",
                schema: "acc_academic");

            migrationBuilder.DropTable(
                name: "ExamenesModulos",
                schema: "acc_academic");

            migrationBuilder.DropTable(
                name: "ExamenesSubModulo",
                schema: "acc_academic");

            migrationBuilder.DropTable(
                name: "Examenes",
                schema: "acc_academic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TemaTags",
                schema: "acc_academic",
                table: "TemaTags");

            migrationBuilder.DropIndex(
                name: "IX_TemaTags_Id_Tema",
                schema: "acc_academic",
                table: "TemaTags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_Nombre",
                schema: "acc_academic",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_ProgresoUsuarios_UsuarioId_SubTemaId_Completado",
                schema: "acc_academic",
                table: "ProgresoUsuarios");

            migrationBuilder.DropIndex(
                name: "IX_Notificaciones_AulaId_FechaEnvio",
                schema: "acc_academic",
                table: "Notificaciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ModuloTags",
                schema: "acc_academic",
                table: "ModuloTags");

            migrationBuilder.DropIndex(
                name: "IX_ModuloTags_Id_Modulo",
                schema: "acc_academic",
                table: "ModuloTags");

            migrationBuilder.DropIndex(
                name: "IX_ExamenesHabilitados_UsuarioId_Tipo_RefId",
                schema: "acc_academic",
                table: "ExamenesHabilitados");

            migrationBuilder.DropIndex(
                name: "IX_Aulas_ModuloId_SubModuloId",
                schema: "acc_academic",
                table: "Aulas");

            migrationBuilder.DropIndex(
                name: "IX_Aulas_SubModuloId",
                schema: "acc_academic",
                table: "Aulas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AulaEstudiantes",
                schema: "acc_academic",
                table: "AulaEstudiantes");

            migrationBuilder.DropIndex(
                name: "IX_AulaEstudiantes_AulaId_UsuarioId",
                schema: "acc_academic",
                table: "AulaEstudiantes");

            migrationBuilder.DropColumn(
                name: "Categoria",
                schema: "acc_academic",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "Descripcion",
                schema: "acc_academic",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "Icono",
                schema: "acc_academic",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "CharpDialog",
                schema: "acc_academic",
                table: "Lecciones");

            migrationBuilder.DropColumn(
                name: "CharpTip",
                schema: "acc_academic",
                table: "Lecciones");

            migrationBuilder.DropColumn(
                name: "Ejemplo",
                schema: "acc_academic",
                table: "Lecciones");

            migrationBuilder.DropColumn(
                name: "NivelBloom",
                schema: "acc_academic",
                table: "Lecciones");

            migrationBuilder.DropColumn(
                name: "Practica",
                schema: "acc_academic",
                table: "Lecciones");

            migrationBuilder.DropColumn(
                name: "VideoId",
                schema: "acc_academic",
                table: "Lecciones");

            migrationBuilder.DropColumn(
                name: "RefId",
                schema: "acc_academic",
                table: "ExamenesHabilitados");

            migrationBuilder.DropColumn(
                name: "ReglaFuente",
                schema: "acc_academic",
                table: "ExamenesHabilitados");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                schema: "acc_academic",
                table: "ContenidoCapitulos");

            migrationBuilder.DropColumn(
                name: "Nivel",
                schema: "acc_academic",
                table: "ContenidoCapitulos");

            migrationBuilder.DropColumn(
                name: "ArchivarAula",
                schema: "acc_academic",
                table: "Aulas");

            migrationBuilder.DropColumn(
                name: "CerrarAula",
                schema: "acc_academic",
                table: "Aulas");

            migrationBuilder.DropColumn(
                name: "FechaActualizacion",
                schema: "acc_academic",
                table: "Aulas");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                schema: "acc_academic",
                table: "Aulas");

            migrationBuilder.DropColumn(
                name: "SubModuloId",
                schema: "acc_academic",
                table: "Aulas");

            migrationBuilder.DropColumn(
                name: "AulaEstudianteId",
                schema: "acc_academic",
                table: "AulaEstudiantes");

            migrationBuilder.DropColumn(
                name: "FechaInscripcion",
                schema: "acc_academic",
                table: "AulaEstudiantes");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                schema: "acc_academic",
                table: "Tags",
                newName: "NombreTag");

            migrationBuilder.RenameColumn(
                name: "IdTag",
                schema: "acc_academic",
                table: "Tags",
                newName: "Id_Tag");

            migrationBuilder.RenameColumn(
                name: "TieneVideo",
                schema: "acc_academic",
                table: "Lecciones",
                newName: "TieneEvaluacion");

            migrationBuilder.RenameColumn(
                name: "Teoria",
                schema: "acc_academic",
                table: "Lecciones",
                newName: "HtmlBody");

            migrationBuilder.RenameColumn(
                name: "Tipo",
                schema: "acc_academic",
                table: "ExamenesHabilitados",
                newName: "Id_SubModulo");

            migrationBuilder.RenameColumn(
                name: "CapituloId",
                schema: "acc_academic",
                table: "CapituloTags",
                newName: "Id_Tag");

            migrationBuilder.RenameColumn(
                name: "TagId",
                schema: "acc_academic",
                table: "CapituloTags",
                newName: "Id_Capitulo");

            migrationBuilder.RenameIndex(
                name: "IX_CapituloTags_CapituloId",
                schema: "acc_academic",
                table: "CapituloTags",
                newName: "IX_CapituloTags_Id_Tag");

            migrationBuilder.RenameColumn(
                name: "AulaId",
                schema: "acc_academic",
                table: "Aulas",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "DescripcionTag",
                schema: "acc_academic",
                table: "Tags",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                schema: "acc_academic",
                table: "ProgresoUsuarios",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Fecha",
                schema: "acc_academic",
                table: "ProgresoUsuarios",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                schema: "acc_academic",
                table: "Notificaciones",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaEnvio",
                schema: "acc_academic",
                table: "Notificaciones",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<int>(
                name: "IdEvaluacion",
                schema: "acc_academic",
                table: "Lecciones",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaHabilitacion",
                schema: "acc_academic",
                table: "ExamenesHabilitados",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<string>(
                name: "Tipo",
                schema: "acc_academic",
                table: "ContenidoCapitulos",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Dificultad",
                schema: "acc_academic",
                table: "ContenidoCapitulos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EtiquetaNivel",
                schema: "acc_academic",
                table: "ContenidoCapitulos",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Tags",
                schema: "acc_academic",
                table: "ContenidoCapitulos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TemaId",
                schema: "acc_academic",
                table: "Capitulos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SubmoduloId",
                schema: "acc_academic",
                table: "Capitulos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ModuloId",
                schema: "acc_academic",
                table: "Capitulos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ModuloId",
                schema: "acc_academic",
                table: "Aulas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DocenteId",
                schema: "acc_academic",
                table: "Aulas",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                schema: "acc_academic",
                table: "Aulas",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                schema: "acc_academic",
                table: "AulaEstudiantes",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TemaTags",
                schema: "acc_academic",
                table: "TemaTags",
                columns: new[] { "Id_Tema", "Id_Tag" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ModuloTags",
                schema: "acc_academic",
                table: "ModuloTags",
                columns: new[] { "Id_Modulo", "Id_Tag" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AulaEstudiantes",
                schema: "acc_academic",
                table: "AulaEstudiantes",
                columns: new[] { "AulaId", "UsuarioId" });

            migrationBuilder.CreateTable(
                name: "TareasAsignadas",
                schema: "acc_academic",
                columns: table => new
                {
                    IdTareaAsignada = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgendaId = table.Column<int>(type: "int", nullable: true),
                    AulaId = table.Column<int>(type: "int", nullable: false),
                    Completada = table.Column<bool>(type: "bit", nullable: true),
                    DescripcionTareaAsignada = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    FechaLimiteTareaAsignada = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TituloTareaAsignada = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TareasAsignadas", x => x.IdTareaAsignada);
                    table.ForeignKey(
                        name: "FK_TareasAsignadas_Agendas_AgendaId",
                        column: x => x.AgendaId,
                        principalSchema: "acc_academic",
                        principalTable: "Agendas",
                        principalColumn: "Id_Agenda");
                    table.ForeignKey(
                        name: "FK_TareasAsignadas_Aulas_AulaId",
                        column: x => x.AulaId,
                        principalSchema: "acc_academic",
                        principalTable: "Aulas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TemaTags_Id_Tag",
                schema: "acc_academic",
                table: "TemaTags",
                column: "Id_Tag");

            migrationBuilder.CreateIndex(
                name: "IX_TemaTags_Id_Tema_Id_Tag",
                schema: "acc_academic",
                table: "TemaTags",
                columns: new[] { "Id_Tema", "Id_Tag" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notificaciones_AulaId",
                schema: "acc_academic",
                table: "Notificaciones",
                column: "AulaId");

            migrationBuilder.CreateIndex(
                name: "IX_ModuloTags_Id_Modulo_Id_Tag",
                schema: "acc_academic",
                table: "ModuloTags",
                columns: new[] { "Id_Modulo", "Id_Tag" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ModuloTags_Id_Tag",
                schema: "acc_academic",
                table: "ModuloTags",
                column: "Id_Tag");

            migrationBuilder.CreateIndex(
                name: "IX_ExamenesHabilitados_Id_SubModulo",
                schema: "acc_academic",
                table: "ExamenesHabilitados",
                column: "Id_SubModulo");

            migrationBuilder.CreateIndex(
                name: "IX_ExamenesHabilitados_UsuarioId",
                schema: "acc_academic",
                table: "ExamenesHabilitados",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Aulas_ModuloId",
                schema: "acc_academic",
                table: "Aulas",
                column: "ModuloId");

            migrationBuilder.CreateIndex(
                name: "IX_TareasAsignadas_AgendaId",
                schema: "acc_academic",
                table: "TareasAsignadas",
                column: "AgendaId");

            migrationBuilder.CreateIndex(
                name: "IX_TareasAsignadas_AulaId",
                schema: "acc_academic",
                table: "TareasAsignadas",
                column: "AulaId");

            migrationBuilder.AddForeignKey(
                name: "FK_AulaEstudiantes_Usuarios_UsuarioId",
                schema: "acc_academic",
                table: "AulaEstudiantes",
                column: "UsuarioId",
                principalSchema: "acc_academic",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Aulas_Modulos_ModuloId",
                schema: "acc_academic",
                table: "Aulas",
                column: "ModuloId",
                principalSchema: "acc_academic",
                principalTable: "Modulos",
                principalColumn: "Id_Modulo",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Aulas_Usuarios_DocenteId",
                schema: "acc_academic",
                table: "Aulas",
                column: "DocenteId",
                principalSchema: "acc_academic",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CapituloTags_Capitulos_Id_Capitulo",
                schema: "acc_academic",
                table: "CapituloTags",
                column: "Id_Capitulo",
                principalSchema: "acc_academic",
                principalTable: "Capitulos",
                principalColumn: "IdCapitulo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CapituloTags_Tags_Id_Tag",
                schema: "acc_academic",
                table: "CapituloTags",
                column: "Id_Tag",
                principalSchema: "acc_academic",
                principalTable: "Tags",
                principalColumn: "Id_Tag",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamenesHabilitados_SubModulos_Id_SubModulo",
                schema: "acc_academic",
                table: "ExamenesHabilitados",
                column: "Id_SubModulo",
                principalSchema: "acc_academic",
                principalTable: "SubModulos",
                principalColumn: "Id_SubModulo",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamenesHabilitados_Usuarios_UsuarioId",
                schema: "acc_academic",
                table: "ExamenesHabilitados",
                column: "UsuarioId",
                principalSchema: "acc_academic",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notificaciones_Aulas_AulaId",
                schema: "acc_academic",
                table: "Notificaciones",
                column: "AulaId",
                principalSchema: "acc_academic",
                principalTable: "Aulas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notificaciones_Usuarios_UsuarioId",
                schema: "acc_academic",
                table: "Notificaciones",
                column: "UsuarioId",
                principalSchema: "acc_academic",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
