-- Insercion de leccion (propuesta final, no ejecutada automaticamente)
-- SubtemaId objetivo: 10
-- Subtema: Scrum: roles, artefactos y sprints

USE [ACC_Academic];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

BEGIN TRY
    BEGIN TRAN;

    DECLARE @SubtemaId INT = 10;

    IF NOT EXISTS (SELECT 1 FROM acc_academic.SubTemas WHERE Id_SubTema = @SubtemaId)
        THROW 53801, 'No existe el SubTemaId=10 en acc_academic.SubTemas.', 1;

        DECLARE @TituloLeccion NVARCHAR(100) = N'Scrum: roles, artefactos y sprints';
            DECLARE @DescripcionLeccion NVARCHAR(500) = N'Explica Scrum de forma simple: quien hace que, que listas se usan y como se trabaja por sprints cortos.';
    DECLARE @NivelBloom NVARCHAR(20) = N'Comprender';
    DECLARE @OrdenSecciones NVARCHAR(MAX) = N'["charpDialog","video","teoria","ejemplo","practica","actividad","charpTip"]';

    DECLARE @TieneActividad BIT = 1;
    DECLARE @UrlActividad NVARCHAR(MAX) = N'https://pendiente.local/actividad/subtema-10-scrum';
    DECLARE @TieneCompilador BIT = 0;
    DECLARE @TieneVideo BIT = 1;
    DECLARE @VideoId NVARCHAR(20) = N'ST10VIDPEND01';

            DECLARE @Teoria NVARCHAR(MAX) = N'
<div class="leccion-teoria">
    <h3>Roles principales</h3>
    <ul>
        <li><strong>Product Owner:</strong> decide prioridades.</li>
        <li><strong>Scrum Master:</strong> ayuda al equipo a trabajar sin bloqueos.</li>
        <li><strong>Equipo de desarrollo:</strong> construye lo acordado en el sprint.</li>
    </ul>
    <h3>Listas y resultado</h3>
    <ul>
        <li><strong>Product Backlog:</strong> lista general de tareas.</li>
        <li><strong>Sprint Backlog:</strong> tareas del sprint actual.</li>
        <li><strong>Incremento:</strong> parte terminada y usable al cierre.</li>
    </ul>
    <img src="https://placehold.co/1200x675?text=Scrum+roles+y+sprint" alt="Elementos de Scrum (pendiente)">
</div>';

            DECLARE @Ejemplo NVARCHAR(MAX) = N'
<div class="leccion-ejemplos">
    <h3>Ejemplo de sprint</h3>
    <p>En dos semanas se toma un grupo de tareas: crear cuenta, login y recuperar contraseña.</p>
    <p>Al cierre, esas funciones ya se pueden usar y revisar.</p>
    <img src="https://placehold.co/1200x675?text=Trabajo+de+sprint+de+inicio+a+cierre" alt="Trabajo de sprint (pendiente)">
</div>';

            DECLARE @Practica NVARCHAR(MAX) = N'
<div class="leccion-practicas">
    <h3>Práctica</h3>
    <ol>
        <li>Escribe 5 tareas de una función de tu proyecto.</li>
        <li>Elige 2 para un sprint corto.</li>
        <li>Describe que quedaria listo al final.</li>
    </ol>
    <div class="alert alert-success">
        <p class="alert-title">Criterio de logro</p>
        <p>Está correcto si eliges tareas realistas y un resultado concreto.</p>
    </div>
