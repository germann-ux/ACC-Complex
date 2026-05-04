-- Insercion de leccion (propuesta final, no ejecutada automaticamente)
-- SubtemaId objetivo: 2
-- Subtema: Fases del diseno en el ciclo de vida

USE [ACC_Academic];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

BEGIN TRY
    BEGIN TRAN;

    DECLARE @SubtemaId INT = 2;

    IF NOT EXISTS (SELECT 1 FROM acc_academic.SubTemas WHERE Id_SubTema = @SubtemaId)
        THROW 53001, 'No existe el SubTemaId=2 en acc_academic.SubTemas.', 1;

    DECLARE @TituloLeccion NVARCHAR(100) = N'Fases del diseño en el ciclo de vida';
    DECLARE @DescripcionLeccion NVARCHAR(500) = N'Explica en qué momento del desarrollo aparece el diseño y cómo se relaciona con entender el problema, programar y probar.';
    DECLARE @NivelBloom NVARCHAR(20) = N'Comprender';
    DECLARE @OrdenSecciones NVARCHAR(MAX) = N'["charpDialog","video","teoria","mermaid","ejemplo","practica","actividad","charpTip"]';

    DECLARE @TieneActividad BIT = 1;
    DECLARE @UrlActividad NVARCHAR(MAX) = N'https://pendiente.local/actividad/subtema-02-fases-diseno';
    DECLARE @TieneCompilador BIT = 0;
    DECLARE @TieneVideo BIT = 1;
    DECLARE @VideoId NVARCHAR(20) = N'ST02VIDPEND01';

    DECLARE @MermaidTitulo NVARCHAR(200) = N'Recorrido basico del desarrollo';
    DECLARE @MermaidDescripcion NVARCHAR(500) = N'Muestra el orden general del trabajo: entender el problema, disenar, programar y probar. El diseno aparece antes de escribir codigo.';
    DECLARE @MermaidCodigo NVARCHAR(MAX) = N'flowchart LR
    A["Entender el problema"] --> B["Disenar el sistema"]
    B --> C["Programar"]
    C --> D["Probar"]
    D --> E["Ajustar si hace falta"]

    style B fill:#ffe7a3,stroke:#b7791f,stroke-width:2px';

    DECLARE @Teoria NVARCHAR(MAX) = N'
<div class="leccion-teoria">
    <h3>El diseño aparece antes de programar</h3>
    <p>En el desarrollo de software no se empieza programando de inmediato. Primero se entiende qué problema se quiere resolver, después se organiza el programa, luego se escribe el código y al final se revisa que funcione bien.</p>
    <p>La fase de diseño está justo en medio de ese camino. Su trabajo es ordenar cómo se construirá el sistema antes de empezar a programarlo.</p>
    <ul>
        <li><strong>Entender el problema:</strong> saber qué necesita el usuario o la institución.</li>
        <li><strong>Diseño:</strong> decidir qué partes tendrá el programa y qué hará cada una.</li>
        <li><strong>Programación:</strong> escribir el código con base en esas decisiones.</li>
        <li><strong>Pruebas:</strong> revisar si el programa cumple lo esperado.</li>
    </ul>
    <img src="https://placehold.co/1200x675?text=Fases+del+ciclo+de+vida" alt="Etapas del desarrollo de software (pendiente)">

    <div class="alert alert-info">
        <p class="alert-title">Idea clave</p>
        <p>Diseñar no es una tarea aparte sin relación con el resto del trabajo. Diseñar es la etapa que conecta entender el problema con construir la solución.</p>
    </div>

    <div class="fomentador">
        <p>Si quieres profundizar más en cómo se organiza el desarrollo antes de programar, puedes ver contenido más detallado en el capítulo sobre ciclo de vida y diseño haciendo clic <a href="Capitulo/Contenido/ID_CONTENIDO_PENDIENTE_CICLO_VIDA_DISENO">aquí</a>.</p>
    </div>
</div>';

    DECLARE @Ejemplo NVARCHAR(MAX) = N'
