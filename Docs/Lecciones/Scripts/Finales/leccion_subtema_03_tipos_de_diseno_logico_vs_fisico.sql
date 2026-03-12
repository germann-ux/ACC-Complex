-- Insercion de leccion (propuesta final, no ejecutada automaticamente)
-- SubtemaId objetivo: 3
-- Subtema: Tipos de diseno: logico vs fisico

USE [ACC_Academic];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

BEGIN TRY
    BEGIN TRAN;

    DECLARE @SubtemaId INT = 3;

    IF NOT EXISTS (SELECT 1 FROM acc_academic.SubTemas WHERE Id_SubTema = @SubtemaId)
        THROW 53101, 'No existe el SubTemaId=3 en acc_academic.SubTemas.', 1;

    DECLARE @TituloLeccion NVARCHAR(100) = N'Tipos de diseño: lógico vs físico';
    DECLARE @DescripcionLeccion NVARCHAR(500) = N'Explica de forma simple dos niveles del diseño: primero se organiza el programa en un plan claro y después ese plan se lleva a una implementación real.';
    DECLARE @NivelBloom NVARCHAR(20) = N'Analizar';
    DECLARE @OrdenSecciones NVARCHAR(MAX) = N'["charpDialog","video","teoria","ejemplo","practica","actividad","charpTip"]';

    DECLARE @TieneActividad BIT = 1;
    DECLARE @UrlActividad NVARCHAR(MAX) = N'https://pendiente.local/actividad/subtema-03-logico-vs-fisico';
    DECLARE @TieneCompilador BIT = 0;
    DECLARE @TieneVideo BIT = 1;
    DECLARE @VideoId NVARCHAR(20) = N'ST03VIDPEND01';

    DECLARE @Teoria NVARCHAR(MAX) = N'
<div class="leccion-teoria">
    <h3>Diseño lógico</h3>
    <p>Es el plan general del programa. Aquí decides qué partes tendrá y qué trabajo hará cada parte.</p>
    <p>Ejemplo: en una plataforma de cursos, defines una parte para alumnos, otra para cursos y otra para inscripciones.</p>

    <h3>Diseño físico</h3>
    <p>Es cuando ese plan se construye en una herramienta real. Aquí creas tablas, campos, conexiones y reglas para que el sistema funcione.</p>
    <p>La idea es mantener el mismo orden del plan, pero ya llevado a código y base de datos.</p>
    <img src="https://placehold.co/1200x675?text=Lógico+vs+Físico+%28imagen+pendiente%29" alt="Comparación entre plan general e implementación real (pendiente)">

    <div class="alert alert-info">
        <p class="alert-title">Punto clave</p>
        <p>Primero organizas cómo se va a construir; después lo implementas. Ese orden ayuda a evitar retrabajo y confusión.</p>
    </div>
</div>';

    DECLARE @Ejemplo NVARCHAR(MAX) = N'
<div class="leccion-ejemplos">
    <h3>Ejemplo: plataforma de cursos</h3>
    <p><strong>Paso 1 (lógico):</strong> decides que un alumno puede entrar a varios cursos y que cada curso guarda su lista de alumnos.</p>
    <p><strong>Paso 2 (físico):</strong> creas tablas como <code>Alumnos</code>, <code>Cursos</code> e <code>Inscripciones</code> para guardar esos datos.</p>
    <p>El primer paso da claridad; el segundo lo vuelve funcional.</p>
    <img src="https://placehold.co/1200x675?text=Plan+de+partes+y+tablas+reales" alt="Paso de organización a implementación real (pendiente)">
</div>';

    DECLARE @Practica NVARCHAR(MAX) = N'
<div class="leccion-practicas">
    <h3>Práctica de clasificación</h3>
    <p>Lee cada decisión y clasifícala como lógica o física:</p>
    <ol>
        <li>Decidir que habrá una parte para alumnos y otra para cursos.</li>
        <li>Crear la tabla <code>Alumnos</code> en SQL Server.</li>
        <li>Definir que un alumno puede estar en varios cursos.</li>
        <li>Configurar la conexión de la aplicación con la base de datos.</li>
    </ol>
    <div class="alert alert-success">
        <p class="alert-title">Criterio de logro</p>
        <p>Está correcto si separas decisiones de organización (lógico) de decisiones de implementación real (físico).</p>
    </div>