</div>';

            DECLARE @CharpTip NVARCHAR(MAX) = N'<p><strong>Tip Charp:</strong> En Scrum, terminar poco pero útil vale más que empezar mucho y dejarlo a medias.</p>';
            DECLARE @CharpDialog NVARCHAR(MAX) = N'<p>En esta lección vas a ordenar Scrum en pasos simples.</p><p>La meta es entender como se reparte el trabajo en ciclos cortos.</p>';

    IF EXISTS (SELECT 1 FROM acc_academic.Lecciones WHERE SubtemaId = @SubtemaId AND TituloLeccion = @TituloLeccion)
        THROW 53802, 'Ya existe una leccion con este titulo para SubtemaId=10.', 1;

    IF @NivelBloom NOT IN (N'Recordar', N'Comprender', N'Aplicar', N'Analizar', N'Evaluar', N'Crear')
        THROW 53803, 'NivelBloom invalido.', 1;
    IF ISJSON(@OrdenSecciones) <> 1
        THROW 53804, 'OrdenSecciones no es JSON valido.', 1;
    IF EXISTS (SELECT j.[value] FROM OPENJSON(@OrdenSecciones) j GROUP BY j.[value] HAVING COUNT(*) > 1)
        THROW 53805, 'OrdenSecciones contiene tokens duplicados.', 1;
    IF EXISTS
    (
        SELECT 1
        FROM OPENJSON(@OrdenSecciones) j
        LEFT JOIN (VALUES (N'video'), (N'teoria'), (N'practica'), (N'ejemplo'), (N'actividad'), (N'compilador'), (N'charpTip'), (N'charpDialog')) permitidas(Token)
               ON permitidas.Token = CAST(j.[value] AS NVARCHAR(50))
        WHERE permitidas.Token IS NULL
    )
        THROW 53806, 'OrdenSecciones contiene tokens fuera del conjunto permitido.', 1;

    DECLARE @SecTeoria BIT      = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'teoria') THEN 1 ELSE 0 END;
    DECLARE @SecEjemplo BIT     = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'ejemplo') THEN 1 ELSE 0 END;
    DECLARE @SecPractica BIT    = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'practica') THEN 1 ELSE 0 END;
    DECLARE @SecCharpTip BIT    = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'charpTip') THEN 1 ELSE 0 END;
    DECLARE @SecCharpDialog BIT = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'charpDialog') THEN 1 ELSE 0 END;
    DECLARE @SecActividad BIT   = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'actividad') THEN 1 ELSE 0 END;
    DECLARE @SecCompilador BIT  = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'compilador') THEN 1 ELSE 0 END;
    DECLARE @SecVideo BIT       = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'video') THEN 1 ELSE 0 END;

    IF @SecTeoria = 1 AND NULLIF(LTRIM(RTRIM(@Teoria)), N'') IS NULL THROW 53807, 'Falta Teoria.', 1;
    IF @SecEjemplo = 1 AND NULLIF(LTRIM(RTRIM(@Ejemplo)), N'') IS NULL THROW 53808, 'Falta Ejemplo.', 1;
    IF @SecPractica = 1 AND NULLIF(LTRIM(RTRIM(@Practica)), N'') IS NULL THROW 53809, 'Falta Practica.', 1;
    IF @SecCharpTip = 1 AND NULLIF(LTRIM(RTRIM(@CharpTip)), N'') IS NULL THROW 53810, 'Falta CharpTip.', 1;
    IF @SecCharpDialog = 1 AND NULLIF(LTRIM(RTRIM(@CharpDialog)), N'') IS NULL THROW 53811, 'Falta CharpDialog.', 1;
    IF @SecCharpTip = 1 AND @CharpTip LIKE N'%<div%' THROW 53812, 'CharpTip no debe incluir <div>.', 1;
    IF @SecCharpDialog = 1 AND @CharpDialog LIKE N'%<div%' THROW 53813, 'CharpDialog no debe incluir <div>.', 1;
    IF @SecActividad = 1 AND (@TieneActividad = 0 OR NULLIF(LTRIM(RTRIM(@UrlActividad)), N'') IS NULL) THROW 53814, 'Actividad requiere flag y URL.', 1;
    IF @SecActividad = 0 AND (@TieneActividad = 1 OR NULLIF(LTRIM(RTRIM(@UrlActividad)), N'') IS NOT NULL) THROW 53815, 'Sin actividad, limpiar flag/URL.', 1;
    IF @SecCompilador = 1 AND @TieneCompilador = 0 THROW 53816, 'Compilador requiere flag.', 1;
    IF @SecCompilador = 0 AND @TieneCompilador = 1 THROW 53817, 'Sin compilador, flag debe ser 0.', 1;
    IF @SecVideo = 1 AND (@TieneVideo = 0 OR NULLIF(LTRIM(RTRIM(ISNULL(@VideoId, N''))), N'') IS NULL OR @VideoId LIKE N'%youtube%' OR @VideoId LIKE N'%http%') THROW 53818, 'Video requiere flag y VideoId limpio.', 1;
    IF @SecVideo = 0 AND (@TieneVideo = 1 OR NULLIF(LTRIM(RTRIM(ISNULL(@VideoId, N''))), N'') IS NOT NULL) THROW 53819, 'Sin video, limpiar flag/VideoId.', 1;

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