<div class="leccion-ejemplos">
    <h3>Ejemplo: sistema para biblioteca</h3>
    <p>Imagina que una biblioteca necesita registrar libros, préstamos y devoluciones.</p>
    <p>Primero se entiende qué necesita la biblioteca: guardar libros, saber quién pidió uno y registrar cuándo se devuelve.</p>
    <p>Después llega el diseño: se decide que habrá una parte para libros, otra para préstamos y otra para devoluciones. Luego se programa cada parte y al final se prueba que todo funcione bien.</p>
    <img src="https://placehold.co/1200x675?text=Analisis+diseno+codigo+pruebas" alt="Ejemplo de fases en un sistema de biblioteca (pendiente)">

    <div class="alert alert-success">
        <p class="alert-title">Lo importante</p>
        <p>El diseño no reemplaza la programación. La prepara para que el trabajo tenga más claridad y menos desorden.</p>
    </div>
</div>';

    DECLARE @Practica NVARCHAR(MAX) = N'
<div class="leccion-practicas">
    <h3>Práctica guiada</h3>
    <p>Lee las siguientes acciones y relaciónalas con la etapa correcta:</p>
    <ol>
        <li>Hablar con el usuario para saber qué necesita.</li>
        <li>Decidir qué partes tendrá el sistema.</li>
        <li>Escribir el código de cada parte.</li>
        <li>Revisar si el programa funciona como se esperaba.</li>
    </ol>
    <p>Después, escribe con tus palabras por qué el diseño ocurre antes de programar.</p>
    <div class="alert alert-success">
        <p class="alert-title">Criterio de logro</p>
        <p>Está correcto si distingues entender, diseñar, programar y probar, y si explicas que el diseño sirve para organizar el sistema antes de escribir código.</p>
    </div>
