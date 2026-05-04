-- Script final de insercion de leccion con bloques interactivos
-- Generado desde legacy: leccion_subtema_05_ventajas_buen_diseno_vs_consecuencias_malo.sql
-- Modelo vigente: acc_academic.Lecciones + acc_academic.BloquesLeccion
USE [ACC_Academic];
GO
SET NOCOUNT ON;
SET XACT_ABORT ON;
GO
BEGIN TRY
    BEGIN TRAN;
    DECLARE @SubtemaId INT = 5;

    IF NOT EXISTS (SELECT 1 FROM acc_academic.SubTemas WHERE Id_SubTema = @SubtemaId)
        THROW 57001, 'No existe el SubTemaId objetivo en acc_academic.SubTemas.', 1;
    DECLARE @TituloLeccion NVARCHAR(100) = N'Ventajas de un buen diseÃ±o vs consecuencias de uno malo';
    DECLARE @DescripcionLeccion NVARCHAR(500) = N'Compara quÃ© ocurre cuando un sistema estÃ¡ bien organizado y quÃ© problemas aparecen cuando el diseÃ±o es confuso.';
    DECLARE @NivelBloom NVARCHAR(64) = N'Analizar';
    DECLARE @OrdenSecciones NVARCHAR(MAX) = N'["charpDialog","video","teoria","ejemplo","practica","actividad","charpTip"]';
    DECLARE @TieneActividad BIT = 1;
    DECLARE @UrlActividad NVARCHAR(MAX) = N'https://pendiente.local/actividad/subtema-05-ventajas-y-riesgos';
    DECLARE @TieneCompilador BIT = 0;
    DECLARE @TieneVideo BIT = 1;
    DECLARE @VideoId NVARCHAR(200) = N'ST05VIDPEND01';
    DECLARE @MermaidTitulo NVARCHAR(160) = NULL;
    DECLARE @MermaidDescripcion NVARCHAR(MAX) = NULL;
    DECLARE @MermaidCodigo NVARCHAR(MAX) = NULL;
    DECLARE @Teoria NVARCHAR(MAX) = N'
<div class="leccion-teoria">
    <h3>El diseÃ±o se nota cuando hay que cambiar algo</h3>
    <p>Un buen diseÃ±o no solo ayuda al inicio del proyecto. Su valor se nota sobre todo cuando hay que corregir errores, agregar funciones nuevas o modificar una parte del sistema.</p>
    <p>Cuando el programa estÃ¡ bien organizado, esos cambios suelen ser mÃ¡s claros y mÃ¡s seguros. Cuando estÃ¡ mal organizado, hasta un cambio pequeÃ±o puede traer problemas en cadena.</p>

    <h3>QuÃ© pasa con un buen diseÃ±o</h3>
    <ul>
        <li>Es mÃ¡s fÃ¡cil entender dÃ³nde hacer un cambio.</li>
        <li>Corregir errores toma menos tiempo.</li>
        <li>Agregar una funciÃ³n nueva implica menos riesgo.</li>
        <li>El sistema resulta mÃ¡s claro para quien lo mantiene.</li>
    </ul>

    <h3>QuÃ© pasa con un mal diseÃ±o</h3>
    <ul>
        <li>Un cambio pequeÃ±o puede afectar partes que no parecÃ­an relacionadas.</li>
        <li>Encontrar errores cuesta mÃ¡s trabajo.</li>
        <li>Agregar funciones nuevas se vuelve mÃ¡s inseguro.</li>
        <li>El equipo empieza a evitar cambios por miedo a romper algo.</li>
    </ul>

    <img src="https://placehold.co/1200x675?text=Diseno+ordenado+vs+diseno+confuso" alt="ComparaciÃ³n entre un diseÃ±o claro y uno confuso (pendiente)">

    <div class="alert alert-info">
        <p class="alert-title">Punto clave</p>
        <p>La diferencia entre un buen diseÃ±o y uno malo no siempre se nota el primer dÃ­a. Muchas veces se hace evidente cuando el sistema empieza a cambiar.</p>
    </div>

    <div class="fomentador">
        <p>Si quieres profundizar mÃ¡s en cÃ³mo impacta el diseÃ±o en los cambios, errores y mantenimiento del sistema, puedes ver contenido mÃ¡s detallado en el capÃ­tulo sobre ventajas y riesgos del diseÃ±o haciendo clic <a href="Capitulo/Contenido/ID_CONTENIDO_PENDIENTE_VENTAJAS_RIESGOS_DISENO">aquÃ­</a>.</p>
    </div>
</div>';
    DECLARE @Practica NVARCHAR(MAX) = N'
<div class="leccion-practicas">
    <h3>PrÃ¡ctica guiada</h3>
    <p>Lee este caso: para agregar un dato nuevo, un equipo tuvo que modificar muchos archivos y despuÃ©s aparecieron errores en otras partes del sistema.</p>
    <ol>
        <li>Escribe dos seÃ±ales que muestran que ese diseÃ±o tenÃ­a problemas.</li>
        <li>Explica una ventaja que habrÃ­a tenido un diseÃ±o mÃ¡s claro en ese mismo caso.</li>
        <li>PropÃ³n una mejora general para evitar que un cambio simple afecte demasiadas partes.</li>
    </ol>
    <div class="alert alert-success">
        <p class="alert-title">Criterio de logro</p>
        <p>EstÃ¡ correcto si identificas seÃ±ales de desorden, explicas una ventaja real de un buen diseÃ±o y propones una mejora orientada a reducir riesgo al cambiar.</p>
    </div>
</div>';
    DECLARE @Ejemplo NVARCHAR(MAX) = N'
<div class="leccion-ejemplos">
    <h3>Un mismo cambio en dos escenarios</h3>
    <p>Imagina que en un sistema escolar ahora se necesita guardar el telÃ©fono del alumno.</p>

    <div class="alert alert-success">
        <p class="alert-title">Escenario con buen diseÃ±o</p>
        <p>El cambio se hace en la parte del sistema que maneja alumnos. Las demÃ¡s partes casi no se tocan y el ajuste resulta mÃ¡s rÃ¡pido y predecible.</p>
    </div>

    <div class="alert alert-error">
        <p class="alert-title">Escenario con mal diseÃ±o</p>
        <p>La informaciÃ³n del alumno estÃ¡ mezclada en varias partes. Para agregar el telÃ©fono hay que tocar muchos archivos y aparecen errores en lugares que no se esperaban.</p>
    </div>

    <img src="https://placehold.co/1200x675?text=Impacto+de+un+mismo+cambio" alt="ComparaciÃ³n del impacto de un mismo cambio (pendiente)">

    <p>El cambio es el mismo, pero el esfuerzo y el riesgo cambian mucho segÃºn el diseÃ±o que tenga el sistema.</p>
</div>';
    DECLARE @CharpTip NVARCHAR(MAX) = N'<p><strong>Tip Charp:</strong> Un buen diseÃ±o suele pasar desapercibido hasta que necesitas cambiar algo. AhÃ­ es donde se nota si el sistema ayuda o estorba.</p>';
    DECLARE @CharpDialog NVARCHAR(MAX) = N'<p>En esta lecciÃ³n vas a comparar lo que ocurre cuando un sistema estÃ¡ bien organizado y lo que ocurre cuando no lo estÃ¡.</p><p>La meta es que relaciones el diseÃ±o con cambios, errores y mantenimiento del trabajo diario.</p>';
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

