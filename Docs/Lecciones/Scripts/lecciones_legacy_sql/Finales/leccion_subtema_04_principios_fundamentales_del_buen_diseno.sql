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
    DECLARE @DescripcionLeccion NVARCHAR(500) = N'Presenta ideas base para mantener un sistema más ordenado, claro y fácil de modificar.';
    DECLARE @NivelBloom NVARCHAR(20) = N'Comprender';
    DECLARE @OrdenSecciones NVARCHAR(MAX) = N'["charpDialog","video","teoria","ejemplo","practica","actividad","charpTip"]';

    DECLARE @TieneActividad BIT = 1;
    DECLARE @UrlActividad NVARCHAR(MAX) = N'https://pendiente.local/actividad/subtema-04-principios-diseno';
    DECLARE @TieneCompilador BIT = 0;
    DECLARE @TieneVideo BIT = 1;
    DECLARE @VideoId NVARCHAR(20) = N'ST04VIDPEND01';

    DECLARE @Teoria NVARCHAR(MAX) = N'
<div class="leccion-teoria">
    <h3>Ideas que ayudan a mantener el orden</h3>
    <p>Un buen diseño no depende solo de que el programa funcione. También importa que sea claro, que no mezcle demasiadas cosas y que pueda modificarse sin romper todo.</p>
    <p>Para lograrlo, existen algunas ideas base que ayudan a revisar si el sistema está bien organizado.</p>
    <ul>
        <li><strong>Una tarea principal por parte:</strong> cada clase o parte del sistema debería encargarse de algo concreto. A esto se le relaciona con la cohesión y la responsabilidad única.</li>
        <li><strong>Pocas dependencias innecesarias:</strong> una parte no debería estar demasiado amarrada a muchas otras. Eso se relaciona con el acoplamiento bajo.</li>
        <li><strong>Evitar mezclar demasiadas funciones:</strong> si una clase hace muchas cosas, cuesta más entenderla, probarla y cambiarla.</li>
        <li><strong>Reunir lo que se repite:</strong> si una misma lógica aparece varias veces, conviene llevarla a un solo lugar para no mantenerla por separado.</li>
    </ul>
    <img src="https://placehold.co/1200x675?text=Principios+de+codigo+ordenado" alt="Ideas base de un diseño ordenado (pendiente)">

    <div class="alert alert-info">
        <p class="alert-title">Punto clave</p>
        <p>Estos principios no existen para hacer el diseño más complicado. Sirven para que el sistema sea más claro de entender y más fácil de cambiar con el tiempo.</p>
    </div>

    <div class="fomentador">
        <p>Si quieres profundizar más en estas ideas base del buen diseño, puedes ver contenido más detallado en el capítulo sobre principios de diseño haciendo clic <a href="Capitulo/Contenido/ID_CONTENIDO_PENDIENTE_PRINCIPIOS_DISENO">aquí</a>.</p>
    </div>
</div>';

    DECLARE @Ejemplo NVARCHAR(MAX) = N'
<div class="leccion-ejemplos">
    <h3>Ejemplo comparativo</h3>
    <p>Imagina una clase que registra usuarios, envía correos y genera reportes.</p>

    <div class="alert alert-error">
        <p class="alert-title">Diseño confuso</p>
        <p>Todo está en una sola clase. Cuando cambias una parte, aumentan las posibilidades de afectar otra que no tenía relación directa.</p>
    </div>

    <div class="alert alert-success">
        <p class="alert-title">Diseño más claro</p>
        <p>Una clase registra usuarios, otra envía correos y otra genera reportes. Cada una tiene una función más concreta y el sistema resulta más fácil de mantener.</p>
    </div>

    <img src="https://placehold.co/1200x675?text=Clase+grande+vs+clases+claras" alt="Comparación entre una clase sobrecargada y varias clases claras (pendiente)">

    <p>La mejora no está en dividir por dividir, sino en separar tareas cuando ya están demasiado mezcladas.</p>
</div>';

    DECLARE @Practica NVARCHAR(MAX) = N'
<div class="leccion-practicas">
    <h3>Práctica guiada</h3>
    <p>Lee este caso: una sola clase se encarga de validar usuarios, guardar datos, enviar correos y generar reportes.</p>
    <ol>
        <li>Escribe qué tareas distintas están mezcladas en esa clase.</li>
        <li>Propón cómo las separarías en partes más claras.</li>
        <li>Explica por qué esa separación haría más fácil entender o cambiar el sistema.</li>
    </ol>
    <div class="alert alert-success">
        <p class="alert-title">Criterio de logro</p>
        <p>Está correcto si detectas mezcla de responsabilidades, propones una separación razonable y explicas una mejora concreta en claridad o mantenimiento.</p>
    </div>
</div>';

    DECLARE @CharpTip NVARCHAR(MAX) = N'<p><strong>Tip Charp:</strong> Si te cuesta explicar en una sola frase para qué sirve una clase, probablemente está haciendo más de lo que debería.</p>';
    DECLARE @CharpDialog NVARCHAR(MAX) = N'<p>En esta lección vas a revisar ideas simples que ayudan a detectar cuándo un diseño está ordenado y cuándo empieza a complicarse.</p><p>La meta es que reconozcas señales básicas de claridad, mezcla de tareas y repetición innecesaria.</p>';

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
