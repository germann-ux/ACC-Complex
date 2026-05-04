-- Propuesta de inserción de lección (no ejecutada automáticamente)
-- Subtema objetivo: "¿Qué es el diseño de software?"
-- Cumple contrato ACC: Bloom, OrdenSecciones, flags, HTML por sección.

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

    IF EXISTS
    (
        SELECT 1
        FROM acc_academic.Lecciones l
        WHERE l.SubtemaId = @SubtemaId
          AND l.TituloLeccion = N'¿Qué es el diseño de software?'
    )
    BEGIN
        THROW 51002, 'Ya existe una lección con ese título para el SubTema objetivo.', 1;
    END;

    DECLARE @TituloLeccion NVARCHAR(100) = N'¿Qué es el diseño de software?';
    DECLARE @DescripcionLeccion NVARCHAR(500) =
        N'Introduce qué es el diseño de software, en qué momento del desarrollo aparece y por qué ayuda a organizar un programa antes de escribir código.';

    DECLARE @NivelBloom NVARCHAR(20) = N'Comprender';
    DECLARE @OrdenSecciones NVARCHAR(MAX) = N'["charpDialog","video","teoria","ejemplo","practica","actividad","charpTip"]';

    DECLARE @TieneActividad BIT = 1;
    DECLARE @UrlActividad NVARCHAR(MAX) = N'https://pendiente.local/actividad/diseno-software/intro';

    DECLARE @TieneCompilador BIT = 0;

    DECLARE @TieneVideo BIT = 0;
    DECLARE @VideoId NVARCHAR(MAX) = N'VIDEO_ID_PENDIENTE_DISENO_001';

    DECLARE @Teoria NVARCHAR(MAX) = N'
<div class="leccion-teoria">
    <h3>Una idea sencilla</h3>
    <p>El diseño de software es la parte en la que decides cómo se va a organizar un programa antes de empezar a programarlo.</p>
    <p>Sirve para pensar con orden qué partes tendrá el sistema, qué hará cada una y cómo trabajarán juntas.</p>
    <img src="https://www.aicad.es/arquitectura-de-software" alt="Organización previa de un programa (pendiente)">

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

    DECLARE @CharpTip NVARCHAR(MAX) = N'<p><strong>Tip Charp:</strong> Si antes de programar ya puedes explicar qué partes tendrá tu sistema y qué hará cada una, ya estás diseñando.</p>';

    DECLARE @CharpDialog NVARCHAR(MAX) = N'<p>En esta lección vas a entender qué significa diseñar software dentro del proceso de desarrollo.</p>
