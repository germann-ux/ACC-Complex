-- Insercion de leccion (propuesta final, no ejecutada automaticamente)
-- SubtemaId objetivo: 6
-- Subtema: Diseno de software vs diseno grafico vs diseno UX

USE [ACC_Academic];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

BEGIN TRY
    BEGIN TRAN;

    DECLARE @SubtemaId INT = 6;

    IF NOT EXISTS (SELECT 1 FROM acc_academic.SubTemas WHERE Id_SubTema = @SubtemaId)
        THROW 53401, 'No existe el SubTemaId=6 en acc_academic.SubTemas.', 1;

        DECLARE @TituloLeccion NVARCHAR(100) = N'Diseño de software vs diseño gráfico vs diseño UX';
            DECLARE @DescripcionLeccion NVARCHAR(500) = N'Aclara que hace cada area para no mezclar responsabilidades: software, gráfico y experiencia de uso.';
    DECLARE @NivelBloom NVARCHAR(20) = N'Analizar';
    DECLARE @OrdenSecciones NVARCHAR(MAX) = N'["charpDialog","video","teoria","ejemplo","practica","actividad","charpTip"]';

    DECLARE @TieneActividad BIT = 1;
    DECLARE @UrlActividad NVARCHAR(MAX) = N'https://pendiente.local/actividad/subtema-06-software-grafico-ux';
    DECLARE @TieneCompilador BIT = 0;
    DECLARE @TieneVideo BIT = 1;
    DECLARE @VideoId NVARCHAR(20) = N'ST06VIDPEND01';

            DECLARE @Teoria NVARCHAR(MAX) = N'
<div class="leccion-teoria">
    <h3>Tres areas diferentes</h3>
    <ul>
        <li><strong>Diseño de software:</strong> organiza como funcionara el programa por dentro.</li>
        <li><strong>Diseño gráfico:</strong> define colores, estilo visual y tipografías.</li>
        <li><strong>Diseño UX:</strong> decide como sera la experiencia al usar la app.</li>
    </ul>
    <img src="https://placehold.co/1200x675?text=Software+gráfico+UX" alt="Diferencia entre areas (pendiente)">
</div>';

            DECLARE @Ejemplo NVARCHAR(MAX) = N'
<div class="leccion-ejemplos">
    <h3>Ejemplo de registro</h3>
    <p><strong>Software:</strong> valida datos y guarda informacion.</p>
    <p><strong>Gráfico:</strong> define como se ve la pantalla.</p>
    <p><strong>UX:</strong> simplifica pasos y mensajes para el usuario.</p>
    <img src="https://placehold.co/1200x675?text=Aporte+de+cada+area" alt="Aporte de cada area (pendiente)">
</div>';

            DECLARE @Practica NVARCHAR(MAX) = N'
<div class="leccion-practicas">
    <h3>Práctica</h3>
    <ol>
        <li>Elegir colores y tipografías.</li>
        <li>Definir validaciones de contraseña.</li>
        <li>Reducir pasos de un formulario.</li>
        <li>Separar logica de datos en otra capa.</li>
    </ol>
    <p>Clasifica cada punto como software, gráfico o UX.</p>
    <div class="alert alert-success">
        <p class="alert-title">Criterio de logro</p>
        <p>Está correcto si explicas por que cada decision pertenece a esa area.</p>
    </div>
