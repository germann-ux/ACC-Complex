-- Insercion de leccion (propuesta final, no ejecutada automaticamente)
-- SubtemaId objetivo: 5
-- Subtema: Ventajas de un buen diseno vs consecuencias de uno malo

USE [ACC_Academic];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

BEGIN TRY
    BEGIN TRAN;

    DECLARE @SubtemaId INT = 5;

    IF NOT EXISTS (SELECT 1 FROM acc_academic.SubTemas WHERE Id_SubTema = @SubtemaId)
        THROW 53301, 'No existe el SubTemaId=5 en acc_academic.SubTemas.', 1;

        DECLARE @TituloLeccion NVARCHAR(100) = N'Ventajas de un buen diseño vs consecuencias de uno malo';
            DECLARE @DescripcionLeccion NVARCHAR(500) = N'Compara que pasa cuando un programa esta bien organizado y que problemas aparecen cuando el diseño es confuso.';
    DECLARE @NivelBloom NVARCHAR(20) = N'Analizar';
    DECLARE @OrdenSecciones NVARCHAR(MAX) = N'["charpDialog","video","teoria","ejemplo","practica","actividad","charpTip"]';

    DECLARE @TieneActividad BIT = 1;
    DECLARE @UrlActividad NVARCHAR(MAX) = N'https://pendiente.local/actividad/subtema-05-ventajas-y-riesgos';
    DECLARE @TieneCompilador BIT = 0;
    DECLARE @TieneVideo BIT = 1;
    DECLARE @VideoId NVARCHAR(20) = N'ST05VIDPEND01';

            DECLARE @Teoria NVARCHAR(MAX) = N'
<div class="leccion-teoria">
    <h3>Con buen diseño</h3>
    <ul>
        <li>Los cambios son más rapidos.</li>
        <li>Es más fácil corregir errores.</li>
        <li>Agregar funciones nuevas es más seguro.</li>
    </ul>
    <h3>Con mal diseño</h3>
    <ul>
        <li>Un cambio pequeno rompe otras partes.</li>
        <li>Corregir errores toma más tiempo.</li>
        <li>El equipo evita tocar código por miedo a romper algo.</li>
    </ul>
    <img src="https://placehold.co/1200x675?text=Diseño+ordenado+vs+diseño+confuso" alt="Comparacion de resultados (pendiente)">
</div>';

            DECLARE @Ejemplo NVARCHAR(MAX) = N'
<div class="leccion-ejemplos">
    <h3>Comparacion</h3>
    <div class="alert alert-success">
        <p class="alert-title">Caso ordenado</p>
        <p>Se agrega un campo nuevo y solo se modifica una parte del código.</p>
    </div>
    <div class="alert alert-error">
        <p class="alert-title">Caso confuso</p>
        <p>El mismo cambio obliga a tocar muchos archivos y aparecen errores en cadena.</p>
    </div>
    <img src="https://placehold.co/1200x675?text=Impacto+de+un+cambio+simple" alt="Impacto de cambio en dos escenarios (pendiente)">
</div>';

            DECLARE @Practica NVARCHAR(MAX) = N'
<div class="leccion-practicas">
    <h3>Práctica</h3>
    <ol>
        <li>Describe un cambio pequeno que hiciste en un proyecto.</li>
        <li>Explica si fue fácil o dificil y por que.</li>
        <li>Propone una mejora de diseño para evitar ese problema.</li>
    </ol>
    <div class="alert alert-success">
        <p class="alert-title">Criterio de logro</p>
        <p>Está correcto si explicas el impacto real del diseño en tiempo y errores.</p>
    </div>
