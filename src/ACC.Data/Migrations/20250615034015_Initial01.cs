using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ACC.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "acc_academic");

            migrationBuilder.CreateTable(
                name: "Modulos",
                schema: "acc_academic",
                columns: table => new
                {
                    Id_Modulo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreModulo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DescripcionModulo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modulos", x => x.Id_Modulo);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                schema: "acc_academic",
                columns: table => new
                {
                    Id_Tag = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreTag = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DescripcionTag = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id_Tag);
                });

            migrationBuilder.CreateTable(
                name: "Tips",
                schema: "acc_academic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Contenido = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tips", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                schema: "acc_academic",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProgresoGeneral = table.Column<decimal>(type: "decimal(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubModulos",
                schema: "acc_academic",
                columns: table => new
                {
                    Id_SubModulo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreSubModulo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DescripcionSubModulo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Id_Modulo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubModulos", x => x.Id_SubModulo);
                    table.ForeignKey(
                        name: "FK_SubModulos_Modulos_Id_Modulo",
                        column: x => x.Id_Modulo,
                        principalSchema: "acc_academic",
                        principalTable: "Modulos",
                        principalColumn: "Id_Modulo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModuloTags",
                schema: "acc_academic",
                columns: table => new
                {
                    Id_Modulo = table.Column<int>(type: "int", nullable: false),
                    Id_Tag = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuloTags", x => new { x.Id_Modulo, x.Id_Tag });
                    table.ForeignKey(
                        name: "FK_ModuloTags_Modulos_Id_Modulo",
                        column: x => x.Id_Modulo,
                        principalSchema: "acc_academic",
                        principalTable: "Modulos",
                        principalColumn: "Id_Modulo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModuloTags_Tags_Id_Tag",
                        column: x => x.Id_Tag,
                        principalSchema: "acc_academic",
                        principalTable: "Tags",
                        principalColumn: "Id_Tag",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Agendas",
                schema: "acc_academic",
                columns: table => new
                {
                    Id_Agenda = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agendas", x => x.Id_Agenda);
                    table.ForeignKey(
                        name: "FK_Agendas_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalSchema: "acc_academic",
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Auditorias",
                schema: "acc_academic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TipoAccion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Detalle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaAccion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auditorias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Auditorias_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalSchema: "acc_academic",
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Aulas",
                schema: "acc_academic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ModuloId = table.Column<int>(type: "int", nullable: false),
                    DocenteId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aulas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aulas_Modulos_ModuloId",
                        column: x => x.ModuloId,
                        principalSchema: "acc_academic",
                        principalTable: "Modulos",
                        principalColumn: "Id_Modulo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Aulas_Usuarios_DocenteId",
                        column: x => x.DocenteId,
                        principalSchema: "acc_academic",
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioModulos",
                schema: "acc_academic",
                columns: table => new
                {
                    Id_Usuario = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id_Modulo = table.Column<int>(type: "int", nullable: false),
                    EsCompletado = table.Column<bool>(type: "bit", nullable: false),
                    Calificacion = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    Progreso = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioModulos", x => new { x.Id_Usuario, x.Id_Modulo });
                    table.ForeignKey(
                        name: "FK_UsuarioModulos_Modulos_Id_Modulo",
                        column: x => x.Id_Modulo,
                        principalSchema: "acc_academic",
                        principalTable: "Modulos",
                        principalColumn: "Id_Modulo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioModulos_Usuarios_Id_Usuario",
                        column: x => x.Id_Usuario,
                        principalSchema: "acc_academic",
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExamenesHabilitados",
                schema: "acc_academic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id_SubModulo = table.Column<int>(type: "int", nullable: false),
                    Habilitado = table.Column<bool>(type: "bit", nullable: false),
                    FechaHabilitacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamenesHabilitados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamenesHabilitados_SubModulos_Id_SubModulo",
                        column: x => x.Id_SubModulo,
                        principalSchema: "acc_academic",
                        principalTable: "SubModulos",
                        principalColumn: "Id_SubModulo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExamenesHabilitados_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalSchema: "acc_academic",
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistorialCalificaciones",
                schema: "acc_academic",
                columns: table => new
                {
                    Id_Historial = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Usuario = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id_Modulo = table.Column<int>(type: "int", nullable: true),
                    Id_SubModulo = table.Column<int>(type: "int", nullable: true),
                    Calificacion = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    FechaCalificacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorialCalificaciones", x => x.Id_Historial);
                    table.ForeignKey(
                        name: "FK_HistorialCalificaciones_Modulos_Id_Modulo",
                        column: x => x.Id_Modulo,
                        principalSchema: "acc_academic",
                        principalTable: "Modulos",
                        principalColumn: "Id_Modulo");
                    table.ForeignKey(
                        name: "FK_HistorialCalificaciones_SubModulos_Id_SubModulo",
                        column: x => x.Id_SubModulo,
                        principalSchema: "acc_academic",
                        principalTable: "SubModulos",
                        principalColumn: "Id_SubModulo");
                    table.ForeignKey(
                        name: "FK_HistorialCalificaciones_Usuarios_Id_Usuario",
                        column: x => x.Id_Usuario,
                        principalSchema: "acc_academic",
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Temas",
                schema: "acc_academic",
                columns: table => new
                {
                    Id_Tema = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreTema = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DescripcionTema = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UltimaVisita = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Id_SubModulo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Temas", x => x.Id_Tema);
                    table.ForeignKey(
                        name: "FK_Temas_SubModulos_Id_SubModulo",
                        column: x => x.Id_SubModulo,
                        principalSchema: "acc_academic",
                        principalTable: "SubModulos",
                        principalColumn: "Id_SubModulo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioSubModulos",
                schema: "acc_academic",
                columns: table => new
                {
                    Id_Usuario = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id_SubModulo = table.Column<int>(type: "int", nullable: false),
                    EsCompletado = table.Column<bool>(type: "bit", nullable: false),
                    Calificacion = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    Progreso = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioSubModulos", x => new { x.Id_Usuario, x.Id_SubModulo });
                    table.ForeignKey(
                        name: "FK_UsuarioSubModulos_SubModulos_Id_SubModulo",
                        column: x => x.Id_SubModulo,
                        principalSchema: "acc_academic",
                        principalTable: "SubModulos",
                        principalColumn: "Id_SubModulo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioSubModulos_Usuarios_Id_Usuario",
                        column: x => x.Id_Usuario,
                        principalSchema: "acc_academic",
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TareasPersonales",
                schema: "acc_academic",
                columns: table => new
                {
                    TareaPersonalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TareaPersonalTitulo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TareaPersonalDescripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Completada = table.Column<bool>(type: "bit", nullable: true),
                    TareaPersonalFechaLimite = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdAgenda = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TareasPersonales", x => x.TareaPersonalId);
                    table.ForeignKey(
                        name: "FK_TareasPersonales_Agendas_IdAgenda",
                        column: x => x.IdAgenda,
                        principalSchema: "acc_academic",
                        principalTable: "Agendas",
                        principalColumn: "Id_Agenda");
                });

            migrationBuilder.CreateTable(
                name: "AulaEstudiantes",
                schema: "acc_academic",
                columns: table => new
                {
                    AulaId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AulaEstudiantes", x => new { x.AulaId, x.UsuarioId });
                    table.ForeignKey(
                        name: "FK_AulaEstudiantes_Aulas_AulaId",
                        column: x => x.AulaId,
                        principalSchema: "acc_academic",
                        principalTable: "Aulas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AulaEstudiantes_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalSchema: "acc_academic",
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Avisos",
                schema: "acc_academic",
                columns: table => new
                {
                    IdAviso = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TituloAviso = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ContenidoAviso = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaAviso = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    DocenteId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AulaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avisos", x => x.IdAviso);
                    table.ForeignKey(
                        name: "FK_Avisos_Aulas_AulaId",
                        column: x => x.AulaId,
                        principalSchema: "acc_academic",
                        principalTable: "Aulas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notificaciones",
                schema: "acc_academic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Mensaje = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    FechaEnvio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Leido = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    AulaId = table.Column<int>(type: "int", nullable: true),
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notificaciones_Aulas_AulaId",
                        column: x => x.AulaId,
                        principalSchema: "acc_academic",
                        principalTable: "Aulas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notificaciones_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalSchema: "acc_academic",
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TareasAsignadas",
                schema: "acc_academic",
                columns: table => new
                {
                    IdTareaAsignada = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TituloTareaAsignada = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DescripcionTareaAsignada = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Completada = table.Column<bool>(type: "bit", nullable: true),
                    FechaLimiteTareaAsignada = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AulaId = table.Column<int>(type: "int", nullable: false),
                    AgendaId = table.Column<int>(type: "int", nullable: true),
                    IdUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "SubTemas",
                schema: "acc_academic",
                columns: table => new
                {
                    Id_SubTema = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreSubTema = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DescripcionSubTema = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Id_Tema = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubTemas", x => x.Id_SubTema);
                    table.ForeignKey(
                        name: "FK_SubTemas_Temas_Id_Tema",
                        column: x => x.Id_Tema,
                        principalSchema: "acc_academic",
                        principalTable: "Temas",
                        principalColumn: "Id_Tema",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TemaTags",
                schema: "acc_academic",
                columns: table => new
                {
                    Id_Tema = table.Column<int>(type: "int", nullable: false),
                    Id_Tag = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemaTags", x => new { x.Id_Tema, x.Id_Tag });
                    table.ForeignKey(
                        name: "FK_TemaTags_Tags_Id_Tag",
                        column: x => x.Id_Tag,
                        principalSchema: "acc_academic",
                        principalTable: "Tags",
                        principalColumn: "Id_Tag",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TemaTags_Temas_Id_Tema",
                        column: x => x.Id_Tema,
                        principalSchema: "acc_academic",
                        principalTable: "Temas",
                        principalColumn: "Id_Tema",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioTemas",
                schema: "acc_academic",
                columns: table => new
                {
                    Id_Usuario = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id_Tema = table.Column<int>(type: "int", nullable: false),
                    EsFavorito = table.Column<bool>(type: "bit", nullable: false),
                    UltimaVisita = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Progreso = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioTemas", x => new { x.Id_Usuario, x.Id_Tema });
                    table.ForeignKey(
                        name: "FK_UsuarioTemas_Temas_Id_Tema",
                        column: x => x.Id_Tema,
                        principalSchema: "acc_academic",
                        principalTable: "Temas",
                        principalColumn: "Id_Tema",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioTemas_Usuarios_Id_Usuario",
                        column: x => x.Id_Usuario,
                        principalSchema: "acc_academic",
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lecciones",
                schema: "acc_academic",
                columns: table => new
                {
                    IdLeccion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TituloLeccion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DescripcionLeccion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    HtmlBody = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TieneActividad = table.Column<bool>(type: "bit", nullable: false),
                    UrlActividad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TieneEvaluacion = table.Column<bool>(type: "bit", nullable: false),
                    IdEvaluacion = table.Column<int>(type: "int", nullable: true),
                    TieneCompilador = table.Column<bool>(type: "bit", nullable: false),
                    OrdenSecciones = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubtemaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lecciones", x => x.IdLeccion);
                    table.ForeignKey(
                        name: "FK_Lecciones_SubTemas_SubtemaId",
                        column: x => x.SubtemaId,
                        principalSchema: "acc_academic",
                        principalTable: "SubTemas",
                        principalColumn: "Id_SubTema",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgresoUsuarios",
                schema: "acc_academic",
                columns: table => new
                {
                    IdProgreso = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubTemaId = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Completado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgresoUsuarios", x => x.IdProgreso);
                    table.ForeignKey(
                        name: "FK_ProgresoUsuarios_SubTemas_SubTemaId",
                        column: x => x.SubTemaId,
                        principalSchema: "acc_academic",
                        principalTable: "SubTemas",
                        principalColumn: "Id_SubTema",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioSubTemas",
                schema: "acc_academic",
                columns: table => new
                {
                    Id_Usuario = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id_SubTema = table.Column<int>(type: "int", nullable: false),
                    EsFavorito = table.Column<bool>(type: "bit", nullable: false),
                    UltimaVisita = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioSubTemas", x => new { x.Id_Usuario, x.Id_SubTema });
                    table.ForeignKey(
                        name: "FK_UsuarioSubTemas_SubTemas_Id_SubTema",
                        column: x => x.Id_SubTema,
                        principalSchema: "acc_academic",
                        principalTable: "SubTemas",
                        principalColumn: "Id_SubTema",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioSubTemas_Usuarios_Id_Usuario",
                        column: x => x.Id_Usuario,
                        principalSchema: "acc_academic",
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Capitulos",
                schema: "acc_academic",
                columns: table => new
                {
                    IdCapitulo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TituloCapitulo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SubtituloCapitulo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ModuloId = table.Column<int>(type: "int", nullable: false),
                    SubmoduloId = table.Column<int>(type: "int", nullable: false),
                    TemaId = table.Column<int>(type: "int", nullable: false),
                    LeccionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Capitulos", x => x.IdCapitulo);
                    table.ForeignKey(
                        name: "FK_Capitulos_Lecciones_LeccionId",
                        column: x => x.LeccionId,
                        principalSchema: "acc_academic",
                        principalTable: "Lecciones",
                        principalColumn: "IdLeccion",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Capitulos_Modulos_ModuloId",
                        column: x => x.ModuloId,
                        principalSchema: "acc_academic",
                        principalTable: "Modulos",
                        principalColumn: "Id_Modulo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Capitulos_SubModulos_SubmoduloId",
                        column: x => x.SubmoduloId,
                        principalSchema: "acc_academic",
                        principalTable: "SubModulos",
                        principalColumn: "Id_SubModulo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Capitulos_Temas_TemaId",
                        column: x => x.TemaId,
                        principalSchema: "acc_academic",
                        principalTable: "Temas",
                        principalColumn: "Id_Tema",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CapituloTags",
                schema: "acc_academic",
                columns: table => new
                {
                    Id_Capitulo = table.Column<int>(type: "int", nullable: false),
                    Id_Tag = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CapituloTags", x => new { x.Id_Capitulo, x.Id_Tag });
                    table.ForeignKey(
                        name: "FK_CapituloTags_Capitulos_Id_Capitulo",
                        column: x => x.Id_Capitulo,
                        principalSchema: "acc_academic",
                        principalTable: "Capitulos",
                        principalColumn: "IdCapitulo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CapituloTags_Tags_Id_Tag",
                        column: x => x.Id_Tag,
                        principalSchema: "acc_academic",
                        principalTable: "Tags",
                        principalColumn: "Id_Tag",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContenidoCapitulos",
                schema: "acc_academic",
                columns: table => new
                {
                    IdContenido = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Subtitulo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Duracion = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Dificultad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IconoBadge = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EtiquetaNivel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CapituloId = table.Column<int>(type: "int", nullable: false),
                    HtmlBody = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContenidoCapitulos", x => x.IdContenido);
                    table.ForeignKey(
                        name: "FK_ContenidoCapitulos_Capitulos_CapituloId",
                        column: x => x.CapituloId,
                        principalSchema: "acc_academic",
                        principalTable: "Capitulos",
                        principalColumn: "IdCapitulo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agendas_IdUsuario",
                schema: "acc_academic",
                table: "Agendas",
                column: "IdUsuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Auditorias_UsuarioId",
                schema: "acc_academic",
                table: "Auditorias",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_AulaEstudiantes_UsuarioId",
                schema: "acc_academic",
                table: "AulaEstudiantes",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Aulas_DocenteId",
                schema: "acc_academic",
                table: "Aulas",
                column: "DocenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Aulas_ModuloId",
                schema: "acc_academic",
                table: "Aulas",
                column: "ModuloId");

            migrationBuilder.CreateIndex(
                name: "IX_Avisos_AulaId",
                schema: "acc_academic",
                table: "Avisos",
                column: "AulaId");

            migrationBuilder.CreateIndex(
                name: "IX_Capitulos_LeccionId",
                schema: "acc_academic",
                table: "Capitulos",
                column: "LeccionId");

            migrationBuilder.CreateIndex(
                name: "IX_Capitulos_ModuloId",
                schema: "acc_academic",
                table: "Capitulos",
                column: "ModuloId");

            migrationBuilder.CreateIndex(
                name: "IX_Capitulos_SubmoduloId",
                schema: "acc_academic",
                table: "Capitulos",
                column: "SubmoduloId");

            migrationBuilder.CreateIndex(
                name: "IX_Capitulos_TemaId",
                schema: "acc_academic",
                table: "Capitulos",
                column: "TemaId");

            migrationBuilder.CreateIndex(
                name: "IX_CapituloTags_Id_Tag",
                schema: "acc_academic",
                table: "CapituloTags",
                column: "Id_Tag");

            migrationBuilder.CreateIndex(
                name: "IX_ContenidoCapitulos_CapituloId",
                schema: "acc_academic",
                table: "ContenidoCapitulos",
                column: "CapituloId");

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
                name: "IX_HistorialCalificaciones_Id_Modulo",
                schema: "acc_academic",
                table: "HistorialCalificaciones",
                column: "Id_Modulo");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialCalificaciones_Id_SubModulo",
                schema: "acc_academic",
                table: "HistorialCalificaciones",
                column: "Id_SubModulo");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialCalificaciones_Id_Usuario",
                schema: "acc_academic",
                table: "HistorialCalificaciones",
                column: "Id_Usuario");

            migrationBuilder.CreateIndex(
                name: "IX_Lecciones_SubtemaId",
                schema: "acc_academic",
                table: "Lecciones",
                column: "SubtemaId");

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
                name: "IX_Notificaciones_AulaId",
                schema: "acc_academic",
                table: "Notificaciones",
                column: "AulaId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificaciones_UsuarioId",
                schema: "acc_academic",
                table: "Notificaciones",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgresoUsuarios_SubTemaId",
                schema: "acc_academic",
                table: "ProgresoUsuarios",
                column: "SubTemaId");

            migrationBuilder.CreateIndex(
                name: "IX_SubModulos_Id_Modulo",
                schema: "acc_academic",
                table: "SubModulos",
                column: "Id_Modulo");

            migrationBuilder.CreateIndex(
                name: "IX_SubTemas_Id_Tema",
                schema: "acc_academic",
                table: "SubTemas",
                column: "Id_Tema");

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

            migrationBuilder.CreateIndex(
                name: "IX_TareasPersonales_IdAgenda",
                schema: "acc_academic",
                table: "TareasPersonales",
                column: "IdAgenda");

            migrationBuilder.CreateIndex(
                name: "IX_Temas_Id_SubModulo",
                schema: "acc_academic",
                table: "Temas",
                column: "Id_SubModulo");

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
                name: "IX_UsuarioModulos_Id_Modulo",
                schema: "acc_academic",
                table: "UsuarioModulos",
                column: "Id_Modulo");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioSubModulos_Id_SubModulo",
                schema: "acc_academic",
                table: "UsuarioSubModulos",
                column: "Id_SubModulo");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioSubTemas_Id_SubTema",
                schema: "acc_academic",
                table: "UsuarioSubTemas",
                column: "Id_SubTema");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioTemas_Id_Tema",
                schema: "acc_academic",
                table: "UsuarioTemas",
                column: "Id_Tema");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Auditorias",
                schema: "acc_academic");

            migrationBuilder.DropTable(
                name: "AulaEstudiantes",
                schema: "acc_academic");

            migrationBuilder.DropTable(
                name: "Avisos",
                schema: "acc_academic");

            migrationBuilder.DropTable(
                name: "CapituloTags",
                schema: "acc_academic");

            migrationBuilder.DropTable(
                name: "ContenidoCapitulos",
                schema: "acc_academic");

            migrationBuilder.DropTable(
                name: "ExamenesHabilitados",
                schema: "acc_academic");

            migrationBuilder.DropTable(
                name: "HistorialCalificaciones",
                schema: "acc_academic");

            migrationBuilder.DropTable(
                name: "ModuloTags",
                schema: "acc_academic");

            migrationBuilder.DropTable(
                name: "Notificaciones",
                schema: "acc_academic");

            migrationBuilder.DropTable(
                name: "ProgresoUsuarios",
                schema: "acc_academic");

            migrationBuilder.DropTable(
                name: "TareasAsignadas",
                schema: "acc_academic");

            migrationBuilder.DropTable(
                name: "TareasPersonales",
                schema: "acc_academic");

            migrationBuilder.DropTable(
                name: "TemaTags",
                schema: "acc_academic");

            migrationBuilder.DropTable(
                name: "Tips",
                schema: "acc_academic");

            migrationBuilder.DropTable(
                name: "UsuarioModulos",
                schema: "acc_academic");

            migrationBuilder.DropTable(
                name: "UsuarioSubModulos",
                schema: "acc_academic");

            migrationBuilder.DropTable(
                name: "UsuarioSubTemas",
                schema: "acc_academic");

            migrationBuilder.DropTable(
                name: "UsuarioTemas",
                schema: "acc_academic");

            migrationBuilder.DropTable(
                name: "Capitulos",
                schema: "acc_academic");

            migrationBuilder.DropTable(
                name: "Aulas",
                schema: "acc_academic");

            migrationBuilder.DropTable(
                name: "Agendas",
                schema: "acc_academic");

            migrationBuilder.DropTable(
                name: "Tags",
                schema: "acc_academic");

            migrationBuilder.DropTable(
                name: "Lecciones",
                schema: "acc_academic");

            migrationBuilder.DropTable(
                name: "Usuarios",
                schema: "acc_academic");

            migrationBuilder.DropTable(
                name: "SubTemas",
                schema: "acc_academic");

            migrationBuilder.DropTable(
                name: "Temas",
                schema: "acc_academic");

            migrationBuilder.DropTable(
                name: "SubModulos",
                schema: "acc_academic");

            migrationBuilder.DropTable(
                name: "Modulos",
                schema: "acc_academic");
        }
    }
}
