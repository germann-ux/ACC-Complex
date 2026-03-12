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
            DECLARE @DescripcionLeccion NVARCHAR(500) = N'Explica en que momento del desarrollo se organiza el programa y como ese orden ayuda antes de programar y probar.';
    DECLARE @NivelBloom NVARCHAR(20) = N'Comprender';
    DECLARE @OrdenSecciones NVARCHAR(MAX) = N'["charpDialog","video","teoria","ejemplo","practica","actividad","charpTip"]';

    DECLARE @TieneActividad BIT = 1;
    DECLARE @UrlActividad NVARCHAR(MAX) = N'https://pendiente.local/actividad/subtema-02-fases-diseno';
    DECLARE @TieneCompilador BIT = 0;
    DECLARE @TieneVideo BIT = 1;
    DECLARE @VideoId NVARCHAR(20) = N'ST02VIDPEND01';

            DECLARE @Teoria NVARCHAR(MAX) = N'
<div class="leccion-teoria">
    <h3></h3>
    <p>Primero entiendes el problema, después organizas el programa, luego programas y al final revisas que funcione.</p>
    <ul>
        <li><strong>Análisis:</strong> entender que necesita el usuario.</li>
        <li><strong>Diseño:</strong> decidir que partes tendra el programa y que hara cada una.</li>
        <li><strong>Implementación:</strong> escribir el código.</li>
        <li><strong>Pruebas:</strong> comprobar que el resultado funciona.</li>
    </ul>
    <img src="https://placehold.co/1200x675?text=Fases+del+ciclo+de+vida" alt="Etapas del desarrollo (pendiente)">
</div>';

            DECLARE @Ejemplo NVARCHAR(MAX) = N'
<div class="leccion-ejemplos">
    <h3>Ejemplo simple</h3>
    <p>En una app de biblioteca, primero se define que debe guardar libros, prestamos y devoluciones.</p>
    <p>Luego se organiza en partes: libros, usuarios y prestamos. Después se programa y al final se prueba.</p>
    <img src="https://placehold.co/1200x675?text=Análisis+diseño+código+pruebas" alt="Ejemplo por etapas (pendiente)">
</div>';

            DECLARE @Practica NVARCHAR(MAX) = N'
<div class="leccion-practicas">
    <h3>Práctica guiada</h3>
    <ol>
        <li>Escribe dos cosas que primero debes entender del usuario.</li>
        <li>Escribe dos decisiones de diseño antes de programar.</li>
        <li>Escribe una prueba simple para validar el resultado.</li>
    </ol>
    <div class="alert alert-success">
        <p class="alert-title">Criterio de logro</p>
        <p>Está correcto si separas entender, organizar, programar y probar.</p>
    </div>
</div>';

            DECLARE @CharpTip NVARCHAR(MAX) = N'<p><strong>Tip Charp:</strong> Si no puedes explicar como estara organizado tu programa, aún falta diseño.</p>';
            DECLARE @CharpDialog NVARCHAR(MAX) = N'<p>En esta lección vas a ubicar el diseño dentro del camino completo de desarrollo.</p><p>La meta es que sepas que hacer antes de escribir código.</p>';

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
            VALUES (N'video'), (N'teoria'), (N'practica'), (N'ejemplo'),
                   (N'actividad'), (N'compilador'), (N'charpTip'), (N'charpDialog')
        ) permitidas(Token) ON permitidas.Token = CAST(j.[value] AS NVARCHAR(50))
        WHERE permitidas.Token IS NULL
    )
        THROW 53006, 'OrdenSecciones contiene tokens fuera del conjunto permitido.', 1;

    DECLARE @SecTeoria BIT      = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'teoria') THEN 1 ELSE 0 END;
    DECLARE @SecEjemplo BIT     = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'ejemplo') THEN 1 ELSE 0 END;
    DECLARE @SecPractica BIT    = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'practica') THEN 1 ELSE 0 END;
    DECLARE @SecCharpTip BIT    = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'charpTip') THEN 1 ELSE 0 END;
    DECLARE @SecCharpDialog BIT = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'charpDialog') THEN 1 ELSE 0 END;
    DECLARE @SecActividad BIT   = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'actividad') THEN 1 ELSE 0 END;
    DECLARE @SecCompilador BIT  = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'compilador') THEN 1 ELSE 0 END;
    DECLARE @SecVideo BIT       = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'video') THEN 1 ELSE 0 END;

    IF @SecTeoria = 1 AND NULLIF(LTRIM(RTRIM(@Teoria)), N'') IS NULL THROW 53007, 'Falta Teoria.', 1;
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