</div>';

            DECLARE @CharpTip NVARCHAR(MAX) = N'<p><strong>Tip Charp:</strong> Un buen diseño se nota cuando cambiar algo no se vuelve una cadena de problemas.</p>';
            DECLARE @CharpDialog NVARCHAR(MAX) = N'<p>En esta lección vas a comparar código ordenado contra código desordenado.</p><p>La meta es ver como el diseño impacta en el trabajo diario.</p>';

    IF EXISTS
    (
        SELECT 1
        FROM acc_academic.Lecciones
        WHERE SubtemaId = @SubtemaId
          AND TituloLeccion = @TituloLeccion
    )
        THROW 53302, 'Ya existe una leccion con este titulo para SubtemaId=5.', 1;

    IF @NivelBloom NOT IN (N'Recordar', N'Comprender', N'Aplicar', N'Analizar', N'Evaluar', N'Crear')
        THROW 53303, 'NivelBloom invalido.', 1;

    IF ISJSON(@OrdenSecciones) <> 1
        THROW 53304, 'OrdenSecciones no es JSON valido.', 1;

    IF EXISTS
    (
        SELECT j.[value]
        FROM OPENJSON(@OrdenSecciones) j
        GROUP BY j.[value]
        HAVING COUNT(*) > 1
    )
        THROW 53305, 'OrdenSecciones contiene tokens duplicados.', 1;

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
        THROW 53306, 'OrdenSecciones contiene tokens fuera del conjunto permitido.', 1;

    DECLARE @SecTeoria BIT      = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'teoria') THEN 1 ELSE 0 END;
    DECLARE @SecEjemplo BIT     = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'ejemplo') THEN 1 ELSE 0 END;
    DECLARE @SecPractica BIT    = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'practica') THEN 1 ELSE 0 END;
    DECLARE @SecCharpTip BIT    = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'charpTip') THEN 1 ELSE 0 END;
    DECLARE @SecCharpDialog BIT = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'charpDialog') THEN 1 ELSE 0 END;
    DECLARE @SecActividad BIT   = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'actividad') THEN 1 ELSE 0 END;
    DECLARE @SecCompilador BIT  = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'compilador') THEN 1 ELSE 0 END;
    DECLARE @SecVideo BIT       = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'video') THEN 1 ELSE 0 END;

    IF @SecTeoria = 1 AND NULLIF(LTRIM(RTRIM(@Teoria)), N'') IS NULL THROW 53307, 'Falta Teoria.', 1;
    IF @SecEjemplo = 1 AND NULLIF(LTRIM(RTRIM(@Ejemplo)), N'') IS NULL THROW 53308, 'Falta Ejemplo.', 1;
    IF @SecPractica = 1 AND NULLIF(LTRIM(RTRIM(@Practica)), N'') IS NULL THROW 53309, 'Falta Practica.', 1;
    IF @SecCharpTip = 1 AND NULLIF(LTRIM(RTRIM(@CharpTip)), N'') IS NULL THROW 53310, 'Falta CharpTip.', 1;
    IF @SecCharpDialog = 1 AND NULLIF(LTRIM(RTRIM(@CharpDialog)), N'') IS NULL THROW 53311, 'Falta CharpDialog.', 1;
    IF @SecCharpTip = 1 AND @CharpTip LIKE N'%<div%' THROW 53312, 'CharpTip no debe incluir <div>.', 1;
    IF @SecCharpDialog = 1 AND @CharpDialog LIKE N'%<div%' THROW 53313, 'CharpDialog no debe incluir <div>.', 1;
    IF @SecActividad = 1 AND (@TieneActividad = 0 OR NULLIF(LTRIM(RTRIM(@UrlActividad)), N'') IS NULL) THROW 53314, 'Actividad requiere flag y URL.', 1;
    IF @SecActividad = 0 AND (@TieneActividad = 1 OR NULLIF(LTRIM(RTRIM(@UrlActividad)), N'') IS NOT NULL) THROW 53315, 'Sin actividad, limpiar flag/URL.', 1;
    IF @SecCompilador = 1 AND @TieneCompilador = 0 THROW 53316, 'Compilador requiere flag.', 1;
    IF @SecCompilador = 0 AND @TieneCompilador = 1 THROW 53317, 'Sin compilador, flag debe ser 0.', 1;
    IF @SecVideo = 1 AND (@TieneVideo = 0 OR NULLIF(LTRIM(RTRIM(ISNULL(@VideoId, N''))), N'') IS NULL OR @VideoId LIKE N'%youtube%' OR @VideoId LIKE N'%http%') THROW 53318, 'Video requiere flag y VideoId limpio.', 1;
    IF @SecVideo = 0 AND (@TieneVideo = 1 OR NULLIF(LTRIM(RTRIM(ISNULL(@VideoId, N''))), N'') IS NOT NULL) THROW 53319, 'Sin video, limpiar flag/VideoId.', 1;

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

