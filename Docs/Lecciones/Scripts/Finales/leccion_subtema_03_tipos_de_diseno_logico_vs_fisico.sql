-- Script final de insercion de leccion con bloques interactivos
-- Generado desde legacy: leccion_subtema_03_tipos_de_diseno_logico_vs_fisico.sql
-- Modelo vigente: acc_academic.Lecciones + acc_academic.BloquesLeccion
USE [ACC_Academic];
GO
SET NOCOUNT ON;
SET XACT_ABORT ON;
GO
BEGIN TRY
    BEGIN TRAN;
    DECLARE @SubtemaId INT = 3;

    IF NOT EXISTS (SELECT 1 FROM acc_academic.SubTemas WHERE Id_SubTema = @SubtemaId)
        THROW 57001, 'No existe el SubTemaId objetivo en acc_academic.SubTemas.', 1;
    DECLARE @TituloLeccion NVARCHAR(100) = N'Tipos de diseÃ±o: lÃ³gico vs fÃ­sico';
    DECLARE @DescripcionLeccion NVARCHAR(500) = N'Explica de forma simple la diferencia entre organizar un programa primero y construirlo despuÃ©s en herramientas reales.';
    DECLARE @NivelBloom NVARCHAR(64) = N'Analizar';
    DECLARE @OrdenSecciones NVARCHAR(MAX) = N'["charpDialog","video","teoria","ejemplo","practica","actividad","charpTip"]';
    DECLARE @TieneActividad BIT = 1;
    DECLARE @UrlActividad NVARCHAR(MAX) = N'https://pendiente.local/actividad/subtema-03-logico-vs-fisico';
    DECLARE @TieneCompilador BIT = 0;
    DECLARE @TieneVideo BIT = 1;
    DECLARE @VideoId NVARCHAR(200) = N'ST03VIDPEND01';
    DECLARE @MermaidTitulo NVARCHAR(160) = NULL;
    DECLARE @MermaidDescripcion NVARCHAR(MAX) = NULL;
    DECLARE @MermaidCodigo NVARCHAR(MAX) = NULL;
    DECLARE @Teoria NVARCHAR(MAX) = N'
<div class="leccion-teoria">
    <h3>Dos momentos del mismo trabajo</h3>
    <p>Cuando diseÃ±as un programa, primero piensas cÃ³mo se va a organizar y despuÃ©s llevas esa organizaciÃ³n a algo real. A esos dos momentos se les suele llamar diseÃ±o lÃ³gico y diseÃ±o fÃ­sico.</p>

    <h3>DiseÃ±o lÃ³gico</h3>
    <p>Es la parte en la que decides el orden general del programa. AquÃ­ piensas quÃ© partes tendrÃ¡, para quÃ© servirÃ¡ cada una y cÃ³mo se relacionarÃ¡n entre sÃ­.</p>
    <p>TodavÃ­a no estÃ¡s programando ni creando tablas. EstÃ¡s organizando la idea del sistema.</p>

    <h3>DiseÃ±o fÃ­sico</h3>
    <p>Es la parte en la que esa organizaciÃ³n se lleva a herramientas reales. AquÃ­ aparecen el cÃ³digo, la base de datos, las tablas, las conexiones y otros elementos concretos del sistema.</p>
    <p>La meta es que lo que planeaste primero se convierta en un sistema que ya puede construirse y funcionar.</p>
    <img src="https://placehold.co/1200x675?text=Logico+vs+Fisico+%28imagen+pendiente%29" alt="ComparaciÃ³n entre organizaciÃ³n e implementaciÃ³n real (pendiente)">

    <div class="alert alert-info">
        <p class="alert-title">Punto clave</p>
        <p>El diseÃ±o lÃ³gico organiza el programa. El diseÃ±o fÃ­sico toma esa organizaciÃ³n y la convierte en algo que ya puede implementarse en tecnologÃ­a real.</p>
    </div>

    <div class="fomentador">
        <p>Si quieres profundizar mÃ¡s en la diferencia entre organizar un sistema y llevarlo a una implementaciÃ³n real, puedes ver contenido mÃ¡s detallado en el capÃ­tulo sobre diseÃ±o lÃ³gico y fÃ­sico haciendo clic <a href="Capitulo/Contenido/ID_CONTENIDO_PENDIENTE_DISENO_LOGICO_FISICO">aquÃ­</a>.</p>
    </div>
