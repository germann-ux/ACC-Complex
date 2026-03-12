-- Insercion de leccion (propuesta final, no ejecutada automaticamente)
-- SubtemaId objetivo: 4
-- Subtema: Principios fundamentales del buen diseno

USE [ACC_Academic];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

BEGIN TRY
    BEGIN TRAN;

    DECLARE @SubtemaId INT = 4;

    IF NOT EXISTS (SELECT 1 FROM acc_academic.SubTemas WHERE Id_SubTema = @SubtemaId)
        THROW 53201, 'No existe el SubTemaId=4 en acc_academic.SubTemas.', 1;

        DECLARE @TituloLeccion NVARCHAR(100) = N'Principios fundamentales del buen diseño';
            DECLARE @DescripcionLeccion NVARCHAR(500) = N'Presenta ideas base para escribir código ordenado, claro y fácil de mantener.';
    DECLARE @NivelBloom NVARCHAR(20) = N'Comprender';
    DECLARE @OrdenSecciones NVARCHAR(MAX) = N'["charpDialog","video","teoria","ejemplo","practica","actividad","charpTip"]';

    DECLARE @TieneActividad BIT = 1;
    DECLARE @UrlActividad NVARCHAR(MAX) = N'https://pendiente.local/actividad/subtema-04-principios-diseno';
    DECLARE @TieneCompilador BIT = 0;
    DECLARE @TieneVideo BIT = 1;
    DECLARE @VideoId NVARCHAR(20) = N'ST04VIDPEND01';

            DECLARE @Teoria NVARCHAR(MAX) = N'
<div class="leccion-teoria">
    <h3>Ideas base</h3>
    <ul>
        <li><strong>Cohesion:</strong> una clase debe enfocarse en una tarea principal.</li>
        <li><strong>Acoplamiento bajo:</strong> una parte no debe depender demasiado de otra.</li>
        <li><strong>Responsabilidad única:</strong> si una clase hace muchas cosas, conviene dividirla.</li>
        <li><strong>Reutilización:</strong> si una logica se repite, es mejor centralizarla.</li>
    </ul>
    <img src="https://placehold.co/1200x675?text=Principios+de+código+ordenado" alt="Principios base de diseño (pendiente)">
</div>';

            DECLARE @Ejemplo NVARCHAR(MAX) = N'
<div class="leccion-ejemplos">
    <h3>Ejemplo</h3>
    <p>Si una sola clase valida usuarios, envia correos y genera reportes, el código se vuelve dificil de mantener.</p>
    <p>Si separas esas tareas en clases pequeñas, todo queda más claro.</p>
    <img src="https://placehold.co/1200x675?text=Clase+grande+vs+clases+pequeñas" alt="Comparacion de enfoque (pendiente)">
</div>';

            DECLARE @Practica NVARCHAR(MAX) = N'
<div class="leccion-practicas">
    <h3>Práctica</h3>
    <ol>
        <li>Elige una clase de tu proyecto.</li>
        <li>Explica cual debería ser su tarea principal.</li>
        <li>Indica que parte moverias a otra clase.</li>
    </ol>
    <div class="alert alert-success">
        <p class="alert-title">Criterio de logro</p>
        <p>Está correcto si detectas mezcla de tareas y propones una separacion concreta.</p>
    </div>
