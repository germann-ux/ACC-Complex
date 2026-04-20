-- Insercion de leccion (propuesta final, no ejecutada automaticamente)
-- SubtemaId objetivo: 111
-- Subtema: Historia breve y evolucion de C#
-- Leccion 1 de 2 para este subtema

USE [ACC_Academic];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

BEGIN TRY
    BEGIN TRAN;

    DECLARE @SubtemaId INT = 111;

    IF NOT EXISTS (SELECT 1 FROM acc_academic.SubTemas WHERE Id_SubTema = @SubtemaId)
        THROW 54401, 'No existe el SubTemaId=111 en acc_academic.SubTemas.', 1;

    DECLARE @TituloLeccion NVARCHAR(100) = N'Origen y evolucion de C#';
    DECLARE @DescripcionLeccion NVARCHAR(500) = N'Explica como surge C#, su relacion inicial con Microsoft y .NET, y como fue creciendo hasta convertirse en un lenguaje moderno.';
    DECLARE @NivelBloom NVARCHAR(20) = N'Comprender';
    DECLARE @OrdenSecciones NVARCHAR(MAX) = N'["charpDialog","video","teoria","mermaid","ejemplo","practica","actividad","charpTip"]';

    DECLARE @TieneActividad BIT = 1;
    DECLARE @UrlActividad NVARCHAR(MAX) = N'https://pendiente.local/actividad/subtema-111-origen-evolucion-csharp';
    DECLARE @TieneCompilador BIT = 0;
    DECLARE @TieneVideo BIT = 1;
    DECLARE @VideoId NVARCHAR(20) = N'ST111VIDPEND01';

    DECLARE @MermaidTitulo NVARCHAR(200) = N'Evolucion general de C#';
    DECLARE @MermaidDescripcion NVARCHAR(500) = N'Muestra el recorrido de C# desde su origen con Microsoft hasta su etapa moderna dentro del ecosistema .NET.';
    DECLARE @MermaidCodigo NVARCHAR(MAX) = N'flowchart LR
    A["Origen con Microsoft"] --> B["Trabajo con .NET"]
    B --> C["Crecimiento del lenguaje"]
    C --> D["Mas escenarios de uso"]
    D --> E["Etapa moderna"]

    style C fill:#1e1e2a,stroke:#9926fe,stroke-width:2px,color:#f8fafc';

    DECLARE @Teoria NVARCHAR(MAX) = N'
<div class="leccion-teoria">
    <h3>Como aparece C#</h3>
    <p>C# surge en el entorno de Microsoft como una respuesta a la necesidad de contar con un lenguaje moderno, claro y fuerte para construir software dentro de su plataforma de desarrollo.</p>
    <p>Desde el inicio estuvo muy relacionado con .NET. Eso hizo que C# no creciera como una herramienta aislada, sino como parte de un ecosistema mas amplio para crear aplicaciones.</p>

    <h3>Como fue creciendo</h3>
    <p>Con el tiempo, C# no se quedo igual. Fue agregando nuevas capacidades y mejorando su forma de expresar ideas en codigo.</p>
    <p>Ese crecimiento permitio que el lenguaje pasara de un contexto mas cerrado a una etapa donde podia usarse en mas tipos de proyecto y con una vision mas moderna del desarrollo.</p>

    <h3>Por que importa conocer esta evolucion</h3>
    <p>Conocer de donde viene C# ayuda a entender por que el lenguaje tiene hoy ciertas caracteristicas y por que sigue siendo una herramienta vigente.</p>
    <img src="https://placehold.co/1200x675?text=Origen+y+evolucion+de+Csharp" alt="Recorrido general de C# (pendiente)">

    <div class="alert alert-info">
        <p class="alert-title">Idea clave</p>
        <p>C# no aparecio terminado desde el primer dia. Fue evolucionando para responder mejor a nuevas formas de construir software.</p>
    </div>

    <div class="fomentador">
        <p>Si quieres profundizar más en el origen de C# y en los cambios que marcaron su crecimiento, puedes ver contenido más detallado en el capítulo sobre evolución del lenguaje haciendo clic <a href="Capitulo/Contenido/ID_CONTENIDO_PENDIENTE_ORIGEN_EVOLUCION_CSHARP">aquí</a>.</p>
    </div>
</div>';

    DECLARE @Ejemplo NVARCHAR(MAX) = N'
<div class="leccion-ejemplos">
    <h3>Un lenguaje que no se quedo quieto</h3>
    <p>Imagina un lenguaje que solo sirviera para un tipo muy limitado de proyecto y que nunca mejorara su forma de trabajar. Con el tiempo, quedaria atras frente a nuevas necesidades.</p>
    <p>C# siguio otro camino: fue creciendo junto a su ecosistema, mejorando su forma de escribir codigo y ampliando los escenarios donde puede usarse.</p>
    <p>Por eso hoy aparece en desarrollo web, servicios, escritorio, videojuegos y muchos otros contextos.</p>
    <img src="https://placehold.co/1200x675?text=Lenguaje+que+evoluciona" alt="Lenguaje que crece con el tiempo (pendiente)">

    <div class="alert alert-success">
        <p class="alert-title">Lo importante</p>
        <p>La evolucion de un lenguaje no es solo un dato historico. Tambien explica por que sigue siendo util en el presente.</p>
    </div>
</div>';

    DECLARE @Practica NVARCHAR(MAX) = N'