</div>';
    DECLARE @Practica NVARCHAR(MAX) = N'
<div class="leccion-practicas">
    <h3>PrÃ¡ctica de clasificaciÃ³n</h3>
    <p>Indica si cada acciÃ³n corresponde al diseÃ±o lÃ³gico o al diseÃ±o fÃ­sico. DespuÃ©s explica brevemente por quÃ©.</p>
    <ol>
        <li>Decidir que habrÃ¡ una parte para alumnos y otra para cursos.</li>
        <li>Crear la tabla <code>Alumnos</code> en SQL Server.</li>
        <li>Definir que un alumno puede estar inscrito en varios cursos.</li>
        <li>Configurar la conexiÃ³n de la aplicaciÃ³n con la base de datos.</li>
    </ol>
    <div class="alert alert-success">
        <p class="alert-title">Criterio de logro</p>
        <p>EstÃ¡ correcto si separas decisiones de organizaciÃ³n del sistema de acciones donde ya se construye en tecnologÃ­a real.</p>
    </div>
</div>';
    DECLARE @Ejemplo NVARCHAR(MAX) = N'
<div class="leccion-ejemplos">
    <h3>Ejemplo: plataforma de cursos</h3>
    <p>Imagina una plataforma donde los alumnos se inscriben a cursos.</p>
    <p><strong>Momento lÃ³gico:</strong> decides que habrÃ¡ una parte para alumnos, otra para cursos y otra para inscripciones. TambiÃ©n decides que un alumno puede estar en varios cursos.</p>
    <p><strong>Momento fÃ­sico:</strong> esa organizaciÃ³n se lleva a algo concreto creando tablas como <code>Alumnos</code>, <code>Cursos</code> e <code>Inscripciones</code>, ademÃ¡s del cÃ³digo necesario para guardar y consultar datos.</p>
    <img src="https://placehold.co/1200x675?text=Plan+general+y+construccion+real" alt="Paso de organizaciÃ³n a implementaciÃ³n real (pendiente)">

    <div class="alert alert-success">
        <p class="alert-title">Lo importante</p>
        <p>Primero decides cÃ³mo se ordenarÃ¡ el sistema; despuÃ©s haces que ese orden exista de verdad en la aplicaciÃ³n.</p>
    </div>