</div>';

            DECLARE @CharpTip NVARCHAR(MAX) = N'<p><strong>Tip Charp:</strong> Si te cuesta explicar para que sirve una clase en una frase, probablemente esta haciendo de más.</p>';
            DECLARE @CharpDialog NVARCHAR(MAX) = N'<p>En esta lección vas a usar reglas simples para detectar cuando un diseño se complica.</p><p>La meta es escribir código más claro para ti y para tu equipo.</p>';

    IF EXISTS
    (
        SELECT 1
        FROM acc_academic.Lecciones
        WHERE SubtemaId = @SubtemaId
          AND TituloLeccion = @TituloLeccion
    )
        THROW 53202, 'Ya existe una leccion con este titulo para SubtemaId=4.', 1;

    IF @NivelBloom NOT IN (N'Recordar', N'Comprender', N'Aplicar', N'Analizar', N'Evaluar', N'Crear')
        THROW 53203, 'NivelBloom invalido.', 1;

    IF ISJSON(@OrdenSecciones) <> 1
        THROW 53204, 'OrdenSecciones no es JSON valido.', 1;

    IF EXISTS
    (
        SELECT j.[value]
        FROM OPENJSON(@OrdenSecciones) j
        GROUP BY j.[value]
        HAVING COUNT(*) > 1
    )
        THROW 53205, 'OrdenSecciones contiene tokens duplicados.', 1;

    IF EXISTS
    (
        SELECT 1
        FROM OPENJSON(@OrdenSecciones) j
        LEFT JOIN
        (
            VALUES (N'video'), (N'teoria'), (N'practica'), (N'ejemplo'),
                   (N'actividad'), (N'compilador'), (N'charpTip'), (N'charpDialog')
        ) permitidas(Token) ON permitidas.Token = CAST(j.[value] AS NVARCHAR(50))
        WHERE permitidas.Token IS NULL
    )
        THROW 53206, 'OrdenSecciones contiene tokens fuera del conjunto permitido.', 1;

    DECLARE @SecTeoria BIT      = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'teoria') THEN 1 ELSE 0 END;
    DECLARE @SecEjemplo BIT     = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'ejemplo') THEN 1 ELSE 0 END;
    DECLARE @SecPractica BIT    = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'practica') THEN 1 ELSE 0 END;
    DECLARE @SecCharpTip BIT    = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'charpTip') THEN 1 ELSE 0 END;
    DECLARE @SecCharpDialog BIT = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'charpDialog') THEN 1 ELSE 0 END;
    DECLARE @SecActividad BIT   = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'actividad') THEN 1 ELSE 0 END;
    DECLARE @SecCompilador BIT  = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'compilador') THEN 1 ELSE 0 END;
    DECLARE @SecVideo BIT       = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'video') THEN 1 ELSE 0 END;

    IF @SecTeoria = 1 AND NULLIF(LTRIM(RTRIM(@Teoria)), N'') IS NULL THROW 53207, 'Falta Teoria.', 1;
    IF @SecEjemplo = 1 AND NULLIF(LTRIM(RTRIM(@Ejemplo)), N'') IS NULL THROW 53208, 'Falta Ejemplo.', 1;
    IF @SecPractica = 1 AND NULLIF(LTRIM(RTRIM(@Practica)), N'') IS NULL THROW 53209, 'Falta Practica.', 1;
    IF @SecCharpTip = 1 AND NULLIF(LTRIM(RTRIM(@CharpTip)), N'') IS NULL THROW 53210, 'Falta CharpTip.', 1;
    IF @SecCharpDialog = 1 AND NULLIF(LTRIM(RTRIM(@CharpDialog)), N'') IS NULL THROW 53211, 'Falta CharpDialog.', 1;
    IF @SecCharpTip = 1 AND @CharpTip LIKE N'%<div%' THROW 53212, 'CharpTip no debe incluir <div>.', 1;
    IF @SecCharpDialog = 1 AND @CharpDialog LIKE N'%<div%' THROW 53213, 'CharpDialog no debe incluir <div>.', 1;
    IF @SecActividad = 1 AND (@TieneActividad = 0 OR NULLIF(LTRIM(RTRIM(@UrlActividad)), N'') IS NULL) THROW 53214, 'Actividad requiere flag y URL.', 1;
    IF @SecActividad = 0 AND (@TieneActividad = 1 OR NULLIF(LTRIM(RTRIM(@UrlActividad)), N'') IS NOT NULL) THROW 53215, 'Sin actividad, limpiar flag/URL.', 1;
    IF @SecCompilador = 1 AND @TieneCompilador = 0 THROW 53216, 'Compilador requiere flag.', 1;
    IF @SecCompilador = 0 AND @TieneCompilador = 1 THROW 53217, 'Sin compilador, flag debe ser 0.', 1;
    IF @SecVideo = 1 AND (@TieneVideo = 0 OR NULLIF(LTRIM(RTRIM(ISNULL(@VideoId, N''))), N'') IS NULL OR @VideoId LIKE N'%youtube%' OR @VideoId LIKE N'%http%') THROW 53218, 'Video requiere flag y VideoId limpio.', 1;
    IF @SecVideo = 0 AND (@TieneVideo = 1 OR NULLIF(LTRIM(RTRIM(ISNULL(@VideoId, N''))), N'') IS NOT NULL) THROW 53219, 'Sin video, limpiar flag/VideoId.', 1;

    INSERT INTO acc_academic.Lecciones
    (
        TituloLeccion, DescripcionLeccion, TieneActividad, UrlActividad, TieneCompilador,
        OrdenSecciones, SubtemaId, Teoria, Practica, Ejemplo, CharpTip, CharpDialog,
        NivelBloom, VideoId, TieneVideo
    )
    VALUES
    (
        @TituloLeccion, @DescripcionLeccion, @TieneActividad, @UrlActividad, @TieneCompilador,
        @OrdenSecciones, @SubtemaId, @Teoria, @Practica, @Ejemplo, @CharpTip, @CharpDialog,
        @NivelBloom, @VideoId, @TieneVideo
    );

    COMMIT TRAN;

    SELECT TOP (1)
        IdLeccion, TituloLeccion, SubtemaId, NivelBloom, OrdenSecciones, TieneActividad, TieneCompilador, TieneVideo
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

