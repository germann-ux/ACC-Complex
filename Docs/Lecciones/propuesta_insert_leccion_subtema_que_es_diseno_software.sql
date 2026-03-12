-- Propuesta de inserción de lección (no ejecutada automáticamente)
-- Subtema objetivo: "¿Qué es el diseño de software?"
-- Cumple contrato ACC: Bloom, OrdenSecciones, flags, HTML por sección.

USE [ACC_Academic];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

BEGIN TRY
    BEGIN TRAN;

    DECLARE @SubtemaId INT;

    -- 1) Resolver el subtema objetivo con variantes de texto
    SELECT TOP (1) @SubtemaId = st.Id_SubTema
    FROM acc_academic.SubTemas st
    WHERE st.NombreSubTema IN
    (
        N'¿Qué es el diseño de software?',
        N'¿Que es el diseño de software?',
        N'Que es el diseño de software?',
        N'Que es el diseno de software?'
    )
    ORDER BY st.Id_SubTema;

    IF @SubtemaId IS NULL
    BEGIN
        THROW 51001, 'No se encontró el SubTema objetivo para la lección.', 1;
    END;

    -- 2) Evitar duplicado exacto para este subtema
    IF EXISTS
    (
        SELECT 1
        FROM acc_academic.Lecciones l
        WHERE l.SubtemaId = @SubtemaId
          AND l.TituloLeccion = N'¿Qué es el diseño de software?'
    )
    BEGIN
        THROW 51002, 'Ya existe una lección con ese título para el SubTema objetivo.', 1;
    END;

    -- 3) Declarar payload de lección
    DECLARE @TituloLeccion NVARCHAR(100) = N'¿Qué es el diseño de software?';
    DECLARE @DescripcionLeccion NVARCHAR(500) =
        N'Explica el concepto de diseño de software, su papel dentro del ciclo de vida y su propósito para organizar decisiones técnicas antes de la implementación.';

    DECLARE @NivelBloom NVARCHAR(20) = N'Comprender';
    DECLARE @OrdenSecciones NVARCHAR(MAX) = N'["charpDialog","video","teoria","ejemplo","practica","actividad","charpTip"]';

    DECLARE @TieneActividad BIT = 1;
    DECLARE @UrlActividad NVARCHAR(MAX) = N'https://pendiente.local/actividad/diseno-software/intro';

    DECLARE @TieneCompilador BIT = 0;

    DECLARE @TieneVideo BIT = 1;
    DECLARE @VideoId NVARCHAR(MAX) = N'VIDEO_ID_PENDIENTE_DISENO_001';

    DECLARE @Teoria NVARCHAR(MAX) = N'
<div class="leccion-teoria">
    <h3>Concepto y propósito del diseño de software</h3>
    <p>El diseño de software define cómo se organizará una solución antes de escribir código. Establece estructura, responsabilidades y relaciones entre componentes para que el sistema sea comprensible y mantenible.</p>
    <img src="https://placehold.co/1200x675?text=Diseno+de+software+%28imagen+pendiente%29" alt="Representación visual del diseño de software (pendiente)">

    <div class="alert alert-info">
        <p class="alert-title">Idea clave</p>
        <p>Diseñar no es producir documentación decorativa: es tomar decisiones técnicas que reducen errores de implementación y facilitan cambios futuros.</p>
    </div>

    <h3>Papel en el ciclo de vida</h3>
    <p>En el ciclo de vida, el diseño conecta el análisis de requisitos con la implementación. Traduce necesidades del problema a una arquitectura que guía el desarrollo.</p>
    <ul>
        <li>Análisis: identifica necesidades y reglas del problema.</li>
        <li>Diseño: define estructura, componentes y flujo de interacción.</li>
        <li>Implementación: construye el código según las decisiones de diseño.</li>
        <li>Pruebas y mantenimiento: valida y ajusta el sistema a nuevos cambios.</li>
    </ul>
</div>';

    DECLARE @Ejemplo NVARCHAR(MAX) = N'