<div class="leccion-practicas">
    <h3>Practica guiada</h3>
    <ol>
        <li>Explica con tus palabras por que C# no se desarrollo separado de .NET.</li>
        <li>Describe una razon por la que un lenguaje necesita evolucionar con el tiempo.</li>
        <li>Indica que ventaja tiene para un programador aprender un lenguaje que sigue creciendo y adaptandose.</li>
    </ol>
    <div class="alert alert-success">
        <p class="alert-title">Criterio de logro</p>
        <p>Esta correcto si relacionas el origen de C# con su ecosistema y reconoces que su evolucion explica su vigencia actual.</p>
    </div>
</div>';

    DECLARE @CharpTip NVARCHAR(MAX) = N'<p><strong>Tip Charp:</strong> Cuando un lenguaje sigue evolucionando, no solo cambia su sintaxis: tambien cambia el tipo de problemas que puede ayudar a resolver.</p>';
    DECLARE @CharpDialog NVARCHAR(MAX) = N'<p>En esta leccion vas a entender de donde viene C# y como fue creciendo con el tiempo.</p><p>La meta es que lo veas como una herramienta que fue cambiando para adaptarse mejor al desarrollo de software.</p>';

    IF EXISTS
    (
        SELECT 1
        FROM acc_academic.Lecciones
        WHERE SubtemaId = @SubtemaId
          AND TituloLeccion = @TituloLeccion
    )
        THROW 54402, 'Ya existe una leccion con este titulo para SubtemaId=111.', 1;

    IF @NivelBloom NOT IN (N'Recordar', N'Comprender', N'Aplicar', N'Analizar', N'Evaluar', N'Crear')
        THROW 54403, 'NivelBloom invalido.', 1;

    IF ISJSON(@OrdenSecciones) <> 1
        THROW 54404, 'OrdenSecciones no es JSON valido.', 1;

    IF EXISTS
    (
        SELECT j.[value]
        FROM OPENJSON(@OrdenSecciones) j
        GROUP BY j.[value]
        HAVING COUNT(*) > 1
    )
        THROW 54405, 'OrdenSecciones contiene tokens duplicados.', 1;

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
        THROW 54406, 'OrdenSecciones contiene tokens fuera del conjunto permitido.', 1;

    DECLARE @SecTeoria BIT      = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'teoria') THEN 1 ELSE 0 END;
    DECLARE @SecMermaid BIT     = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'mermaid') THEN 1 ELSE 0 END;
    DECLARE @SecEjemplo BIT     = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'ejemplo') THEN 1 ELSE 0 END;
    DECLARE @SecPractica BIT    = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'practica') THEN 1 ELSE 0 END;
    DECLARE @SecCharpTip BIT    = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'charpTip') THEN 1 ELSE 0 END;
    DECLARE @SecCharpDialog BIT = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'charpDialog') THEN 1 ELSE 0 END;
    DECLARE @SecActividad BIT   = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'actividad') THEN 1 ELSE 0 END;
    DECLARE @SecCompilador BIT  = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'compilador') THEN 1 ELSE 0 END;
    DECLARE @SecVideo BIT       = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'video') THEN 1 ELSE 0 END;

    IF @SecTeoria = 1 AND NULLIF(LTRIM(RTRIM(@Teoria)), N'') IS NULL THROW 54407, 'Falta Teoria.', 1;
    IF @SecMermaid = 1 AND NULLIF(LTRIM(RTRIM(@MermaidCodigo)), N'') IS NULL THROW 544071, 'Falta MermaidCodigo.', 1;
    IF @SecEjemplo = 1 AND NULLIF(LTRIM(RTRIM(@Ejemplo)), N'') IS NULL THROW 54408, 'Falta Ejemplo.', 1;
    IF @SecPractica = 1 AND NULLIF(LTRIM(RTRIM(@Practica)), N'') IS NULL THROW 54409, 'Falta Practica.', 1;
    IF @SecCharpTip = 1 AND NULLIF(LTRIM(RTRIM(@CharpTip)), N'') IS NULL THROW 54410, 'Falta CharpTip.', 1;
    IF @SecCharpDialog = 1 AND NULLIF(LTRIM(RTRIM(@CharpDialog)), N'') IS NULL THROW 54411, 'Falta CharpDialog.', 1;
    IF @SecCharpTip = 1 AND @CharpTip LIKE N'%<div%' THROW 54412, 'CharpTip no debe incluir <div>.', 1;
    IF @SecCharpDialog = 1 AND @CharpDialog LIKE N'%<div%' THROW 54413, 'CharpDialog no debe incluir <div>.', 1;
    IF @SecActividad = 1 AND (@TieneActividad = 0 OR NULLIF(LTRIM(RTRIM(@UrlActividad)), N'') IS NULL) THROW 54414, 'Actividad requiere flag y URL.', 1;
    IF @SecActividad = 0 AND (@TieneActividad = 1 OR NULLIF(LTRIM(RTRIM(@UrlActividad)), N'') IS NOT NULL) THROW 54415, 'Sin actividad, limpiar flag/URL.', 1;
    IF @SecCompilador = 1 AND @TieneCompilador = 0 THROW 54416, 'Compilador requiere flag.', 1;
    IF @SecCompilador = 0 AND @TieneCompilador = 1 THROW 54417, 'Sin compilador, flag debe ser 0.', 1;
    IF @SecVideo = 1 AND (@TieneVideo = 0 OR NULLIF(LTRIM(RTRIM(ISNULL(@VideoId, N''))), N'') IS NULL OR @VideoId LIKE N'%youtube%' OR @VideoId LIKE N'%http%') THROW 54418, 'Video requiere flag y VideoId limpio.', 1;
    IF @SecVideo = 0 AND (@TieneVideo = 1 OR NULLIF(LTRIM(RTRIM(ISNULL(@VideoId, N''))), N'') IS NOT NULL) THROW 54419, 'Sin video, limpiar flag/VideoId.', 1;

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
