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
    DECLARE @DescripcionLeccion NVARCHAR(500) = N'Compara qué ocurre cuando un sistema está bien organizado y qué problemas aparecen cuando el diseño es confuso.';
    DECLARE @NivelBloom NVARCHAR(20) = N'Analizar';
    DECLARE @OrdenSecciones NVARCHAR(MAX) = N'["charpDialog","video","teoria","ejemplo","practica","actividad","charpTip"]';

    DECLARE @TieneActividad BIT = 1;
    DECLARE @UrlActividad NVARCHAR(MAX) = N'https://pendiente.local/actividad/subtema-05-ventajas-y-riesgos';
    DECLARE @TieneCompilador BIT = 0;
    DECLARE @TieneVideo BIT = 1;
    DECLARE @VideoId NVARCHAR(20) = N'ST05VIDPEND01';

    DECLARE @Teoria NVARCHAR(MAX) = N'
<div class="leccion-teoria">
    <h3>El diseño se nota cuando hay que cambiar algo</h3>
    <p>Un buen diseño no solo ayuda al inicio del proyecto. Su valor se nota sobre todo cuando hay que corregir errores, agregar funciones nuevas o modificar una parte del sistema.</p>
    <p>Cuando el programa está bien organizado, esos cambios suelen ser más claros y más seguros. Cuando está mal organizado, hasta un cambio pequeño puede traer problemas en cadena.</p>

    <h3>Qué pasa con un buen diseño</h3>
    <ul>
        <li>Es más fácil entender dónde hacer un cambio.</li>
        <li>Corregir errores toma menos tiempo.</li>
        <li>Agregar una función nueva implica menos riesgo.</li>
        <li>El sistema resulta más claro para quien lo mantiene.</li>
    </ul>

    <h3>Qué pasa con un mal diseño</h3>
    <ul>
        <li>Un cambio pequeño puede afectar partes que no parecían relacionadas.</li>
        <li>Encontrar errores cuesta más trabajo.</li>
        <li>Agregar funciones nuevas se vuelve más inseguro.</li>
        <li>El equipo empieza a evitar cambios por miedo a romper algo.</li>
    </ul>

    <img src="https://placehold.co/1200x675?text=Diseno+ordenado+vs+diseno+confuso" alt="Comparación entre un diseño claro y uno confuso (pendiente)">

    <div class="alert alert-info">
        <p class="alert-title">Punto clave</p>
        <p>La diferencia entre un buen diseño y uno malo no siempre se nota el primer día. Muchas veces se hace evidente cuando el sistema empieza a cambiar.</p>
    </div>

    <div class="fomentador">
        <p>Si quieres profundizar más en cómo impacta el diseño en los cambios, errores y mantenimiento del sistema, puedes ver contenido más detallado en el capítulo sobre ventajas y riesgos del diseño haciendo clic <a href="Capitulo/Contenido/ID_CONTENIDO_PENDIENTE_VENTAJAS_RIESGOS_DISENO">aquí</a>.</p>
    </div>
</div>';

    DECLARE @Ejemplo NVARCHAR(MAX) = N'
<div class="leccion-ejemplos">
    <h3>Un mismo cambio en dos escenarios</h3>
    <p>Imagina que en un sistema escolar ahora se necesita guardar el teléfono del alumno.</p>

    <div class="alert alert-success">
        <p class="alert-title">Escenario con buen diseño</p>
        <p>El cambio se hace en la parte del sistema que maneja alumnos. Las demás partes casi no se tocan y el ajuste resulta más rápido y predecible.</p>
    </div>

    <div class="alert alert-error">
        <p class="alert-title">Escenario con mal diseño</p>
        <p>La información del alumno está mezclada en varias partes. Para agregar el teléfono hay que tocar muchos archivos y aparecen errores en lugares que no se esperaban.</p>
    </div>

    <img src="https://placehold.co/1200x675?text=Impacto+de+un+mismo+cambio" alt="Comparación del impacto de un mismo cambio (pendiente)">

    <p>El cambio es el mismo, pero el esfuerzo y el riesgo cambian mucho según el diseño que tenga el sistema.</p>
</div>';

    DECLARE @Practica NVARCHAR(MAX) = N'
<div class="leccion-practicas">
    <h3>Práctica guiada</h3>
    <p>Lee este caso: para agregar un dato nuevo, un equipo tuvo que modificar muchos archivos y después aparecieron errores en otras partes del sistema.</p>
    <ol>
        <li>Escribe dos señales que muestran que ese diseño tenía problemas.</li>
        <li>Explica una ventaja que habría tenido un diseño más claro en ese mismo caso.</li>
        <li>Propón una mejora general para evitar que un cambio simple afecte demasiadas partes.</li>
    </ol>
    <div class="alert alert-success">
        <p class="alert-title">Criterio de logro</p>
        <p>Está correcto si identificas señales de desorden, explicas una ventaja real de un buen diseño y propones una mejora orientada a reducir riesgo al cambiar.</p>
    </div>
</div>';

    DECLARE @CharpTip NVARCHAR(MAX) = N'<p><strong>Tip Charp:</strong> Un buen diseño suele pasar desapercibido hasta que necesitas cambiar algo. Ahí es donde se nota si el sistema ayuda o estorba.</p>';
    DECLARE @CharpDialog NVARCHAR(MAX) = N'<p>En esta lección vas a comparar lo que ocurre cuando un sistema está bien organizado y lo que ocurre cuando no lo está.</p><p>La meta es que relaciones el diseño con cambios, errores y mantenimiento del trabajo diario.</p>';

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