<div class="leccion-ejemplos">
    <h3>Ejemplo comparativo</h3>

    <div class="alert alert-error">
        <p class="alert-title">Sin diseño previo</p>
        <p>Todo se concentra en una sola clase con métodos extensos. Al agregar una nueva regla de negocio, aparecen efectos colaterales y aumenta el tiempo de corrección.</p>
    </div>

    <div class="alert alert-success">
        <p class="alert-title">Con diseño previo</p>
        <p>Se separan responsabilidades por módulos: usuarios, préstamos y catálogo. Los cambios se implementan en el componente correcto sin afectar funciones no relacionadas.</p>
    </div>

    <img src="https://placehold.co/1200x675?text=Arquitectura+modular+%28imagen+pendiente%29" alt="Esquema modular de ejemplo (pendiente)">

    <p>La diferencia no está en escribir más código, sino en decidir mejor la estructura antes de implementarlo.</p>
</div>';

    DECLARE @Practica NVARCHAR(MAX) = N'
<div class="leccion-practicas">
    <h3>Práctica guiada</h3>
    <p>Caso: sistema de biblioteca escolar para registrar libros, préstamos y devoluciones.</p>
    <ol>
        <li>Identifica tres responsabilidades principales del sistema.</li>
        <li>Propón tres componentes o módulos para cubrir esas responsabilidades.</li>
        <li>Describe un riesgo técnico de implementar sin diseño previo.</li>
    </ol>

    <div class="alert alert-success">
        <p class="alert-title">Criterio de logro</p>
        <p>La respuesta es adecuada si diferencia responsabilidades, propone una estructura coherente y justifica un riesgo real de mantenimiento o escalabilidad.</p>
    </div>
</div>';

    -- Reglas ACC: sin wrapper raíz para charpTip/charpDialog
    DECLARE @CharpTip NVARCHAR(MAX) = N'<p><strong>Tip Charp:</strong> Si una decisión afecta estructura, responsabilidades o escalabilidad, es una decisión de diseño y conviene definirla antes de codificar.</p>';

    DECLARE @CharpDialog NVARCHAR(MAX) = N'<p>En esta lección vas a ubicar el diseño de software dentro del proceso completo de desarrollo.</p>