</div>';
    DECLARE @CharpTip NVARCHAR(MAX) = N'<p><strong>Tip Charp:</strong> Si estÃ¡s decidiendo cÃ³mo se ordena el programa, vas en lÃ³gico. Si ya lo estÃ¡s montando en cÃ³digo o base de datos, vas en fÃ­sico.</p>';
    DECLARE @CharpDialog NVARCHAR(MAX) = N'<p>En esta lecciÃ³n vas a distinguir dos momentos que trabajan juntos: organizar el sistema y construirlo.</p><p>La meta es que sepas quÃ© decisiones pertenecen al diseÃ±o lÃ³gico y cuÃ¡les al diseÃ±o fÃ­sico.</p>';
    IF @NivelBloom NOT IN (N'Recordar', N'Comprender', N'Aplicar', N'Analizar', N'Evaluar', N'Crear')
        THROW 57002, 'NivelBloom invalido.', 1;
    IF ISJSON(@OrdenSecciones) <> 1
        THROW 57003, 'OrdenSecciones legacy no es JSON valido.', 1;
    IF EXISTS (SELECT [value] FROM OPENJSON(@OrdenSecciones) GROUP BY [value] HAVING COUNT(*) > 1)
        THROW 57004, 'OrdenSecciones legacy contiene tokens duplicados.', 1;
    IF EXISTS
    (
        SELECT 1
        FROM OPENJSON(@OrdenSecciones) j
        LEFT JOIN
        (
            VALUES
                (N'video'),
                (N'teoria'),
                (N'practica'),
                (N'ejemplo'),
                (N'actividad'),
                (N'compilador'),
                (N'mermaid'),
                (N'charpTip'),
                (N'charpDialog')
        ) permitidas(Token) ON permitidas.Token = CAST(j.[value] AS NVARCHAR(50))
        WHERE permitidas.Token IS NULL
    )
        THROW 57005, 'OrdenSecciones legacy contiene tokens no soportados por BloquesLeccion.', 1;
    IF EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'teoria') AND NULLIF(LTRIM(RTRIM(ISNULL(@Teoria, N''))), N'') IS NULL
        THROW 57006, 'El bloque teoria requiere contenido.', 1;
    IF EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'practica') AND NULLIF(LTRIM(RTRIM(ISNULL(@Practica, N''))), N'') IS NULL
        THROW 57007, 'El bloque practica requiere contenido.', 1;
    IF EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'ejemplo') AND NULLIF(LTRIM(RTRIM(ISNULL(@Ejemplo, N''))), N'') IS NULL
        THROW 57008, 'El bloque ejemplo requiere contenido.', 1;
    IF EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'mermaid') AND NULLIF(LTRIM(RTRIM(ISNULL(@MermaidCodigo, N''))), N'') IS NULL
        THROW 57009, 'El bloque mermaid requiere codigo.', 1;
    IF EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'charpTip') AND NULLIF(LTRIM(RTRIM(ISNULL(@CharpTip, N''))), N'') IS NULL
        THROW 57010, 'El bloque charpTip requiere texto.', 1;
    IF EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'charpDialog') AND NULLIF(LTRIM(RTRIM(ISNULL(@CharpDialog, N''))), N'') IS NULL
        THROW 57011, 'El bloque charpDialog requiere texto.', 1;
    IF EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'actividad') AND (@TieneActividad = 0 OR NULLIF(LTRIM(RTRIM(ISNULL(@UrlActividad, N''))), N'') IS NULL)
        THROW 57012, 'El bloque actividad requiere flag y URL.', 1;
    IF EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'compilador') AND @TieneCompilador = 0
        THROW 57013, 'El bloque compilador requiere flag.', 1;
    IF EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'video') AND (@TieneVideo = 0 OR NULLIF(LTRIM(RTRIM(ISNULL(@VideoId, N''))), N'') IS NULL)
        THROW 57014, 'El bloque video requiere flag y VideoId.', 1;
    DECLARE @LeccionesARemover TABLE (IdLeccion INT PRIMARY KEY);

    INSERT INTO @LeccionesARemover (IdLeccion)
    SELECT IdLeccion
    FROM acc_academic.Lecciones
    WHERE SubtemaId = @SubtemaId
      AND (TituloLeccion = @TituloLeccion OR IdLeccion < 4000);

    DELETE b
    FROM acc_academic.BloquesLeccion b
    INNER JOIN @LeccionesARemover r ON r.IdLeccion = b.LeccionId;

    DELETE l
    FROM acc_academic.Lecciones l
    INNER JOIN @LeccionesARemover r ON r.IdLeccion = l.IdLeccion;
    INSERT INTO acc_academic.Lecciones
    (
        TituloLeccion,
        DescripcionLeccion,
        SubtemaId
    )
    VALUES
    (
        @TituloLeccion,
        @DescripcionLeccion,
        @SubtemaId
    );
    DECLARE @LeccionId INT = CONVERT(INT, SCOPE_IDENTITY());
    ;WITH SourceBlocks AS
    (
        SELECT
            TRY_CONVERT(INT, j.[key]) AS SourceOrden,
            CAST(j.[value] AS NVARCHAR(50)) AS Token
        FROM OPENJSON(@OrdenSecciones) j
    ),
    MappedBlocks AS
    (
        SELECT
            ROW_NUMBER() OVER (ORDER BY SourceOrden) AS Orden,
            CASE Token
                WHEN N'teoria' THEN N'TextoHtml'
                WHEN N'practica' THEN N'TextoHtml'
                WHEN N'ejemplo' THEN N'TextoHtml'
                WHEN N'mermaid' THEN N'Mermaid'
                WHEN N'charpTip' THEN N'CharpTip'
                WHEN N'charpDialog' THEN N'CharpDialog'
                WHEN N'actividad' THEN N'ActividadExterna'
                WHEN N'compilador' THEN N'Compilador'
                WHEN N'video' THEN N'Video'
            END AS TipoBloque,
            CASE Token
                WHEN N'teoria' THEN N'Teoria'
                WHEN N'practica' THEN N'Practica'
                WHEN N'ejemplo' THEN N'Ejemplo'
                WHEN N'mermaid' THEN NULLIF(@MermaidTitulo, N'')
                ELSE NULL
            END AS Titulo,
            CASE Token
                WHEN N'teoria' THEN CONCAT(N'{"html":"', STRING_ESCAPE(ISNULL(@Teoria, N''), 'json'), N'"}')
                WHEN N'practica' THEN CONCAT(N'{"html":"', STRING_ESCAPE(ISNULL(@Practica, N''), 'json'), N'"}')
                WHEN N'ejemplo' THEN CONCAT(N'{"html":"', STRING_ESCAPE(ISNULL(@Ejemplo, N''), 'json'), N'"}')
                WHEN N'mermaid' THEN CONCAT(N'{"codigo":"', STRING_ESCAPE(ISNULL(@MermaidCodigo, N''), 'json'), N'","descripcion":"', STRING_ESCAPE(ISNULL(@MermaidDescripcion, N''), 'json'), N'"}')
                WHEN N'charpTip' THEN CONCAT(N'{"texto":"', STRING_ESCAPE(ISNULL(@CharpTip, N''), 'json'), N'"}')
                WHEN N'charpDialog' THEN CONCAT(N'{"texto":"', STRING_ESCAPE(ISNULL(@CharpDialog, N''), 'json'), N'"}')
                WHEN N'actividad' THEN CONCAT(N'{"url":"', STRING_ESCAPE(ISNULL(@UrlActividad, N''), 'json'), N'","textoBoton":"Ver actividad interactiva"}')
                WHEN N'compilador' THEN N'{"lenguaje":"csharp","codigoInicial":"","instrucciones":""}'
                WHEN N'video' THEN CONCAT(N'{"videoId":"', STRING_ESCAPE(ISNULL(@VideoId, N''), 'json'), N'","proveedor":"youtube"}')
            END AS ConfiguracionJson
        FROM SourceBlocks
    )
    INSERT INTO acc_academic.BloquesLeccion
    (
        LeccionId,
        TipoBloque,
        Orden,
        ConfiguracionJson,
        Titulo,
        NivelBloom,
        EsObligatorio,
        Puntaje
    )
    SELECT
        @LeccionId,
        TipoBloque,
        Orden,
        ConfiguracionJson,
        Titulo,
        NULLIF(@NivelBloom, N''),
        CAST(0 AS BIT),
        NULL
    FROM MappedBlocks;
    COMMIT TRAN;
    SELECT
        l.IdLeccion,
        l.TituloLeccion,
        l.SubtemaId,
        COUNT(b.IdBloqueLeccion) AS TotalBloques
    FROM acc_academic.Lecciones l
    LEFT JOIN acc_academic.BloquesLeccion b ON b.LeccionId = l.IdLeccion
    WHERE l.IdLeccion = @LeccionId
    GROUP BY l.IdLeccion, l.TituloLeccion, l.SubtemaId;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRAN;
    THROW;
END CATCH;
GO

