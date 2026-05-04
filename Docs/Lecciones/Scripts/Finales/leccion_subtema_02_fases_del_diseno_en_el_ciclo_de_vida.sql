-- Script final de insercion de leccion con bloques interactivos
-- Generado desde legacy: leccion_subtema_02_fases_del_diseno_en_el_ciclo_de_vida.sql
-- Modelo vigente: acc_academic.Lecciones + acc_academic.BloquesLeccion
USE [ACC_Academic];
GO
SET NOCOUNT ON;
SET XACT_ABORT ON;
GO
BEGIN TRY
    BEGIN TRAN;
    DECLARE @SubtemaId INT = 2;

    IF NOT EXISTS (SELECT 1 FROM acc_academic.SubTemas WHERE Id_SubTema = @SubtemaId)
        THROW 57001, 'No existe el SubTemaId objetivo en acc_academic.SubTemas.', 1;
    DECLARE @TituloLeccion NVARCHAR(100) = N'Fases del diseÃ±o en el ciclo de vida';
    DECLARE @DescripcionLeccion NVARCHAR(500) = N'Explica en quÃ© momento del desarrollo aparece el diseÃ±o y cÃ³mo se relaciona con entender el problema, programar y probar.';
    DECLARE @NivelBloom NVARCHAR(64) = N'Comprender';
    DECLARE @OrdenSecciones NVARCHAR(MAX) = N'["charpDialog","video","teoria","mermaid","ejemplo","practica","actividad","charpTip"]';
    DECLARE @TieneActividad BIT = 1;
    DECLARE @UrlActividad NVARCHAR(MAX) = N'https://pendiente.local/actividad/subtema-02-fases-diseno';
    DECLARE @TieneCompilador BIT = 0;
    DECLARE @TieneVideo BIT = 1;
    DECLARE @VideoId NVARCHAR(200) = N'ST02VIDPEND01';
    DECLARE @MermaidTitulo NVARCHAR(160) = N'Recorrido basico del desarrollo';
    DECLARE @MermaidDescripcion NVARCHAR(MAX) = N'Muestra el orden general del trabajo: entender el problema, disenar, programar y probar. El diseno aparece antes de escribir codigo.';
    DECLARE @MermaidCodigo NVARCHAR(MAX) = N'flowchart LR
    A["Entender el problema"] --> B["Disenar el sistema"]
    B --> C["Programar"]
    C --> D["Probar"]
    D --> E["Ajustar si hace falta"]

    style B fill:#ffe7a3,stroke:#b7791f,stroke-width:2px';
    DECLARE @Teoria NVARCHAR(MAX) = N'
<div class="leccion-teoria">
    <h3>El diseÃ±o aparece antes de programar</h3>
    <p>En el desarrollo de software no se empieza programando de inmediato. Primero se entiende quÃ© problema se quiere resolver, despuÃ©s se organiza el programa, luego se escribe el cÃ³digo y al final se revisa que funcione bien.</p>
    <p>La fase de diseÃ±o estÃ¡ justo en medio de ese camino. Su trabajo es ordenar cÃ³mo se construirÃ¡ el sistema antes de empezar a programarlo.</p>
    <ul>
        <li><strong>Entender el problema:</strong> saber quÃ© necesita el usuario o la instituciÃ³n.</li>
        <li><strong>DiseÃ±o:</strong> decidir quÃ© partes tendrÃ¡ el programa y quÃ© harÃ¡ cada una.</li>
        <li><strong>ProgramaciÃ³n:</strong> escribir el cÃ³digo con base en esas decisiones.</li>
        <li><strong>Pruebas:</strong> revisar si el programa cumple lo esperado.</li>
    </ul>
    <img src="https://placehold.co/1200x675?text=Fases+del+ciclo+de+vida" alt="Etapas del desarrollo de software (pendiente)">

    <div class="alert alert-info">
        <p class="alert-title">Idea clave</p>
        <p>DiseÃ±ar no es una tarea aparte sin relaciÃ³n con el resto del trabajo. DiseÃ±ar es la etapa que conecta entender el problema con construir la soluciÃ³n.</p>
    </div>

    <div class="fomentador">
        <p>Si quieres profundizar mÃ¡s en cÃ³mo se organiza el desarrollo antes de programar, puedes ver contenido mÃ¡s detallado en el capÃ­tulo sobre ciclo de vida y diseÃ±o haciendo clic <a href="Capitulo/Contenido/ID_CONTENIDO_PENDIENTE_CICLO_VIDA_DISENO">aquÃ­</a>.</p>
    </div>
</div>';
    DECLARE @Practica NVARCHAR(MAX) = N'
<div class="leccion-practicas">
    <h3>PrÃ¡ctica guiada</h3>
    <p>Lee las siguientes acciones y relaciÃ³nalas con la etapa correcta:</p>
    <ol>
        <li>Hablar con el usuario para saber quÃ© necesita.</li>
        <li>Decidir quÃ© partes tendrÃ¡ el sistema.</li>
        <li>Escribir el cÃ³digo de cada parte.</li>
        <li>Revisar si el programa funciona como se esperaba.</li>
    </ol>
    <p>DespuÃ©s, escribe con tus palabras por quÃ© el diseÃ±o ocurre antes de programar.</p>
    <div class="alert alert-success">
        <p class="alert-title">Criterio de logro</p>
        <p>EstÃ¡ correcto si distingues entender, diseÃ±ar, programar y probar, y si explicas que el diseÃ±o sirve para organizar el sistema antes de escribir cÃ³digo.</p>
    </div>
</div>';
    DECLARE @Ejemplo NVARCHAR(MAX) = N'
<div class="leccion-ejemplos">
    <h3>Ejemplo: sistema para biblioteca</h3>
    <p>Imagina que una biblioteca necesita registrar libros, prÃ©stamos y devoluciones.</p>
    <p>Primero se entiende quÃ© necesita la biblioteca: guardar libros, saber quiÃ©n pidiÃ³ uno y registrar cuÃ¡ndo se devuelve.</p>
    <p>DespuÃ©s llega el diseÃ±o: se decide que habrÃ¡ una parte para libros, otra para prÃ©stamos y otra para devoluciones. Luego se programa cada parte y al final se prueba que todo funcione bien.</p>
    <img src="https://placehold.co/1200x675?text=Analisis+diseno+codigo+pruebas" alt="Ejemplo de fases en un sistema de biblioteca (pendiente)">

    <div class="alert alert-success">
        <p class="alert-title">Lo importante</p>
        <p>El diseÃ±o no reemplaza la programaciÃ³n. La prepara para que el trabajo tenga mÃ¡s claridad y menos desorden.</p>
    </div>
</div>';
    DECLARE @CharpTip NVARCHAR(MAX) = N'<p><strong>Tip Charp:</strong> Si todavÃ­a no sabes quÃ© partes tendrÃ¡ tu programa, aÃºn no es momento de programar: primero toca diseÃ±ar.</p>';
    DECLARE @CharpDialog NVARCHAR(MAX) = N'<p>En esta lecciÃ³n vas a ubicar el diseÃ±o dentro del recorrido completo de desarrollo.</p><p>La meta es que entiendas en quÃ© momento aparece y por quÃ© ocurre antes de escribir cÃ³digo.</p>';
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