<p>La meta es distinguir qué decisiones se toman en diseño y por qué esas decisiones impactan la calidad del código que se implementa después.</p>';

    -- 4) Validaciones de contrato técnico/pedagógico
    IF @NivelBloom NOT IN (N'Recordar', N'Comprender', N'Aplicar', N'Analizar', N'Evaluar', N'Crear')
    BEGIN
        THROW 51003, 'NivelBloom inválido para el contrato ACC.', 1;
    END;

    IF ISJSON(@OrdenSecciones) <> 1
    BEGIN
        THROW 51004, 'OrdenSecciones no es JSON válido.', 1;
    END;

    IF EXISTS
    (
        SELECT j.[value]
        FROM OPENJSON(@OrdenSecciones) j
        GROUP BY j.[value]
        HAVING COUNT(*) > 1
    )
    BEGIN
        THROW 51005, 'OrdenSecciones contiene secciones repetidas.', 1;
    END;

    IF EXISTS
    (
        SELECT 1
        FROM OPENJSON(@OrdenSecciones) j
        LEFT JOIN
        (
            VALUES
                (N'video'),
                (N'teoria'),
                (N'practica'),
                (N'ejemplo'),
                (N'actividad'),
                (N'compilador'),
                (N'charpTip'),
                (N'charpDialog')
        ) AS permitidas(Seccion)
            ON permitidas.Seccion = j.[value]
        WHERE permitidas.Seccion IS NULL
    )
    BEGIN
        THROW 51006, 'OrdenSecciones contiene secciones fuera del conjunto permitido.', 1;
    END;

    DECLARE @SecTeoria BIT      = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'teoria') THEN 1 ELSE 0 END;
    DECLARE @SecEjemplo BIT     = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'ejemplo') THEN 1 ELSE 0 END;
    DECLARE @SecPractica BIT    = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'practica') THEN 1 ELSE 0 END;
    DECLARE @SecCharpTip BIT    = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'charpTip') THEN 1 ELSE 0 END;
    DECLARE @SecCharpDialog BIT = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'charpDialog') THEN 1 ELSE 0 END;
    DECLARE @SecActividad BIT   = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'actividad') THEN 1 ELSE 0 END;
    DECLARE @SecCompilador BIT  = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'compilador') THEN 1 ELSE 0 END;
    DECLARE @SecVideo BIT       = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'video') THEN 1 ELSE 0 END;

    IF @SecTeoria = 1 AND NULLIF(LTRIM(RTRIM(@Teoria)), N'') IS NULL
        THROW 51007, 'La sección teoria está en OrdenSecciones pero Teoria viene vacía.', 1;

    IF @SecEjemplo = 1 AND NULLIF(LTRIM(RTRIM(@Ejemplo)), N'') IS NULL
        THROW 51008, 'La sección ejemplo está en OrdenSecciones pero Ejemplo viene vacío.', 1;

    IF @SecPractica = 1 AND NULLIF(LTRIM(RTRIM(@Practica)), N'') IS NULL
        THROW 51009, 'La sección practica está en OrdenSecciones pero Practica viene vacía.', 1;

    IF @SecCharpTip = 1 AND NULLIF(LTRIM(RTRIM(@CharpTip)), N'') IS NULL
        THROW 51010, 'La sección charpTip está en OrdenSecciones pero CharpTip viene vacío.', 1;

    IF @SecCharpDialog = 1 AND NULLIF(LTRIM(RTRIM(@CharpDialog)), N'') IS NULL
        THROW 51011, 'La sección charpDialog está en OrdenSecciones pero CharpDialog viene vacío.', 1;

    IF @SecCharpTip = 1 AND @CharpTip LIKE N'%<div%'
        THROW 51012, 'CharpTip no debe incluir wrapper raíz <div>.', 1;

    IF @SecCharpDialog = 1 AND @CharpDialog LIKE N'%<div%'
        THROW 51013, 'CharpDialog no debe incluir wrapper raíz <div>.', 1;

    IF @SecActividad = 1 AND (@TieneActividad = 0 OR NULLIF(LTRIM(RTRIM(@UrlActividad)), N'') IS NULL)
        THROW 51014, 'Sección actividad requiere TieneActividad=1 y UrlActividad válida.', 1;

    IF @SecActividad = 0 AND (@TieneActividad = 1 OR NULLIF(LTRIM(RTRIM(@UrlActividad)), N'') IS NOT NULL)
        THROW 51015, 'Sin sección actividad, TieneActividad debe ser 0 y UrlActividad debe estar limpia.', 1;

    IF @SecCompilador = 1 AND @TieneCompilador = 0
        THROW 51016, 'Sección compilador requiere TieneCompilador=1.', 1;

    IF @SecCompilador = 0 AND @TieneCompilador = 1
        THROW 51017, 'Sin sección compilador, TieneCompilador debe ser 0.', 1;

    IF @SecVideo = 1 AND (@TieneVideo = 0 OR NULLIF(LTRIM(RTRIM(ISNULL(@VideoId, N''))), N'') IS NULL OR @VideoId LIKE N'%youtube%' OR @VideoId LIKE N'%http%')
        THROW 51018, 'Sección video requiere TieneVideo=1 y VideoId con ID puro de YouTube.', 1;

    IF @SecVideo = 0 AND (@TieneVideo = 1 OR NULLIF(LTRIM(RTRIM(ISNULL(@VideoId, N''))), N'') IS NOT NULL)
        THROW 51019, 'Sin sección video, TieneVideo debe ser 0 y VideoId debe estar limpio.', 1;

    -- 5) Inserción
    INSERT INTO acc_academic.Lecciones
    (
        TituloLeccion,
        DescripcionLeccion,
        TieneActividad,
        UrlActividad,
        TieneCompilador,
        OrdenSecciones,
        SubtemaId,
        Teoria,
        Practica,
        Ejemplo,
        CharpTip,
        CharpDialog,
        NivelBloom,
        VideoId,
        TieneVideo
    )
    VALUES
    (
        @TituloLeccion,
        @DescripcionLeccion,
        @TieneActividad,
        @UrlActividad,
        @TieneCompilador,
        @OrdenSecciones,
        @SubtemaId,
        @Teoria,
        @Practica,
        @Ejemplo,
        @CharpTip,
        @CharpDialog,
        @NivelBloom,
        @VideoId,
        @TieneVideo
    );

    COMMIT TRAN;

    SELECT TOP (1)
        l.IdLeccion,
        l.TituloLeccion,
        l.SubtemaId,
        l.NivelBloom,
        l.OrdenSecciones,
        l.TieneActividad,
        l.TieneCompilador,
        l.TieneVideo
    FROM acc_academic.Lecciones l
    WHERE l.SubtemaId = @SubtemaId
      AND l.TituloLeccion = @TituloLeccion
    ORDER BY l.IdLeccion DESC;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRAN;
    THROW;
END CATCH;
GO
