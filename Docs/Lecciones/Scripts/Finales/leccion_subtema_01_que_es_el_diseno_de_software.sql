-- Script final de insercion de leccion con bloques interactivos
-- Generado desde legacy: leccion_subtema_01_que_es_el_diseno_de_software.sql
-- Modelo vigente: acc_academic.Lecciones + acc_academic.BloquesLeccion
USE [ACC_Academic];
GO
SET NOCOUNT ON;
SET XACT_ABORT ON;
GO
BEGIN TRY
    BEGIN TRAN;
    DECLARE @SubtemaId INT;
-- Resolver el subtema objetivo con variantes de texto.
    SELECT TOP (1) @SubtemaId = st.Id_SubTema
    FROM acc_academic.SubTemas st
    WHERE st.NombreSubTema IN
    (
        N'¿Qué es el diseño de software?',
        N'¿Que es el diseño de software?',
        N'Que es el diseño de software?',
        N'Que es el diseno de software?'
    )
    ORDER BY st.Id_SubTema;

    IF @SubtemaId IS NULL
    BEGIN
        THROW 51001, 'No se encontró el SubTema objetivo para la lección.', 1;
    END;
    DECLARE @TituloLeccion NVARCHAR(100) = N'¿Qué es el diseño de software?';
    DECLARE @DescripcionLeccion NVARCHAR(500) = N'Introduce qué es el diseño de software, en qué momento del desarrollo aparece y por qué ayuda a organizar un programa antes de escribir código.';
    DECLARE @NivelBloom NVARCHAR(64) = N'Comprender';
    DECLARE @OrdenSecciones NVARCHAR(MAX) = N'["charpDialog","teoria","ejemplo","mermaid","practica","charpTip"]';
    DECLARE @TieneActividad BIT = 0;
    DECLARE @UrlActividad NVARCHAR(MAX) = N'';
    DECLARE @TieneCompilador BIT = 0;
    DECLARE @TieneVideo BIT = 0;
    DECLARE @VideoId NVARCHAR(200) = NULL;
    DECLARE @MermaidTitulo NVARCHAR(160) = N'Organización del sistema de biblioteca';
    DECLARE @MermaidDescripcion NVARCHAR(MAX) = N'Representa el ejemplo de biblioteca separado en partes claras para mostrar cómo se ordena un sistema antes de programarlo.';
    DECLARE @MermaidCodigo NVARCHAR(MAX) = N'flowchart TD
    A[Sistema de biblioteca] --> B[Libros]
    A --> C[Préstamos]
    A --> D[Devoluciones]
    B --> E[Registrar y consultar]
    C --> F[Prestar y controlar]
    D --> G[Confirmar entrega]';
    DECLARE @Teoria NVARCHAR(MAX) = N'
<div class="leccion-teoria">
    <h3>Una idea sencilla</h3>
    <p>El diseño de software es la parte en la que decides cómo se va a organizar un programa antes de empezar a programarlo.</p>
    <p>Sirve para pensar con orden qué partes tendrá el sistema, qué hará cada una y cómo trabajarán juntas.</p>

    <div class="alert alert-info">
        <p class="alert-title">Idea clave</p>
        <p>Diseñar no es decorar documentos. Diseñar es tomar decisiones para que el programa tenga orden antes de escribir código.</p>
    </div>

    <div class="fomentador">
        <p>Si quieres profundizar más en qué es el diseño de software, puedes ver contenido más detallado en el capítulo sobre este tema haciendo clic <a href="Capitulo/Contenido/ID_CONTENIDO_PENDIENTE_DISENO_SOFTWARE">aquí</a>.</p>
    </div>

    <h3>¿Cuándo aparece?</h3>
    <p>En el desarrollo, primero entiendes qué problema se quiere resolver. Después organizas el programa. Luego lo programas y al final lo pruebas.</p>
    <ul>
        <li><strong>Entender el problema:</strong> saber qué necesita el usuario.</li>
        <li><strong>Diseñar:</strong> decidir cómo se organizará el programa.</li>
        <li><strong>Programar:</strong> escribir el código.</li>
        <li><strong>Probar:</strong> revisar si el resultado funciona como se esperaba.</li>
    </ul>

    <h3>¿Para qué sirve?</h3>
    <p>Sirve para evitar desorden, reducir errores y hacer más claro el trabajo antes de construir el sistema.</p>
</div>';
    DECLARE @Practica NVARCHAR(MAX) = N'
<div class="leccion-practicas">
    <h3>Práctica guiada</h3>
    <p>Caso: vas a crear un sistema escolar que permita registrar alumnos, materias y calificaciones.</p>
    <ol>
        <li>Escribe tres partes que debería tener ese sistema.</li>
        <li>Explica qué haría cada parte con una frase breve.</li>
        <li>Describe un problema que podría aparecer si programas todo sin organizarlo antes.</li>
    </ol>

    <div class="alert alert-success">
        <p class="alert-title">Criterio de logro</p>
        <p>La respuesta es adecuada si separa funciones del sistema, explica para qué sirve cada una y reconoce un problema real de trabajar sin orden previo.</p>
    </div>
</div>';
    DECLARE @Ejemplo NVARCHAR(MAX) = N'
<div class="leccion-ejemplos">
    <h3>Ejemplo: sistema de biblioteca</h3>
    <p>Imagina un sistema para registrar libros, préstamos y devoluciones.</p>

    <div class="alert alert-error">
        <p class="alert-title">Sin diseño previo</p>
        <p>Se empieza a programar de inmediato. Todo queda mezclado y después cuesta entender dónde registrar libros, dónde prestar y dónde devolver.</p>
    </div>

    <div class="alert alert-success">
        <p class="alert-title">Con diseño previo</p>
        <p>Primero se decide que habrá una parte para libros, otra para préstamos y otra para devoluciones. Después programar resulta más claro.</p>
    </div>

    <p>La diferencia está en que antes de escribir código ya sabes cómo se va a ordenar el programa.</p>
    <p>A continuación verás ese mismo ejemplo convertido en un diagrama simple para observar con claridad cómo se separan sus partes.</p>
</div>';
    DECLARE @CharpTip NVARCHAR(MAX) = N'<p><strong>Tip Charp:</strong> Si antes de programar ya puedes explicar qué partes tendrá tu sistema y qué hará cada una, ya estás diseñando.</p>';
    DECLARE @CharpDialog NVARCHAR(MAX) = N'<p>En esta lección vas a entender qué significa diseñar software dentro del proceso de desarrollo.</p>
<p>La meta es que distingas el momento en que se organiza el programa antes de escribir código.</p>';
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