</div>';

    DECLARE @CharpTip NVARCHAR(MAX) = N'<p><strong>Tip Charp:</strong> Si estás organizando qué partes tendrá tu programa, estás en lógico; si ya lo estás montando en herramientas reales, estás en físico.</p>';
    DECLARE @CharpDialog NVARCHAR(MAX) = N'<p>En esta lección vas a ver dos pasos que trabajan juntos: planear y construir.</p><p>Si los separas bien, entiendes mejor qué decidir primero y qué implementar después.</p>';

    IF EXISTS
    (
        SELECT 1
        FROM acc_academic.Lecciones
        WHERE SubtemaId = @SubtemaId
          AND TituloLeccion = @TituloLeccion
    )
        THROW 53102, 'Ya existe una leccion con este titulo para SubtemaId=3.', 1;

    IF @NivelBloom NOT IN (N'Recordar', N'Comprender', N'Aplicar', N'Analizar', N'Evaluar', N'Crear')
        THROW 53103, 'NivelBloom invalido.', 1;

    IF ISJSON(@OrdenSecciones) <> 1
        THROW 53104, 'OrdenSecciones no es JSON valido.', 1;

    IF EXISTS
    (
        SELECT j.[value]
        FROM OPENJSON(@OrdenSecciones) j
        GROUP BY j.[value]
        HAVING COUNT(*) > 1
    )
        THROW 53105, 'OrdenSecciones contiene tokens duplicados.', 1;

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
        THROW 53106, 'OrdenSecciones contiene tokens fuera del conjunto permitido.', 1;

    DECLARE @SecTeoria BIT      = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'teoria') THEN 1 ELSE 0 END;
    DECLARE @SecEjemplo BIT     = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'ejemplo') THEN 1 ELSE 0 END;
    DECLARE @SecPractica BIT    = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'practica') THEN 1 ELSE 0 END;
    DECLARE @SecCharpTip BIT    = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'charpTip') THEN 1 ELSE 0 END;
    DECLARE @SecCharpDialog BIT = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'charpDialog') THEN 1 ELSE 0 END;
    DECLARE @SecActividad BIT   = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'actividad') THEN 1 ELSE 0 END;
    DECLARE @SecCompilador BIT  = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'compilador') THEN 1 ELSE 0 END;
    DECLARE @SecVideo BIT       = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'video') THEN 1 ELSE 0 END;

    IF @SecTeoria = 1 AND NULLIF(LTRIM(RTRIM(@Teoria)), N'') IS NULL THROW 53107, 'Falta Teoria.', 1;
    IF @SecEjemplo = 1 AND NULLIF(LTRIM(RTRIM(@Ejemplo)), N'') IS NULL THROW 53108, 'Falta Ejemplo.', 1;
    IF @SecPractica = 1 AND NULLIF(LTRIM(RTRIM(@Practica)), N'') IS NULL THROW 53109, 'Falta Practica.', 1;
    IF @SecCharpTip = 1 AND NULLIF(LTRIM(RTRIM(@CharpTip)), N'') IS NULL THROW 53110, 'Falta CharpTip.', 1;
    IF @SecCharpDialog = 1 AND NULLIF(LTRIM(RTRIM(@CharpDialog)), N'') IS NULL THROW 53111, 'Falta CharpDialog.', 1;
    IF @SecCharpTip = 1 AND @CharpTip LIKE N'%<div%' THROW 53112, 'CharpTip no debe incluir <div>.', 1;
    IF @SecCharpDialog = 1 AND @CharpDialog LIKE N'%<div%' THROW 53113, 'CharpDialog no debe incluir <div>.', 1;
    IF @SecActividad = 1 AND (@TieneActividad = 0 OR NULLIF(LTRIM(RTRIM(@UrlActividad)), N'') IS NULL) THROW 53114, 'Actividad requiere flag y URL.', 1;
    IF @SecActividad = 0 AND (@TieneActividad = 1 OR NULLIF(LTRIM(RTRIM(@UrlActividad)), N'') IS NOT NULL) THROW 53115, 'Sin actividad, limpiar flag/URL.', 1;
    IF @SecCompilador = 1 AND @TieneCompilador = 0 THROW 53116, 'Compilador requiere flag.', 1;
    IF @SecCompilador = 0 AND @TieneCompilador = 1 THROW 53117, 'Sin compilador, flag debe ser 0.', 1;
    IF @SecVideo = 1 AND (@TieneVideo = 0 OR NULLIF(LTRIM(RTRIM(ISNULL(@VideoId, N''))), N'') IS NULL OR @VideoId LIKE N'%youtube%' OR @VideoId LIKE N'%http%') THROW 53118, 'Video requiere flag y VideoId limpio.', 1;
    IF @SecVideo = 0 AND (@TieneVideo = 1 OR NULLIF(LTRIM(RTRIM(ISNULL(@VideoId, N''))), N'') IS NOT NULL) THROW 53119, 'Sin video, limpiar flag/VideoId.', 1;

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
