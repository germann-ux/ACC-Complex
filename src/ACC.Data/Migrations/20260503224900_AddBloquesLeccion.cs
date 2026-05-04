using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ACC.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddBloquesLeccion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BloquesLeccion",
                schema: "acc_academic",
                columns: table => new
                {
                    IdBloqueLeccion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeccionId = table.Column<int>(type: "int", nullable: false),
                    TipoBloque = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Orden = table.Column<int>(type: "int", nullable: false),
                    ConfiguracionJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: true),
                    NivelBloom = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    EsObligatorio = table.Column<bool>(type: "bit", nullable: false),
                    Puntaje = table.Column<decimal>(type: "decimal(6,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloquesLeccion", x => x.IdBloqueLeccion);
                    table.ForeignKey(
                        name: "FK_BloquesLeccion_Lecciones_LeccionId",
                        column: x => x.LeccionId,
                        principalSchema: "acc_academic",
                        principalTable: "Lecciones",
                        principalColumn: "IdLeccion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.Sql("""
                IF EXISTS (
                    SELECT 1
                    FROM [acc_academic].[Lecciones]
                    WHERE [OrdenSecciones] IS NOT NULL
                      AND [OrdenSecciones] <> N''
                      AND ISJSON([OrdenSecciones]) <> 1
                )
                    THROW 51000, 'OrdenSecciones contiene JSON invalido; no se puede migrar a BloquesLeccion.', 1;

                IF EXISTS (
                    SELECT 1
                    FROM [acc_academic].[Lecciones] L
                    CROSS APPLY OPENJSON(CASE WHEN ISJSON(L.[OrdenSecciones]) = 1 THEN L.[OrdenSecciones] ELSE N'[]' END) J
                    WHERE J.[value] NOT IN (
                        N'teoria',
                        N'practica',
                        N'ejemplo',
                        N'mermaid',
                        N'charpTip',
                        N'charpDialog',
                        N'actividad',
                        N'compilador',
                        N'video'
                    )
                )
                    THROW 51001, 'OrdenSecciones contiene tokens no soportados por la migracion a BloquesLeccion.', 1;

                ;WITH SourceBlocks AS
                (
                    SELECT
                        L.[IdLeccion] AS [LeccionId],
                        TRY_CONVERT(int, J.[key]) AS [SourceOrden],
                        J.[value] AS [Token],
                        L.[Teoria],
                        L.[Practica],
                        L.[Ejemplo],
                        L.[MermaidTitulo],
                        L.[MermaidDescripcion],
                        L.[MermaidCodigo],
                        L.[CharpTip],
                        L.[CharpDialog],
                        L.[NivelBloom],
                        L.[UrlActividad],
                        L.[VideoId]
                    FROM [acc_academic].[Lecciones] L
                    CROSS APPLY OPENJSON(CASE WHEN ISJSON(L.[OrdenSecciones]) = 1 THEN L.[OrdenSecciones] ELSE N'[]' END) J
                ),
                MappedBlocks AS
                (
                    SELECT
                        [LeccionId],
                        ROW_NUMBER() OVER (PARTITION BY [LeccionId] ORDER BY [SourceOrden]) AS [Orden],
                        CASE [Token]
                            WHEN N'teoria' THEN N'TextoHtml'
                            WHEN N'practica' THEN N'TextoHtml'
                            WHEN N'ejemplo' THEN N'TextoHtml'
                            WHEN N'mermaid' THEN N'Mermaid'
                            WHEN N'charpTip' THEN N'CharpTip'
                            WHEN N'charpDialog' THEN N'CharpDialog'
                            WHEN N'actividad' THEN N'ActividadExterna'
                            WHEN N'compilador' THEN N'Compilador'
                            WHEN N'video' THEN N'Video'
                        END AS [TipoBloque],
                        CASE [Token]
                            WHEN N'teoria' THEN N'Teoria'
                            WHEN N'practica' THEN N'Practica'
                            WHEN N'ejemplo' THEN N'Ejemplo'
                            WHEN N'mermaid' THEN NULLIF([MermaidTitulo], N'')
                            ELSE NULL
                        END AS [Titulo],
                        NULLIF([NivelBloom], N'') AS [NivelBloom],
                        CASE [Token]
                            WHEN N'teoria' THEN CONCAT(N'{"html":"', STRING_ESCAPE(ISNULL([Teoria], N''), 'json'), N'"}')
                            WHEN N'practica' THEN CONCAT(N'{"html":"', STRING_ESCAPE(ISNULL([Practica], N''), 'json'), N'"}')
                            WHEN N'ejemplo' THEN CONCAT(N'{"html":"', STRING_ESCAPE(ISNULL([Ejemplo], N''), 'json'), N'"}')
                            WHEN N'mermaid' THEN CONCAT(N'{"codigo":"', STRING_ESCAPE(ISNULL([MermaidCodigo], N''), 'json'), N'","descripcion":"', STRING_ESCAPE(ISNULL([MermaidDescripcion], N''), 'json'), N'"}')
                            WHEN N'charpTip' THEN CONCAT(N'{"texto":"', STRING_ESCAPE(ISNULL([CharpTip], N''), 'json'), N'"}')
                            WHEN N'charpDialog' THEN CONCAT(N'{"texto":"', STRING_ESCAPE(ISNULL([CharpDialog], N''), 'json'), N'"}')
                            WHEN N'actividad' THEN CONCAT(N'{"url":"', STRING_ESCAPE(ISNULL([UrlActividad], N''), 'json'), N'","textoBoton":"Ver actividad interactiva"}')
                            WHEN N'compilador' THEN N'{"lenguaje":"csharp","codigoInicial":"","instrucciones":""}'
                            WHEN N'video' THEN CONCAT(N'{"videoId":"', STRING_ESCAPE(ISNULL([VideoId], N''), 'json'), N'","proveedor":"youtube"}')
                        END AS [ConfiguracionJson]
                    FROM SourceBlocks
                )
                INSERT INTO [acc_academic].[BloquesLeccion]
                    ([LeccionId], [TipoBloque], [Orden], [ConfiguracionJson], [Titulo], [NivelBloom], [EsObligatorio], [Puntaje])
                SELECT
                    [LeccionId],
                    [TipoBloque],
                    [Orden],
                    [ConfiguracionJson],
                    [Titulo],
                    [NivelBloom],
                    CAST(0 AS bit),
                    NULL
                FROM MappedBlocks;
                """);

            migrationBuilder.CreateIndex(
                name: "IX_BloquesLeccion_LeccionId_Orden",
                schema: "acc_academic",
                table: "BloquesLeccion",
                columns: new[] { "LeccionId", "Orden" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BloquesLeccion_LeccionId_TipoBloque",
                schema: "acc_academic",
                table: "BloquesLeccion",
                columns: new[] { "LeccionId", "TipoBloque" });

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

            migrationBuilder.DropColumn(
                name: "NivelBloom",
                schema: "acc_academic",
                table: "Lecciones");

            migrationBuilder.DropColumn(
                name: "OrdenSecciones",
                schema: "acc_academic",
                table: "Lecciones");

            migrationBuilder.DropColumn(
                name: "Practica",
                schema: "acc_academic",
                table: "Lecciones");

            migrationBuilder.DropColumn(
                name: "Teoria",
                schema: "acc_academic",
                table: "Lecciones");

            migrationBuilder.DropColumn(
                name: "TieneActividad",
                schema: "acc_academic",
                table: "Lecciones");

            migrationBuilder.DropColumn(
                name: "TieneCompilador",
                schema: "acc_academic",
                table: "Lecciones");

            migrationBuilder.DropColumn(
                name: "TieneVideo",
                schema: "acc_academic",
                table: "Lecciones");

            migrationBuilder.DropColumn(
                name: "UrlActividad",
                schema: "acc_academic",
                table: "Lecciones");

            migrationBuilder.DropColumn(
                name: "VideoId",
                schema: "acc_academic",
                table: "Lecciones");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<string>(
                name: "NivelBloom",
                schema: "acc_academic",
                table: "Lecciones",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OrdenSecciones",
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
                name: "Teoria",
                schema: "acc_academic",
                table: "Lecciones",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "TieneActividad",
                schema: "acc_academic",
                table: "Lecciones",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TieneCompilador",
                schema: "acc_academic",
                table: "Lecciones",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TieneVideo",
                schema: "acc_academic",
                table: "Lecciones",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UrlActividad",
                schema: "acc_academic",
                table: "Lecciones",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VideoId",
                schema: "acc_academic",
                table: "Lecciones",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.Sql("""
                UPDATE L
                SET
                    [Teoria] = ISNULL(JSON_VALUE(T.ConfiguracionJson, '$.html'), N''),
                    [NivelBloom] = ISNULL(T.NivelBloom, N'')
                FROM [acc_academic].[Lecciones] L
                INNER JOIN [acc_academic].[BloquesLeccion] T
                    ON T.LeccionId = L.IdLeccion AND T.TipoBloque = N'TextoHtml' AND T.Titulo = N'Teoria';

                UPDATE L
                SET [Practica] = ISNULL(JSON_VALUE(T.ConfiguracionJson, '$.html'), N'')
                FROM [acc_academic].[Lecciones] L
                INNER JOIN [acc_academic].[BloquesLeccion] T
                    ON T.LeccionId = L.IdLeccion AND T.TipoBloque = N'TextoHtml' AND T.Titulo = N'Practica';

                UPDATE L
                SET [Ejemplo] = ISNULL(JSON_VALUE(T.ConfiguracionJson, '$.html'), N'')
                FROM [acc_academic].[Lecciones] L
                INNER JOIN [acc_academic].[BloquesLeccion] T
                    ON T.LeccionId = L.IdLeccion AND T.TipoBloque = N'TextoHtml' AND T.Titulo = N'Ejemplo';

                UPDATE L
                SET
                    [MermaidTitulo] = T.Titulo,
                    [MermaidDescripcion] = JSON_VALUE(T.ConfiguracionJson, '$.descripcion'),
                    [MermaidCodigo] = JSON_VALUE(T.ConfiguracionJson, '$.codigo')
                FROM [acc_academic].[Lecciones] L
                INNER JOIN [acc_academic].[BloquesLeccion] T
                    ON T.LeccionId = L.IdLeccion AND T.TipoBloque = N'Mermaid';

                UPDATE L
                SET [CharpTip] = JSON_VALUE(T.ConfiguracionJson, '$.texto')
                FROM [acc_academic].[Lecciones] L
                INNER JOIN [acc_academic].[BloquesLeccion] T
                    ON T.LeccionId = L.IdLeccion AND T.TipoBloque = N'CharpTip';

                UPDATE L
                SET [CharpDialog] = JSON_VALUE(T.ConfiguracionJson, '$.texto')
                FROM [acc_academic].[Lecciones] L
                INNER JOIN [acc_academic].[BloquesLeccion] T
                    ON T.LeccionId = L.IdLeccion AND T.TipoBloque = N'CharpDialog';

                UPDATE L
                SET
                    [TieneActividad] = 1,
                    [UrlActividad] = JSON_VALUE(T.ConfiguracionJson, '$.url')
                FROM [acc_academic].[Lecciones] L
                INNER JOIN [acc_academic].[BloquesLeccion] T
                    ON T.LeccionId = L.IdLeccion AND T.TipoBloque = N'ActividadExterna';

                UPDATE L
                SET [TieneCompilador] = 1
                FROM [acc_academic].[Lecciones] L
                WHERE EXISTS (
                    SELECT 1
                    FROM [acc_academic].[BloquesLeccion] T
                    WHERE T.LeccionId = L.IdLeccion AND T.TipoBloque = N'Compilador'
                );

                UPDATE L
                SET
                    [TieneVideo] = 1,
                    [VideoId] = JSON_VALUE(T.ConfiguracionJson, '$.videoId')
                FROM [acc_academic].[Lecciones] L
                INNER JOIN [acc_academic].[BloquesLeccion] T
                    ON T.LeccionId = L.IdLeccion AND T.TipoBloque = N'Video';

                UPDATE L
                SET [OrdenSecciones] = ISNULL((
                    SELECT
                        CASE T.TipoBloque
                            WHEN N'TextoHtml' THEN LOWER(T.Titulo)
                            WHEN N'ActividadExterna' THEN N'actividad'
                            WHEN N'Compilador' THEN N'compilador'
                            WHEN N'Mermaid' THEN N'mermaid'
                            WHEN N'CharpTip' THEN N'charpTip'
                            WHEN N'CharpDialog' THEN N'charpDialog'
                            WHEN N'Video' THEN N'video'
                        END AS [value]
                    FROM [acc_academic].[BloquesLeccion] T
                    WHERE T.LeccionId = L.IdLeccion
                      AND T.TipoBloque IN (N'TextoHtml', N'ActividadExterna', N'Compilador', N'Mermaid', N'CharpTip', N'CharpDialog', N'Video')
                    ORDER BY T.Orden
                    FOR JSON PATH
                ), N'[]')
                FROM [acc_academic].[Lecciones] L;
                """);

            migrationBuilder.DropTable(
                name: "BloquesLeccion",
                schema: "acc_academic");
        }
    }
}