<p>La meta es que distingas el momento en que se organiza el programa antes de escribir código.</p>';

    IF @NivelBloom NOT IN (N'Recordar', N'Comprender', N'Aplicar', N'Analizar', N'Evaluar', N'Crear')
    BEGIN
        THROW 51003, 'NivelBloom inválido para el contrato ACC.', 1;
    END;

    IF ISJSON(@OrdenSecciones) <> 1
    BEGIN
        THROW 51004, 'OrdenSecciones no es JSON válido.', 1;
    END;

    IF EXISTS
    (
        SELECT j.[value]
        FROM OPENJSON(@OrdenSecciones) j
        GROUP BY j.[value]
        HAVING COUNT(*) > 1
    )
    BEGIN
        THROW 51005, 'OrdenSecciones contiene secciones repetidas.', 1;
    END;

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
                (N'charpTip'),
                (N'charpDialog')
        ) AS permitidas(Seccion)
            ON permitidas.Seccion = j.[value]
        WHERE permitidas.Seccion IS NULL
    )
    BEGIN
        THROW 51006, 'OrdenSecciones contiene secciones fuera del conjunto permitido.', 1;
    END;

    DECLARE @SecTeoria BIT      = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'teoria') THEN 1 ELSE 0 END;
    DECLARE @SecEjemplo BIT     = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'ejemplo') THEN 1 ELSE 0 END;
    DECLARE @SecPractica BIT    = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'practica') THEN 1 ELSE 0 END;
    DECLARE @SecCharpTip BIT    = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'charpTip') THEN 1 ELSE 0 END;
    DECLARE @SecCharpDialog BIT = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'charpDialog') THEN 1 ELSE 0 END;
    DECLARE @SecActividad BIT   = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'actividad') THEN 1 ELSE 0 END;
    DECLARE @SecCompilador BIT  = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'compilador') THEN 1 ELSE 0 END;
    DECLARE @SecVideo BIT       = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'video') THEN 1 ELSE 0 END;

    IF @SecTeoria = 1 AND NULLIF(LTRIM(RTRIM(@Teoria)), N'') IS NULL
        THROW 51007, 'La sección teoria está en OrdenSecciones pero Teoria viene vacía.', 1;

    IF @SecEjemplo = 1 AND NULLIF(LTRIM(RTRIM(@Ejemplo)), N'') IS NULL
        THROW 51008, 'La sección ejemplo está en OrdenSecciones pero Ejemplo viene vacío.', 1;

    IF @SecPractica = 1 AND NULLIF(LTRIM(RTRIM(@Practica)), N'') IS NULL
        THROW 51009, 'La sección practica está en OrdenSecciones pero Practica viene vacía.', 1;

    IF @SecCharpTip = 1 AND NULLIF(LTRIM(RTRIM(@CharpTip)), N'') IS NULL
        THROW 51010, 'La sección charpTip está en OrdenSecciones pero CharpTip viene vacío.', 1;

    IF @SecCharpDialog = 1 AND NULLIF(LTRIM(RTRIM(@CharpDialog)), N'') IS NULL
        THROW 51011, 'La sección charpDialog está en OrdenSecciones pero CharpDialog viene vacío.', 1;

    IF @SecCharpTip = 1 AND @CharpTip LIKE N'%<div%'
        THROW 51012, 'CharpTip no debe incluir wrapper raíz <div>.', 1;

    IF @SecCharpDialog = 1 AND @CharpDialog LIKE N'%<div%'
        THROW 51013, 'CharpDialog no debe incluir wrapper raíz <div>.', 1;

    IF @SecActividad = 1 AND (@TieneActividad = 0 OR NULLIF(LTRIM(RTRIM(@UrlActividad)), N'') IS NULL)
        THROW 51014, 'Sección actividad requiere TieneActividad=1 y UrlActividad válida.', 1;

    IF @SecActividad = 0 AND (@TieneActividad = 1 OR NULLIF(LTRIM(RTRIM(@UrlActividad)), N'') IS NOT NULL)
        THROW 51015, 'Sin sección actividad, TieneActividad debe ser 0 y UrlActividad debe estar limpia.', 1;

    IF @SecCompilador = 1 AND @TieneCompilador = 0
        THROW 51016, 'Sección compilador requiere TieneCompilador=1.', 1;

    IF @SecCompilador = 0 AND @TieneCompilador = 1
        THROW 51017, 'Sin sección compilador, TieneCompilador debe ser 0.', 1;

    IF @SecVideo = 1 AND (@TieneVideo = 0 OR NULLIF(LTRIM(RTRIM(ISNULL(@VideoId, N''))), N'') IS NULL OR @VideoId LIKE N'%youtube%' OR @VideoId LIKE N'%http%')
        THROW 51018, 'Sección video requiere TieneVideo=1 y VideoId con ID puro de YouTube.', 1;

    IF @SecVideo = 0 AND (@TieneVideo = 1 OR NULLIF(LTRIM(RTRIM(ISNULL(@VideoId, N''))), N'') IS NOT NULL)
        THROW 51019, 'Sin sección video, TieneVideo debe ser 0 y VideoId debe estar limpio.', 1;

    INSERT INTO acc_academic.Lecciones
    (
        TituloLeccion,
        DescripcionLeccion,
        TieneActividad,
        UrlActividad,
        TieneCompilador,
        OrdenSecciones,
        SubtemaId,
        Teoria,
        Practica,
        Ejemplo,
        CharpTip,
        CharpDialog,
        NivelBloom,
        VideoId,
        TieneVideo
    )
    VALUES
    (
        @TituloLeccion,
        @DescripcionLeccion,
        @TieneActividad,
        @UrlActividad,
        @TieneCompilador,
        @OrdenSecciones,
        @SubtemaId,
        @Teoria,
        @Practica,
        @Ejemplo,
        @CharpTip,
        @CharpDialog,
        @NivelBloom,
        @VideoId,
        @TieneVideo
    );

    COMMIT TRAN;

    SELECT TOP (1)
        l.IdLeccion,
        l.TituloLeccion,
        l.SubtemaId,
        l.NivelBloom,
        l.OrdenSecciones,
        l.TieneActividad,
        l.TieneCompilador,
        l.TieneVideo
    FROM acc_academic.Lecciones l
    WHERE l.SubtemaId = @SubtemaId
      AND l.TituloLeccion = @TituloLeccion
    ORDER BY l.IdLeccion DESC;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRAN;
    THROW;
END CATCH;
GO