</div>';

    DECLARE @CharpTip NVARCHAR(MAX) = N'<p><strong>Tip Charp:</strong> Si todavía no sabes qué partes tendrá tu programa, aún no es momento de programar: primero toca diseñar.</p>';
    DECLARE @CharpDialog NVARCHAR(MAX) = N'<p>En esta lección vas a ubicar el diseño dentro del recorrido completo de desarrollo.</p><p>La meta es que entiendas en qué momento aparece y por qué ocurre antes de escribir código.</p>';

    IF EXISTS
    (
        SELECT 1
        FROM acc_academic.Lecciones
        WHERE SubtemaId = @SubtemaId
          AND TituloLeccion = @TituloLeccion
    )
        THROW 53002, 'Ya existe una leccion con este titulo para SubtemaId=2.', 1;

    IF @NivelBloom NOT IN (N'Recordar', N'Comprender', N'Aplicar', N'Analizar', N'Evaluar', N'Crear')
        THROW 53003, 'NivelBloom invalido.', 1;

    IF ISJSON(@OrdenSecciones) <> 1
        THROW 53004, 'OrdenSecciones no es JSON valido.', 1;

    IF EXISTS
    (
        SELECT j.[value]
        FROM OPENJSON(@OrdenSecciones) j
        GROUP BY j.[value]
        HAVING COUNT(*) > 1
    )
        THROW 53005, 'OrdenSecciones contiene tokens duplicados.', 1;

    IF EXISTS
    (
        SELECT 1
        FROM OPENJSON(@OrdenSecciones) j
        LEFT JOIN
        (
            VALUES (N'video'), (N'teoria'), (N'mermaid'), (N'practica'), (N'ejemplo'),
                   (N'actividad'), (N'compilador'), (N'charpTip'), (N'charpDialog')
        ) permitidas(Token) ON permitidas.Token = CAST(j.[value] AS NVARCHAR(50))
        WHERE permitidas.Token IS NULL
    )
        THROW 53006, 'OrdenSecciones contiene tokens fuera del conjunto permitido.', 1;

    DECLARE @SecTeoria BIT      = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'teoria') THEN 1 ELSE 0 END;
    DECLARE @SecMermaid BIT     = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'mermaid') THEN 1 ELSE 0 END;
    DECLARE @SecEjemplo BIT     = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'ejemplo') THEN 1 ELSE 0 END;
    DECLARE @SecPractica BIT    = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'practica') THEN 1 ELSE 0 END;
    DECLARE @SecCharpTip BIT    = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'charpTip') THEN 1 ELSE 0 END;
    DECLARE @SecCharpDialog BIT = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'charpDialog') THEN 1 ELSE 0 END;
    DECLARE @SecActividad BIT   = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'actividad') THEN 1 ELSE 0 END;
    DECLARE @SecCompilador BIT  = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'compilador') THEN 1 ELSE 0 END;
    DECLARE @SecVideo BIT       = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'video') THEN 1 ELSE 0 END;

    IF @SecTeoria = 1 AND NULLIF(LTRIM(RTRIM(@Teoria)), N'') IS NULL THROW 53007, 'Falta Teoria.', 1;
    IF @SecMermaid = 1 AND NULLIF(LTRIM(RTRIM(@MermaidCodigo)), N'') IS NULL THROW 530071, 'Falta MermaidCodigo.', 1;
    IF @SecEjemplo = 1 AND NULLIF(LTRIM(RTRIM(@Ejemplo)), N'') IS NULL THROW 53008, 'Falta Ejemplo.', 1;
    IF @SecPractica = 1 AND NULLIF(LTRIM(RTRIM(@Practica)), N'') IS NULL THROW 53009, 'Falta Practica.', 1;
    IF @SecCharpTip = 1 AND NULLIF(LTRIM(RTRIM(@CharpTip)), N'') IS NULL THROW 53010, 'Falta CharpTip.', 1;
    IF @SecCharpDialog = 1 AND NULLIF(LTRIM(RTRIM(@CharpDialog)), N'') IS NULL THROW 53011, 'Falta CharpDialog.', 1;
    IF @SecCharpTip = 1 AND @CharpTip LIKE N'%<div%' THROW 53012, 'CharpTip no debe incluir <div>.', 1;
    IF @SecCharpDialog = 1 AND @CharpDialog LIKE N'%<div%' THROW 53013, 'CharpDialog no debe incluir <div>.', 1;
    IF @SecActividad = 1 AND (@TieneActividad = 0 OR NULLIF(LTRIM(RTRIM(@UrlActividad)), N'') IS NULL) THROW 53014, 'Actividad requiere flag y URL.', 1;
    IF @SecActividad = 0 AND (@TieneActividad = 1 OR NULLIF(LTRIM(RTRIM(@UrlActividad)), N'') IS NOT NULL) THROW 53015, 'Sin actividad, limpiar flag/URL.', 1;
    IF @SecCompilador = 1 AND @TieneCompilador = 0 THROW 53016, 'Compilador requiere flag.', 1;
    IF @SecCompilador = 0 AND @TieneCompilador = 1 THROW 53017, 'Sin compilador, flag debe ser 0.', 1;
    IF @SecVideo = 1 AND (@TieneVideo = 0 OR NULLIF(LTRIM(RTRIM(ISNULL(@VideoId, N''))), N'') IS NULL OR @VideoId LIKE N'%youtube%' OR @VideoId LIKE N'%http%') THROW 53018, 'Video requiere flag y VideoId limpio.', 1;
    IF @SecVideo = 0 AND (@TieneVideo = 1 OR NULLIF(LTRIM(RTRIM(ISNULL(@VideoId, N''))), N'') IS NOT NULL) THROW 53019, 'Sin video, limpiar flag/VideoId.', 1;

    INSERT INTO acc_academic.Lecciones
    (
        TituloLeccion, DescripcionLeccion, TieneActividad, UrlActividad, TieneCompilador,
        OrdenSecciones, SubtemaId, MermaidTitulo, MermaidDescripcion, MermaidCodigo,
        Teoria, Practica, Ejemplo, CharpTip, CharpDialog,
        NivelBloom, VideoId, TieneVideo
    )
    VALUES
    (
        @TituloLeccion, @DescripcionLeccion, @TieneActividad, @UrlActividad, @TieneCompilador,
        @OrdenSecciones, @SubtemaId, @MermaidTitulo, @MermaidDescripcion, @MermaidCodigo,
        @Teoria, @Practica, @Ejemplo, @CharpTip, @CharpDialog,
        @NivelBloom, @VideoId, @TieneVideo
    );

    COMMIT TRAN;

    SELECT TOP (1)
        IdLeccion, TituloLeccion, SubtemaId, NivelBloom, OrdenSecciones, MermaidTitulo, TieneActividad, TieneCompilador, TieneVideo
    FROM acc_academic.Lecciones
    WHERE SubtemaId = @SubtemaId
      AND TituloLeccion = @TituloLeccion
    ORDER BY IdLeccion DESC;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRAN;
    THROW;
END CATCH;
GO