</div>';

            DECLARE @CharpTip NVARCHAR(MAX) = N'<p><strong>Tip Charp:</strong> Que algo se vea bien no garantiza que funcione bien, y que funcione no garantiza buena experiencia.</p>';
            DECLARE @CharpDialog NVARCHAR(MAX) = N'<p>En esta lección vas a separar tres conceptos que suelen confundirse.</p><p>La meta es entender que cada area aporta algo distinto al producto final.</p>';

    IF EXISTS
    (
        SELECT 1
        FROM acc_academic.Lecciones
        WHERE SubtemaId = @SubtemaId
          AND TituloLeccion = @TituloLeccion
    )
        THROW 53402, 'Ya existe una leccion con este titulo para SubtemaId=6.', 1;

    IF @NivelBloom NOT IN (N'Recordar', N'Comprender', N'Aplicar', N'Analizar', N'Evaluar', N'Crear')
        THROW 53403, 'NivelBloom invalido.', 1;

    IF ISJSON(@OrdenSecciones) <> 1
        THROW 53404, 'OrdenSecciones no es JSON valido.', 1;

    IF EXISTS
    (
        SELECT j.[value]
        FROM OPENJSON(@OrdenSecciones) j
        GROUP BY j.[value]
        HAVING COUNT(*) > 1
    )
        THROW 53405, 'OrdenSecciones contiene tokens duplicados.', 1;

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
        THROW 53406, 'OrdenSecciones contiene tokens fuera del conjunto permitido.', 1;

    DECLARE @SecTeoria BIT      = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'teoria') THEN 1 ELSE 0 END;
    DECLARE @SecEjemplo BIT     = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'ejemplo') THEN 1 ELSE 0 END;
    DECLARE @SecPractica BIT    = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'practica') THEN 1 ELSE 0 END;
    DECLARE @SecCharpTip BIT    = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'charpTip') THEN 1 ELSE 0 END;
    DECLARE @SecCharpDialog BIT = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'charpDialog') THEN 1 ELSE 0 END;
    DECLARE @SecActividad BIT   = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'actividad') THEN 1 ELSE 0 END;
    DECLARE @SecCompilador BIT  = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'compilador') THEN 1 ELSE 0 END;
    DECLARE @SecVideo BIT       = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'video') THEN 1 ELSE 0 END;

    IF @SecTeoria = 1 AND NULLIF(LTRIM(RTRIM(@Teoria)), N'') IS NULL THROW 53407, 'Falta Teoria.', 1;
    IF @SecEjemplo = 1 AND NULLIF(LTRIM(RTRIM(@Ejemplo)), N'') IS NULL THROW 53408, 'Falta Ejemplo.', 1;
    IF @SecPractica = 1 AND NULLIF(LTRIM(RTRIM(@Practica)), N'') IS NULL THROW 53409, 'Falta Practica.', 1;
    IF @SecCharpTip = 1 AND NULLIF(LTRIM(RTRIM(@CharpTip)), N'') IS NULL THROW 53410, 'Falta CharpTip.', 1;
    IF @SecCharpDialog = 1 AND NULLIF(LTRIM(RTRIM(@CharpDialog)), N'') IS NULL THROW 53411, 'Falta CharpDialog.', 1;
    IF @SecCharpTip = 1 AND @CharpTip LIKE N'%<div%' THROW 53412, 'CharpTip no debe incluir <div>.', 1;
    IF @SecCharpDialog = 1 AND @CharpDialog LIKE N'%<div%' THROW 53413, 'CharpDialog no debe incluir <div>.', 1;
    IF @SecActividad = 1 AND (@TieneActividad = 0 OR NULLIF(LTRIM(RTRIM(@UrlActividad)), N'') IS NULL) THROW 53414, 'Actividad requiere flag y URL.', 1;
    IF @SecActividad = 0 AND (@TieneActividad = 1 OR NULLIF(LTRIM(RTRIM(@UrlActividad)), N'') IS NOT NULL) THROW 53415, 'Sin actividad, limpiar flag/URL.', 1;
    IF @SecCompilador = 1 AND @TieneCompilador = 0 THROW 53416, 'Compilador requiere flag.', 1;
    IF @SecCompilador = 0 AND @TieneCompilador = 1 THROW 53417, 'Sin compilador, flag debe ser 0.', 1;
    IF @SecVideo = 1 AND (@TieneVideo = 0 OR NULLIF(LTRIM(RTRIM(ISNULL(@VideoId, N''))), N'') IS NULL OR @VideoId LIKE N'%youtube%' OR @VideoId LIKE N'%http%') THROW 53418, 'Video requiere flag y VideoId limpio.', 1;
    IF @SecVideo = 0 AND (@TieneVideo = 1 OR NULLIF(LTRIM(RTRIM(ISNULL(@VideoId, N''))), N'') IS NOT NULL) THROW 53419, 'Sin video, limpiar flag/VideoId.', 1;

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

